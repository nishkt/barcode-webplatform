<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="innodiswebapp1._1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Log In</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width = device-width, initial-scale = 1" />
    
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    
</head>
<body>
    <!-- Navigation -->
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <a id="A1" class="navbar-brand" runat="server">
                    <img id="Img1" src="img/innodislogo1.png" runat="server" alt="logo" />
                </a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right">
                    <li style="margin-top:14px;margin-bottom:14px;">Innodis Back-End System</li>     
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    <div class="container-fluid">
        <form id="form1" runat="server">
            
            <div class ="container-fluid text-center" style="border:1px, bed62f">
                <div class="col-sm-8 col-sm-offset-2 form-box">
                    <div class="form-top">
                        <h3>Please Log In</h3>
                    </div>
                    <div class="form-bottom">
                        <form class="login-form">
                            <fieldset id = "login">
                                <div class="form-group">
                                    <label class="sr-only" for "Username">Username:</label> 
                                    <asp:TextBox ID="Username" class="form_username form-control" name="Username" placeholder="Username.." runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="sr-only" for "pwd">Password:</label> 
                                    <asp:TextBox ID="pwd" name="pwd" class ="form-password form-control" TextMode="Password" placeholder="Password.." runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnLogin" class ="btn btn-default" name="btnLogin" runat="server" Text="Submit" 
                                    onclick="btnLogin_Click" />
                                </div>
                                <div class ="form-group">
                                    <asp:Label ID="ErrorLabel" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                    
                </div>
                
            </div>
            
        </form>    
    </div>
</body>
</html>
