<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TechOnline.Register" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/jquery-1.10.2.js"></script>

    <div class="row" id="ErrorPanel">
        <br />
        <div class="col-sm-3"></div>
        <div class="col-sm-6" id="divErrorDisplay" runat="server">

        </div>
        <div class="col-sm-3"></div>
    </div>


    <div class="row">

        <div class="col-sm-3"></div>

        <div class="col-sm-6">
            <p class="page-header"><strong>Register</strong></p>
            <br />

            <asp:Label ID="Label1" runat="server" Text="First Name *" Width="150px"></asp:Label>
            <input type="text" id="txtFirstName" runat="server" required /><br />
            <br />

            <asp:Label ID="Label2" runat="server" Text="Last Name *" Width="150px"></asp:Label>
            <input type="text" id="txtLastName" runat="server" required /><br />
            <br />

            <asp:Label ID="Label3" runat="server" Text="Email *" Width="150px"></asp:Label>
            <input type="email" id="txtEmail" runat="server" required /><br />
            <br />

            <asp:Label ID="Label8" runat="server" Text="Retype Email *" Width="150px"></asp:Label>
            <input type="email" id="txtRetypeEmail" runat="server" required /><br />
            <br />

            <asp:Label ID="Label4" runat="server" Text="Password *" Width="150px"></asp:Label>
            <input type="password" id="txtPassword" runat="server" required /><br />
            <br />

            <asp:Label ID="Label9" runat="server" Text="Retype Password *" Width="150px"></asp:Label>
            <input type="password" id="txtRetypePassword" runat="server" required /><br />
            <br />

            <asp:Label ID="Label5" runat="server" Text="Phone Number *" Width="150px"></asp:Label>
            <input type="text" id="txtPhoneNumber" runat="server" maxlength="10" onkeypress="return isNumber(event)" required /><br /><br />

            <asp:Label ID="Label6" runat="server" Text="Gender *" for="drpGender" Width="150px"></asp:Label>
            <select id="drpGender" runat="server" required>
                <option value="">----</option>
            </select><br />
            <br />

            <asp:Label ID="Label7" runat="server" Text="Birthday *" Width="150px"></asp:Label>
            <select id="drpDay" runat="server" required>
                <option value="">----</option>
            </select>

            <select id="drpMonth" runat="server" required>
                <option value="">----</option>
            </select>

            <select id="drpYear" runat="server" required>
                <option value="">----</option>
            </select>

            <br />
            <br />
            <asp:Button ID="btnRegister" runat="server" Text="Register Now" CssClass="btn btn-primary" OnClick="btnRegister_Click" OnClientClick="return ficky()" />

        </div>

        <div class="col-sm-3"></div>
    </div>

    <div class="row">
        <div class="col-sm-3">            
            <input type="text" id="txtRole" runat="server"  value="40" hidden/> <%--In the database id 40 is the primary key that represent Customer--%>
            <input type="text" id="txtActive" runat="server" value="True" hidden/>
        </div>
    </div>


    <script>

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function ficky() {
                      
            var intYear = document.getElementById('<%=drpYear.ClientID%>').value;           
            var intMonth = document.getElementById('<%=drpMonth.ClientID%>').value;           
            var intDay = document.getElementById('<%=drpDay.ClientID%>').value;  
            var leapYear = [1904, 1908, 1912, 1916, 1920, 1924, 1928, 1932, 1936, 1940, 1944, 1948, 1952, 1956, 1960, 1964, 1968, 1972, 1976, 1980, 1984, 1988, 1992, 1996, 2000];

            if (intYear != "" & intMonth != "" & intDay != "")
            {
                for (var x = 0; x < leapYear.length; x++)
                {
                    if (intMonth == 2 && intDay > 29 && intYear == leapYear[x])
                    {
                        alert("Invalid date selected");
                        return false;
                    }                    
                }                              
            } 

            if (document.getElementById('<%=txtPhoneNumber.ClientID%>').value.length != 10)
            {
                alert("Invalid phone number entered");
                return false;
            }
        }

    </script>
</asp:Content>



