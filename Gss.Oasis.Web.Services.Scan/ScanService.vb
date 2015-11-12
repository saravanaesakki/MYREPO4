Imports System.Web.Http.SelfHost
Imports System.Web.Http
Imports Gss.Oasis.Web.Services.Helper

Public Class ScanService

    Private _server As HttpSelfHostServer
    Private _config As HttpSelfHostConfiguration
    Private Port As String = "6029"
    Private serviceAddress As String = "http://localhost:"
    Private objConFig As Config

    Sub New()
        Try
            InitializeComponent()

            Dim diParent As IO.DirectoryInfo = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent
            Dim strfile As String = IO.Path.Combine(diParent.FullName, "Config.Json")
            If Not IO.File.Exists(strfile) Then
                Return
            End If
            Dim strJson As String = IO.File.ReadAllText(strfile)
            objConFig = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Config)(strJson)
            If Not IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Log") Then
                IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Log")
            End If

            Dim strTemp As String = objConFig.Services.Scan.Port
            If Not String.IsNullOrWhiteSpace(strTemp) Then
                Port = strTemp
            End If

        Catch ex As Exception
            IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory & "Log\Log-" & DateTime.Now.Ticks.ToString() & ".txt", "Error in Authentication Service on New() - " & ex.ToString())
        End Try
    End Sub


    Private Sub Init()
        serviceAddress = serviceAddress & Port
        _config = New HttpSelfHostConfiguration(serviceAddress)
        _config.Routes.MapHttpRoute( _
            name:="DefaultApi", _
            routeTemplate:="api/{controller}/{action}", _
            defaults:=New With {.Detail = RouteParameter.Optional})
        _server = New HttpSelfHostServer(_config)
        _config.MaxReceivedMessageSize = 2147483647
        _server.OpenAsync()
    End Sub


    Protected Overrides Sub OnStart(ByVal args() As String)

        Try
            Init()
            'InstanceHelper.CreateInstances(objConFig)
            DBInstanceHelper.CreateInstances(objConFig)
        Catch ex As Exception
            IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory & "Log\Log-" & DateTime.Now.Ticks.ToString() & ".txt", "Error in Scan Service OnStart function - " & ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub OnContinue()
        Try
            Init()
            InstanceHelper.CreateInstances(objConFig)
        Catch ex As Exception
            IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory & "Log\Log-" & DateTime.Now.Ticks.ToString() & ".txt", "Error in Authentication Service OnStart function - " & ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub OnPause()


    End Sub

    Protected Overrides Sub OnStop()
        _server.CloseAsync()
        InstanceHelper.DestroyInstances()
    End Sub
End Class
