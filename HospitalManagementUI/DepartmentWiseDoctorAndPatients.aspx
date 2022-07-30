<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DepartmentWiseDoctorAndPatients.aspx.cs"
    Inherits="HospitalManagementUI.DepartmentWiseDoctorAndPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
        
        <div class="form-group">
            <asp:Label ID="Label6" runat="server">Department</asp:Label>
             <asp:DropDownList ID="ddlDepartment" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server" CssClass="form-control">
                
             </asp:DropDownList>
        </div>
    <asp:GridView ID="gvDoctors" runat="server" BackColor="Black" 
    BorderColor="#33CCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
        CellSpacing="2" OnSelectedIndexChanged="gvDoctors_SelectedIndexChanged" CssClass=""
         PageSize="2">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="id" ItemStyle-Width=10%  ReadOnly="true" HeaderText="DoctorId" >
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="dr_name" ItemStyle-Width=30%  HeaderText="DoctorName" >
<ItemStyle Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:CommandField ButtonType="Button"  ShowSelectButton="True" SelectText="Get Patients" />
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


    <%--2nd for patients--%>
    <div class="gvPatientsGrid">
        <h3>Filtered Patients</h3>
    <asp:GridView ID="gvPatients" runat="server" BackColor="Black" ShowHeaderWhenEmpty="true" 
    BorderColor="#33CCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
        CellSpacing="2" OnSelectedIndexChanged="gvPatients_SelectedIndexChanged"
         PageSize="2">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="id" ItemStyle-Width=10%  ReadOnly="true" HeaderText="Patient ID" >
<ItemStyle Width="10%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="dr_id" ItemStyle-Width=20%  HeaderText="OPD Doctor Id" >
            </asp:BoundField>
            <asp:BoundField DataField="p_name" ItemStyle-Width=20%  HeaderText="Patient Name" >
            </asp:BoundField>
            <asp:CommandField ButtonType="Button"  ShowSelectButton="True" SelectText="Assign To IPD Doctor"/>
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
    </div>
</asp:Content>