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
    public partial class ProductDetails : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string selectedItem = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnection);

            SqlCommand cmd = new SqlCommand("select * from Product WHERE Id = @ProdId", con);
            cmd.Parameters.AddWithValue("@ProdId", Request.QueryString["Id"]);
            var imgsrc = "";
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                        imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                        divImage.InnerHtml = "<br><br><br><img class='img-responsive' src='" + imgsrc + "' style='max-width:350px; max-height:350px;margin-left:50px;' alt='No image added' /> <img class='picbig' src='" + imgsrc + "' />  ";
                    }
                    catch (Exception)
                    {
                        divImage.InnerHtml = "";
                    }
                    string details = "";
                    string stockDetails = "";
                    string strWarranty = "";
                    string eBucsText = "";

                    //Check warranty
                    if (Convert.ToInt32(reader["Warranty"]) > 0)
                    {
                        strWarranty = "<span class='glyphicon glyphicon-ok'></span> " + reader["Warranty"] + " Year Limited Warranty <br />";
                    } else
                    {
                        strWarranty = "<span class='glyphicon glyphicon-remove'></span> No warranty on this item <br />";
                    }

                    // Check if there is stock
                    if (Convert.ToInt32(reader["Quantity"]) < 5)
                    {
                        stockDetails = "<strong Style='color:Red;'>In Stock - Only " + reader["Quantity"] + " left </strong>";
                    } else if (Convert.ToInt32(reader["Quantity"]) == 0)
                    {
                        stockDetails = "<strong Style='color:Red;'>Out of Stock </strong>";
                    } else
                    {
                        stockDetails = "<strong>In Stock </strong>";
                    }

                    // Check if can accept credit
                    if(Convert.ToDecimal(reader["SellingPrice"]) > 999) 
                    {
                        decimal monthly = (Convert.ToDecimal(reader["SellingPrice"]) / 12);
                        decimal repayment = decimal.Round((monthly * (decimal)0.18) + monthly,2);

                        eBucsText = "<p class='text-muted'> eB" + Convert.ToInt32((Convert.ToDecimal(reader["SellingPrice"]) * 10)) + " | Discovery Miles " + Convert.ToInt32((Convert.ToDecimal(reader["SellingPrice"]) * 10)) + " | On Credit: R "+ repayment + " / month </p><br>";

                    } else
                    {
                        eBucsText = "<p class='text-muted'> eB" + Convert.ToInt32((Convert.ToDecimal(reader["SellingPrice"]) * 10)) + " | Discovery Miles " + Convert.ToInt32((Convert.ToDecimal(reader["SellingPrice"]) * 10)) + " </p><br>";
                    }

                    // Add selling price to screen
                    if (Convert.ToDecimal(reader["SellingPrice"]) < Convert.ToDecimal(reader["Price"]))
                    {
                        details += "<p class='page-header'> <strong> " + reader["Name"] + " </strong><br><br>  " + reader["Description"] + " <br> <a href='ProductReview'>Write a review </a> </p><br>" +                        
                        "<span> <strong  Style='color:Red;'>  R " + reader["SellingPrice"] + " </strong> List Price <del>R " + reader["Price"] + " </del>  </span><br>" +
                        eBucsText + stockDetails + "<br>" +
                       "<span class='glyphicon glyphicon-ok'></span> Eligible Cash on Delivery <br />" +
                       "<span class='glyphicon glyphicon-ok'></span> Free delivery available <br />" +
                       "<span class='glyphicon glyphicon-ok'></span> Hassle - Free Exchanges & Returns for 30 Days <br />" +
                       "<span class='glyphicon glyphicon-ok'></span> " + reader["Warranty"] + " Year Limited Warranty <br />";

                    } else
                    {
                        details += "<p class='page-header'> <strong> " + reader["Name"] + " </strong><br><br>  " + reader["Description"] + " <br> <a href='ProductReview'>Write a review </a> </p><br>" +                             
                        "<span> <strong  Style='color:Red;'>  R " + reader["SellingPrice"] + " </strong> </span><br> " +
                       eBucsText + stockDetails + "<br>" +
                       "<span class='glyphicon glyphicon-ok'></span> Eligible Cash on Delivery <br />" +
                       "<span class='glyphicon glyphicon-ok'></span> Free delivery available <br />" +
                       "<span class='glyphicon glyphicon-ok'></span> Hassle - Free Exchanges & Returns for 30 Days <br />" + strWarranty;
                    }

                    divDetails.InnerHtml = details;
                    selectedItem = reader["Id"].ToString();
                    // Cart panel

                    divCart.InnerHtml = "<div style='border:solid; border-color:lavender; margin-top:50px; margin-right:50px; margin-left:50px; text-align:center;'><br/><br/>" +
                             "<span> <strong  Style='color:Red;'>  R " + reader["SellingPrice"] + " </strong> </span><br><br>" +
                             "<button type='button' style='width: 250px; background-color:green; color:white;'> <span class='glyphicon glyphicon-plus' style='color:white'></span>  <span class='glyphicon glyphicon-shopping-cart' style='color:white'></span> Add to cart </button> <br><br>" +
                             "<span class='glyphicon glyphicon-heart' style='color:red'></span> <a href='#' onclick='addToWishList(" + reader["Id"] + ");' id='wishItem' runat='server'> Add to wish list </a>  <br><br>" +
                            "</div> ";
                }

                    con.Close();
            } catch(Exception)
            {

            }

        }


        public void linkWishList_Click(object sender, EventArgs e)
        {
            //Response.Cookies["WishListItem"].Value = "";
            //Response.Cookies["WishListDate"].Value = "";

            if (Request.Cookies["WishListItem"].Value == "" || Request.Cookies["WishListItem"].Value == null)
            {
                Response.Cookies["WishListItem"].Value = selectedItem + ",";
                Response.Cookies["WishListDate"].Value = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ",";
            }
            else
            {
                string collectionString = Request.Cookies["WishListItem"].Value.Remove(Request.Cookies["WishListItem"].Value.Length - 1);
                string[] items = collectionString.Split(',');
                bool addValue = true;

                for (int x = 0; x < items.Length; x++)
                {
                    if (items[x] == selectedItem)
                    {
                        divErrorDisplay.InnerHtml = "<p class='alert alert-success' runat='server'>Item already added to your wish list.</p>";
                        addValue = false;
                    }
                }

                if (addValue == true)
                {
                    Response.Cookies["WishListItem"].Value += Request.Cookies["WishListItem"].Value + selectedItem + ",";
                    Response.Cookies["WishListDate"].Value += Request.Cookies["WishListDate"].Value + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ",";
                    Response.Redirect(Request.RawUrl);
                    divErrorDisplay.InnerHtml = "<p class='alert alert-success' runat='server'>Item successfully added to wish list.</p>";
                }
            }

        }
    }
}