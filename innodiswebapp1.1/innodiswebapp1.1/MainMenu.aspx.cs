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
    public partial class MainMenu : System.Web.UI.Page
    {
        //DataTable ClientData = new DataTable();
        //OracleConnection myConn = new OracleConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            //myConn.ConnectionString = "Direct = true;User Id=xxean;Password=ean123;Server=innebnidm02.innodisgroup.com;Port = 1541;Sid = prd1;";

            //if (!Page.IsPostBack)
            //{
            //    string name = "nishant";
            //    Session["n"] = name;
                
            //    fillIndividualClientDropDown();
            //    Session["CD"] = ClientData;
            //}
        }

        protected void btninsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("InserDetails.aspx");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPromotions.aspx");
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }
    }
}
