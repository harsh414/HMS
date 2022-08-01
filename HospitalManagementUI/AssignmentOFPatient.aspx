<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignmentOFPatient.aspx.cs" Inherits="HospitalManagementUI.AssignmentOFPatient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="form-group">
                <asp:Label ID="Label5" runat="server">ASSIGN TO IPD</asp:Label>
                 <asp:DropDownList ID="ddlAssign" AutoPostBack="True" OnSelectedIndexChanged="ddlAssign_SelectedIndexChanged" runat="server" CssClass="form-control">
                    <asp:ListItem value="" Text="Select"/>
                     <asp:ListItem value="1" Text="Yes"/>
                    <asp:ListItem value="0" Text="No"/>
                 </asp:DropDownList>
         </div>

        <div class="form-group">
            <asp:Label ID="Label1" runat="server">Test</asp:Label>
             <asp:DropDownList ID="ddlTest" AutoPostBack="True" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged" runat="server" CssClass="form-control">
                
             </asp:DropDownList>
        </div>

         <div class="form-group">
            <asp:Label ID="Label2" runat="server">Medicine</asp:Label>
             <asp:DropDownList ID="ddlMedicine" AutoPostBack="True" OnSelectedIndexChanged="ddlMedicine_SelectedIndexChanged" runat="server" CssClass="form-control">
                
             </asp:DropDownList>
        </div>
         <div class="form-group">
            <asp:Label ID="Label6" runat="server">Medicine Quantity</asp:Label>
            <asp:TextBox ID="mqty" Enabled="False" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="Label3" runat="server">Operation</asp:Label>
             <asp:DropDownList Enabled="False" ID="ddlOperation" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged" runat="server" CssClass="form-control">
                
             </asp:DropDownList>
        </div>
        <div class="form-group">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
        </div>
    </div>
    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>

</asp:Content>
