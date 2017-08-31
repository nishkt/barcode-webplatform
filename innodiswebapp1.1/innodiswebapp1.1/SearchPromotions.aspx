<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPromotions.aspx.cs" Inherits="innodiswebapp1._1.SearchPromotions" %>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search Information</title>
    
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width = device-width, initial-scale = 1" />
    
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <!-- Navigation -->
        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation" runat="server">
            <div class="container">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <a id="A1" class="navbar-brand" runat="server">
                        <img id="Img1" src="img/innodislogo1.png" runat="server" alt="logo" />
                    </a>
                </div>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul id="Ul1" class="nav navbar-nav" runat="server">
                        <li>
                            <!--class ="btn btn-default"-->
                           <asp:Button ID="btninsert" class ="btn btn-default navbar-btn" Text="Insert New Pricing" 
                            runat="server" onclick="btninsert_Click" />
                        </li>
                        <li>
                            <asp:Button ID="btnsearch" class ="btn btn-default navbar-btn" 
                            Text="Search Existing Pricing" runat="server" onclick="btnsearch_Click"/>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li>
                            <asp:Button ID="btnlogout" class ="btn btn-default navbar-btn" Text="Logout" 
                            runat="server" onclick="btnlogout_Click"/>
                        </li>
                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container -->
        </nav>
    <div>
        <div class ="container-fluid text-center">
            <div class="col-sm-8 col-sm-offset-2">
                <div class="form-top">
                    <h3>Enter the Promotion Details to View</h3>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Choose a Date for Pricing"></asp:Label>
                    <BDP:BasicDatePicker ID="promoDate" runat="server" 
                    DateFormat="dd-MM-yyyy" />
                </div>
                
                <%--<div class="form-group">
                    <asp:RadioButton ID="RadioButtonGroup" class="radio-inline" GroupName="ClientType" Text="Group" 
                    runat="server" AutoPostBack="True" 
                    oncheckedchanged="RadioButtonGroup_CheckedChanged" />
                    <asp:RadioButton ID="RadioButtonIndividual" class="radio-inline" GroupName="ClientType" 
                    Text="Individual Client" runat="server" AutoPostBack="True" 
                    oncheckedchanged="RadioButtonIndividual_CheckedChanged" />
                </div>
                <div class="form-group">
                    <asp:DropDownList ID="DropDownIndividualClient" class ="form-control" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownGroupClient" class ="form-control" runat="server">
                    </asp:DropDownList>
                </div>--%>
                <div class="form-group">
                    <asp:Button ID="Button1" class ="btn btn-default" runat="server" Text="Display Promotion Information" 
                        onclick="Button1_Click1" />
                </div>
                <div class="form-group">
                    <asp:Label ID="resultLabel" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                </div>
                <div class="form-group" style="width:100%;">
                    <asp:GridView ID="GridViewPromotion" class="table-hover" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>  
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="form-group">
                    <asp:Button ID="deleteInfo" class ="btn btn-default" runat="server" 
                        Text="Delete Promotions" OnClientClick="return confirm('Are you sure you want to delete?')" onclick="deleteInfo_Click" />
                </div>
                <div class="form-group">
                    <asp:Button ID="mainmenu" class ="btn btn-default" runat="server" 
                        Text="Return to Main Menu" onclick="mainmenu_Click" />
                </div>                
            </div>
        </div>   
    </div>
    
    </form>
</body>
</html>
