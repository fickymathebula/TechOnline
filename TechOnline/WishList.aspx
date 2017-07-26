<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeBehind="WishList.aspx.cs" Inherits="TechOnline.WishList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildContent1" runat="server">
   
    <h2 style="text-align:center;">Wish List</h2>

   <div class="row">

        <div class="col-sm-2" runat="server" id="leftPanel">
                       
        </div>

        <div class="col-sm-10" runat="server" style="left: 0px; top: 0px" >
            <br />
            <asp:Button ID="btnAddToCartTop" runat="server" Text="Add to cart" style="background-color:green; color:white;" OnClick="btnAddToCart_Click"/>
            <br /><br />
            <div class="row" id="rightPanel" runat="server">
                
            </div>
             <asp:GridView ID="gridWishList" runat="server" ></asp:GridView>
            <br />
            <asp:Button ID="btnAddToCartBottom" runat="server" Text="Add to cart" style="background-color:green; color:white;" OnClick="btnAddToCart_Click"/>
        </div>

    </div>

</asp:Content>
