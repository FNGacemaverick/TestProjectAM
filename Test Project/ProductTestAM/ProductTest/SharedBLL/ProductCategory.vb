#Region " ProductCategory Class "

Public Class ProductCategory

#Region " Fields "

    Private _ID As Int32
    Private _Description As String

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
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal Value As String)
            _Description = Value
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
    ' Function: GetProductCategories
    '*********************************************************************
    Public Shared Function GetProductCategories() As ProductCategoryCollection
        Dim oCollection As New ProductCategoryCollection
        Dim ds As DataSet = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), "PT_S_PT_DECODE_ProductCategories")

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim oItem As New ProductCategory
            
            Load(oItem, dr)

            oCollection.Add(oItem)
        Next

        Return oCollection
    End Function

    '*********************************************************************
    ' Function: Load
    '*********************************************************************
    Private Shared Function Load(ByRef oItem As ProductCategory, ByRef dr As DataRow) As Boolean
        oItem.ID = Convert.ToInt32(dr("ID"))
        oItem.Description = dr("Description").ToString()
        
        Return True
    End Function

#End Region

End Class

#End Region

#Region " ProductCategoryCollection Class "

Public Class ProductCategoryCollection
    Inherits ArrayList

    Public Shadows Default Property Item(index As Integer) As ProductCategory
        Get
            Return MyBase.Item(index)
        End Get
        Set(ByVal value As ProductCategory)
            MyBase.Item(index) = value
        End Set
    End Property

End Class

#End Region