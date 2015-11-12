Imports Cassandra
Imports Gss.Oasis.Web.Services.Helper
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.ComponentModel
Imports System.Reflection
Imports System.Collections.Specialized
Imports System.IO
Imports System.Xml
Imports System.Runtime.Serialization
Imports Gss.Oasis.SL.Core.Web.Entities
Imports Gss.Hibernate
Imports System.Net.Mime.MediaTypeNames


Public Class ScanHelper

    Private objTrnDBSession As NHibernateSession

    Public Sub New()
        objTrnDBSession = DBInstanceHelper.OpenNHibernateSession(True)
    End Sub



#Region "Get Scan data"
    Dim CassandraIns As ISession = DBInstanceHelper.CassandraInstance("dep_cas")
    Dim dtimageformats As New DataTable
    Dim dttemplate As New DataTable
    Dim dtAPPUSER As New DataTable
    Dim UICGCC_ID As Integer = 0
    Dim WFTPAID As String = ""
    Dim VWFTPAID As Integer = 0
    Dim dtTMPPI As New List(Of Hashtable)
    Dim ResultTable As List(Of List(Of Hashtable)) = Nothing
    Dim STPCID As Integer = 0
    Dim Comment As String = ""
    Dim NeedComment As String = ""
    Dim TokenId As Integer = 0
    Dim UID As Integer = 0
    Dim STS_ID As Integer = 0
    Dim AppId As String = ""
    Dim AppRoles As String = ""
    Dim DTCode As String
    Dim RS_CODE As String = ""
    Dim dttak_short_code As String = ""
    Dim at_code As String
    Dim scs_code As String = ""
    Dim DTTCode As String
    Dim APP_CODE As String
    Dim APP_DESCRIPTION As String
    Dim APPU_ID As String
    Dim IS_DEFAULT As String



    Private Function IsParamNothing(Param As String)
        If Param Is Nothing OrElse String.IsNullOrEmpty(Param) OrElse Param.ToString() = "undefined" OrElse Param.ToString() = "null" Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Function Prepereparam(clientparam As Dictionary(Of String, Object)) As Dictionary(Of String, Object)
        If clientparam Is Nothing Then
            Dim PARAMS As New Dictionary(Of String, Object)
            PARAMS.Add("DT_CODE", "DT_MANDATE")
            PARAMS.Add("DTT_CODE", "DTT_MANDATE")
            PARAMS.Add("APP_ID", "1")
            PARAMS.Add("WFTPA_ID", "73472")
            PARAMS.Add("RS_CODE", "IMS1")
            PARAMS.Add("dttak_short_code", "1")
            PARAMS.Add("at_code", "IMG")
            Return PARAMS
        End If
        Return clientparam
    End Function



    Private Sub getparamInfo(clientparam As Dictionary(Of String, Object))

        If clientparam.ContainsKey("UICGCC_ID") Then
            'Prepare Client Side Params
            If Not IsParamNothing(clientparam("UICGCC_ID").ToString()) Then
                Dim s As String = clientparam("UICGCC_ID").ToString().Replace("C", "")
                If IsNumeric(s) Then
                    UICGCC_ID = CInt(s)
                End If
            End If
        End If
        If clientparam.ContainsKey("WFTPA_ID") Then
            If Not IsParamNothing(clientparam("WFTPA_ID").ToString()) Then
                WFTPAID = clientparam("WFTPA_ID").ToString()
            End If
        End If
        If clientparam.ContainsKey("DT_CODE") Then
            If Not IsParamNothing(clientparam("DT_CODE").ToString()) Then
                DTCode = clientparam("DT_CODE").ToString()
            End If
        End If
        If clientparam.ContainsKey("DTT_CODE") Then
            If Not IsParamNothing(clientparam("DTT_CODE").ToString()) Then
                DTTCode = clientparam("DTT_CODE").ToString()
            End If
        End If

        If clientparam.ContainsKey("VWFTPA_ID") Then
            If Not IsParamNothing(clientparam("VWFTPA_ID").ToString()) Then
                VWFTPAID = clientparam("VWFTPA_ID")
            End If
        End If

        If clientparam.ContainsKey("STPC_ID") Then
            If Not IsParamNothing(clientparam("STPC_ID").ToString()) Then
                STPCID = clientparam("STPC_ID")
            End If
        End If

        If clientparam.ContainsKey("COMMENT") Then
            If Not IsParamNothing(clientparam("COMMENT").ToString()) Then
                Comment = clientparam("COMMENT").ToString()
            End If
        End If
        If clientparam.ContainsKey("NEED_COMMENT") Then
            If Not IsParamNothing(clientparam("NEED_COMMENT").ToString()) Then
                NeedComment = clientparam("NEED_COMMENT").ToString()
            End If
        End If

        If clientparam.ContainsKey("TOKEN_ID") Then
            If Not IsParamNothing(clientparam("TOKEN_ID").ToString()) Then
                TokenId = clientparam("TOKEN_ID").ToString()
            End If
        End If

        If clientparam.ContainsKey("APP_ID") Then
            If Not IsParamNothing(clientparam("APP_ID").ToString()) Then
                AppId = clientparam("APP_ID").ToString()
            End If
        End If
        If clientparam.ContainsKey("APP_CODE") Then
            If Not IsParamNothing(clientparam("APP_CODE").ToString()) Then
                APP_CODE = clientparam("APP_CODE").ToString()
            End If
        End If
        If clientparam.ContainsKey("APP_DESCRIPTION") Then
            If Not IsParamNothing(clientparam("APP_DESCRIPTION").ToString()) Then
                APP_DESCRIPTION = clientparam("APP_DESCRIPTION").ToString()
            End If
        End If
        If clientparam.ContainsKey("APPU_ID") Then
            If Not IsParamNothing(clientparam("APPU_ID").ToString()) Then
                APPU_ID = clientparam("APPU_ID").ToString()
            End If
        End If

        If clientparam.ContainsKey("IS_DEFAULT") Then
            If Not IsParamNothing(clientparam("IS_DEFAULT").ToString()) Then
                IS_DEFAULT = clientparam("IS_DEFAULT").ToString()
            End If
        End If
        If clientparam.ContainsKey("U_ID") Then
            If Not IsParamNothing(clientparam("U_ID").ToString()) Then
                UID = clientparam("U_ID")
            End If
        End If
        If clientparam.ContainsKey("STS_ID") Then
            If Not IsParamNothing(clientparam("STS_ID").ToString()) Then
                STS_ID = clientparam("STS_ID")
            End If
        End If

        If clientparam.ContainsKey("APPR_ID") Then
            If Not IsParamNothing(clientparam("APPR_ID").ToString()) Then
                AppRoles = clientparam("APPR_ID").ToString()
            End If
        End If
        If clientparam.ContainsKey("at_code") Then
            If Not IsParamNothing(clientparam("at_code").ToString()) Then
                at_code = clientparam("at_code").ToString()
            End If

        End If
        If clientparam.ContainsKey("scs_code") Then
            If Not IsParamNothing(clientparam("scs_code").ToString()) Then
                scs_code = clientparam("scs_code").ToString()
            End If
        End If

        If clientparam.ContainsKey("RS_CODE") Then
            If Not IsParamNothing(clientparam("RS_CODE").ToString()) Then
                RS_CODE = clientparam("RS_CODE").ToString()
            End If
        End If
        If clientparam.ContainsKey("dttak_short_code") Then
            If Not IsParamNothing(clientparam("dttak_short_code").ToString()) Then
                dttak_short_code = clientparam("dttak_short_code").ToString()
            End If
        End If


    End Sub

    Public Function GetScanData(param As Dictionary(Of String, Object))

        'Note: Need to comment after test
        'param = Prepereparam(param)
        'Note: Need to comment after test

        getparamInfo(param)
        Dim objCG As New CGInfo

        '  DerializeDataTable()
        ' Get dt_params values for Need_image_template and Need_sep_logic 
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select relation_json from " & CassandraIns.Keyspace & ". dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        Dim dtrelatioParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = AppId, Key .DT_CODE = DTCode}))
        'Dim DTINFO As New DT_INFOs
        'Dim DRow As New DT_INFO
        'Dim objDT_infos As DT_INFOs = ParseDT_INFO(dtParams)
        Dim ds As New DataSet
        Dim dtappinfo As DataTable = getApplicationsetup(CassandraIns, AppId) ' table 1
        dtappinfo.TableName = "APPLICATION_SETUP"
        Dim dtparams As DataTable = getDtParams(CassandraIns, AppId, DTCode, WFTPAID) ' table2
        dtparams.TableName = "DT_PARAMS"
        Dim dtApphndlr As DataTable = getAppHandler(CassandraIns) 'table3
        dtApphndlr.TableName = "APP_HANDLERS"
        Dim dtdatatemplatedttypes As DataTable = getdatatemplatedttypes(CassandraIns, AppId, DTCode) 'table4
        dtdatatemplatedttypes.TableName = "DATA_TEMPLATE_DT_TYPES"
        Dim dtdttypes As DataTable = New DataTable 'table5 unused
        dtdttypes.TableName = "DT_TYPES"
        Dim dtAttachment As DataTable = getdtAttachment(CassandraIns, AppId, DTTCode) 'table6
        dtAttachment.TableName = "DTT_ATTACHMENTS"
        Dim dtImageFormats1 As DataTable = getdtImageFormats(CassandraIns, AppId, DTTCode) 'table7 unused
        dtImageFormats1.TableName = "DTTAD_IMAGE_FORMATS"
        Dim dtdttadetails As DataTable = getdttadetails(CassandraIns, AppId, DTTCode) 'table8
        dtdttadetails.TableName = "DTTA_DETAILS"
        Dim dtEntityKeycolumns As DataTable = getENTITY_KEY_COLUMNS(CassandraIns, AppId, DTCode) 'table9
        dtEntityKeycolumns.TableName = "ENTITY_KEY_COLUMNS"
        Dim dtFormatDetails As DataTable = getdtFormatDetails(CassandraIns, AppId, DTCode) 'table10 unused
        dtFormatDetails.TableName = "DATA_FORMAT_DETAILS"
        Dim dtdtypeFormatDetails As DataTable = getdtypeFormatDetails(CassandraIns, AppId, DTCode) 'table11 unused
        dtdtypeFormatDetails.TableName = "DT_TYPE_DATA_FORMATS"
        Dim dtwftpParams As DataTable = getwftParams(CassandraIns, AppId, WFTPAID) 'table12
        dtwftpParams.TableName = "WFTPA_PARAMS"
        Dim dtAppuser As DataTable = getappuser(AppId, APP_CODE, APP_DESCRIPTION, APPU_ID, UID) 'Clientside java script'table13
        dtAppuser.TableName = "APP_USERS"
        Dim dtApplication As DataTable = getapplication() 'Clientside java script 'table14
        dtApplication.TableName = "APPLICATIONS"
        Dim dtEntityRelation As DataTable = getEntityRelation(CassandraIns, AppId, DTCode) 'table15
        dtEntityRelation.TableName = "ENTITY_RELATIONS"
        Dim dtresourceserver As DataTable = getResourceServer(CassandraIns, RS_CODE) 'table16
        dtresourceserver.TableName = "RESOURCE_SERVER"
        Dim dtdatatemplatedttypeswithdtypes As DataTable = getdatatemplatedttypeswithdtypes(CassandraIns, "", "") 'table17
        dtdatatemplatedttypeswithdtypes.TableName = "CHILD_DTT"
        Dim dtAcceskeys As DataTable = getAccesskey(CassandraIns, dttak_short_code) 'table18
        dtAcceskeys.TableName = "ACCESS_KEYS"
        Dim dtDataTemplates As DataTable = getDataTemplates(CassandraIns, "", "") 'table19
        dtDataTemplates.TableName = "DATA_TEMPLATES_FOLDERS"
        Dim dtDataTemplatesdtypesjoin As DataTable = getDataTemplatesdtypesjoin(CassandraIns, "", DTTCode) 'table20
        dtDataTemplatesdtypesjoin.TableName = "GET_SCAN_INFO"
        Dim dtAttachment_types As DataTable = getAttachmentTypes(CassandraIns, at_code) 'table21
        dtAttachment_types.TableName = "ATTACHMENT_TYPES"
        Dim dtDATA_TEMPLATE_DT_TYPES As DataTable = getDATA_TEMPLATE_DT_TYPES(CassandraIns, "", "") 'table22
        dtDATA_TEMPLATE_DT_TYPES.TableName = "GET_DTT_TARGET_TABLES"
        Dim dtResourceServerwithjoins As DataTable = getResourceServerwithjoins(CassandraIns, RS_CODE) 'table23
        dtResourceServerwithjoins.TableName = "SYSTEM_RESOURCE_SERVERS"
        Dim dtScanSettings As DataTable = getscanSettings(CassandraIns) 'table24
        dtScanSettings.TableName = "SCAN_SETTINGS"
        ds.Tables.Add(dtappinfo)
        ds.Tables.Add(dtparams)
        ds.Tables.Add(dtApphndlr)
        ds.Tables.Add(dtdatatemplatedttypes)
        ds.Tables.Add(dtdttypes)
        ds.Tables.Add(dtAttachment)
        ds.Tables.Add(dtImageFormats1)
        ds.Tables.Add(dtdttadetails)
        ds.Tables.Add(dtEntityKeycolumns)
        ds.Tables.Add(dtFormatDetails)
        ds.Tables.Add(dtdtypeFormatDetails)
        ds.Tables.Add(dtwftpParams)
        ds.Tables.Add(dtAppuser)
        ds.Tables.Add(dtApplication)
        ds.Tables.Add(dtEntityRelation)
        ds.Tables.Add(dtresourceserver)
        ds.Tables.Add(dtdatatemplatedttypeswithdtypes)
        ds.Tables.Add(dtAcceskeys)
        ds.Tables.Add(dtDataTemplates)
        ds.Tables.Add(dtDataTemplatesdtypesjoin)
        ds.Tables.Add(dtAttachment_types)
        ds.Tables.Add(dtDATA_TEMPLATE_DT_TYPES)
        ds.Tables.Add(dtResourceServerwithjoins)
        ds.Tables.Add(dtScanSettings)

        Dim dsScannerdata As New DataSet

        Dim str As String = dtScanSettings.Select("scs_code='RANGER_SCAN'")(0)("scan_options").ToString
        dsScannerdata.ReadXml(XmlReader.Create(New StringReader(str)))
        ' 2 tables added. 25. scannerinfo 26.getscanner
        For Each tbl As DataTable In dsScannerdata.Tables
            ds.Tables.Add(tbl.Copy())
        Next

        Return ds
    End Function

    Public Function DerializeDataTable() As DataTable
        Const json As String = "[{""Name"":""AAA"",""Age"":""22"",""Job"":""PPP""}]"
        Dim table = JsonConvert.DeserializeObject(Of DataTable)(json)
        Return table
    End Function
    'table 1
    Private Function getApplicationsetup(ByVal CassandraIns As ISession, ByVal pAppId As String) As DataTable
        'Dim qrystr As String = String.Format("Select as_name,as_value,app_id from dep_cas.application_setup where as_name in {0}  allow filtering;", "('GDPIC_LIC_KEY' ,'SCAN_TEMP_PATH')")
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare(qrystr)
        'Dim dtasrows As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId}))
        Dim dt As New DataTable
        dt.Columns.Add("AS_NAME")
        dt.Columns.Add("AS_VALUE")
        dt.Columns.Add("APP_ID")
        'For Each as_row As Row In dtasrows
        '    Dim dr As DataRow = dt.NewRow
        '    dr("AS_NAME") = as_row("AS_NAME").ToString
        '    dr("AS_VALUE") = as_row("AS_VALUE").ToString
        '    dr("APP_ID") = as_row("APP_ID").ToString
        '    dt.Rows.Add(dr)
        'Next
        Return dt
    End Function
    'table 2
    Private Function getDtParams(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String, pwftpa_id As String) As DataTable
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select param_json from " & CassandraIns.Keyspace & ".dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DT_CODE = pDTCode}))
        Dim strParams As String = dtParams(0)("param_json").ToString
        Dim table = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        table.Columns.Add("WFTPA_ID")
        table.Columns.Add("dt_cODE")
        table.Columns.Add("DTP_PARAM_NAME")
        table.Columns.Add("DTP_CATEGORY")
        table.Columns.Add("DTP_PARAM_VALUE")
        For Each row As DataRow In table.Rows
            row("WFTPA_ID") = pwftpa_id
            row("dt_cODE") = pDTCode
            row("DTP_PARAM_NAME") = row("PARAM_NAME")
            row("DTP_CATEGORY") = row("CATEGORY")
            row("DTP_PARAM_VALUE") = row("PARAM_VALUE")
        Next
        Return table
    End Function
    'table 3
    Private Function getAppHandler(ByVal CassandraIns As ISession) As DataTable
        Dim qrystr As String = String.Format("Select ah_code,assembly_full_name,ah_category from " & CassandraIns.Keyspace & ".app_handlers where ah_category in{0} allow filtering;", "('SCANNER','CATEGORIZATION','GROUPING')")
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare(qrystr)
        Dim dtapphandler As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .AH_CODE = ""}))
        Dim dt As New DataTable
        dt.Columns.Add("ah_code")
        dt.Columns.Add("assembly_full_name")
        dt.Columns.Add("ah_category")
        For Each apphndlr_row As Row In dtapphandler
            Dim dr As DataRow = dt.NewRow
            dr("ah_code") = apphndlr_row("ah_code").ToString
            dr("assembly_full_name") = apphndlr_row("assembly_full_name").ToString
            dr("ah_category") = apphndlr_row("ah_category").ToString
            dt.Rows.Add(dr)
        Next
        Return dt
    End Function
    'table 4
    Private Function getdatatemplatedttypes(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select relation_json from " & CassandraIns.Keyspace & ".dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DT_CODE = pDTCode}))
        dtDATATEMPLATE_ENTITYRELATIO(dtParams)
        Return dttemplate
    End Function
    'table 5
    Private Function getdttypes(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select relation_json from " & CassandraIns.Keyspace & ".dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DT_CODE = pDTCode}))
        Dim strParams As String = dtParams(0)("relation_json").ToString
        Dim table = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        Return table
    End Function
    'table 6
    Private Function getdtAttachment(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTTCode As String) As DataTable
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select dtt_dfd_json from " & CassandraIns.Keyspace & ".dtt_info where app_id = :APP_ID and dtt_code = :DTT_CODE allow filtering;")
        Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DTT_CODE = pDTTCode}))
        dtattach_imgfrmt_dtdetails(dtParams)
        Return dtimageformats
    End Function

    Private Sub dtattach_imgfrmt_dtdetails(dtParams As RowSet)

        Dim Attachmentvalues As DT_ATTACH_INFO = Parseattachinfo(dtParams)

        Dim DTINFO As New DT_ATTACH_INFO
        Dim attachjsonvalue As ATTACHED_JSON = Attachmentvalues(0).ATTACHED_JSON

        'dtimageformats.Columns.Add("DTTADIF_ID")
        If Not dtimageformats.Columns.Contains("DTTAD_ID") Then
            dtimageformats.Columns.Add("DTTAD_ID")
        End If
        If Not dtimageformats.Columns.Contains("IMAGE_COLOR") Then
            dtimageformats.Columns.Add("IMAGE_COLOR")
        End If
        If Not dtimageformats.Columns.Contains("IMAGE_FORMAT") Then
            dtimageformats.Columns.Add("IMAGE_FORMAT")
        End If
        If Not dtimageformats.Columns.Contains("RESOULTION") Then
            dtimageformats.Columns.Add("RESOULTION")
        End If
        If Not dtimageformats.Columns.Contains("COMPRESSION") Then
            dtimageformats.Columns.Add("COMPRESSION")
        End If
        If Not dtimageformats.Columns.Contains("IS_DEFAULT") Then
            dtimageformats.Columns.Add("IS_DEFAULT")
        End If
        If Not dtimageformats.Columns.Contains("DTTA_ID") Then
            dtimageformats.Columns.Add("DTTA_ID")
        End If

        If Not dtimageformats.Columns.Contains("DT_CODE") Then
            dtimageformats.Columns.Add("DT_CODE")
        End If
        If Not dtimageformats.Columns.Contains("DTT_CODE") Then
            dtimageformats.Columns.Add("DTT_CODE")
        End If
        If Not dtimageformats.Columns.Contains("ATTACHMENT_TITLE") Then
            dtimageformats.Columns.Add("ATTACHMENT_TITLE")
        End If

        If Not dtimageformats.Columns.Contains("ATTACHMENT_SOURCE") Then
            dtimageformats.Columns.Add("ATTACHMENT_SOURCE")
        End If

        If Not dtimageformats.Columns.Contains("AT_CODE") Then
            dtimageformats.Columns.Add("AT_CODE")
        End If
        If Not dtimageformats.Columns.Contains("SORT_ORDER") Then
            dtimageformats.Columns.Add("SORT_ORDER")
        End If
        If Not dtimageformats.Columns.Contains("DTTAD_ID") Then
            dtimageformats.Columns.Add("DTTAD_ID")
        End If
        If Not dtimageformats.Columns.Contains("IMAGE_SIDE") Then
            dtimageformats.Columns.Add("IMAGE_SIDE")
        End If
        If Not dtimageformats.Columns.Contains("IMAGE_LABEL_NAME") Then
            dtimageformats.Columns.Add("IMAGE_LABEL_NAME")
        End If
        If Not dtimageformats.Columns.Contains("PAGE_NO") Then
            dtimageformats.Columns.Add("PAGE_NO")
        End If

        For Each DTT_ATTACH_info As DT_INFO In attachjsonvalue
            For Each DTT_ATTACH_values As DTT_ATTACHMENT In DTT_ATTACH_info.dtt_attachment
                For Each dtt_attach_details As DTTA_DETAILS In DTT_ATTACH_values.dtta_details
                    Dim sortorder As Integer = 0
                    For Each dttad_image_formats As DTTAD_IMG_FORMAT In dtt_attach_details.DTTAD_IMG_FORMAT
                        Dim drimageformats As DataRow = dtimageformats.NewRow
                        'drimageformats("DTTADIF_ID") = dttad_image_formats.DTTADIF_ID
                        drimageformats("DTTAD_ID") = dtt_attach_details.DTTAD_ID
                        drimageformats("IMAGE_COLOR") = dttad_image_formats.IMG_COLOR
                        drimageformats("IMAGE_FORMAT") = dttad_image_formats.IMG_FORMAT
                        drimageformats("RESOULTION") = dttad_image_formats.RESOLUTION
                        drimageformats("COMPRESSION") = dttad_image_formats.COMPRESSION
                        drimageformats("IS_DEFAULT") = dttad_image_formats.IS_DEFAULT
                        If dttad_image_formats.IS_DEFAULT = True Then
                            drimageformats("IS_DEFAULT") = "Y"
                        End If


                        drimageformats("DTTA_ID") = DTT_ATTACH_values.DTTA_ID
                        drimageformats("DT_CODE") = DTCode
                        drimageformats("DTT_CODE") = DTTCode
                        drimageformats("ATTACHMENT_TITLE") = DTT_ATTACH_values.ATTACH_TITLE
                        drimageformats("ATTACHMENT_SOURCE") = "SCAN"
                        drimageformats("AT_CODE") = "IMG"
                        drimageformats("SORT_ORDER") = sortorder
                        drimageformats("DTTAD_ID") = dtt_attach_details.DTTAD_ID
                        drimageformats("IMAGE_SIDE") = dtt_attach_details.IMG_SIDE
                        drimageformats("IMAGE_LABEL_NAME") = dtt_attach_details.LABEL_NAME
                        drimageformats("PAGE_NO") = dtt_attach_details.PAGE_NO
                        dtimageformats.Rows.Add(drimageformats)
                        sortorder = sortorder + 1
                    Next
                Next
            Next
        Next
    End Sub

    Private Function SlashRemoval(ByVal inputval As String) As String
        Dim output As String = inputval

        If output.Contains("\""") Then
            output = output.Replace("\""", """")
        End If
        If output.Contains("null") Then
            output = output.Replace("null", "[]")
        End If

        Return output
    End Function

    Private Function Parseattachinfo(ByVal dtParams As RowSet) As DT_ATTACH_INFO

        Dim DTINFO As New DT_ATTACH_INFO
        Try
            If dtParams IsNot Nothing Then
                For Each dr As Row In dtParams
                    Dim DRow As New DT_ATTACHED
                    If dr.GetColumn("dtt_dfd_json") IsNot Nothing AndAlso Not String.IsNullOrEmpty(dr.Item("dtt_dfd_json").ToString()) Then
                        'Dim table = JsonConvert.DeserializeObject(Of DataTable)(dr.Item("param_json").ToString())
                        'Return table
                        Dim jsonval As String = dr.Item("dtt_dfd_json").ToString()
                        Dim slashremovedjson As String = SlashRemoval(jsonval)
                        Dim jobj As JObject = JsonConvert.DeserializeObject(Of JObject)(slashremovedjson)
                        '  For Each obj As JObject In jobj.OfType(Of JObject)()
                        DRow.ATTACHED_JSON.Add(JsonConvert.DeserializeObject(Of DT_INFO)(jobj.ToString()))
                        '   Next
                    End If
                    DTINFO.Add(DRow)
                Next
            End If
        Catch ex As Exception
            Trace.WriteLine(ex.Message.ToString())
        End Try
        Return DTINFO


    End Function


    Private Sub dtDATATEMPLATE_ENTITYRELATIO(dtParams As RowSet)
        Dim DTINFO As New DT_DATATEMPLATE_INFOs
        Dim DRow As New DT_DATATEMPLATE
        Dim Attachmentvalues As DT_DATATEMPLATE_INFOs = ParseDT_ATTACH_INFO(dtParams)
        'Dim dtadetails As New DataTable
        'Dim dtattach As New DataTable
        'Dim dtimageformats As New DataTable
        If Not dttemplate.Columns.Contains("DTT_CODE") Then
            dttemplate.Columns.Add("DTT_CODE")
        End If
        If Not dttemplate.Columns.Contains("DTTYPES_DTCODE") Then
            dttemplate.Columns.Add("DTTYPES_DTCODE")
        End If
        If Not dttemplate.Columns.Contains("DTT_DTTCODE") Then
            dttemplate.Columns.Add("DTT_DTTCODE")
        End If

        If Not dttemplate.Columns.Contains("DTTT_DT_CODE") Then
            dttemplate.Columns.Add("DTTT_DT_CODE")
        End If
        If Not dttemplate.Columns.Contains("ATTACHMENT_TABLE_NAME") Then
            dttemplate.Columns.Add("ATTACHMENT_TABLE_NAME")
        End If
        If Not dttemplate.Columns.Contains("TARGET_TABLE") Then
            dttemplate.Columns.Add("TARGET_TABLE")
        End If
        If Not dttemplate.Columns.Contains("DT_CODE") Then
            dttemplate.Columns.Add("DT_CODE")
        End If
        If Not dttemplate.Columns.Contains("SORT_ORDER") Then
            dttemplate.Columns.Add("SORT_ORDER")
        End If
        If Not dttemplate.Columns.Contains("DTT_DESCRIPTION") Then
            dttemplate.Columns.Add("DTT_DESCRIPTION")
        End If
        If Not dttemplate.Columns.Contains("PRIMARY_TABLE") Then
            dttemplate.Columns.Add("PRIMARY_TABLE")
        End If
        If Not dttemplate.Columns.Contains("PRIMARY_COLUMN") Then
            dttemplate.Columns.Add("PRIMARY_COLUMN")
        End If
        If Not dttemplate.Columns.Contains("DTT_CATAGORY") Then
            dttemplate.Columns.Add("DTT_CATAGORY")
        End If
        If Not dttemplate.Columns.Contains("FOREIGN_COLUMN") Then
            dttemplate.Columns.Add("FOREIGN_COLUMN")
        End If
        If Not dttemplate.Columns.Contains("PRIMARY_TABLE_NAME") Then
            dttemplate.Columns.Add("PRIMARY_TABLE_NAME")
        End If
        If Not dttemplate.Columns.Contains("PRIMARY_KEY_COLUMN") Then
            dttemplate.Columns.Add("PRIMARY_KEY_COLUMN")
        End If
        If Not dttemplate.Columns.Contains("FOREIGN_TABLE_NAME") Then
            dttemplate.Columns.Add("FOREIGN_TABLE_NAME")
        End If
        If Not dttemplate.Columns.Contains("FOREIGN_KEY_COLUMN") Then
            dttemplate.Columns.Add("FOREIGN_KEY_COLUMN")
        End If
        If Not dttemplate.Columns.Contains("PARENT_DTT_CODE") Then
            dttemplate.Columns.Add("PARENT_DTT_CODE")
        End If
        If Not dttemplate.Columns.Contains("FOLDER_STRUCTURE") Then
            dttemplate.Columns.Add("FOLDER_STRUCTURE")
        End If
        If Not dttemplate.Columns.Contains("FOLDER_NAME_PATTERN") Then
            dttemplate.Columns.Add("FOLDER_NAME_PATTERN")
        End If
        If Not dttemplate.Columns.Contains("FOLDER_NAME_TYPE") Then
            dttemplate.Columns.Add("FOLDER_NAME_TYPE")
        End If
        If Not dttemplate.Columns.Contains("FOLDER_NAME") Then
            dttemplate.Columns.Add("FOLDER_NAME")

        End If
        Dim sort_order As Integer = 0
        Dim attachjsonvalue As RELATION_JSON = Attachmentvalues(0).RELATION_JSON
        For Each DTT_DATATEMPLATE_values As DTT_DATATEMPLATE In attachjsonvalue

            sort_order = sort_order + 1
            If DTT_DATATEMPLATE_values.CHILD_DTT_RELEATIONS Is Nothing Then
                Dim drdttemplate As DataRow = dttemplate.NewRow
                drdttemplate("DTT_CODE") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("DT_CODE") = DTCode
                drdttemplate("SORT_ORDER") = sort_order
                drdttemplate("DTT_DESCRIPTION") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("PRIMARY_TABLE") = DTT_DATATEMPLATE_values.TARGET_TABLE
                drdttemplate("PRIMARY_COLUMN") = DTT_DATATEMPLATE_values.PRIMARY_COLUMN
                drdttemplate("DTT_CATAGORY") = "S"
                drdttemplate("FOREIGN_COLUMN") = DTT_DATATEMPLATE_values.FOREIGN_COLUMN
                drdttemplate("DTTYPES_DTCODE") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("DTT_DTTCODE") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("DTTT_DT_CODE") = DTCode
                drdttemplate("ATTACHMENT_TABLE_NAME") = "TRN_ATTACHMENTS"
                drdttemplate("TARGET_TABLE") = DTT_DATATEMPLATE_values.TARGET_TABLE
                drdttemplate("FOREIGN_TABLE_NAME") = DTT_DATATEMPLATE_values.TARGET_TABLE ' Note : sub table as targettable

                drdttemplate("parent_dtt_code") = "" ' ' Note : sub table as targettable
                'drdttemplate("DTT_CODE") = dt_entity_relation.DTT_CODE
                drdttemplate("FOLDER_STRUCTURE") = ""
                drdttemplate("FOLDER_NAME_PATTERN") = ""
                drdttemplate("FOLDER_NAME_TYPE") = ""
                drdttemplate("FOLDER_NAME") = ""
                drdttemplate("PRIMARY_TABLE_NAME") = ""
                drdttemplate("PRIMARY_KEY_COLUMN") = ""
                drdttemplate("FOREIGN_KEY_COLUMN") = ""
                dttemplate.Rows.Add(drdttemplate)
            Else
                Dim drdttemplate As DataRow = dttemplate.NewRow
                drdttemplate("DTT_CODE") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("DT_CODE") = DTCode
                drdttemplate("SORT_ORDER") = sort_order
                drdttemplate("DTT_DESCRIPTION") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("PRIMARY_TABLE") = DTT_DATATEMPLATE_values.TARGET_TABLE
                drdttemplate("PRIMARY_COLUMN") = DTT_DATATEMPLATE_values.PRIMARY_COLUMN
                drdttemplate("DTT_CATAGORY") = "S"
                drdttemplate("FOREIGN_COLUMN") = DTT_DATATEMPLATE_values.FOREIGN_COLUMN
                drdttemplate("DTTYPES_DTCODE") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("DTT_DTTCODE") = DTT_DATATEMPLATE_values.DTT_CODE
                drdttemplate("DTTT_DT_CODE") = DTCode
                drdttemplate("ATTACHMENT_TABLE_NAME") = "TRN_ATTACHMENTS"
                drdttemplate("TARGET_TABLE") = DTT_DATATEMPLATE_values.TARGET_TABLE
                drdttemplate("FOREIGN_TABLE_NAME") = DTT_DATATEMPLATE_values.TARGET_TABLE ' Note : sub table as targettable

                drdttemplate("PARENT_DTT_CODE") = "" ' ' Note : sub table as targettable
                'drdttemplate("DTT_CODE") = dt_entity_relation.DTT_CODE
                drdttemplate("FOLDER_STRUCTURE") = ""
                drdttemplate("FOLDER_NAME_PATTERN") = ""
                drdttemplate("FOLDER_NAME_TYPE") = ""
                drdttemplate("FOLDER_NAME") = ""
                drdttemplate("PRIMARY_TABLE_NAME") = ""
                drdttemplate("PRIMARY_KEY_COLUMN") = ""
                drdttemplate("FOREIGN_KEY_COLUMN") = ""
                dttemplate.Rows.Add(drdttemplate)

                For Each dt_entity_relation As CHILD_DTT_RELEATIONS In DTT_DATATEMPLATE_values.CHILD_DTT_RELEATIONS
                    Dim childdttemplate As DataRow = dttemplate.NewRow
                    childdttemplate("DTT_CODE") = dt_entity_relation.DTT_CODE
                    childdttemplate("DT_CODE") = DTCode
                    childdttemplate("SORT_ORDER") = sort_order
                    childdttemplate("DTT_DESCRIPTION") = dt_entity_relation.DTT_CODE
                    childdttemplate("PRIMARY_TABLE") = dt_entity_relation.TARGET_TABLE
                    childdttemplate("PRIMARY_COLUMN") = dt_entity_relation.PRIMARY_COLUMN
                    childdttemplate("DTT_CATAGORY") = "S"
                    childdttemplate("FOREIGN_COLUMN") = dt_entity_relation.FOREIGN_COLUMN
                    childdttemplate("DTTYPES_DTCODE") = dt_entity_relation.DTT_CODE
                    childdttemplate("DTT_DTTCODE") = dt_entity_relation.DTT_CODE
                    childdttemplate("DTTT_DT_CODE") = DTCode
                    childdttemplate("ATTACHMENT_TABLE_NAME") = "TRN_ATTACHMENTS"
                    childdttemplate("TARGET_TABLE") = dt_entity_relation.TARGET_TABLE
                    childdttemplate("FOREIGN_TABLE_NAME") = DTT_DATATEMPLATE_values.TARGET_TABLE ' Note : sub table as targettable

                    childdttemplate("PARENT_DTT_CODE") = DTT_DATATEMPLATE_values.DTT_CODE ' ' Note : sub table as targettable
                    'drdttemplate("DTT_CODE") = dt_entity_relation.DTT_CODE
                    childdttemplate("FOLDER_STRUCTURE") = ""
                    childdttemplate("FOLDER_NAME_PATTERN") = ""
                    childdttemplate("FOLDER_NAME_TYPE") = ""
                    childdttemplate("FOLDER_NAME") = ""
                    childdttemplate("PRIMARY_TABLE_NAME") = dt_entity_relation.TARGET_TABLE
                    childdttemplate("PRIMARY_KEY_COLUMN") = dt_entity_relation.PRIMARY_COLUMN
                    childdttemplate("FOREIGN_KEY_COLUMN") = ""
                    dttemplate.Rows.Add(childdttemplate)
                Next
            End If

        Next
    End Sub
    'table 7
    Private Function getdtImageFormats(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        Return dtimageformats.Copy
    End Function
    'table 8
    Private Function getdttadetails(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        Return dtimageformats.Copy
    End Function
    'table 9
    Private Function getENTITY_KEY_COLUMNS(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select relation_json from dep_cas.dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DT_CODE = pDTCode}))
        'Dim strParams As String = dtParams(0)("relation_json").ToString
        'Dim table = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        'Dim dv2 As New DataView(dtEntityRelation)
        Dim dtentity As DataTable = dttemplate.Copy
        Dim dt As New DataTable
        dt.Columns.Add("TABLE_NAME")
        dt.Columns.Add("KEY_COLUMN")
        For Each row As DataRow In dtentity.Rows
            Dim dr As DataRow = dt.NewRow
            dr("TABLE_NAME") = row("primary_table_name")
            dr("KEY_COLUMN") = row("primary_key_column")
            dt.Rows.Add(dr)
            Dim dr1 As DataRow = dt.NewRow
            dr1("TABLE_NAME") = row("PRIMARY_TABLE")
            dr1("KEY_COLUMN") = row("PRIMARY_COLUMN")
            dt.Rows.Add(dr1)
        Next
        Return dt
    End Function
    'table 10
    Private Function getdtFormatDetails(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select dtformat_json from dep_cas.dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DT_CODE = pDTCode}))
        'Dim strParams As String = dtParams(0)("dtformat_json").ToString
        'Dim table = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        Return New DataTable
    End Function
    'table 11
    Private Function getdtypeFormatDetails(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pDTCode As String) As DataTable
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select dtdf_json from dep_cas.dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .DT_CODE = pDTCode}))
        'Dim strParams As String = dtParams(0)("dtdf_json").ToString
        'Dim table = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        'apply condition 1=0
        Return New DataTable
    End Function
    'table 12
    Private Function getwftParams(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pWftpa_id As String) As DataTable
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select param_json from " & CassandraIns.Keyspace & ".wf_info where app_id = :APP_ID and Wftpa_id = :WFTPA_ID allow filtering;")
        Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = pAppId, Key .WFTPA_ID = pWftpa_id}))
        If dtParams.Count = 0 Then
            Return New DataTable
        End If
        Dim strParams As String = dtParams(0)("param_json").ToString
        Dim table = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        table.Columns.Add("WFTPA_ID")
        For Each row As DataRow In table.Rows
            row("WFTPA_ID") = pWftpa_id
        Next
        Return table
    End Function
    'table 13
    Private Function getappuser(AppId As String, APP_CODE As String, APP_DESCRIPTION As String, APPU_ID As String, UID As String) As DataTable
        dtAPPUSER.Columns.Add("App_Id")
        dtAPPUSER.Columns.Add("APP_CODE")
        dtAPPUSER.Columns.Add("APP_DESCRIPTION")
        dtAPPUSER.Columns.Add("APPU_ID")
        dtAPPUSER.Columns.Add("UID")
        dtAPPUSER.Rows.Add(AppId, APP_CODE, APP_DESCRIPTION, APPU_ID, UID)
        Return dtAPPUSER
    End Function
    'table 14
    Private Function getapplication() As DataTable
        Dim dt As DataTable = dtAPPUSER.Copy
        Return dt
    End Function
    'table 15
    Private Function getEntityRelation(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pdtt_code As String) As DataTable
        Dim dt As DataTable = dttemplate.Copy
        Return dt
    End Function
    'table 16
    Private Function getResourceServer(ByVal CassandraIns As ISession, ByVal prscode As String) As DataTable
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select rs_json from dep_cas.resouce_info where rs_code = :RS_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .RS_CODE = prscode}))
        'Dim strParams As String = dtParams(0)("rs_json").ToString
        Dim table As New DataTable ' = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        Return table
    End Function
    'table 17
    Private Function getdatatemplatedttypeswithdtypes(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pdtt_code As String) As DataTable
        Dim dt As DataTable = dttemplate.Copy
        Return dt

    End Function
    'table 18
    Private Function getAccesskey(ByVal CassandraIns As ISession, ByVal pdttak_short_code As String) As DataTable
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select ak_json from dep_cas.access_key_info where dttak_short_code = :DTTAK_SHORT_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .DTTAK_SHORT_CODE = pdttak_short_code}))
        'Dim strParams As String = dtParams(0)("ak_json").ToString
        Dim table As New DataTable '= JsonConvert.DeserializeObject(Of DataTable)(strParams)
        Return table
    End Function
    'table 19
    Private Function getDataTemplates(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pdtt_code As String) As DataTable
        Dim dt As DataTable = dttemplate.Copy
        Return dt

    End Function
    'table 20
    Private Function getDataTemplatesdtypesjoin(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pdtt_code As String) As DataTable
        Dim dt As DataTable = dtimageformats.Copy
        Return dt
    End Function
    'table 21
    Private Function getAttachmentTypes(ByVal CassandraIns As ISession, ByVal pat_code As String) As DataTable
        Dim qrystr As String = String.Format("Select at_id,at_code,at_extensions,at_description,watermark_code from " & CassandraIns.Keyspace & ". attachment_types where at_code = :AT_CODE allow filtering;")
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare(qrystr)
        Dim dtasrows As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .AT_CODE = "IMG"}))
        Dim dt As New DataTable
        dt.Columns.Add("at_id")
        dt.Columns.Add("AT_CODE")
        dt.Columns.Add("AT_EXTENSIONS")
        dt.Columns.Add("AT_DESCRIPTION")
        dt.Columns.Add("WATERMARK_CODE")
        For Each as_row As Row In dtasrows
            Dim dr As DataRow = dt.NewRow
            dr("at_id") = as_row("at_id").ToString
            dr("at_code") = as_row("at_code").ToString
            dr("at_extensions") = as_row("at_extensions").ToString
            dr("at_description") = as_row("at_description").ToString
            dr("watermark_code") = as_row("watermark_code").ToString
            dt.Rows.Add(dr)
        Next
        Return dt
    End Function
    'table 22
    Private Function getDATA_TEMPLATE_DT_TYPES(ByVal CassandraIns As ISession, ByVal pAppId As String, ByVal pdtt_code As String) As DataTable
        Dim dt As DataTable = dttemplate.Copy
        Return dt
    End Function
    'table 23
    Private Function getResourceServerwithjoins(ByVal CassandraIns As ISession, ByVal prscode As String) As DataTable
        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select rs_json from dep_cas.resouce_info where rs_code = :RS_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .RS_CODE = prscode}))
        'Dim strParams As String = dtParams(0)("rs_json").ToString
        Dim table As New DataTable ' = JsonConvert.DeserializeObject(Of DataTable)(strParams)
        Return table
    End Function
    'table 24
    Private Function getscanSettings(ByVal CassandraIns As ISession) As DataTable
        Dim qrystr As String = String.Format("Select scan_options,scs_code from " & CassandraIns.Keyspace & ".scan_settings where scs_code in('RANGER_SCAN','DEFAULT_SETTINGS') allow filtering;")
        Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare(qrystr)
        Dim dtasrows As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .SCS_CODE = ""}))
        Dim dt As New DataTable
        dt.Columns.Add("scan_options")
        dt.Columns.Add("scs_code")
        For Each as_row As Row In dtasrows
            Dim dr As DataRow = dt.NewRow
            dr("scs_code") = as_row("scs_code").ToString
            dr("scan_options") = as_row("scan_options").ToString
            dt.Rows.Add(dr)
        Next
        Return dt
    End Function


    Private Function ParseDT_ATTACH_INFO(ByVal pDTINFO As RowSet) As DT_DATATEMPLATE_INFOs
        Dim DTINFO As New DT_DATATEMPLATE_INFOs
        Try
            If pDTINFO IsNot Nothing Then
                For Each dr As Row In pDTINFO
                    Dim DRow As New DT_DATATEMPLATE
                    If dr.GetColumn("relation_json") IsNot Nothing AndAlso Not String.IsNullOrEmpty(dr.Item("relation_json").ToString()) Then
                        'Dim table = JsonConvert.DeserializeObject(Of DataTable)(dr.Item("param_json").ToString())
                        'Return table
                        Dim jobj As JArray = JsonConvert.DeserializeObject(Of JArray)(dr.Item("relation_json").ToString())
                        For Each obj As JObject In jobj.OfType(Of JObject)()
                            DRow.RELATION_JSON.Add(JsonConvert.DeserializeObject(Of DTT_DATATEMPLATE)(obj.ToString()))
                        Next
                    End If
                    DTINFO.Add(DRow)
                Next
            End If
        Catch ex As Exception
            Trace.WriteLine(ex.Message.ToString())
        End Try
        Return DTINFO
    End Function

#End Region



#Region "Save Details"

    Public Function __SaveTransaction(pItemSets As ItemSets, pDTR As DT_RELATIONS) As ItemSets


        Dim trntbl As ItemSet = pItemSets.Item(0)
        Dim trnTs As ItemSet = pItemSets.Item(2)
        Dim trnAttachmt As ItemSet = pItemSets.Item(1)


        'For Each trinsert In trnInsert.Items
        '    Dim objtraninserttable As New Entity(trnInsert.TableName)
        '    objtraninserttable.Session = objTrnDBSession
        '    objTargettable.Insert(trinsert)
        'Next




        'trn
        For Each trnt In trntbl.Items


            Dim objtraninserttable As New Entity(trntbl.TableName)
            objtraninserttable.Session = objTrnDBSession

            Dim val = trnt("__LOCAL_" & trnt.KeyColumn)
            trnt.Remove("__LOCAL_" & trnt.KeyColumn)
            trnt.Remove(trnt.KeyColumn)
            's.Save(trntbl.TableName, trnt)
            objtraninserttable.Insert(trnt)

            'ts
            Dim objtransettable As New Entity(trnTs.TableName)
            objtransettable.Session = objTrnDBSession
            Dim tsRows = From ts In trnTs.Items Where ts.Item("__LOCAL_" & trnt.KeyColumn) = val
            Dim tsitem = tsRows(0)
            tsitem("TRN_ID") = trnt.Item(trnt.KeyColumn)
            tsitem.Remove("__LOCAL_" & trnt.KeyColumn)
            tsitem.Remove("TS_ID")
            If Not tsitem.Contains("STATUS") Then
                tsitem.Add("STATUS", "CREATED")
            End If
            If Not tsitem.Contains("PROCESS_STATUS") Then
                tsitem.Add("PROCESS_STATUS", "CREATED")
            End If
            objtransettable.Insert(tsitem)

            'tranAttachment
            Dim objtranAtachtable As New Entity(trnAttachmt.TableName)
            objtranAtachtable.Session = objTrnDBSession
            Dim trnAttachmtRows = From ts1 In trnAttachmt.Items Where ts1.Item("__PARENT_TRN_ID") = val
            Dim trnAttItem = trnAttachmtRows(0)
            trnAttItem("TRN_ID") = trnt.Item(trnt.KeyColumn)
            trnAttItem.Remove("__PARENT_TABLE_NAME")
            trnAttItem.Remove("__PARENT_TRN_ID")
            trnAttItem.Remove("DTTA_DIF_ID")
            trnAttItem.Remove("DTTAK_ID")
            objtranAtachtable.Insert(trnAttItem)

        Next

        objTrnDBSession.Close(True)

    End Function

    Private Sub __SaveTable()

    End Sub


    Private Sub __SaveSet(pSet As ItemSet, pSession As NHibernate.ISession, objDTTR As DTT_RELATION, pItemSets As ItemSets)
        For Each itm As Item In pSet.Items
            If itm.KeyColumn = 0 Then
                pSession.Save(objDTTR.TARGET_TABLE, itm)
            Else
                Dim ItemfromDb = pSession.CreateQuery(String.Format("from {1} where {0} = :{0}", objDTTR.PRIMARY_COLUMN, objDTTR.TARGET_TABLE)).SetParameter(objDTTR.PRIMARY_COLUMN, itm.KeyColumn).List()
                For Each key As String In ItemfromDb
                    If itm.ContainsKey(key) Then
                        ItemfromDb(key) = itm(key)
                    End If
                Next
                pSession.Update(objDTTR.TARGET_TABLE, ItemfromDb)
            End If
            If pSet.Items.Count = 1 AndAlso pSet.Items(0).KeyColumn = 0 Then
                AssignInsertedValues(pItemSets, objDTTR, pSet.Items(0))
            End If
            'Insert Childs
            For Each childDTTR As DTT_RELATION In objDTTR.CHILD_DTT_RELEATIONS
                Dim ist As ItemSet = (From its In pItemSets Where its.DTT_Code = childDTTR.DTT_CODE Select its).FirstOrDefault
                If ist Is Nothing Then
                    Continue For
                End If
                __SaveSet(ist, pSession, childDTTR, pItemSets)
            Next
        Next
    End Sub

    Private Sub AssignInsertedValues(pSets As ItemSets, pDTTR As DTT_RELATION, pItem As Item)
        For Each DTR As DTT_RELATION In pDTTR.CHILD_DTT_RELEATIONS
            Dim Itmset As ItemSet = (From iset In pSets Where iset.DTT_Code = DTR.DTT_CODE Select iset).FirstOrDefault()
            If Itmset Is Nothing Then
                Continue For
            End If
            For Each itm As Item In Itmset.Items
                itm(DTR.FOREIGN_COLUMN) = pItem(pDTTR.PRIMARY_COLUMN)
            Next
        Next
    End Sub


    Public Function SaveScanDetails(pXmldata As String, pDTCode As String, pActionId As Integer, pUserID As Integer, pcomment As String, pCommentTemp As String, pSTPC_ID As Integer, pSchemaName As String, pUICGCC_ID As Integer, pKeyColumn As String) As String
        Dim pDataSource As System.Data.DataSet = DeSerializeSet(pXmldata)
        Dim trtable = pDataSource.Tables(0)
        Dim trAttachmnettable = pDataSource.Tables(1)
        Dim tstable = pDataSource.Tables(0).Copy
        tstable.Columns.Remove(pKeyColumn)
        'tstable.Columns.Remove("__LOCAL_" & pKeyColumn)
        Dim tsclmns = From clmn As DataColumn In pDataSource.Tables(0).Copy.Columns Where clmn.ColumnName <> pKeyColumn AndAlso clmn.ColumnName <> "__LOCAL_" & pKeyColumn AndAlso clmn.ColumnName <> "CREATED_BY" AndAlso clmn.ColumnName <> "VERSION_NO" Select clmn.ColumnName
        For Each clm As String In tsclmns
            trtable.Columns.Remove(clm)
        Next

        'Dim tAttachmtclmns = From clmn As DataColumn In pDataSource.Tables(1).Copy.Columns Where clmn.ColumnName = "__Parent_TRN_ID" And clmn.ColumnName = "__parent_table_name" Select clmn.ColumnName 'And clmn.ColumnName = "dtta_DIF_ID" And clmn.ColumnName = "dttak_id" Select clmn.ColumnName
        'For Each clm As String In tAttachmtclmns
        '    trAttachmnettable.Columns.Remove(clm)
        'Next


        Dim dssave As New DataSet
        dssave.Tables.Add(trtable.Copy)
        dssave.Tables.Add(trAttachmnettable.Copy)
        tstable.TableName = "TRANSACTION_SET"
        dssave.Tables.Add(tstable)
        __savedata(dssave, pKeyColumn)
        Return "SUCCESS"
    End Function
    Public Function SaveAttachment(pXmldata As String, pATCode As String)
        Dim ds As DataSet = DeSerializeSet(pXmldata)
        Dim row As DataRow = ds.Tables(0).Rows(0)
        Dim dtAttachmentDetail As New DataTable
        Dim strAppId As String = ""
        If row IsNot Nothing AndAlso Not String.IsNullOrEmpty(row("APP_ID").ToString()) Then
            strAppId = row("APP_ID").ToString()
        End If
        Dim objSolr As New SolrHelper(DBInstanceHelper.CassandraInstance("dep_cas"), DBInstanceHelper.CassandraInstance("cnf_cas"), DBInstanceHelper.CassandraInstance("usr_cas"), strAppId)
        For Each dr As DataRow In ds.Tables(0).Rows
            __PrepareData(dtAttachmentDetail, IO.Path.GetFileName(dr("DestinationFilePath").ToString), dr("ImageByte"))
            If objSolr.bolNeedSolr Then
                Dim ByteData As Byte() = dr("IMAGEBYTE")
                Dim oContentStream As New MemoryStream(ByteData)
                Dim RelativePath As String = dr("DESTINATIONFILEPATH").ToString()
                Dim Filename As String = dr("DESTINATIONFILEPATH").ToString()
                Dim DTCODE As String = dr("DT_CODE").ToString()
                Dim DTTCODE As String = dr("DTT_CODE").ToString()
                Dim DTTAID As Integer = 0
                If Not String.IsNullOrEmpty(dr("DTTA_ID").ToString()) Then
                    DTTAID = dr("DTTA_ID")
                End If
                objSolr.ATMTIndex(DTCODE, DTTCODE, RelativePath, Filename, oContentStream, DTTAID)
            End If
        Next
        Dim RSDBInfo As String = row("RS_DB_INFO").ToString
        SaveAttachmentToDB(dtAttachmentDetail)
        Return "SUCCESS"
    End Function

    Public Sub SaveAttachmentToDB(pDtAttachmentData As DataTable)
        For Each dr In pDtAttachmentData.Rows
            Dim CassandraIns As ISession = DBInstanceHelper.CassandraInstance("res_cas")
            Dim rsinsert As PreparedStatement = CassandraIns.Prepare("insert into " & CassandraIns.Keyspace & ".trna_data(TRNAD_ID,RELATIVE_PATH,TEXT_DATA) VALUES(UUID(),?,?)")
            DBInstanceHelper.CassandraInstance("res_cas").Execute(rsinsert.Bind(dr("RELATIVE_PATH").ToString.ToLower.Trim, dr("TEXT_DATA")))
        Next
    End Sub
    Public Sub __PrepareData(ByRef dtAttachmentDetail As DataTable, pRelativePath As String, pByteData() As Byte)
        If dtAttachmentDetail.Columns.Count <= 0 Then
            dtAttachmentDetail.Columns.Add("RELATIVE_PATH", GetType(String))
            dtAttachmentDetail.Columns.Add("AT_CODE", GetType(String))
            dtAttachmentDetail.Columns.Add("TEXT_DATA", GetType(String))
            dtAttachmentDetail.Columns.Add("BYTE_DATA", GetType(Byte()))
        End If
        Dim dr As DataRow = dtAttachmentDetail.NewRow
        Dim AT_CODE As String = "IMG"
        dr("RELATIVE_PATH") = pRelativePath.ToLower
        dr("AT_CODE") = AT_CODE

        If AT_CODE = "IMG" Then
            dr("TEXT_DATA") = Convert.ToBase64String(pByteData)
        Else
            dr("BYTE_DATA") = pByteData
        End If
        dtAttachmentDetail.Rows.Add(dr)
    End Sub

    Private Sub __savedata(ByVal ds As DataSet, pKeyColumn As String)
        Dim LSTITEMSETS As New ItemSets


        For Each dt As DataTable In ds.Tables
            Dim isetsvalues As New ItemSet
            isetsvalues.TableName = dt.TableName
            Dim LSTITEMS As New Items
            For Each drow As DataRow In dt.Rows
                Dim itemval As New Item
                itemval.KeyColumn = pKeyColumn
                For Each dcols As DataColumn In dt.Columns
                    If Not itemval.Contains(dcols.ColumnName) Then
                        itemval.Add(dcols.ColumnName, drow(dcols.ColumnName))
                    End If
                Next
                LSTITEMS.Add(itemval)
            Next
            isetsvalues.Items = LSTITEMS
            LSTITEMSETS.Add(isetsvalues)
        Next

        'Dim dtParamstmt As PreparedStatement = CassandraIns.Prepare("Select relation_json from dep_cas.dt_info where app_id = :APP_ID and dt_code = :DT_CODE allow filtering;")
        'Dim dtParams As RowSet = CassandraIns.Execute(dtParamstmt.Bind(New With {Key .APP_ID = 1, Key .DT_CODE = "DT_MANDATE"}))
        'Dim strParams As String = dtParams(0)("relation_json").ToString
        '  Dim DTTR As Gss.Oasis.Web.Services.Helper.DT_RELATIONS = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DT_RELATIONS)(strParams)
        __SaveTransaction(LSTITEMSETS, Nothing)
    End Sub
    Private Function DeSerializeSet(pJsonData As String) As DataSet
        Dim pStrdata As Byte() = Convert.FromBase64String(pJsonData)
        Using mstream As New MemoryStream(pStrdata)
            Dim serializer As DataContractSerializer = New DataContractSerializer(GetType(SDataset))
            Dim binaryDictionaryReader As XmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(mstream, XmlDictionaryReaderQuotas.Max)
            Dim dataSet As SDataset = serializer.ReadObject(binaryDictionaryReader, True)
            Return DataConverter.SDSToDS(dataSet)
        End Using
    End Function
    Private Function SerializeSet(pSet As SDataset) As String
        Dim jsonString As String = ""
        Dim serializer As DataContractSerializer = New DataContractSerializer(GetType(Gss.Oasis.SL.Core.Web.Entities.SDataset))
        Using mstream As New MemoryStream
            Dim binaryDictionaryWriter As XmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(mstream)
            serializer.WriteObject(binaryDictionaryWriter, pSet)
            binaryDictionaryWriter.Flush()
            Dim result As Byte() = DirectCast(mstream, MemoryStream).ToArray()
            jsonString = Convert.ToBase64String(result)
        End Using
        Return jsonString
    End Function
#End Region

    Public Function SaveAttachmentDetails(pXmldata As String, pTable_name As String, pKeyColumn As String, pUpdate As String, pUsedClmns As String, pSchemaName As String) As String
        Dim dr As New List(Of Hashtable)
        Dim ds As DataSet = DeSerializeSet(pXmldata)
        Dim dt As DataTable = ds.Tables(0)
        dr = convertDataTableToHashTable(dt)
        Dim oAtmEntity As New Entity("TRN_ATTACHMENTS")
        oAtmEntity.Session = objTrnDBSession
        oAtmEntity.Insert(dr)
        objTrnDBSession.Close(True)
        Return "SUCCESS"
    End Function
    Private Function convertDataTableToHashTable(dt As DataTable) As List(Of Hashtable)

        Dim htlist As New List(Of Hashtable)
        For Each dr As DataRow In dt.Rows
            Dim ht As New Hashtable
            For Each dc As DataColumn In dt.Columns
                ht.Add(dc.ColumnName, dr(dc.ColumnName).ToString())
            Next
            htlist.Add(ht)
        Next
        Return htlist
    End Function

End Class


