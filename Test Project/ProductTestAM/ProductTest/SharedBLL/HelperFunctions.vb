Imports System.IO
Imports System.Xml

Public Class HelperFunctions
    
    '*********************************************************************
    ' Function: ShowAlert
    '*********************************************************************
    Public Shared Sub ShowAlert(ByRef oPage As System.Web.UI.Page, ByVal sMsg As String)
        sMsg = sMsg.Replace("'", "\'")  ' Account for apostrophes
        oPage.ClientScript.RegisterStartupScript(oPage.GetType(), "alert", String.Format("<script>alert('{0}');</script>", sMsg))
    End Sub

    '*********************************************************************
    ' Function: ShowConfirmation
    '*********************************************************************
    Public Shared Sub ShowConfirmation(ByVal ctrl As System.Web.UI.WebControls.WebControl, ByVal sMsg As String)
        sMsg = sMsg.Replace("'", "\'")  ' Account for apostrophes
        ctrl.Attributes.Add("onclick", String.Format("return confirm('{0}');", sMsg))
    End Sub

    '*********************************************************************
    ' Function:  SelectDropDownValue
    '*********************************************************************
    Public Shared Sub SelectDropDownValue(ByRef cbo As System.Web.UI.WebControls.DropDownList, ByVal sValue As String)
        cbo.SelectedIndex = cbo.Items.IndexOf(cbo.Items.FindByValue(sValue))
    End Sub

    '*********************************************************************
    ' Function:  GetQSValue
    '*********************************************************************
    Public Shared Function GetQSValue(ByVal sQS As String, ByVal sName As String) As String
        sQS = sQS.TrimStart("?")

        Dim sNameValPairs() As String = sQS.Split("&")
        Dim s As String
        Dim sNameVal() As String

        For Each s In sNameValPairs
            sNameVal = s.Split("=")
            If sNameVal(0).ToLower() = sName.ToLower() Then
                Return sNameVal(1)
            End If
        Next

        Return String.Empty
    End Function

    '*********************************************************************
    ' Function:  GetIntQSValue
    '*********************************************************************
    Public Shared Function GetIntQSValue(ByVal sQS As String, ByVal sName As String) As Integer
        Dim sValue As String = GetQSValue(sQS, sName)

        If IsNumeric(sValue) Then
            Return Convert.ToInt32(sValue)
        End If

        Return 0
    End Function

    '*********************************************************************
    ' Function:  DisplayWindow
    '*********************************************************************
    Public Shared Sub DisplayWindow(ByRef oPage As System.Web.UI.Page, ByVal sUrl As String, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim sScript As String = String.Format("displayWindow('{0}', {1}, {2});", sUrl, iWidth, iHeight)
        oPage.ClientScript.RegisterStartupScript(oPage.GetType, "displayWindow", sScript, True)
    End Sub

End Class
