Partial Class ProductSearch
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Const cg_sCookieName As String = "ProductSearch"

    '*********************************************************************
    ' Property:  SortField
    '*********************************************************************
    Property SortField() As String
        Get
            Dim o As Object = ViewState("SortField")
            If o Is Nothing Then
                Return [String].Empty
            End If
            Return CStr(o)
        End Get

        Set(ByVal Value As String)
            If Value = SortField Then
                ' same as current sort file, toggle sort direction
                SortAscending = Not SortAscending
            End If
            ViewState("SortField") = Value
        End Set
    End Property

    '*********************************************************************
    ' Property:  SortAscending
    '*********************************************************************
    Property SortAscending() As Boolean
        Get
            Dim o As Object = ViewState("SortAscending")
            If o Is Nothing Then
                Return True
            End If
            Return CBool(o)
        End Get

        Set(ByVal Value As Boolean)
            ViewState("SortAscending") = Value
        End Set
    End Property

    '*********************************************************************
    ' Function: Page_Load
    '*********************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack
            Load_ProductCategories()

            LoadCookie()

            If ValidateInput(False)
                BindData()
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
    ' Function: LoadCookie
    '*********************************************************************
    Private Sub LoadCookie()
        Try
            Dim oCookie As HttpCookie = Context.Request.Cookies(cg_sCookieName)

            If Not oCookie Is Nothing
                txtName.Text = oCookie.Values("Name")
                HelperFunctions.SelectDropDownValue(cboCategory, oCookie.Values("CategoryID"))
            End If
        Catch ex As Exception
        End Try
    End Sub

    '***************************************************************************
    ' Function: SaveCookie
    '***************************************************************************
    Private Sub SaveCookie()
        Dim oCookie As HttpCookie = Context.Request.Cookies(cg_sCookieName)
        Dim bCookieExists As Boolean = (Not oCookie Is Nothing)

        If Not bCookieExists
            oCookie = New HttpCookie(cg_sCookieName)
        End If

        oCookie.Values("Name") = txtName.Text
        oCookie.Values("CategoryID") = Convert.ToInt32(cboCategory.SelectedValue)

        oCookie.Expires = Now.AddDays(1)

        If bCookieExists
            Context.Response.Cookies.Set(oCookie)
        Else
            Context.Response.Cookies.Add(oCookie)
        End If
    End Sub

    '***************************************************************************
    ' Function: ValidateInput
    '***************************************************************************
    Private Function ValidateInput(ByVal bShowError As Boolean) As Boolean
        If txtName.Text.Length = 0 And _
                cboCategory.SelectedIndex = 0

            If bShowError
                HelperFunctions.ShowAlert(Page, "Please enter your search criteria.")
            End If
            
            Return False
        End If

        Return True
    End Function

    '***************************************************************************
    ' Function: BindData
    '***************************************************************************
    Private Sub BindData()
        Dim oProducts As ProductCollection = Product.GetProducts(txtName.Text, Convert.ToInt32(cboCategory.SelectedValue))

        oProducts.Sort(Me.SortField, Me.SortAscending)

        dgProducts.DataSource = oProducts
        dgProducts.DataBind()
    End Sub

    '***************************************************************************
    ' Function: btnSearch_Click
    '***************************************************************************
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not ValidateInput(True) Then Exit Sub

        BindData()
        SaveCookie()
    End Sub

    '***************************************************************************
    ' Function: dgProducts_ItemCreated
    '***************************************************************************
    Private Sub dgProducts_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProducts.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem
            Dim cmdDelete As ImageButton = CType(e.Item.FindControl("cmdDelete"), ImageButton)
            HelperFunctions.ShowConfirmation(cmdDelete, "Are you sure you want to remove this product?")
        End If
    End Sub

    '***************************************************************************
    ' Function: dgProducts_ItemCommand
    '***************************************************************************
    Private Sub dgProducts_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProducts.ItemCommand
        If e.CommandName.ToLower() = "delete"
            Dim iProductID As Int32 = Convert.ToInt32(dgProducts.DataKeys(e.Item.ItemIndex))
            Product.Remove(iProductID)
            BindData()
        End If
    End Sub

    '***************************************************************************
    ' Function: dgProducts_SortCommand
    '***************************************************************************
    Private Sub dgProducts_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgProducts.SortCommand
        Me.SortField = e.SortExpression
        BindData()
    End Sub

    '***************************************************************************
    ' Function: btnCreatePDF_Click
    '***************************************************************************
    Private Sub btnCreatePDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreatePDF.Click
        If Not ValidateInput(True) Then Exit Sub

        Dim sUrl As String = String.Format("ProductReport.aspx?Name={0}&CategoryID={1}", HttpUtility.UrlEncode(txtName.Text), Convert.ToInt32(cboCategory.SelectedValue))
        HelperFunctions.DisplayWindow(Page, sUrl, 800, 600)
    End Sub

End Class

