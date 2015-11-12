Imports System.Web.Http
Imports Newtonsoft.Json
Imports Gss.Oasis.SL.Core.Web.Entities
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Collections.Specialized
Imports System.ServiceProcess

Public Class ScanController
    Inherits ApiController

    <HttpPost()> _
<AllowAnonymous()> _
    Public Function GetScanData() As String
        Dim dsScanInfo As New DataSet
        Dim dtDTTAD As New DataTable
        Dim dtDTTADIF As New DataTable
        Dim dtSRS As New DataTable
        Dim lstResult As New List(Of ScannableItem)
        '  Dim lstDtParams As New List(Of DtParams)
        Dim str As String = Request.Content.ReadAsStringAsync().Result
        Dim ClientParam As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(str)
        Dim objUIH As New ScanHelper
        dsScanInfo = objUIH.GetScanData(ClientParam)
        Dim sds As SDataset = DataConverter.DSToSDS(dsScanInfo)
        Return SerializeSet(sds)
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


    <HttpPost()> _
<AllowAnonymous()> _
    Public Function SaveScanDetails(<FromUri> pDTCode As String, pActionId As Integer, pUserID As Integer, pcomment As String, pCommentTemp As String, pSTPC_ID As Integer, pSchemaName As String, pUICGCC_ID As Integer, pKeyColumn As String) As String
        Dim Str As String = Request.Content.ReadAsStringAsync().Result
        ' Dim ClientParams As SDataset = HttpUtility.ParseQueryString(ClientParamStr)
        'Using sr As New System.IO.StreamReader(Str)
        Dim XmlSrting = Str
        Dim objUIH As New ScanHelper
        Return objUIH.SaveScanDetails(XmlSrting, pDTCode, pActionId, pUserID, pcomment, pCommentTemp, pSTPC_ID, pSchemaName, pUICGCC_ID, pKeyColumn)
    End Function
    <HttpPost()> _
<AllowAnonymous()> _
    Public Function SaveAttachment(<FromUri> pATCode As String) As String

        Dim XmlString = Request.Content.ReadAsStringAsync().Result
        Dim resultval As String() = XmlString.Split(Environment.NewLine)
        XmlString = resultval(3)
        Dim objRes As New ScanHelper
        Return objRes.SaveAttachment(XmlString, pATCode)
    End Function
    <HttpPost()> _
<AllowAnonymous()> _
    Public Function SaveAttachmentDetails(<FromUri> pTable_name As String, pKeyColumn As String, pUpdate As String, pUsedClmns As String, pSchemaName As String) As String

        Dim Str As String = Request.Content.ReadAsStringAsync().Result
        ' Dim ClientParams As SDataset = HttpUtility.ParseQueryString(ClientParamStr)
        'Using sr As New System.IO.StreamReader(Str)
        Dim XmlSrting = Str
        Dim objUIH As New ScanHelper
        Return objUIH.SaveAttachmentDetails(XmlSrting, pTable_name, pKeyColumn, pUpdate, pUsedClmns, pSchemaName)
    End Function

    <HttpGet()> _
<AllowAnonymous()> _
    Public Function CallPuttyService() As String
        Dim svc As New ServiceController
        svc.ServiceName = "TransactionCreatorService"
        svc.Start()
        Return "Service started.."
    End Function
End Class
