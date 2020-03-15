
var CreateTableWithListItems = function (listItems, title) {
    var html = "<p>" + title + "</p>";
    listItems.forEach(function (item) {
        html += "<tr>";
        html += "<td>" + item.Id + "</td>";
        html += "<td>" + item.Title + "</td>";
        html += "</tr>";
    });
    $(".outputTable").html(html);
}