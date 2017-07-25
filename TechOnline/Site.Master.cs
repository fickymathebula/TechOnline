using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TechOnline
{
    public partial class SiteMaster : MasterPage
    {       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FirstName"] != null)
            {
                lblWelcome.InnerHtml = "Hi " + Session["FirstName"];                
                lblRegister.InnerHtml = "";
            }
            else
            {
                lblWelcome.InnerHtml = "Hi!";
                lblLoginOrLogout.InnerHtml = "<a runat='server' href='Login.aspx'>Login</a>";
                lblRegister.InnerHtml = "<a runat='server' href='Login.aspx'>Register</a>";
            }            
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Default");
        }
    }
}