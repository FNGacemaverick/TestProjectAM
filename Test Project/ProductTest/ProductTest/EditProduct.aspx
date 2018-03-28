<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditProduct.aspx.vb" MasterPageFile="~/Default.Master" Inherits="ProductTest.EditProduct"%>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
	
	<table class="ThinBorder" cellPadding="5" width="100%" border="0">
		<tr class="BoxHeader">
			<td colSpan="2">Product Details</td>
		</tr>
		<tr class="BoxMain">
			<td width="120"><b>Name:</b></td>
			<td><asp:TextBox ID="txtName" Runat="server" /></td>
		</tr>
        <tr class="BoxMain">
            <td><b>Category:</b></td>
			<td><asp:DropDownList ID="cboCategory" Runat="server" /></td>
        </tr>
		<tr class="BoxMain">
			<td><b>Price:</b></td>
			<td><asp:TextBox ID="txtPrice" Runat="server" /></td>
		</tr>
        <tr class="BoxMain">
			<td colspan="2">&nbsp;</td>
		</tr>
        <tr class="BoxMain">
            <td colspan="2">
                <asp:Button ID="btnSave" Runat="server" Text="Save" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            <asp:Button ID="btnCancel" Runat="server" Text="Cancel" />
            </td>
        </tr>
	</table><br>
	
	

</asp:Content>


