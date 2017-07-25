using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechOnline
{
    public partial class SubMaster : System.Web.UI.MasterPage
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(strConnection);

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProductDepartment", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem myList = new ListItem();
                    myList.Text = reader["Description"].ToString();
                    myList.Value = reader["Id"].ToString();
                    //ProDepartment.Items.Add(myList);
                    
                    // Exclude 1- Liquor & Soft Drinks, 12 - DIY & Auto, 22 - Vouchers
                    if ((int)reader["Id"] != 1 && (int)reader["Id"] != 12 && (int)reader["Id"] !=22)
                    {
                        SearchProDept.Items.Add(myList);
                    }                   
                }

                con.Close();

            }

            
        }
    }
}