<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TechOnline.Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" id="ErrorPanel">
       
        <div class="col-sm-3"></div>
        <div class="col-sm-6" id="divErrorDisplay" runat="server">
            
        </div>
        <div class="col-sm-3"></div>
    </div>

    <div class="row">

         <div class="col-sm-3"></div>

        <div class="col-sm-3">

            <p class="page-header"><strong>Existing Users</strong></p><br />


             My email address:<br />
            <input type="email" id="txUsername" runat="server"/><br />
            Password:<br />
            <input type="password" id="txPassword" runat="server"/><br /><br />

            <%--<asp:LinkButton ID="btnLogin" 
                runat="server"
                CssClass="btn btn-primary" OnClick="btnLogin_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-lock"> Login </span>
            </asp:LinkButton>--%>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click"/>

            <br /><br />
            Forgot password? <a href="#">Click here</a>
        </div>

        <div class="col-sm-3">
           <p class="page-header"><strong>New Users</strong></p><br />

             My email address:<br />
            <input type="email" id="txRegister" runat="server"/><br /><br />

            <%--<button type="button" class="btn btn-info" id="btnRegister" runat="server">
             <span class="glyphicon glyphicon-lock"></span> Register
            </button>

             <asp:LinkButton ID="btnRegister" 
                runat="server"
                CssClass="btn btn-primary" OnClick="btnRegister_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-lock"> Register </span>
            </asp:LinkButton>--%>

             <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click"/>

        </div>

        <div class="col-sm-3"></div>
    </div>

</asp:Content>
