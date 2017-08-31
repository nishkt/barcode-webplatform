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
using System.Collections.Generic;
    
namespace innodiswebapp1._1
{
    //for html input, use .Value to retrieve values. for asp.net inputs from the toolbox, use .Text to retrieve values

    public partial class InserDetails : System.Web.UI.Page
    {
        OracleConnection myConn = new OracleConnection();

        string rdate;
        string ncode;
        string clientname;
        string sdate;
        string edate;
        string icode;
        string iDes;
        string iPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            TodayDate.Text = DateTime.Today.ToString("dd-MM-yyyy");

            ErrorLabel.Text = "";

            myConn.ConnectionString = "Direct = true;User Id=xxean;Password=ean123;Server=innebnidm02.innodisgroup.com;Port = 1541;Sid = prd1;";

            DropIndividual.Visible = false;
            txtSearch.Visible = false;
            lblMessage.Visible = false;

            DropGroup.Visible = false;

            if (!Page.IsPostBack)
            {
                fillIndividualClientDropDown();
                fillGroupClientDropDown();
                fillItemInformation();
            }
          //      }
        }

        private void fillIndividualClientDropDown()
        {
            try
            {
                DropIndividual.Items.Clear();
                string q = "select distinct nadcod, ordctn from innodis_sa_preorh";

                myConn.Open();
                OracleCommand cmd = new OracleCommand(q, myConn);

                DataTable ClientData = new DataTable();
                ClientData.Columns.Add("nadcod", typeof(string));
                ClientData.Columns.Add("ordctn", typeof(string));
                ClientData.Columns.Add("ConcatenatedField", typeof(string), "nadcod + ' | ' +ordctn");
                OracleDataAdapter sda = new OracleDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(ClientData);

                DropIndividual.DataSource = ClientData;
                DropIndividual.DataTextField = "ConcatenatedField";
                DropIndividual.DataValueField = "nadcod";
                DropIndividual.DataBind();

                myConn.Close();
            }
            catch(OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                    //enter error handler
            }
        }

        private void fillGroupClientDropDown()
        {
            try
            {
                DropGroup.Items.Clear();
                string q = "select distinct profile_class_id, group_name from innodis_ean_cusgroup";

                myConn.Open();
                OracleCommand cmd = new OracleCommand(q, myConn);

                DataTable ClientDataGroup = new DataTable();
                ClientDataGroup.Columns.Add("profile_class_id", typeof(int));
                ClientDataGroup.Columns.Add("group_name", typeof(string));
                ClientDataGroup.Columns.Add("ConcatenatedField", typeof(string), "profile_class_id + ' | ' +group_name");
                OracleDataAdapter sda = new OracleDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(ClientDataGroup);

                DropGroup.DataSource = ClientDataGroup;
                DropGroup.DataTextField = "ConcatenatedField";
                DropGroup.DataValueField = "profile_class_id";
                DropGroup.DataBind();

                myConn.Close();
            }
            catch (OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        private void fillItemInformation()
        {
            try
            {
                DropDownProducts.Items.Clear();
                string q = "select distinct strcod, allcod from innodis_sa_preord order by strcod";

                myConn.Open();
                OracleCommand cmd = new OracleCommand(q, myConn);

                DataTable ProductData = new DataTable();
                ProductData.Columns.Add("strcod", typeof(string));
                ProductData.Columns.Add("allcod", typeof(string));
                ProductData.Columns.Add("ConcatenatedField", typeof(string), "strcod + ' | ' +allcod");
                OracleDataAdapter sda = new OracleDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(ProductData);

                DropDownProducts.DataSource = ProductData;
                DropDownProducts.DataTextField = "ConcatenatedField";
                DropDownProducts.DataValueField = "strcod";
                DropDownProducts.DataBind();

                myConn.Close();
            }
            catch (OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                //enter error handler
            }
        }

        private void insertInfoDefault()
        {
            try
            {
                saveItemDescription(DropDownProducts.SelectedValue.ToString());
                //change the old default pricing promotionddateend to the new promotiondatestart-1
                string w = "update innodis_webinterface set promotiondateend = TO_DATE(:promotiondateend, 'dd/MM/yyyy'), pricingtype=:pricingexpiry where stkcod = :stkcod and pricingtype = :pricingtype and TO_DATE(:promodateend, 'dd/MM/yyy') between promotiondatestart and promotiondateend";
                
                int ptype = 0;//pricingtype
                string endDate = "";

                if (RadioDefault.Checked && RadioNormal.Checked)
                {
                    ptype = 6;
                    endDate = (promoStartDate.SelectedDate.AddYears(5)).ToString("dd/MM/yyyy");
                }
                else if (RadioDefault.Checked && RadioPromotion.Checked)
                {
                    ptype = 5;
                    endDate = promoEndDate.SelectedDate.ToString("dd/MM/yyyy");
                }

                myConn.Open();

                using (OracleCommand cmd2 = new OracleCommand())
                {
                    cmd2.Connection = myConn;
                    //cmd.BindByName = true;
                    cmd2.CommandText = w;

                    cmd2.Parameters.Add(new OracleParameter("promotiondateend", OracleDbType.VarChar));
                    cmd2.Parameters.Add(new OracleParameter("pricingexpiry", OracleDbType.Number));
                    cmd2.Parameters.Add(new OracleParameter("stkcod", OracleDbType.VarChar));
                    cmd2.Parameters.Add(new OracleParameter("pricingtype", OracleDbType.Number));

                    cmd2.Parameters[0].Value = (promoStartDate.SelectedDate.AddDays(-1)).ToString("dd/MM/yyyy");
                    cmd2.Parameters[1].Value = 7;
                    cmd2.Parameters[2].Value = DropDownProducts.SelectedValue.ToString();
                    cmd2.Parameters[3].Value = ptype;

                    cmd2.ExecuteNonQuery();
                }

                string q = "insert into xxean.innodis_webinterface (requesteddate, promotiondatestart, promotiondateend, stkcod, stkdes, itemprice, pricingtype) values (TO_DATE(:todaydate, 'dd/MM/yyyy'),TO_DATE(:promostartdate, 'dd/MM/yyyy'),TO_DATE(:promoenddate, 'dd/MM/yyyy'),:itemcode,:itemdescription,:itemprice, :pricingtype)";

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = myConn;
                    //cmd.BindByName = true;
                    cmd.CommandText = q;

                    cmd.Parameters.Add(new OracleParameter("todaydate", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("promostartdate", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("promoenddate", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("itemcode", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("itemdescription", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("itemprice", OracleDbType.VarChar));
                    cmd.Parameters.Add(new OracleParameter("pricingtype", OracleDbType.Number));

                    cmd.Parameters[0].Value = TodayDate.Text.ToString();
                    cmd.Parameters[1].Value = promoStartDate.SelectedDate.ToString("dd/MM/yyyy");
                    cmd.Parameters[2].Value = endDate;
                    cmd.Parameters[3].Value = DropDownProducts.SelectedValue.ToString();
                    cmd.Parameters[4].Value = iDes;
                    cmd.Parameters[5].Value = itemPrice.Text.ToString(); ;
                    cmd.Parameters[6].Value = ptype;

                    cmd.ExecuteNonQuery();
                    
                }
                
                //the next 2 lines and if statement of code allows a message box to appear if the pricing information has been inserted into the database correctly
                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Pricing Added Successfully');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }

                myConn.Close();
                
            }
            catch(OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }

                ErrorLabel.Text = "There was an error adding the pricing. Please try again";
            }
            
        }

        private void insertInfoGroup()
        {
            try
            {
                List<String> clientCode = new List<String>();
                List<String> clientName = new List<String>();

                string gname = "";

                //string[] clientCode = new string[100];

                saveItemDescription(DropDownProducts.SelectedValue.ToString());

                int ptype = 0;//pricingtype
                string endDate = "";

                if (RadioGroup.Checked && RadioNormal.Checked)
                {
                    ptype = 4;
                    endDate = (promoStartDate.SelectedDate.AddYears(5)).ToString("dd/MM/yyyy");
                }
                else if (RadioGroup.Checked && RadioPromotion.Checked)
                {
                    ptype = 3;
                    endDate = promoEndDate.SelectedDate.ToString("dd/MM/yyyy");
                }

                string q = "select customer_number, customer_name, group_name from innodis_ean_cusgroup where profile_class_id = :groupid";
                myConn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = myConn;
                    //cmd.BindByName = true;
                    cmd.CommandText = q;

                    cmd.Parameters.Add(new OracleParameter("groupid", OracleDbType.VarChar));

                    cmd.Parameters[0].Value = DropGroup.SelectedValue.ToString();

                    OracleDataReader reader = cmd.ExecuteReader();

                    //int i = 0;
                    while (reader.Read())
                    {
                        //clientCode[i] = rdr[i].ToString();
                        clientCode.Add(reader[0].ToString());
                        clientName.Add(reader[1].ToString());
                        gname = reader[2].ToString();
                    }

                    for (int i = 0; i < clientCode.Count; i++)
                    {
                        //update previous pricing entry for each client
                        string v = "update innodis_webinterface set promotiondateend = TO_DATE(:promotiondateend, 'dd/MM/yyyy'), pricingtype=:pricingexpiry where stkcod = :stkcod and pricingtype = :pricingtype and nadcode = :clientcode and TO_DATE(:promotiondateend, 'dd/MM/yyyy') between promotiondatestart and promotiondateend";

                        using (OracleCommand cmd3 = new OracleCommand())
                        {
                            cmd3.Connection = myConn;
                            //cmd.BindByName = true;
                            cmd3.CommandText = v;

                            cmd3.Parameters.Add(new OracleParameter("promotiondateend", OracleDbType.VarChar));
                            cmd3.Parameters.Add(new OracleParameter("pricingexpiry", OracleDbType.Number));
                            cmd3.Parameters.Add(new OracleParameter("stkcod", OracleDbType.VarChar));
                            cmd3.Parameters.Add(new OracleParameter("pricingtype", OracleDbType.Number));
                            cmd3.Parameters.Add(new OracleParameter("clientcode", OracleDbType.VarChar));

                            cmd3.Parameters[0].Value = (promoStartDate.SelectedDate.AddDays(-1)).ToString("dd/MM/yyyy");
                            cmd3.Parameters[1].Value = 7;
                            cmd3.Parameters[2].Value = DropDownProducts.SelectedValue.ToString();
                            cmd3.Parameters[3].Value = ptype;
                            cmd3.Parameters[4].Value = clientCode[i];

                            cmd3.ExecuteNonQuery();
                        }

                        //insert pricing for each clientid
                        string w = "insert into xxean.innodis_webinterface (requesteddate, nadcode, cussur, promotiondatestart, promotiondateend, stkcod, stkdes, itemprice, pricingtype, profileclassid, groupname) values (TO_DATE(:todaydate, 'dd/MM/yyyy'),:nadcod,:cussur,TO_DATE(:promostartdate, 'dd/MM/yyyy'),TO_DATE(:promoenddate, 'dd/MM/yyyy'),:itemcode,:itemdescription,:itemprice,:pricingtype, :groupid, :groupname)";

                        using (OracleCommand cmd2 = new OracleCommand())
                        {
                            cmd2.Connection = myConn;
                            cmd2.CommandText = w;

                            cmd2.Parameters.Add(new OracleParameter("todaydate", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("nadcod", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("cussur", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("promostartdate", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("promoenddate", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("itemcode", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("itemdescription", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("itemprice", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("pricingtype", OracleDbType.Number));
                            cmd2.Parameters.Add(new OracleParameter("groupid", OracleDbType.VarChar));
                            cmd2.Parameters.Add(new OracleParameter("groupname", OracleDbType.VarChar));


                            cmd2.Parameters[0].Value = TodayDate.Text.ToString();
                            cmd2.Parameters[1].Value = clientCode[i];
                            cmd2.Parameters[2].Value = clientName[i];
                            cmd2.Parameters[3].Value = promoStartDate.SelectedDate.ToString("dd-MM-yyyy");
                            cmd2.Parameters[4].Value = endDate;
                            cmd2.Parameters[5].Value = DropDownProducts.SelectedValue.ToString();
                            cmd2.Parameters[6].Value = iDes;
                            cmd2.Parameters[7].Value = itemPrice.Text.ToString(); ;
                            cmd2.Parameters[8].Value = ptype;
                            cmd2.Parameters[9].Value = DropGroup.SelectedValue.ToString();
                            cmd2.Parameters[10].Value = gname;
                            
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }

                //the next 2 lines and if statement of code allows a message box to appear if the pricing information has been inserted into the database correctly
                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Pricing Added Successfully');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }

                
                myConn.Close();
            }
            catch(OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }


                ErrorLabel.Text = "There was an error adding the pricing. Please try again";
            }
        }

        private void insertInfo()
        {
            rdate = TodayDate.Text.ToString();
            ncode = DropIndividual.SelectedValue.ToString();
            icode = DropDownProducts.SelectedValue.ToString();
            
            saveItemDescription(icode);

            iPrice = itemPrice.Text.ToString();
            sdate = promoStartDate.SelectedDate.ToString("dd-MM-yyyy");
            //edate = promoEndDate.SelectedDate.ToString("dd-MM-yyyy");
            //clientname = "";//change this to include clientname

            int ptype = 0;//pricingtype
            string endDate = "";

            if (RadioIndividual.Checked && RadioNormal.Checked)
            {
                ptype = 2;
                endDate = (promoStartDate.SelectedDate.AddYears(5)).ToString("dd-MM-yyyy");
            }
            else if (RadioIndividual.Checked && RadioPromotion.Checked)
            {
                ptype = 1;
                endDate = promoEndDate.SelectedDate.ToString("dd-MM-yyyy");
            }

            if (DateTime.ParseExact(sdate, "dd-MM-yyyy", null) >= DateTime.ParseExact(rdate, "dd-MM-yyyy", null) && DateTime.ParseExact(sdate, "dd-MM-yyyy", null) <= DateTime.ParseExact(endDate, "dd-MM-yyyy", null))
            {
                try
                {
                    myConn.Open();

                    //update previous pricing entry for each client
                    string w = "update innodis_webinterface set promotiondateend = TO_DATE(:promotiondateend, 'dd/MM/yyyy'), pricingtype=:pricingexpiry where stkcod = :stkcod and pricingtype = :pricingtype and nadcode = :clientcode and TO_DATE(:promotiondateend, 'dd/MM/yyyy') between promotiondatestart and promotiondateend";

                    using (OracleCommand cmd2 = new OracleCommand())
                    {
                        cmd2.Connection = myConn;
                        cmd2.CommandText = w;

                        cmd2.Parameters.Add(new OracleParameter("promotiondateend", OracleDbType.VarChar));
                        cmd2.Parameters.Add(new OracleParameter("pricingexpiry", OracleDbType.Number));
                        cmd2.Parameters.Add(new OracleParameter("stkcod", OracleDbType.VarChar));
                        cmd2.Parameters.Add(new OracleParameter("pricingtype", OracleDbType.Number));
                        cmd2.Parameters.Add(new OracleParameter("clientcode", OracleDbType.VarChar));

                        cmd2.Parameters[0].Value = (promoStartDate.SelectedDate.AddDays(-1)).ToString("dd/MM/yyyy");
                        cmd2.Parameters[1].Value = 7;
                        cmd2.Parameters[2].Value = DropDownProducts.SelectedValue.ToString();
                        cmd2.Parameters[3].Value = ptype;
                        cmd2.Parameters[4].Value = ncode;

                        cmd2.ExecuteNonQuery();
                    }
                    
                    string q = "insert into xxean.innodis_webinterface (requesteddate, nadcode, cussur, promotiondatestart, promotiondateend, stkcod, stkdes, itemprice, pricingtype) values(TO_DATE(:todaydate, 'dd/MM/yyyy'),:nadcod,:cussur,TO_DATE(:promostartdate, 'dd/MM/yyyy'),TO_DATE(:promoenddate, 'dd/MM/yyyy'),:itemcode,:itemdescription,:itemprice, :pricingtype)";

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = myConn;
                        //cmd.BindByName = true;
                        cmd.CommandText = q;

                        cmd.Parameters.Add(new OracleParameter("todaydate", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("nadcod", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("cussur", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("promostartdate", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("promoenddate", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("itemcode", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("itemdescription", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("itemprice", OracleDbType.VarChar));
                        cmd.Parameters.Add(new OracleParameter("pricingtype", OracleDbType.Number));

                        cmd.Parameters[0].Value = rdate;
                        cmd.Parameters[1].Value = ncode;
                        cmd.Parameters[2].Value = clientname;
                        cmd.Parameters[3].Value = sdate;
                        cmd.Parameters[4].Value = endDate;
                        cmd.Parameters[5].Value = icode;
                        cmd.Parameters[6].Value = iDes;
                        cmd.Parameters[7].Value = iPrice;
                        cmd.Parameters[8].Value = ptype;

                        cmd.ExecuteNonQuery();
                        myConn.Close();

                        //the next 2 lines and if statement of code allows a message box to appear if the pricing information has been inserted into the database correctly
                        Type cstype = this.GetType();

                        // Get a ClientScriptManager reference from the Page class.
                        ClientScriptManager cs = Page.ClientScript;

                        // Check to see if the startup script is already registered.
                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            String cstext = "alert('Pricing Added Successfully');";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
                    }
                }
                catch (OracleException ex)
                {

                    if (myConn.State == ConnectionState.Open)
                        myConn.Close();

                    ErrorLabel.Text = "There was an error adding the pricing. Please try again";
                    //string display = "There has been an error due to " + ex.Message;
                    //System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    //sb2.Append("<script type = 'text/javascript'>");
                    //sb2.Append("window.onload=function(){");
                    //sb2.Append("alert('");
                    //sb2.Append(display);
                    //sb2.Append("')};");
                    //sb2.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb2.ToString());
                }
            }
            else
            {
                ErrorLabel.Text = "Please insert correct date intervals";
            }

            //after submit button has been clicked and the message for successful or error has been shown, reset the textboxes and comboboxes to original settings
            RadioGroup.Checked = false;
            RadioIndividual.Checked = false;
            txtSearch.Text = "";
            DropDownProducts.SelectedIndex = 0;
            promoStartDate.Clear();
            promoEndDate.Clear();
            itemPrice.Text = "";
        }

        private void saveItemDescription(string itemcode)
        {
            try
            {
                string q = "select allcod from innodis_sa_preord where strcod = '" + itemcode + "'";
                myConn.Open();
                OracleCommand cmd = new OracleCommand(q, myConn);

                iDes = cmd.ExecuteScalar().ToString();

                myConn.Close();
            }
            catch (OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        private void saveClientName()
        {
            try
            {
                myConn.Open();
                string q = "select ordctn from innodis_sa_preorh where nadcod = :nadcod";

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = myConn;
                    cmd.CommandText = q;

                    cmd.Parameters.Add(new OracleParameter("nadcod", OracleDbType.VarChar));

                    cmd.Parameters[0].Value = DropIndividual.SelectedValue.ToString();

                    clientname = cmd.ExecuteScalar().ToString();
                }
                myConn.Close();
            }
            catch (OracleException ex)
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();
            }
        }

        protected void mainmenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenu.aspx");
        }

        protected void RadioGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioGroup.Checked)
            {
                DropGroup.Visible = true;

                DropIndividual.Visible = false;
                txtSearch.Visible = false;
                lblMessage.Visible = false;
            }
        }
            
        protected void RadioIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioIndividual.Checked)
            {
                DropIndividual.Visible = true;
                // fillIndividualClientDropDown();
                txtSearch.Visible = true;
                lblMessage.Visible = true;
                
                DropGroup.Visible = false;
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPromotions.aspx");
        }

        protected void btninsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("InserDetails.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MainMenu.aspx");
        }

        protected void insertInfo_Click(object sender, EventArgs e)
        {
            if (RadioGroup.Checked)
            {
                insertInfoGroup();
            }
            else if (RadioIndividual.Checked)
            {
                saveClientName();
                insertInfo();
            }
            else if(RadioDefault.Checked)
            {
                insertInfoDefault();
            }
            else
            {
                //message saying "One of the Radio buttons was not selected"
            }
            
        }

        protected void RadioDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioDefault.Checked)
            {
                DropIndividual.Visible = false;
                txtSearch.Visible = false;
                lblMessage.Visible = false;

                DropGroup.Visible = false;
            }
        }

        protected void RadioNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioNormal.Checked)
            {
                Label2.Visible = false;
                promoEndDate.Visible = false;
            }
        }

        protected void RadioPromotion_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioPromotion.Checked)
            {
                Label2.Visible = true;
                promoEndDate.Visible = true;
            }
        }
    }
}