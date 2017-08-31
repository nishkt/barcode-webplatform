var ddlText, ddlValue, ddl, lblMesg;
        
function CacheItems() {
    ddlText = new Array();
    ddlValue = new Array();
    ddl = document.getElementById("<%=ddlItems.ClientID %>");
    lblMesg = document.getElementById("<%=lblMessage.ClientID%>");
    for (var i = 0; i < ddl.options.length; i++) {
        ddlText[ddlText.length] = ddl.options[i].text;
        ddlValue[ddlValue.length] = ddl.options[i].value;
    }
}

window.onload = CacheItems;

function FilterItems(value) {
    ddl.options.length = 0;
    for (var i = 0; i < ddlText.length; i++) {
        if (ddlText[i].toLowerCase().indexOf(value) != -1) {
            AddItem(ddlText[i], ddlValue[i]);
        }
    }
    lblMesg.innerHTML = ddl.options.length + " items found.";
    if (ddl.options.length == 0) {
        AddItem("No items found.", "");
    }
}

function AddItem(text, value) {
    var opt = document.createElement("option");
    opt.text = text;
    opt.value = value;
    ddl.options.add(opt);
}