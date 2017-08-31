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
    public partial class SearchPromotions : System.Web.UI.Page
    {
        OracleConnection myConn = new OracleConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            myConn.ConnectionString = "Direct = true;User Id=;Password=;Server=;Port = ;Sid = ;";

            if (!Page.IsPostBack)
            {
                deleteInfo.Visible = false;
            }
        }

        private void loadGridView()
        {
            try
            {
                string q = "select id, cussur, TO_CHAR(promotiondatestart, 'dd/MM/yyyy'), TO_CHAR(promotiondateend, 'dd/MM/yyyy'), stkdes, itemprice from innodis_webinterface where promotiondateend>= TO_DATE ('" + promoDate.SelectedDate.ToString("dd-MM-yyyy") + "', 'dd/mm/yyyy') AND promotiondatestart<= TO_DATE ('" + promoDate.SelectedDate.ToString("dd-MM-yyyy") + "', 'dd/mm/yyyy')";
                myConn.Open();
                OracleCommand cmd = new OracleCommand(q, myConn);

                DataTable PromoInfo = new DataTable();
                OracleDataAdapter sda = new OracleDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(PromoInfo);

                if (PromoInfo.Rows.Count == 0)
                {
                    resultLabel.Text = "Results Not Found for the Date!";
                    GridViewPromotion.Visible = false;
                }
                else
                {
                    GridViewPromotion.Visible = true;

                    PromoInfo.Columns["TO_CHAR(promotiondatestart,'dd/MM/yyyy')"].ColumnName = "PromotionStartDate";
                    PromoInfo.Columns["TO_CHAR(promotiondateend,'dd/MM/yyyy')"].ColumnName = "PromotionEndDate";
                    PromoInfo.AcceptChanges();

                    GridViewPromotion.DataSource = PromoInfo;
                    GridViewPromotion.DataBind();
                    resultLabel.Text = "Results Found!";
                } 
            }
            catch (OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                resultLabel.Text = ex.Message;
            }
        }

        protected void SelectGridViewRow()
        {
            foreach (GridViewRow row in GridViewPromotion.Rows)
            {
                try
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked)
                        {
                            int id = Int32.Parse(row.Cells[1].Text);

                            myConn.Open();
                            string q = "delete from xxean.innodis_webinterface where id=:prim";

                            using (OracleCommand cmd = new OracleCommand())
                            {
                                cmd.Connection = myConn;
                                cmd.CommandText = q;

                                cmd.Parameters.Add(new OracleParameter("prim", OracleDbType.VarChar));

                                cmd.Parameters[0].Value = id;

                                cmd.ExecuteNonQuery();
                                myConn.Close();
                            }

                            //the next 2 lines and if statement of code allows a message box to appear if the pricing information has been deleted correctly from the database
                            Type cstype = this.GetType();

                            // Get a ClientScriptManager reference from the Page class.
                            ClientScriptManager cs = Page.ClientScript;

                            // Check to see if the startup script is already registered.
                            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                            {
                                String cstext = "alert('Pricing Deleted Successfully');";
                                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                            }
                        }
                    }
                }
                catch (OracleException ex)
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }

                    Type cstype = this.GetType();

                    // Get a ClientScriptManager reference from the Page class.
                    ClientScriptManager cs = Page.ClientScript;

                    // Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Pricing not deleted. Please try again');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                        
                }
                
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            loadGridView();
            deleteInfo.Visible = true;
        }

        protected void mainmenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenu.aspx");
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

        protected void deleteInfo_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Are you sure?')", true);

            SelectGridViewRow();
            //clear textbox for date input
            //promoDate.Clear();
            loadGridView();
            //fillIndividualClientDropDown();
        }
    }
}
