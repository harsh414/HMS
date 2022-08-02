<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientsInIpdandRoomAssignment.aspx.cs" Inherits="HospitalManagementUI.PatientsInIpdandRoomAssignment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <h2> Patients assigned to IPD treatment are :</h2>

     <asp:GridView ID="gvIPDpatients" runat="server" BackColor="Black" 
    BorderColor="#33CCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
        CellSpacing="2" OnSelectedIndexChanged="gvIPDpatients_SelectedIndexChanged" CssClass=""
         PageSize="2" Height="16px" Width="614px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="id" ItemStyle-Width=10%  ReadOnly="true" HeaderText="PatientId" >
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="p_name" ItemStyle-Width=30%  HeaderText="Patient Name" >
<ItemStyle Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:CommandField ButtonType="Button"  ShowSelectButton="True" SelectText="Select Room" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#009999" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#009999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
</div>
</asp:Content>
