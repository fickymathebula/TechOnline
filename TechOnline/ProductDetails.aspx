<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="TechOnline.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="row">

        <div class="row" id="ErrorPanel">

        <div class="col-sm-3"></div>
        <div class="col-sm-6" id="divErrorDisplay" runat="server">
            
        </div>
        <div class="col-sm-3"></div>
    </div>

        <br /><br />

        <div class="col-sm-4" id="divImage" runat="server">           
           
        </div>
        <div class="col-sm-4" id="divDetails" runat="server">
             

        </div>
        <div class="col-sm-4" id="divCart" runat="server">

            <div style="border:solid;border-color:lavender; margin-top:50px; margin-right:50px; margin-left:50px; text-align:center;">
                 
            </div>            
        </div>

    </div>

    <style>

        .picbig
        {
            position:absolute;
            width: 0px;
            -webkit-transition:width 0.3s linear 0s;
            transition:width 0.3s linear 0s;
            z-index:10;
        }

        .img-responsive:hover + .picbig
        {
                width:500px;
                 position:absolute;
        }
 </style>

</asp:Content>
