
var GetListItemsISAPI = function () {
    var url = _spPageContextInfo.webAbsoluteUrl + "/_vti_bin/CustomISAPI/CustomISAPI.svc/GetListItems/" + data.ListName;
    $.ajax({
        url: url,
        type: "GET",
        headers: {
            "accept": "application/json; odata=verbose"
        },
        success: function (results) {
            console.log(results);
            CreateTableWithListItems(results.Response, "ISAPI");
        },
        error: function (error) {
            console.error(error);
        }
    });
}

var AddItemISAPI = function (isElevated) {
    var url = _spPageContextInfo.webAbsoluteUrl + "/_vti_bin/CustomISAPI/CustomISAPI.svc/AddListItem";
    var postData = {
        listName: data.ListName,
        itemTitle: "TestA1",
        isElevated: isElevated
    };
    $.ajax({
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(postData),
        url: url,
        dataType: "json",
        processdata: true,
        success: function (results) {
            console.log(results);
            GetListItemsISAPI();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

var GetListItemsREST = function () {
    var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + data.ListName + "')/Items?&$top = 1000"
    $.ajax({
        url: url,
        type: "GET",
        headers: {
            "accept": "application/json; odata=verbose"
        },
        success: function (results) {
            console.log(results);
            CreateTableWithListItems(results.d.results, "REST");
        },
        error: function (error) {
            console.error(error);
        }
    });
}

var AddItemREST = function () {
    var listTitle = data.ListName;
    var item = {
        "__metadata": { "type": GetItemTypeForListName(listTitle) },
        "Title": "TestB1"
    };

    $.ajax({
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/contextinfo",
        method: "POST",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            $.ajax({
                url: _spPageContextInfo.siteAbsoluteUrl + "/_api/web/lists/getbytitle('" + listTitle + "')/items",
                type: "POST",
                data: JSON.stringify(item),
                headers: {
                    "Accept": "application/json;odata=verbose",
                    "Content-Type": "application/json;odata=verbose",
                    "X-RequestDigest": data.d.GetContextWebInformation.FormDigestValue,
                    "X-HTTP-Method": "POST"
                },
                success: function (results) {
                    console.log(results);
                    GetListItemsREST();
                },
                error: function (error) {
                    console.error(error);
                }
            });
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function GetItemTypeForListName(name) {
    return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
}

var GetListItemsJSOM = function () {
    SP.SOD.executeFunc('sp.js', 'SP.ClientContext', GetListItemsJSOMReady);
}

function GetListItemsJSOMReady() {
    var clientContext = new SP.ClientContext(_spPageContextInfo.webAbsoluteUrl);
    var splist = clientContext.get_web().get_lists().getByTitle(data.ListName);
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml(
        "<View><Query>" +
        "<Where>" +
        "</Where>" +
        "</Query></View>");
    this.collListItem = splist.getItems(camlQuery);
    clientContext.load(collListItem);
    clientContext.executeQueryAsync(Function.createDelegate(this, this.onGetListItemsSucceeded), Function.createDelegate(this, this.onFailed));        
}

function onGetListItemsSucceeded(sender, args) {
    var listItemEnumerator = collListItem.getEnumerator();
    var html = "<p>JSOM</p>";
    while (listItemEnumerator.moveNext()) {
        var listItem = listItemEnumerator.get_current();
        html += "<tr>";
        html += "<td>" + listItem.get_id() + "</td>";
        html += "<td>" + listItem.get_item('Title') + "</td>";
        html += "</tr>";
    }
    $(".outputTable").html(html);
}

function onFailed(sender, args) {
    console.error('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
}

var AddItemJSOM = function () {
    SP.SOD.executeFunc('sp.js', 'SP.ClientContext', AddItemJSOMReady);
}

function AddItemJSOMReady() {
    var clientContext = new SP.ClientContext(_spPageContextInfo.webAbsoluteUrl);
    var splist = clientContext.get_web().get_lists().getByTitle(data.ListName);

    var itemCreateInfo = new SP.ListItemCreationInformation();
    this.listItem = splist.addItem(itemCreateInfo);
    listItem.set_item('Title', 'TestC1');
    listItem.update();

    clientContext.load(listItem);

    clientContext.executeQueryAsync(Function.createDelegate(this, this.onAddItemSucceeded), Function.createDelegate(this, this.onFailed));
}

function onAddItemSucceeded() {
    console.log('Item created: ' + listItem.get_id());
    GetListItemsJSOM();
}
