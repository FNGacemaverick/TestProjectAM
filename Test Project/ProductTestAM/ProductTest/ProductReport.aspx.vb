Public Partial Class ProductReport
    Inherits System.Web.UI.Page

    '*********************************************************************
    ' Function: Page_Load
    '*********************************************************************
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConfigureReport()
    End Sub

    '*********************************************************************
    ' Function: ConfigureReport
    '*********************************************************************
    Private Sub ConfigureReport()
        Dim sName As String = HttpUtility.UrlDecode(HelperFunctions.GetQSValue(Request.Url.Query, "Name"))
        Dim iCategoryID As Int32 = HelperFunctions.GetIntQSValue(Request.Url.Query, "CategoryID")
        Dim bSold As Boolean = HttpUtility.UrlDecode(HelperFunctions.GetQSValue(Request.Url.Query, "Sold"))
        Dim sColor As String = HttpUtility.UrlDecode(HelperFunctions.GetQSValue(Request.Url.Query, "Color"))

        ctrlReport.Cache.Enabled = False
        ctrlReport.Report.Load(Server.MapPath("~") & "\XML Files\ProductReport.xml", "ProductReport")
        ctrlReport.Report.DataSource.Recordset = Product.GetProductsDT(sName, iCategoryID, bSold, sColor)
        ctrlReport.ShowPDF()
    End Sub

    Private Sub ShowPDF(ByVal sFileName As String)
        sFileName = sFileName.Replace("~", "\")

        If System.IO.File.Exists(sFileName) Then
            With Response
                .Clear()
                .ContentType = "application/pdf"
                .AppendHeader("Content-Disposition", String.Format("filename={0}", sFileName))
                .WriteFile(sFileName)
                .Flush()
                .End()

            End With
        End If

    End Sub

End Class