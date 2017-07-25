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
    public partial class Login : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            divErrorDisplay.InnerHtml = "";

            if (txRegister.Value.Equals("") || txRegister.Value == null)
            {
                divErrorDisplay.InnerHtml = "<br> <p class='alert alert-danger' runat='server'>Please enter email address to register</p>";
                txRegister.Focus();
            }
            else
            {

                bool found = false;

                SqlConnection con = new SqlConnection(strConnection);

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM UserAccount", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (txRegister.Value.Equals(reader["Email"].ToString()))
                    {
                        found = true;
                        break;
                    }

                }

                if (found == true)
                {
                    divErrorDisplay.InnerHtml = "<br><p class='alert alert-danger' runat='server'>The email address " + txRegister.Value + " is already registered. </p>";
                }
                else
                {   
                    Response.Redirect("~/Register?defaultEmail=" +txRegister.Value.ToString());
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            divErrorDisplay.InnerHtml = "";

            if (txUsername.Value.Equals("") || txPassword.Value.Equals(""))
            {
                divErrorDisplay.InnerHtml = "<br><p class='alert alert-danger' runat='server'>Please enter your email address and password</p>";
                txUsername.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection(strConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM UserAccount WHERE Email = @Email AND Password = @Password ", con);
                cmd.Parameters.AddWithValue("@Email", txUsername.Value);
                cmd.Parameters.AddWithValue("@Password", txPassword.Value);

                SqlDataReader reader = cmd.ExecuteReader();
               
                if (reader.Read() == true)
                {
                    Session["FirstName"] = reader["FirstName"];
                    Session["LastName"] = reader["LastName"];
                    Session["Email"] = reader["Email"];
                    Session["PhoneNumber"] = reader["PhoneNumber"];
                    Session["DateOfBirth"] = reader["DateOfBirth"];
                    Session["Active"] = reader["Active"];
                    Session["RoleId"] = reader["RoleId"];
                    Session["Password"] = reader["Password"];
                    Session["CapturedDate"] = reader["CapturedDate"];
                    Session["GenderId"] = reader["GenderId"];

                    con.Close();
                    Response.Redirect("~/Default");
                    divErrorDisplay.InnerHtml = "";
                }
                else
                {
                    divErrorDisplay.InnerHtml = "<br><p class='alert alert-danger' runat='server'>Email or Password incorrect. Please try again and ensure Caps Lock is not enabled.</p>";
                }
               
            }
        }
    }
}