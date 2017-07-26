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
    public partial class Search : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string leftText = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnection);

            try
            {
                string[] collectData = Request.QueryString["defaultData"].Split(',');

                string searchText = collectData[0];
                string searchDept = collectData[1];

                string sqlQry = "";
                string disp = "";
                string image = "";

                string filterText = "";
                
                if (searchDept.Equals("*")) // Select from all departments
                {
                    sqlQry = "select prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                    "from Product prd " +
                    "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                    "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                    "WHERE prd.Description like '%" + searchText + "%' " +
                    "OR prd.Name like '%" + searchText + "%' ";

                    filterText = "<p style='color:lightblue'>FILTERS</p><strong>Search</strong> - '" + searchText + "' ";

                }
                else if (searchDept.Equals("0"))
                {
                    sqlQry = "select  prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                    "from Product prd " +
                    "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                    "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                    "WHERE pd.Id = @dept";

                }
                else // Specific department selected
                {
                    sqlQry = "select prd.Id, prd.Name, prd.Description, prd.Barcode, prd.Quantity, prd.Price, prd.CategoryId, prd.Image, prd.SellingPrice, prd.CapturedDate, prd.BrandId   " +
                    "from Product prd " +
                    "left join ProductCategory pc on pc.Id = prd.CategoryId " +
                    "left join ProductDepartment pd on pd.Id = pc.DepartmentId " +
                    "WHERE pd.Id = @deptValue " +
                    "AND ( prd.Description like '%" + searchText + "%' " +
                    "OR prd.Name like '%" + searchText + "%' ) ";

                    SqlCommand deptCmd = new SqlCommand("SELECT * FROM ProductDepartment WHERE Id = " + collectData[2] + "", con);
                    deptCmd.Parameters.AddWithValue("@deptValue", searchDept);
                    con.Open();
                    SqlDataReader myReader = deptCmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        filterText = "<p style='color:lightblue'>FILTERS</p><strong>Department </strong> - '" + myReader["Description"] + "'  <br>  <strong>Search</strong> - '" + searchText + "' ";
                    }
                    con.Close();

                }

                SqlCommand cmd = new SqlCommand(sqlQry, con);
                cmd.Parameters.AddWithValue("@deptValue", searchDept);
                cmd.Parameters.AddWithValue("@dept", searchText);


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                        var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                        image = "<img src= '" + imgsrc + "'  height = '100px' width = '150px' />";
                    }
                    catch (Exception)
                    {
                        image = "<img src='' alt='Image not available' height = '100px' width = '150px' />";
                    }


                    disp += "<p class='page-header' style='height:50px'> <div class='col-sm-10'>" +
                            "<div class='col-sm-3' ><a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> " + image + " </a></div> <br><br>" +
                            "<div class='col-sm-6' >" +
                            "<a style='color: black;' runat='server' href='ProductDetails?Id=" + reader["Id"] + "'> <p> " + reader["Description"] + " <br> <strong> R " + reader["SellingPrice"] + " </strong> <br> </a>" +
                            "</div> " +
                             "<div class='col-sm-3' style='text-align:right;'> " +
                            "<button type ='button' style ='background-color:green; color:white;'> <span class='glyphicon glyphicon-plus' style='color:white'></span>  " +
                            "<span class='glyphicon glyphicon-shopping-cart' style='color:white'></span> Add to cart</button> <br><br>" +
                            "<span class='glyphicon glyphicon-heart' style='color:red'></span> <a href='#' onclick='addToWishList(" + reader["Id"] + ");' > Add to wish list </a>  <br><br>" +
                            "</div> " +
                            "</div></p><br><br>";

                }
                con.Close();

                leftText += filterText + "<br><br> <p style='color:lightblue'>DEPARTMENT</p><br>";
                getDepartments();

                rightPanel.InnerHtml = disp;
                leftPanel.InnerHtml = leftText;
            }
            catch (Exception)
            {
                con.Close();
            }

           
        }

        public void getDepartments()
        {
            using (SqlConnection con = new SqlConnection(strConnection))
            {
                con.Close();
                SqlCommand deptCmd = new SqlCommand("SELECT * FROM ProductDepartment", con);

                con.Open();
                SqlDataReader deptReader = deptCmd.ExecuteReader();
                while (deptReader.Read())
                {
                    int count = 0;
                    SqlCommand catCmd = new SqlCommand("SELECT * FROM ProductCategory Where DepartmentId = @dept ", con);
                    catCmd.Parameters.AddWithValue("@dept", deptReader["Id"].ToString());


                    SqlDataReader catReader = catCmd.ExecuteReader();
                    while (catReader.Read())
                    {
                        SqlCommand prodCmd = new SqlCommand("SELECT * FROM Product Where CategoryId = @catId ", con);
                        prodCmd.Parameters.AddWithValue("@catId", catReader["Id"].ToString());
                        SqlDataReader prodReader = prodCmd.ExecuteReader();

                        while (prodReader.Read())
                        {
                            count++;
                        }
                    }

                    leftText += "<p> <a style='color:black' href='Search?defaultData=" + deptReader["Id"] + ",0  '>" + deptReader["Description"] + "</a> (<span style='color:lightblue'> " + count + " </span> ) </p>";
                }

                con.Close();
            }
        }

        public void linkWishList_Click(object sender, EventArgs e)
        {
            string selectedItem = txtTemp.Value;

            Response.Write("<script> alert('"+selectedItem+"'); </script>");

            //if (Request.Cookies["WishListItem"].Value == "" || Request.Cookies["WishListItem"].Value == null)
            //{
            //    Response.Cookies["WishListItem"].Value = selectedItem + ",";
            //    Response.Cookies["WishListDate"].Value = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ",";
            //}
            //else
            //{
            //    string collectionString = Request.Cookies["WishListItem"].Value.Remove(Request.Cookies["WishListItem"].Value.Length - 1);
            //    string[] items = collectionString.Split(',');
            //    bool addValue = true;

            //    for (int x = 0; x < items.Length; x++)
            //    {
            //        if (items[x] == selectedItem)
            //        {
            //            divErrorDisplay.InnerHtml = "<p class='alert alert-success' runat='server'>Item already added to your wish list.</p>";
            //            addValue = false;
            //        }
            //    }

            //    if (addValue == true)
            //    {
            //        Response.Cookies["WishListItem"].Value += Request.Cookies["WishListItem"].Value + selectedItem + ",";
            //        Response.Cookies["WishListDate"].Value += Request.Cookies["WishListDate"].Value + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ",";
            //        Response.Redirect(Request.RawUrl);
            //        divErrorDisplay.InnerHtml = "<p class='alert alert-success' runat='server'>Item successfully added to wish list.</p>";
            //    }
            //}

            txtTemp.Value = "";
        }

    }
}