Partial Class EditProduct
    Inherits System.Web.UI.Page

    Private Property ProductID() As Int32
        Get
            Return Convert.ToInt32(ViewState("ProductID"))
        End Get
        Set(ByVal Value As Int32)
            ViewState("ProductID") = Value
        End Set
    End Property
    Public Property PreviousScreen() As String
        Get
            Return ViewState("PreviousScreen")
        End Get
        Set(ByVal Value As String)
            ViewState("PreviousScreen") = Value
        End Set
    End Property

    '*********************************************************************
    ' Function: Page_Load
    '*********************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack
            Me.ProductID = HelperFunctions.GetIntQSValue(Request.Url.Query, "ID")

            Load_ProductCategories()

            If Me.ProductID > 0
                BindData()
            End If

            If Not Request.UrlReferrer Is Nothing
                Me.PreviousScreen = Request.UrlReferrer.ToString()
            End If
        End If
    End Sub

    '*********************************************************************
    ' Function: Load_ProductCategories
    '*********************************************************************
    Private Sub Load_ProductCategories()
        cboCategory.DataSource = ProductCategory.GetProductCategories()
        cboCategory.DataTextField = "Description"
        cboCategory.DataValueField = "ID"
        cboCategory.DataBind()
        cboCategory.Items.Insert(0, New ListItem("--- Select ---", "0"))
    End Sub

    '*********************************************************************
    ' Function: BindData
    '*********************************************************************
    Private Sub BindData()
        Dim oProduct As New Product(Me.ProductID)
        oProduct.Load()

        txtName.Text = oProduct.Name
        HelperFunctions.SelectDropDownValue(cboCategory, oProduct.CategoryID)
        txtPrice.Text = oProduct.Price.ToString("0.00")
    End Sub

    '*********************************************************************
    ' Function: ValidateInput
    '*********************************************************************
    Private Function ValidateInput() As Boolean
        If txtName.Text.Length = 0
            HelperFunctions.ShowAlert(Page, "Please enter a name.")
            Return False
        End If

        If Convert.ToInt32(cboCategory.SelectedValue) = 0
            HelperFunctions.ShowAlert(Page, "Please select a category.")
            Return False
        End If

        If txtPrice.Text.Length = 0 Or Not IsNumeric(txtPrice.Text)
            HelperFunctions.ShowAlert(Page, "Please enter a valid price.")
            Return False
        End If

        Return True
    End Function

    '*********************************************************************
    ' Function: SaveRecord
    '*********************************************************************
    Private Function SaveRecord() As Boolean
        Dim oProduct As New Product(Me.ProductID)

        If oProduct.ID > 0
            oProduct.Load()
        End If

        oProduct.Name = txtName.Text.Trim()
        oProduct.CategoryID = Convert.ToInt32(cboCategory.SelectedValue)
        oProduct.Price = Convert.ToDouble(txtPrice.Text)
        'Dim tempVal As Boolean = Convert.ToBoolean(ckSold.Text)
        oProduct.Sold = Convert.ToInt16(ckSold.Checked)
        oProduct.Color = txtColor.Text.Trim()
        Dim bRetVal As Boolean = oProduct.Save()

        Me.ProductID = oProduct.ID

        Return bRetVal
    End Function

    '*********************************************************************
    ' Function: btnSave_Click
    '*********************************************************************
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not ValidateInput() Then Exit Sub

        If SaveRecord()
            ReturnToPreviousScreen()
        Else
            HelperFunctions.ShowAlert(Page, "An error occured while saving the product.")
        End If
    End Sub

    '*********************************************************************
    ' Function: btnCancel_Click
    '*********************************************************************
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ReturnToPreviousScreen()
    End Sub

    '*********************************************************************
    ' Function: ReturnToPreviousScreen
    '*********************************************************************
    Private Sub ReturnToPreviousScreen()
        Dim sUrl As String = String.Empty

        If Me.PreviousScreen Is Nothing
            Me.PreviousScreen = String.Empty
        End If

        If Me.PreviousScreen.Length > 0
            sUrl = Me.PreviousScreen
        Else
            sUrl = "~"
        End If

        Response.Redirect(sUrl)
    End Sub

End Class
