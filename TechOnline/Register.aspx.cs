using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechOnline
{
    public partial class Register : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(strConnection);

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DataLookup WHERE DataKey like 'User_Gender' ", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem genderList = new ListItem();
                    genderList.Text = reader["DataValue"].ToString();
                    genderList.Value = reader["id"].ToString();
                    drpGender.Items.Add(genderList);
                }

                // Auto-load birthday months
                for (int x = 1; x <= 12; x++)
                {
                    string strMonth = "";
                    if (x == 1) { strMonth = "January"; }
                    else if (x == 2) { strMonth = "February"; }
                    else if (x == 3) { strMonth = "March"; }
                    else if (x == 4) { strMonth = "April"; }
                    else if (x == 5) { strMonth = "May"; }
                    else if (x == 6) { strMonth = "June"; }
                    else if (x == 7) { strMonth = "July"; }
                    else if (x == 8) { strMonth = "August"; }
                    else if (x == 9) { strMonth = "September"; }
                    else if (x == 10) { strMonth = "October"; }
                    else if (x == 11) { strMonth = "November"; } else if (x == 12) { strMonth = "December"; }

                    ListItem monthList = new ListItem();
                    monthList.Text = strMonth.ToString();
                    monthList.Value = x.ToString();
                    drpMonth.Items.Add(monthList);
                }

                // Auto-load birthday days
                for (int x = 1; x <= 31; x++)
                {
                    drpDay.Items.Add(x.ToString());
                }

                // Auto-load birthday days
                for (int x = 2001; x >= 1900; x--)
                {
                    drpYear.Items.Add(x.ToString());
                }
            }

            // Auto set fields
            txtEmail.Value = Request.QueryString["defaultEmail"];
            txtEmail.Disabled = true;
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate
            string strError = "";
            bool valid = true;

            if (!txtEmail.Value.Equals(txtRetypeEmail.Value))
            {
                strError += "Email address entered dont match <br />";
                txtRetypeEmail.Focus();
                valid = false;
            }

            if (!txtPassword.Value.Equals(txtRetypePassword.Value))
            {
                strError += "Passwords dont match <br />";
                txtPassword.Focus();
                valid = false;
            }
            
            divErrorDisplay.InnerHtml = "<p class='alert alert-danger' runat='server'> "+ strError +" </p>";
            DateTime birthDate = new DateTime(Convert.ToInt32(drpYear.Value), Convert.ToInt32(drpMonth.Value), Convert.ToInt32(drpDay.Value));
            DateTime todayDate = DateTime.Now;
            
            if (valid == true)
            {
                // Save to database
                int userId = 0;

                SqlConnection con = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("INSERT INTO UserAccount (FirstName, LastName, Email, PhoneNumber, DateOfBirth, Active, RoleId, Password, CapturedDate, GenderId) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @DateOfBirth, @Active, @RoleId, @Password, @CapturedDate, @GenderId) ", con);
                
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Value);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Value);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Value);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Value);
                cmd.Parameters.AddWithValue("@DateOfBirth", birthDate.Date);
                cmd.Parameters.AddWithValue("@Active", true);
                cmd.Parameters.AddWithValue("@RoleId", txtRole.Value);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Value);
                cmd.Parameters.AddWithValue("@CapturedDate", todayDate.Date);
                cmd.Parameters.AddWithValue("@GenderId", drpGender.Value);

                

                string myUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).ToString();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.fickstech.co.za");
                mail.From = new MailAddress("no-reply@tech-online.co.za");
                mail.To.Add(txtEmail.Value.ToString()); // Send mail to Employee                   
                mail.Subject = "Tech-Online Registration";
                mail.Body = "<h3> Welcome to Tech-Online, <br/>Your email address was registered at our site<br>" +
                    "To begin shopping please click <a href='"+ myUrl + "'>here</a> <br />" +
                    "<br/><br/><br/> Kind Regards,<br/>Tech-Online Team<br/> <img src='Images/Small-Logo.png' /> ";
                mail.IsBodyHtml = true;
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ficky@fickstech.co.za", "a11P@ss.g0");
               
                try
                {
                    SmtpServer.Send(mail);
                }
                catch (Exception)
                {
                   
                }
                
                con.Open();
                userId = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                // Auto-Login User:
                Session["UserId"] = userId;
                Session["FirstName"] = txtFirstName.Value;
                Session["LastName"] = txtLastName.Value;
                Session["Email"] = txtEmail.Value;
                Session["PhoneNumber"] = txtPhoneNumber.Value;
                Session["DateOfBirth"] = birthDate;
                Session["Active"] = txtActive.Value;
                Session["RoleId"] = txtRole.Value;
                Session["Password"] = txtPassword.Value;
                Session["CapturedDate"] = todayDate;
                Session["GenderId"] = drpGender.Value;

                //divErrorDisplay.InnerHtml = "<p class='alert alert-success' runat='server'> You are successfully registered, click <a href='/Login'> here </a>  to login.</p>";
                Response.Redirect("~/Default");
            }

        }
    }
}