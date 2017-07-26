using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TechOnline
{
    public partial class WishList : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string collectionString = Request.Cookies["WishListItem"].Value.Remove(Request.Cookies["WishListItem"].Value.Length - 1);
                string collectionDate = Request.Cookies["WishListDate"].Value.Remove(Request.Cookies["WishListDate"].Value.Length - 1);

                string[] items = collectionString.Split(',');
                string[] itemsDate = collectionDate.Split(',');
                               
                SqlConnection con = new SqlConnection(strConnection);
                DataTable table = new DataTable("myWishList");
                table.Columns.Add("Item Ref", typeof(string));
                table.Columns.Add("Title", typeof(string));
                table.Columns.Add("Availability", typeof(string));
                table.Columns.Add("DateAdded", typeof(string));
                table.Columns.Add("Price", typeof(string));

                for (int x = 0; x < items.Length; x++)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Id =@prodId ", con);
                    cmd.Parameters.AddWithValue("@prodId", items[x]);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if(reader.Read())
                    {
                        DataRow dr = table.NewRow();
                        dr["Item Ref"] = items[x];
                        dr["Title"] = reader["Description"];
                        dr["Availability"] = reader["Description"];
                        dr["DateAdded"] = itemsDate[x];
                        dr["Price"] = "R " + reader["SellingPrice"];

                        table.Rows.Add(dr);
                        //addData += "<div class='col-sm-4' runat='server' > "+ reader["Description"] +" </div> <br>" +
                        //    "<div class='col-sm-4' runat='server' > " + items[x] + " </div>" +
                        //    "<div class='col-sm-4' runat='server' > " + reader["SellingPrice"] + " </div>";
                    }
                    con.Close();
                }
                
                gridWishList.DataSource = table;
                gridWishList.DataBind();
               
            }
            catch (Exception) { }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {

        }
    }
}