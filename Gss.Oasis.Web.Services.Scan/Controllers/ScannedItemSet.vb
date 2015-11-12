Public Class ScannedItemSet
    Public Property Items As New List(Of ScannedItem)
End Class


Public Class ScannedItem
    Public Property LOCAL_ID As Integer = 0
    Public Property IMAGE_ID As Integer = 0
    Public Property DTT_CODE As String = ""
    Public Property DTTA_ID As Integer = 0
    Public Property DTTAD_ID As Integer = 0
    Public Property DTTADIF_ID As Integer = 0
    Public Property ORIGINAL_FILE_NAME As String = ""
    Public Property RELATIVE_PATH As String = ""
    Public Property SOURCE_DETAILS As String = ""
    Public Property DTTAC_DESC As String = ""
End Class


Public Class ScannedData
    Public Property CREATED_BY As Integer = 0
    Public Property WFTPA_ID As Integer = 0
    Public Property SYSTEM_ID As Integer = 0
    Public Property STS_ID As Integer = 0
    Public Property DTTAK_ID As Integer = 0
    Public Property ItemSets As New List(Of ScannedItemSet)
End Class

Public Class ScanParams
    Public Property pData As String = ""
    Public Property pDt As String = ""
    Public Property pDtt As String = ""
    Public Property pSystemID As Integer = 0
    Public Property pACSCHEMA As String = ""
    Public Property pADSCHEMA As String = ""
End Class