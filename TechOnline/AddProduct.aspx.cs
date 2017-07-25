using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechOnline
{
    public partial class AddProduct : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            loadBrands();
            loadCategories();
            loadProductLocation();
        }

        public void loadBrands()
        {
            SqlConnection con = new SqlConnection(strConnection);

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM DataLookup WHERE DataKey = @item ", con);
            cmd.Parameters.AddWithValue("@item", "Product_Brand");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem genderList = new ListItem();
                genderList.Text = reader["DataValue"].ToString();
                genderList.Value = reader["Id"].ToString();
                drpBrand.Items.Add(genderList);
            }
        }

        public void loadCategories()
        {
            SqlConnection con = new SqlConnection(strConnection);

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ProductCategory", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem genderList = new ListItem();
                genderList.Text = reader["Description"].ToString();
                genderList.Value = reader["Id"].ToString();
                drpCategory.Items.Add(genderList);
            }
        }

        public void loadProductLocation()
        {
            SqlConnection con = new SqlConnection(strConnection);

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM DataLookup WHERE DataKey = @item ", con);
            cmd.Parameters.AddWithValue("@item", "Product_Location");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem genderList = new ListItem();
                genderList.Text = reader["DataValue"].ToString();
                genderList.Value = reader["Id"].ToString();
                drpStorageLocation.Items.Add(genderList);
            }
        }


        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            HttpPostedFile myImage = flImage.PostedFile;
            string fileName = Path.GetFileName(myImage.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = myImage.ContentLength;
            
            bool formValid = true;
            string strError = "";
            byte[] bytes = null;

            if (fileName != "")
            {
                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" ||
                    fileExtension.ToLower() == ".tiff" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".bmp")
                {
                    Stream stream = myImage.InputStream;
                    BinaryReader br = new BinaryReader(stream);
                    bytes = br.ReadBytes((int)stream.Length);

                } else
                {
                    formValid = false;
                    strError += "Invalid file loaded, only (.jpg, .jpeg, .png, .tiff, .gif, .bmp) accepted<br />";
                }
            }
            else
            {
                formValid = false;
                strError += "Please add product image <br />";
            }

            try { Convert.ToDecimal(txtPrice.Value); }
            catch {
                formValid = false;
                strError += "Price entered is not valid <br />";
                txtPrice.Focus();
            }
            try { Convert.ToDecimal(txtSellingPrice.Value); }
            catch {
                strError += "Selling price entered is not valid  <br />";
                txtSellingPrice.Focus();
                formValid = false;
            }

            if (formValid == true)
            {
                DateTime todayDate = DateTime.Now;
                SqlConnection con = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (Name, Description, Barcode, Quantity, Price, CategoryId, Image, SellingPrice, CapturedDate, BrandId, Warranty, StorageLocation) " +
                    " VALUES (@Name, @Description, @Barcode, @Quantity, @Price, @CategoryId, @Image , @SellingPrice, @CapturedDate, @BrandId, @Warranty, @StorageLocation) ", con);

                SqlParameter parmNewId = new SqlParameter()
                {
                    ParameterName = "@newId",
                    Value = -1,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.AddWithValue("@Name", txtName.Value);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Value);
                cmd.Parameters.AddWithValue("@Barcode", txtBarCode.Value);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Value);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Value);
                cmd.Parameters.AddWithValue("@CategoryId", drpCategory.Value);
                cmd.Parameters.AddWithValue("@Image", bytes);
                cmd.Parameters.AddWithValue("@SellingPrice", txtSellingPrice.Value);
                cmd.Parameters.AddWithValue("@CapturedDate", todayDate.Date);
                cmd.Parameters.AddWithValue("@BrandId", drpBrand.Value);
                cmd.Parameters.AddWithValue("@Warranty", txtWarranty.Value);
                cmd.Parameters.AddWithValue("@StorageLocation", drpStorageLocation.Value);
                cmd.Parameters.Add(parmNewId);               

                con.Open();
                int proId = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                Response.Redirect("~/ProductDetails?Id=" + proId);

            } else
            {
                divErrorDisplay.InnerHtml = "<p class='alert alert-danger' runat='server'> " + strError + " </p>";
            }
        }
    }
}