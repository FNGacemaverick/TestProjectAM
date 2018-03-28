<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductSearch.aspx.vb" MasterPageFile="~/Default.Master" Inherits="ProductTest.ProductSearch"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
	
<asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">

	<table class="ThinBorder" cellPadding="5" width="100%" border="0">
		<tr class="BoxHeader">
			<td colSpan="2">Product Search</td>
		</tr>
		<tr class="BoxMain">
			<td width="120"><b>Name:</b></td>
			<td><asp:TextBox ID="txtName" Runat="server" /></td>
		</tr>
        <tr class="BoxMain">
			<td width="120"><b>Sold:</b></td>
			<td><asp:CheckBox ID="ckSold" Runat="server" /></td>
		</tr>
        <tr class="BoxMain">
			<td width="120"><b>Color:</b></td>
			<td><asp:TextBox ID="txtColor" Runat="server" /></td>
		</tr>
        <tr class="BoxMain">
            <td><b>Category:</b></td>
			<td><asp:DropDownList ID="cboCategory" Runat="server" /></td>
        </tr>
		<tr class="BoxMain">
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr class="BoxMain">
			<td colspan="2">
			    <asp:Button ID="btnSearch" Runat="server" Text="Search" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			    <asp:Button ID="btnCreatePDF" Runat="server" Text="Create PDF" />			
			</td>
		</tr>
	</table>

</asp:Panel>	

<asp:UpdatePanel ID="upnlResults">
    
    <ContentTemplate>

	<table class="ThinBorder" cellPadding="5" width="100%" border="0">
        <tr class="BoxMain">
            <td colspan="2" style="text-align:center;">
                <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" PostBackUrl="~/EditProduct.aspx" />
            </td>
        </tr>
		<tr class="BoxHeader">
			<td colspan="2">Products</td>
		</tr>
		<tr class="BoxMain">
			<td colspan="2">
				<asp:datagrid id="dgProducts" runat="server" Width="100%" DataKeyField="ID">
					<Columns>
						<asp:TemplateColumn ItemStyle-Width="50">
							<ItemTemplate>
								<asp:ImageButton ID="cmdDelete" Runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Name" SortExpression="Name">
							<ItemTemplate>
								<asp:HyperLink ID="lnkName" Runat="server" Text='<%# Container.DataItem.Name %>' NavigateUrl='<%# "EditProduct.aspx?ID=" & Container.DataItem.ID %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Category" SortExpression="Category" HeaderText="Category" />
						<asp:BoundColumn DataField="Price" SortExpression="Price" HeaderText="Price" DataFormatString="{0:C}" />
                        <asp:BoundColumn DataField="Color" SortExpression="Color" HeaderText="Color"/>
                        <asp:BoundColumn DataField="Sold" SortExpression="Sold" HeaderText="Sold" />
					</Columns>
				</asp:datagrid>
			</td>
		</tr>
	</table><br>
    </ContentTemplate>

    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
    </Triggers>


</asp:UpdatePanel>

</asp:Content>

