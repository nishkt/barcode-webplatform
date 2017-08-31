<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InserDetails.aspx.cs" Inherits="innodiswebapp1._1.InserDetails" %>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Insert Information</title>
    
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width = device-width, initial-scale = 1" />
    
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    
    <script type="text/javascript">
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=DropIndividual.ClientID %>");
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
        
    </script>
    
</head>
<body onkeydown = "return (event.keyCode!=13)">
    <form id="form1" runat="server">
        <!-- Navigation -->
        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation" runat="server"><div class="container">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <a id="A1" class="navbar-brand" runat="server">
                        <img src="img/innodislogo1.png" runat="server" alt="logo" />
                    </a>               
                </div>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul id="Ul1" class="nav navbar-nav" runat="server">
                        <li>
                            <!--class ="btn btn-default"-->
                            <asp:Button ID="btninsert" class ="btn btn-default navbar-btn" Text="Insert New Pricing" 
                            runat="server" onclick="btninsert_Click" CausesValidation="False"/>
                        </li>
                        <li>
                            <asp:Button ID="btnsearch" class ="btn btn-default navbar-btn" 
                            Text="Search Existing Pricing" runat="server" onclick="btnsearch_Click" 
                                CausesValidation="False"/>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li>
                            <asp:Button ID="btnlogout" class ="btn btn-default navbar-btn" Text="Logout" 
                            runat="server" onclick="btnlogout_Click" CausesValidation="False"/>
                        </li>
                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </div>
        </nav>    
        <div class ="container-fluid text-center">
            <div class="col-sm-8 col-sm-offset-2 form-box">
                <div class="form-top">
                    <h3><asp:Label ID="TodayDate" runat="server"></asp:Label></h3>
                </div>
                
                
                
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Choose Pricing Type"></asp:Label>
                    <asp:RadioButton ID="RadioNormal" runat="server" class="radio-inline" 
                        GroupName="PricingType" Text="Normal" 
                        oncheckedchanged="RadioNormal_CheckedChanged"/>
                    <asp:RadioButton ID="RadioPromotion" runat="server" class="radio-inline" 
                        GroupName="PricingType" Text="Promotion" 
                        oncheckedchanged="RadioPromotion_CheckedChanged"/>
                </div>
                
                
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Choose Client Type"></asp:Label>
                    <asp:RadioButton ID="RadioGroup" class="radio-inline" GroupName="ClientType" Text="Group" 
                    runat="server" AutoPostBack="True" 
                    oncheckedchanged="RadioGroup_CheckedChanged" />
                    <asp:RadioButton ID="RadioIndividual" class="radio-inline" GroupName="ClientType" 
                    Text="Individual Client" runat="server" AutoPostBack="True" 
                    oncheckedchanged="RadioIndividual_CheckedChanged" />
                    <asp:RadioButton ID="RadioDefault" class="radio-inline" GroupName="ClientType" 
                    Text="Default" runat="server" AutoPostBack="True" 
                        oncheckedchanged="RadioDefault_CheckedChanged" />
                </div>
                
                <div class="form-group">
                    <asp:TextBox ID="txtSearch" class ="form-control" runat="server"
                    onkeyup = "FilterItems(this.value)" placeholder="Type Client's Name to filter"></asp:TextBox>
                    <asp:DropDownList ID="DropIndividual" class ="form-control" 
                        runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    
                </div>
                <div class="form-group">
                    <asp:DropDownList ID="DropGroup" class ="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                
                <%--<div class="form-group">--%>
                <div class="form-group"  style ="width:100%;">
                    <asp:Label ID="Label1" runat="server" Text="Promotion Start Date:"></asp:Label>
                    <BDP:BasicDatePicker ID="promoStartDate" runat="server" 
                    DateFormat="dd-MM-yyyy"/>
                </div>
                <div class="form-group">
                    <asp:RequiredFieldValidator ID="StartDateValidator1" runat="server" 
                    ControlToValidate="promoStartDate"
                    ErrorMessage="Promotion Start Date is required.">
                    </asp:RequiredFieldValidator>
                </div>
              
                <div class="form-group"  style ="width:100%;">
                    <asp:Label ID="Label2" runat="server" Text="Promotion End Date:"></asp:Label>
                    <BDP:BasicDatePicker ID="promoEndDate" runat="server" DateFormat="dd-MM-yyyy"/>
                </div>
                <div class="form-group">
                    <asp:RequiredFieldValidator ID="EndDateValidator" runat="server" 
                    ControlToValidate="promoEndDate"
                    ErrorMessage="Promotion End Date is required.">
                    </asp:RequiredFieldValidator>
                </div>
                
                <div class="form-group">
                    <asp:DropDownList ID="DropDownProducts" class="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                
                <div class="form-group">
                    &nbsp;<asp:Label ID="Label3" runat="server" Text="Price of Product (per kg) :"></asp:Label>
                    <asp:TextBox ID="itemPrice" placeholder="Product Price" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:RequiredFieldValidator ID="ItemPriceValidator" runat="server" 
                    ControlToValidate="itemPrice"
                    ErrorMessage="Item Price is required.">
                    </asp:RequiredFieldValidator>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="ErrorLabel" runat="server" Font-Bold="True" Font-Size="Larger" 
                        ForeColor="Red"></asp:Label>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="insertData" class="btn btn-default" runat="server" 
                        Text="Submit"  onclick="insertInfo_Click" />
                        <!--OnClientClick="return confirm('Are you sure to submit the following information?')"-->
                </div>
                <div class="form-group">
                    <asp:Button ID="mainmenu" class ="btn btn-default" runat="server" 
                        Text="Return to Main Menu" onclick="mainmenu_Click" 
                        CausesValidation="False" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
