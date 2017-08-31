using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Devart.Data.Oracle;

namespace innodiswebapp1._1
{
    public partial class _Default : System.Web.UI.Page
    {
        OracleConnection myConn = new OracleConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            myConn.ConnectionString = "Direct = true;User Id=xxean;Password=ean123;Server=innebnidm02.innodisgroup.com;Port = 1541;Sid = prd1;";
        }

        private void validateLogin()
        {
            string validate;
            try
            {
                myConn.Open();
                string q = "select APPS.FND_WEB_SEC.validate_login(:username,:password) from dual";

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = myConn;
                    cmd.CommandText = q;

                    cmd.Parameters.Add(new OracleParameter("username", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("password", OracleDbType.VarChar));

                    cmd.Parameters[0].Value = Username.Text.ToString();
                    cmd.Parameters[1].Value = pwd.Text.ToString();

                    validate = cmd.ExecuteScalar().ToString();

                    if (validate == "Y")
                    {
                        //FormsAuthentication.RedirectFromLoginPage(Username.Text, Persist.Checked);
                        Response.Redirect("MainMenu.aspx");
                    }
                    else if (validate == "N")
                    {
                        ErrorLabel.Text = "Username or password is incorrect. Please try again";
                    }
                }

                myConn.Close();
            }
            catch(OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();

                ErrorLabel.Text = "You are not connected. Please check your network settings";
            }
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = Username.Text.ToString();
            string passwd = pwd.Text.ToString();

            //uses pl/sql function package given by hansley
            validateLogin();

            //Response.Redirect("MainMenu.aspx");
        }
    }
}
