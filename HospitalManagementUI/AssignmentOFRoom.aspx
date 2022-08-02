<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignmentOFRoom.aspx.cs" Inherits="HospitalManagementUI.AssignmentOFRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
 <div class="container">
    <h2> Different Rooms for IPD patients</h2>

     <asp:GridView ID="gvRoom"  runat="server" BackColor="Black" 
    BorderColor="#33CCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
        CellSpacing="2" OnSelectedIndexChanged="gvRoom_SelectedIndexChanged" CssClass=""
         PageSize="2" Height="16px" Width="722px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
           <asp:BoundField DataField= "id" ItemStyle-Width=10%  ReadOnly="true" HeaderText="Room Id" > 
                <ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="room_type" ItemStyle-Width=30%  HeaderText="Room Type" >
                <ItemStyle Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="occupancy" ItemStyle-Width=30%  HeaderText="Allowed Occupancy" >
                <ItemStyle Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="price" ItemStyle-Width=30%  HeaderText="Room Charges(per day)" >
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

     <hr /> 
        <div class="form-group">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
        </div>
</div>
</asp:Content>
