     
function GetListItems(type) {
    switch (type) {
        case 'ISAPI':
            GetListItemsISAPI();
            break;
        case 'REST':
            GetListItemsREST();
            break;
        case 'JSOM':
            GetListItemsJSOM();
            break;
        default:
            console.error('type not supported');
    }
}

function AddItem(type) {
    switch (type) {
        case 'ISAPI':
            AddItemISAPI(false);
            break;
        case 'ISAPIElevated':
            AddItemISAPI(true);
            break;
        case 'REST':
            AddItemREST();
            break;
        case 'JSOM':
            AddItemJSOM();
            break;
        default:
            console.error('type not supported');
    }
}