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

            int wishListCount = 0;
            try
            {
                if (Request.Cookies["WishListItem"].Value != "" || Request.Cookies["WishListItem"].Value != null)
                {
                    string collectionString = Request.Cookies["WishListItem"].Value.Remove(Request.Cookies["WishListItem"].Value.Length - 1);
                    string[] items = collectionString.Split(',');

                    for (int x = 0; x < items.Length; x++)
                    {
                        wishListCount++;
                    }
                }
            }
            catch (Exception) { }

            wishListDisp.InnerHtml = "  " + wishListCount;
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Default");
        }
    }
}