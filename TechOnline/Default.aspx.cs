using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.HtmlControls;

namespace TechOnline
{
    public partial class _Default : Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                addComputers();
                addCellPhones();
                addGamming();
                addTvs();                
            }
            
        }

        public void addComputers()  // Computers & Tables 
        {
            SqlConnection con = new SqlConnection(strConnection);
                     
            SqlCommand firstRow = new SqlCommand("select  prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                "from Product prd " +
                "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                "WHERE pd.Id = @ProdValue", con);
            firstRow.Parameters.AddWithValue("@ProdValue", "2");
            string rowString = "";
            int count = 0;
            string image = "";
            string categoryId = "";

            try
            {
                con.Open();
                SqlDataReader reader = firstRow.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                        var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                        image = "<img src= '" + imgsrc + "'  height = '100' width = '150px' />";
                    }
                    catch (Exception)
                    {
                        image = "<img src='' alt='Image not available' height = '100' width = '150px' />";
                    }

                    if (count < 6)
                    {
                        if (Convert.ToDecimal(reader["SellingPrice"]) < Convert.ToDecimal(reader["Price"]))
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> <br> <del> R " + reader["Price"] + " </del> </p> </a> </div>";

                        }
                        else
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> </p> </a> </div>";
                        }
                    }
                    count++;
                    categoryId = reader["CategoryId"].ToString();
                }
                con.Close();

            } catch(Exception)
            { }

            divFirstRow.InnerHtml = rowString;     
            firstRowHeader.InnerHtml = "<div class='col-sm-6'> <p><a href='Search?defaultData=2,0' style ='color:black;'> Take a look at our affordable computers</a></p> </div>" +
                                        "<div class='col-sm-6' style='text-align: right'> <a href = 'Search?defaultData=2,0' class='btn btn-info' role='button'>View More</a></div>";
        }


        public void addCellPhones() // Cellphones & GPS 
        {
            SqlConnection con = new SqlConnection(strConnection);

            SqlCommand firstRow = new SqlCommand("select  prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                "from Product prd " +
                "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                "WHERE pd.Id = @ProdValue", con);
            firstRow.Parameters.AddWithValue("@ProdValue", "3");
            string rowString = "";
            int count = 0;
            string image = "";
            string categoryId = "";

            try
            {
                con.Open();
                SqlDataReader reader = firstRow.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                        var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                        image = "<img src= '" + imgsrc + "'  height = '100' width = '150px' />";
                    }
                    catch (Exception)
                    {
                        image = "<img src='' alt='Image not available' height = '100' width = '150px' />";
                    }

                    if (count < 6)
                    {
                        if (Convert.ToDecimal(reader["SellingPrice"]) < Convert.ToDecimal(reader["Price"]))
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> <br> <del> R " + reader["Price"] + " </del> </p> </a> </div>";

                        }
                        else
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> </p> </a> </div>";
                        }
                    }
                    count++;
                    categoryId = reader["CategoryId"].ToString();
                }
                con.Close();
            }
            catch (Exception)
            { }

            divSecondRow.InnerHtml = rowString;
            secondRowHeader.InnerHtml = "<div class='col-sm-6'> <p><a href = 'Search?defaultData=3,0' style ='color:black;'> Own a smartphone for upgrade</a></p> </div>" +
                                        "<div class='col-sm-6' style='text-align: right'> <a href = 'Search?defaultData=3,0' class='btn btn-info' role='button'>View More</a></div>";
        }

        public void addGamming() // Gamming
        {
            SqlConnection con = new SqlConnection(strConnection);

            SqlCommand firstRow = new SqlCommand("select  prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                "from Product prd " +
                "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                "WHERE pd.Id = @ProdValue", con);
            firstRow.Parameters.AddWithValue("@ProdValue", "8");
            string rowString = "";
            int count = 0;
            string image = "";
            string categoryId = "";

            try
            {
                con.Open();
                SqlDataReader reader = firstRow.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                        var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                        image = "<img src= '" + imgsrc + "'  height = '100' width = '150px' />";
                    }
                    catch (Exception)
                    {
                        image = "<img src='' alt='Image not available' height = '100' width = '150px' />";
                    }

                    if (count < 6)
                    {
                        if (Convert.ToDecimal(reader["SellingPrice"]) < Convert.ToDecimal(reader["Price"]))
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> <br> <del> R " + reader["Price"] + " </del> </p> </a> </div>";

                        }
                        else
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> </p> </a> </div>";
                        }
                    }
                    count++;
                    categoryId = reader["CategoryId"].ToString();
                }
                
                con.Close();
            }
            catch (Exception)
            { }

            divThirdRow.InnerHtml = rowString;
            thirdRowHeader.InnerHtml = "<div class='col-sm-6'> <p><a href = 'Search?defaultData=8,0' style ='color:black;'> Gaming is fun, checkout the latest games here</a></p> </div>" +
                                        "<div class='col-sm-6' style='text-align: right'> <a href = 'Search?defaultData=8,0' class='btn btn-info' role='button'>View More</a></div>";
        }

        public void addTvs()  // TV, Audio & Video
        {          
            SqlConnection con = new SqlConnection(strConnection);

            SqlCommand firstRow = new SqlCommand("select  prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                "from Product prd " +
                "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                "WHERE pd.Id = @ProdValue", con);
            firstRow.Parameters.AddWithValue("@ProdValue", "4");
            string rowString = "";
            int count = 0;
            string image = "";
            string categoryId = "";

            try
            {
                con.Open();
                SqlDataReader reader = firstRow.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                        var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                        image = "<img src= '" + imgsrc + "'  height = '100' width = '150px' />";
                    }
                    catch (Exception)
                    {
                        image = "<img src='' alt='Image not available' height = '100' width = '150px' />";
                    }

                    if (count < 6)
                    {                        
                        if (Convert.ToDecimal(reader["SellingPrice"]) < Convert.ToDecimal(reader["Price"]))
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> "+image+" <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> <br> <del> R " + reader["Price"] + " </del> </p> </a> </div>";

                        }
                        else
                        {
                            rowString += "<div class='col-sm-2' > <a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " <br> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> </p> </a> </div>";
                        }
                    }
                    count++;
                    categoryId = reader["CategoryId"].ToString();
                }
                con.Close();
            }
            catch (Exception)
            { }

            divFourthRow.InnerHtml = rowString;
            fourthRowHeader.InnerHtml = "<div class='col-sm-6'> <p><a href = 'Search?defaultData=4,0' style ='color:black;'> Experience the great entertainment at home</a ></p> </div>" +
                                        "<div class='col-sm-6' style='text-align: right'> <a href = 'Search?defaultData=4,0' class='btn btn-info' role='button'>View More</a></div>";
        }
                
        public void addProducts(string depValue, HtmlGenericControl myDiv)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");

            div.ID = "divFourthRow";
            div.InnerHtml = "Hello";

        }


    }
}