Public Class ScannableItem
    Public Property DT_CODE As String = ""
    Public Property DTT_CODE As String = ""
    Public Property DTTA_ID As Integer = 0
    Public Property DTTAD_ID As Integer = 0
    Public Property SORT_ORDER As Integer = 0
    Public Property DTT_DESC As String = ""
    Public Property IMAGE_LABEL_NAME As String = ""
    Public Property IMAGE_SIDE As String = ""
    Public Property PAGE_NO As Integer = 0
    Public Property DTTADIF_DETAILS As New List(Of ImageFormatDetails)
End Class

Public Class ImageFormatDetails
    Public Property DTTADIF_ID As Integer = 0
    Public Property DTTAD_ID As Integer = 0
    Public Property IMAGE_COLOR As String = ""
    Public Property IMAGE_FORMAT As String = ""
    Public Property RESOULTION As String = ""
    Public Property STORAGE_PATH As String = ""
    Public Property TRANSFER_MODE As String = ""
    Public Property HOST_NAME As String = ""
    Public Property PORT_NO As String = ""
End Class








Public Class DtParam
    Public Property DT_CODE As String = ""
    Public Property DTP_CATEGORY As String = ""
    Public Property DTP_PARAM_NAME As String = ""
    Public Property DTP_PARAM_VALUE As String = ""
    Public Property WFTPA_ID As Integer = 0
End Class


Public Class DT_DATATEMPLATE_INFOs
    Inherits List(Of DT_DATATEMPLATE)
End Class

Public Class DT_DATATEMPLATE
    Public Property RELATION_JSON As New RELATION_JSON
End Class

Public Class RELATION_JSON
    Inherits List(Of DTT_DATATEMPLATE)
End Class

Public Class DTT_DATATEMPLATE

    Public Property TARGET_TABLE As String
    Public Property PRIMARY_COLUMN As String
    Public Property DTT_CODE As String
    Public Property FOREIGN_COLUMN As String
    'Public Property dt_code As String = ""
    'Public Property DTT_DESCRIPTION As String = ""
    'Public Property PRIMARY_TABLE As String = ""
    'Public Property DTT_CATAGORY As String = "S"
    'Public Property sort_order As String = ""
    Public Property CHILD_DTT_RELEATIONS As List(Of CHILD_DTT_RELEATIONS)
End Class

Public Class CHILD_DTT_RELEATIONS
    Public Property TARGET_TABLE As String
    Public Property PRIMARY_COLUMN As String
    Public Property DTT_CODE As String
    Public Property FOREIGN_COLUMN As String
    'Public Property DTT_CODE As String = ""
    'Public Property DTT_DESCRIPTION As String = ""
    'Public Property primary_table As String = ""
    'Public Property primary_column As String = ""
    'Public Property foreign_table_name As String = ""
    'Public Property foreign_column As String = ""
    'Public Property sort_order As String = ""
    'Public Property parent_dtt_code As String = ""
    'Public Property FOLDER_NAME As String = ""
    'Public FOLDER_STRUCTURE As String = ""
    'Public FOLDER_NAME_PATTERN As String = ""
    'Public FOLDER_NAME_TYPE As String = ""

End Class
Public Class ItemSet
    Public Property DT_Code As String = ""
    Public Property DTT_Code As String = ""
    Public Property TableName As String = ""
    Public Property Items As Items = Nothing
End Class


Public Class ItemSets
    Inherits List(Of ItemSet)
End Class

Public Class Item
    Inherits Hashtable
    Public Property KeyColumn As String
End Class
Public Class Items
    Inherits List(Of Item)
End Class



Public Class DT_ATTACH_INFO
    Inherits List(Of DT_ATTACHED)
End Class

Public Class DT_ATTACHED
    Public Property ATTACHED_JSON As New ATTACHED_JSON
End Class

Public Class ATTACHED_JSON
    'Inherits List(Of DTT_ATTACHMENT)
    Inherits List(Of DT_INFO)
End Class

Public Class DT_INFO

    Public Property DTTA_COUNT As String = ""
    Public Property DF_COUNT As String = ""
    Public Property dtt_attachment As List(Of DTT_ATTACHMENT)
    Public Property data_formats As List(Of DATA_FORMATS)
End Class
Public Class DTT_ATTACHMENT

    Public Property CODE As String = ""
    Public Property DESC As String = ""
    Public Property DTTA_ID As String = ""
    Public Property CLICK As String = ""
    Public Property ATTACH_TITLE As String = ""
    Public Property ATTACH_SOURCE As String = ""
    Public Property ATTACH_TYPE As String = ""
    Public Property DTTAD_COUNT As String = ""
    Public Property dtta_details As List(Of DTTA_DETAILS)
End Class

Public Class DTTA_DETAILS
    Public Property CODE As String = ""
    Public Property DESC As String = ""
    Public Property CLICK As String = ""
    Public Property LABEL_NAME As String = ""
    Public Property TEMPLATE_IMAGE As String = ""
    Public Property IMG_SIDE As String = ""
    Public Property DTTAD_ID As String = ""
    Public Property PAGE_NO As String = ""
    Public Property DTTAD_IMG_FORMAT As List(Of DTTAD_IMG_FORMAT)
End Class

Public Class DTTAD_IMG_FORMAT
    Public Property COMPRESSION As String = ""
    Public Property IMG_COLOR As String = ""
    Public Property IMG_FORMAT As String = ""
    Public Property IS_DEFAULT As String = ""
    Public Property RESOLUTION As String = ""
    Public Property CLICK As String = ""

End Class
Public Class DATA_FORMATS
    Public Property CODE As String = ""
    Public Property DESC As String = ""
    Public Property CLICK As String = ""
    Public Property CSS As String = ""
    Public Property DFD_COUNT As String = ""
    Public Property DTTA_ID As String = ""
    Public Property DTTAD_ID As String = ""
    Public Property IMG_COORDINATES As String = ""
    Public Property DF_CODE As String = ""
    Public Property DF_DESCRIPTION As String = ""
    Public Property df_details As List(Of DF_DETAILS)
End Class
Public Class DF_DETAILS
    Public Property CLICK As String = ""
    Public Property LABEL_NAME As String = ""
    Public Property DFD_ID As String = ""
    Public Property TARGET_COLUMN As String = ""
    Public Property ALLOW_NULL As String = ""
    Public Property DATA_LENGTH As String = ""
    Public Property DATA_SCALE As String = ""
    Public Property DEFAULT_VALUE As String = ""
    Public Property CONTROL_TYPE As String = ""

    Public Property DATA_TYPE As String = ""
    'Public Property df_search As List(Of DF_SEARCH)
    'Public Property df_ui As List(Of DF_UI)
End Class
Public Class DF_UI
    Public Property FIELD_COORDINATES As String = ""
    Public Property DATA_ENTRY_CTRL As String = ""
    Public Property DATA_SOURCE As String = ""
    Public Property VALIDATION_BEHAVIOR As String = ""
    Public Property STYLE_BEHAVIOR As String = ""

End Class
Public Class DF_SEARCH
    Public Property SEARCH_CTRL As String = ""
    Public Property BINDING_NAME As String = ""
    Public Property DATA_SOURCE As String = ""
    Public Property VALIDATION_BEHAVIOR As String = ""
    Public Property STYLE_BEHAVIOR As String = ""

End Class







