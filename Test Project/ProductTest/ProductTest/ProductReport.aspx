<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductReport.aspx.vb" Inherits="ProductTest.ProductReport" %>
<%@ Register TagPrefix="C1WebReport" Namespace="C1.Web.C1WebReport" Assembly="C1.Web.C1WebReport.2" %>

<html>
	<head>
		<title>Product Report</title>
		<script src="General.js" type="text/javascript"></script>
		<link href="styles.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
	<form id="Form1" method="post" runat="server">
	
	<C1WebReport:C1WebReport ID="ctrlReport" runat="server" />

	</form>
	</body>
</html>