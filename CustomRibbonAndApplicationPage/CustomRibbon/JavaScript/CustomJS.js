
function CustromRibbonMethod1() {
    alert('CustromRibbonMethod1');
}

function CustromRibbonMethod2() {
    var options = {
        title: "Custom Dialog",
        url: "/sites/AdamTest/_layouts/15/ApplicationPage/ApplicationPageCustom.aspx",
        showClose: true,
        autoSize: true,
        dialogReturnValueCallback: CloseCallback   
    };

    SP.UI.ModalDialog.showModalDialog(options);
}

function CustromRibbonMethod3() {
    alert('CustromRibbonMethod3');
}

function CloseCallback(result, value) {
    if (result === SP.UI.DialogResult.OK) {
        alert('DialogResult.OK');
    }
    if (result === SP.UI.DialogResult.cancel) {
        alert('DialogResult.cancel');
    }
}