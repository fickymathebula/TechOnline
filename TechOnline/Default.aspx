<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/SubMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechOnline._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ChildContent1" runat="server">

    <style>

        body{
            color: black;
        }
    </style>

    <div class="row">
        <div class="col-sm-4"></div>
        <div class="col-sm-7">
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                </ol>

                <!-- Wrapper for slides -->
                <div class="carousel-inner" style="height:200px;width:100%">
                    <div class="item active">
                        <img src="Images/Gifts-for-Baby-LP.jpg" alt="Babies"  style="width: 100%; height: 200px">
                    </div>

                    <div class="item">
                        <img src="Images/special-offer.jpg" alt="Specials" style="width: 100%; height: 150px" >
                    </div>

                    <div class="item">
                        <img src="Images/Cellular-Dept.jpg" alt="Celullar" style="width: 100%; height: 150px">
                    </div>

                    <!-- Left and right controls -->
                <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                    <span class="sr-only">Next</span>
                </a>
                </div>
            </div>
        </div>
          <div class="col-sm-1"></div>
    </div>


    <br />
    <br />


    <%--Laptops--%>
     <div class="row" id="firstRowHeader" runat="server">           
        
        </div>
        <br />
    <div class="row" id="divFirstRow" runat="server">
    </div>
      <br />  <br />
    <%--Cellphones--%>
        <div class="row" id="secondRowHeader" runat="server">
                      
        </div>
        <br />
        <div class="row" id="divSecondRow" runat="server">
        </div>
      <br />  <br />
        <%--Gamming--%>
        <div class="row" id="thirdRowHeader" runat="server">
            
        </div>
        <br />
        <div class="row" id="divThirdRow" runat="server">
        </div>
      <br />  <br />
        <%--TV--%>
     <div class="row" id="fourthRowHeader" runat="server">
            
        </div>
     <br />
        <div class="row" id="divFourthRow" runat="server">
        </div>
</asp:Content>
