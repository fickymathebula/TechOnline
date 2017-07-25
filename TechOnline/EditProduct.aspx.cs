﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechOnline
{
    public partial class EditProduct : System.Web.UI.Page
    {
        string strConnection = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        byte[] originalImage = null;
        string proId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            
                loadBrands();
                loadCategories();
                loadProductLocation();

                SqlConnection con = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("select * from Product WHERE Id = @ProdId", con);
                cmd.Parameters.AddWithValue("@ProdId", Request.QueryString["Id"]);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        try
                        {
                            proId = reader["Id"].ToString();
                            originalImage = (byte[])reader["Image"];
                            var base64 = Convert.ToBase64String((byte[])reader["Image"]);
                            var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                            divImage.InnerHtml = "<br><br><br><img class='img-responsive' src='" + imgsrc + "' style='max-width:200px; max-height:200px;' alt='No image added' />";
                        }
                        catch (Exception)
                        {
                            divImage.InnerHtml = "";
                        }
                        if (!IsPostBack)
                        {
                            txtName.Value = reader["Name"].ToString();
                            txtDescription.Value = reader["Description"].ToString();
                            txtBarCode.Value = reader["Barcode"].ToString();
                            txtQuantity.Value = reader["Quantity"].ToString();
                            txtPrice.Value = reader["Price"].ToString();
                            drpCategory.Value = reader["CategoryId"].ToString();
                            txtSellingPrice.Value = reader["SellingPrice"].ToString();
                            drpBrand.Value = reader["BrandId"].ToString();
                            txtWarranty.Value = reader["Warranty"].ToString();
                            drpStorageLocation.Value = reader["StorageLocation"].ToString();
                        }

                    }

                    con.Close();
                }
                catch (Exception) { }
            
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

        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            HttpPostedFile myImage = flImage.PostedFile;
            string fileName = Path.GetFileName(myImage.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = myImage.ContentLength;

            bool formValid = true;
            string strError = "";
            byte[] bytes = null;
            Stream stream = myImage.InputStream;
            BinaryReader br = new BinaryReader(stream);

            // Compare images

            // Check if db has image, if yes compare else validate current image


            if (originalImage == null)
            {
                if (fileName != "")
                {
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" ||
                        fileExtension.ToLower() == ".tiff" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".bmp")
                    {                       
                        bytes = br.ReadBytes((int)stream.Length);
                    }
                    else
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

                // Insert with byte as new image

            } else if(originalImage != null )
            {
                if (fileName == "")
                {
                    bytes = originalImage;
                } else
                {
                    bytes = br.ReadBytes((int)stream.Length);
                }
            }
            

            try { Convert.ToDecimal(txtPrice.Value); }
            catch
            {
                formValid = false;
                strError += "Price entered is not valid <br />";
                txtPrice.Focus();
            }
            try { Convert.ToDecimal(txtSellingPrice.Value); }
            catch
            {
                strError += "Selling price entered is not valid  <br />";
                txtSellingPrice.Focus();
                formValid = false;
            }

            if (formValid == true)
            {
                DateTime todayDate = DateTime.Now;
                SqlConnection con = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("UPDATE Product SET Name = @Name, Description = @Description, Barcode = @Barcode, Quantity = @Quantity, Price = @Price, CategoryId = @CategoryId, Image = @Image, SellingPrice = @SellingPrice, BrandId = @BrandId, Warranty = @Warranty, StorageLocation = @StorageLocation WHERE Id = @Id ", con);
                cmd.Parameters.AddWithValue("@Id", proId.ToString());
                cmd.Parameters.AddWithValue("@Name", txtName.Value);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Value);
                cmd.Parameters.AddWithValue("@Barcode", txtBarCode.Value);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Value);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Value);
                cmd.Parameters.AddWithValue("@CategoryId", drpCategory.Value);
                cmd.Parameters.AddWithValue("@Image", bytes);
                cmd.Parameters.AddWithValue("@SellingPrice", txtSellingPrice.Value);
                cmd.Parameters.AddWithValue("@BrandId", drpBrand.Value);
                cmd.Parameters.AddWithValue("@Warranty", txtWarranty.Value);
                cmd.Parameters.AddWithValue("@StorageLocation", drpStorageLocation.Value); 

                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                Response.Redirect("~/ProductDetails?Id=" + proId);

            }
            else
            {
                divErrorDisplay.InnerHtml = "<p class='alert alert-danger' runat='server'> " + strError + " </p>";
            }
        }


    }
}