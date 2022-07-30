<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientRegistration.aspx.cs" Inherits="HospitalManagementUI.DataRegistration.PatientRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
        
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Patient Name"></asp:Label>
            <asp:TextBox ID="txtpname" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Age"></asp:Label>
            <asp:TextBox ID="txtage" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="Label3" runat="server">Gender</asp:Label>
             <asp:DropDownList ID="txtgender" AutoPostBack="True" OnSelectedIndexChanged="txtgender_SelectedIndexChanged" runat="server" CssClass="form-control">
                <asp:ListItem value="male" Text="Male"/>
                <asp:ListItem value="female" Text="Female"/>
             </asp:DropDownList>
        </div>

        <div class="form-group">
            <asp:Label ID="Label4" runat="server" Text="Patient Address"></asp:Label>
            <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Mobile Number"></asp:Label>
            <asp:TextBox ID="txtphone" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="Label6" runat="server">Assign To Department</asp:Label>
             <asp:DropDownList ID="ddlDepartment" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server" CssClass="form-control">
                
             </asp:DropDownList>
        </div>

        <div class="form-group">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" 
                OnClick="btnSave_Click"/>
        </div>
    </div>
    <br/>
    <%--<asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
    <hr />--%>
    <asp:GridView ID="gvPatients" runat="server" BackColor="black" 
    BorderColor="#33cccc" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
    GridLines="Both" AutoGenerateColumns="False" 
        OnRowCancelingEdit="gvPatients_RowCancelingEdit" OnRowDeleting="gvPatients_RowDeleting"
        OnRowEditing="gvPatients_RowEditing" OnRowUpdating="gvPatients_RowUpdating"
        OnSelectedIndexChanged="gvPatients_SelectedIndexChanged" CellSpacing="2" 
         PageSize="2">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="id" ItemStyle-Width=10%  ReadOnly="true" HeaderText="PatientId" />
            <asp:BoundField DataField="p_name" ItemStyle-Width=15%  HeaderText="PatientName" />
            <asp:BoundField DataField="age" ItemStyle-Width=10%  HeaderText="Age" />
            <asp:BoundField DataField="gender" ItemStyle-Width=10%  HeaderText="Gender" />
            <asp:BoundField DataField="address" ItemStyle-Width=30%  HeaderText="Address" />
            <asp:BoundField DataField="phone" ItemStyle-Width=10%  HeaderText="Phone" />
            <asp:CommandField ButtonType="Button"  ShowSelectButton="True" />
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#33cc33" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
</asp:Content>
