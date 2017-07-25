<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="TechOnline.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
     <div class="row" id="ErrorPanel">

        <div class="col-sm-3"></div>
        <div class="col-sm-6" id="divErrorDisplay" runat="server">

        </div>
        <div class="col-sm-3"></div>
    </div>

   
    <div class="row">

        <div class="col-sm-3"></div>

        <div class="col-sm-6">
            <p class="page-header"><strong>Edit Product</strong></p>
            <br />

            <asp:Label ID="Label1" runat="server" Text="Product Name *" Width="150px"></asp:Label>
            <input type="text" id="txtName" runat="server" required /><br />
            <br />

            <asp:Label ID="Label2" runat="server" Text="Product Description *" Width="150px"></asp:Label>
            <input type="text" id="txtDescription" runat="server" required /><br />
            <br />

            <asp:Label ID="Label3" runat="server" Text="Bar Code" Width="150px"></asp:Label>
            <input type="text" id="txtBarCode" runat="server" required /><br />
            <br />

            <asp:Label ID="Label8" runat="server" Text="Quantity *" Width="150px"></asp:Label>
            <input type="number" id="txtQuantity" runat="server" required /><br />
            <br />

            <asp:Label ID="Label4" runat="server" Text="Price *" Width="150px"></asp:Label>
            <input type="text" id="txtPrice" runat="server" required /><br />
            <br />

            <asp:Label ID="Label6" runat="server" Text="Selling Price *" for="txtSellingPrice" Width="150px"></asp:Label>
            <input type="text" id="txtSellingPrice" runat="server" required /><br />
            <br />  
            
            <asp:Label ID="Label10" runat="server" Text="Warranty *" for="txtWarranty" Width="150px"></asp:Label>
            <input type="number" id="txtWarranty" runat="server" required /><br />
            <br /> 

            <asp:Label ID="Label9" runat="server" Text="Category *" Width="150px"></asp:Label>
            <select id="drpCategory" runat="server" required>
                <option value="">----</option>
            </select><br />
            <br />

            <asp:Label ID="Label7" runat="server" Text="Brand *" Width="150px"></asp:Label>
            <select id="drpBrand" runat="server" required>
                <option value="">----</option>
            </select> 
            <br />
            <br />
            <asp:Label ID="Label11" runat="server" Text="Storage Location *" Width="150px"></asp:Label>
            <select id="drpStorageLocation" runat="server" required>
                <option value="">----</option>
            </select> 
            <br />
            <div class="col-sm-4" id="divImage" runat="server">
            </div><br />
             <asp:Label ID="Label5" runat="server" Text="Image" Width="500px"></asp:Label><br />
            <%--<asp:FileUpload ID="flImage" runat="server" />--%>
             <input type ="file" id ="flImage" name ="image" runat="server"/>
            <br />
            <br />
            <asp:Button ID="btnEditProduct" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnEditProduct_Click" />

        </div>

        <div class="col-sm-3"></div>
    </div>

</asp:Content>
