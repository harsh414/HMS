<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayBill.aspx.cs" Inherits="HospitalManagementUI.DisplayBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
         <div class="form-group">
                <asp:Label ID="Label1" runat="server">Patients</asp:Label>
                 <asp:DropDownList ID="ddlPatient" AutoPostBack="True" OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged" runat="server" CssClass="form-control">
        
                 </asp:DropDownList>
          </div>

            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Get BILL" CssClass="btn btn-primary" 
                    OnClick="btnSave_Click"/>
            </div>
 
    <asp:Label ID="Label2" runat="server" Text="TOTAL BILL IN INR"></asp:Label>
    <asp:GridView ID="gvBill" runat="server" BackColor="black" 
    BorderColor="#33cccc" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
    GridLines="Both" AutoGenerateColumns="False" >
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="p_id" ItemStyle-Width=10%  ReadOnly="true" HeaderText="PatientId" />
            <asp:BoundField DataField="consultancy_fee" ItemStyle-Width=15%  HeaderText="consultancy fee" />
            <asp:BoundField DataField="room_type" ItemStyle-Width=10%  HeaderText="room type" />
            <asp:BoundField DataField="no_of_days" ItemStyle-Width=10%  HeaderText="no_of_days" />
            <asp:BoundField DataField="dr_name" ItemStyle-Width=30%  HeaderText="dr_name" />
            <asp:BoundField DataField="op_charge" ItemStyle-Width=30%  HeaderText="op_charge" />
            <asp:BoundField DataField="room_charge" ItemStyle-Width=30%  HeaderText="room_charge" />
            <asp:BoundField DataField="test_charge" ItemStyle-Width=30%  HeaderText="test_charge" />
            <asp:BoundField DataField="med_charge" ItemStyle-Width=30%  HeaderText="med_charge" />
            <asp:BoundField DataField="final_bill" ItemStyle-Width=30%  HeaderText="final_bill" />
      
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
        </div>
</asp:Content>
