<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="innodiswebapp1._1.MainMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Main Menu</title>
    
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
                            runat="server" onclick="btninsert_Click"/>
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
        <div class ="container-fluid text-center">
            <div class="col-sm-6 col-sm-offset-3 form-box">
                <p>Hello. You can Insert new promotional pricing or View existing promotions by clicking the links in the navigation bar!</p>
            </div>
        </div>
    </form>
</body>
</html>
