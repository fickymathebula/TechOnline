<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TechOnline.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildContent1" runat="server">
    
     <div class="row" id="ErrorPanel">
            <br /><br />
        <div class="col-sm-3"></div>
        <div class="col-sm-6" id="divErrorDisplay" runat="server">
            
        </div>
        <div class="col-sm-3"></div>
    </div>

    <div class="row" id="divDisplay" runat="server" >

    </div>
    
    <div class="row">

        <div class="col-sm-2" runat="server" id="leftPanel">
                       
        </div>

        <div class="col-sm-10" runat="server" >
            <div class="row" id="rightPanel" runat="server">
                
                
            </div>

        </div>

    </div>

     <asp:Button ID="linkWishList" runat="server" OnClick="linkWishList_Click" hidden="true"> </asp:Button> 
     <input type="text" runat="server"  id="txtTemp" hidden/> 

     <script>

        function addToWishList(id)
        {   
            document.getElementById('<%=linkWishList.ClientID%>').click();
            document.getElementById('<%=txtTemp.ClientID%>').value = id;
        }

    </script>

</asp:Content>
