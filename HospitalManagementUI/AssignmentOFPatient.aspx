<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignmentOFPatient.aspx.cs" Inherits="HospitalManagementUI.AssignmentOFPatient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
            <asp:Label ID="Label6" runat="server">ASSIGN TO IPD</asp:Label>
             <asp:DropDownList ID="ddlAssign" AutoPostBack="True" OnSelectedIndexChanged="ddlAssign_SelectedIndexChanged" runat="server" CssClass="form-control">
                <asp:ListItem value="" Text="Select"/>
                 <asp:ListItem value="1" Text="Yes"/>
                <asp:ListItem value="0" Text="No"/>
             </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
     </div>
</asp:Content>
