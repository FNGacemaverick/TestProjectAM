#Region " Product Class "

Public Class Product

#Region " Fields "

    Private _ID As Int32
    Private _Name As String
    Private _CategoryID As Int32
    Private _Category As String
    Private _Price As Double
    Private _DateCreated As DateTime

#End Region

#Region " Public Properties "

    Public Property ID() As Int32
        Get
            Return _ID
        End Get
        Set(ByVal Value As Int32)
            _ID = Value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
    Public Property CategoryID() As Int32
        Get
            Return _CategoryID
        End Get
        Set(ByVal Value As Int32)
            _CategoryID = Value
        End Set
    End Property
    Public Property Category() As String
        Get
            Return _Category
        End Get
        Set(ByVal Value As String)
            _Category = Value
        End Set
    End Property
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal Value As Double)
            _Price = Value
        End Set
    End Property
    Public Property DateCreated() As DateTime
        Get
            Return _DateCreated
        End Get
        Set(ByVal Value As DateTime)
            _DateCreated = Value
        End Set
    End Property

#End Region

#Region " Constructors "

    '****************************************************************************
    ' Function: New
    '****************************************************************************
    Public Sub New()
    End Sub

    '****************************************************************************
    ' Function: New
    '****************************************************************************
    Public Sub New(ByVal iID As Int32)
        _ID = iID
    End Sub

#End Region

#Region " Methods "

    '*********************************************************************
    ' Function: GetProducts
    '*********************************************************************
    Public Shared Function GetProducts(ByVal sName As String, ByVal iCategoryID As Int32) As ProductCollection
        Dim oCollection As New ProductCollection
        Dim ds As DataSet = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), "PT_S_PT_DATA_Products", _
                                                        0, sName, iCategoryID)

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim oItem As New Product()
            
            Load(oItem, dr)

            oCollection.Add(oItem)
        Next

        Return oCollection
    End Function

    '*********************************************************************
    ' Function: GetProductsDT
    '*********************************************************************
    Public Shared Function GetProductsDT(ByVal sName As String, ByVal iCategoryID As Int32) As DataTable
        Dim ds As DataSet = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), "PT_S_PT_DATA_Products", _
                                                        0, sName, iCategoryID)

        Return ds.Tables(0)
    End Function

    '*********************************************************************
    ' Function: Load
    '*********************************************************************
    Private Shared Function Load(ByRef oItem As Product, ByRef dr As DataRow) As Boolean
        oItem.ID = Convert.ToInt32(dr("ID"))
        oItem.Name = dr("Name").ToString()
        oItem.CategoryID = Convert.ToInt32(dr("CategoryID"))
        oItem.Category = dr("Category").ToString()
        oItem.Price = Convert.ToDouble(dr("Price"))
        oItem.DateCreated = Convert.ToDateTime(dr("DateCreated"))
        
        Return True
    End Function

    '*********************************************************************
    ' Function: Load
    '*********************************************************************
    Public Function Load() As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), "PT_S_PT_DATA_Products", _
                                    _ID, String.Empty, 0)

        If ds.Tables(0).Rows.Count <> 1 Then
            Return False
        End If

        Load(Me, ds.Tables(0).Rows(0))

        Return True
    End Function

    '*********************************************************************
    ' Function: Remove
    '*********************************************************************
    Public Shared Sub Remove(ByVal iID As Integer)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), "PT_D_PT_DATA_Products", iID)
    End Sub

    '*********************************************************************
    ' Function: Save
    '*********************************************************************
    Public Function Save() As Boolean
        If _ID = 0 Then
            Return Insert()
        Else
            If _ID > 0 Then
                Return Update()
            Else
                _ID = 0
                Return False
            End If
        End If
    End Function

    '*********************************************************************
    ' Function: Insert
    '*********************************************************************
    Private Function Insert() As Boolean
        _ID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConfigurationManager.AppSettings("ConnectionString"), "PT_N_PT_DATA_Products", _
                                _Name, _CategoryID, _Price))

        Return _ID > 0
    End Function

    '*********************************************************************
    ' Function: Update
    '*********************************************************************
    Private Function Update() As Boolean
        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), "PT_U_PT_DATA_Products", _
                                _ID, _Name, _CategoryID, _Price)

        Return True
    End Function

#End Region

End Class

#End Region

#Region " ProductCollection Class "

Public Class ProductCollection
    Inherits ArrayList

    Public Shadows Default Property Item(index As Integer) As Product
        Get
            Return MyBase.Item(index)
        End Get
        Set(ByVal value As Product)
            MyBase.Item(index) = value
        End Set
    End Property

    Public Enum SortFields
        ID
        Name
        Category
        Price
        DateCreated
    End Enum

    '*********************************************************************
    ' Function: Sort
    '*********************************************************************
    Public Overloads Sub Sort(ByVal sSortField As String, ByVal bIsAscending As Boolean)
        Select Case sSortField
            Case "ID"
                Sort(SortFields.ID, bIsAscending)

            Case "Name"
                Sort(SortFields.Name, bIsAscending)

            Case "Category"
                Sort(SortFields.Category, bIsAscending)

            Case "Price"
                Sort(SortFields.Price, bIsAscending)

            Case "DateCreated"
                Sort(SortFields.DateCreated, bIsAscending)
        End Select
    End Sub

    '*********************************************************************
    ' Function: Sort
    '*********************************************************************
    Public Overloads Sub Sort(ByVal iSortField As SortFields, ByVal bIsAscending As Boolean)
        Select Case iSortField
            Case SortFields.ID
                MyBase.Sort(New IDComparer)

            Case SortFields.Name
                MyBase.Sort(New NameComparer)

            Case SortFields.Category
                MyBase.Sort(New CategoryComparer)

            Case SortFields.Price
                MyBase.Sort(New PriceComparer)

            Case SortFields.DateCreated
                MyBase.Sort(New DateCreatedComparer)
        End Select

        If Not bIsAscending Then
            MyBase.Reverse()
        End If
    End Sub

    Private NotInheritable Class IDComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Return CType(x, Product).ID.CompareTo(CType(y, Product).ID)
        End Function
    End Class

    Private NotInheritable Class NameComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Return CType(x, Product).Name.CompareTo(CType(y, Product).Name)
        End Function
    End Class

    Private NotInheritable Class CategoryComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Return CType(x, Product).Category.CompareTo(CType(y, Product).Category)
        End Function
    End Class

    Private NotInheritable Class PriceComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Return CType(x, Product).Price.CompareTo(CType(y, Product).Price)
        End Function
    End Class

    Private NotInheritable Class DateCreatedComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Return CType(x, Product).DateCreated.CompareTo(CType(y, Product).DateCreated)
        End Function
    End Class

End Class

#End Region