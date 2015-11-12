Imports System.Configuration
Imports System.Reflection

<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer
    Private strDefaultServiceName As String = "ScanService"

    Private strServiceName As String = GetConfigurationValue("SERVICENAME")

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ServiceProcessInstaller1 = New System.ServiceProcess.ServiceProcessInstaller()
        Me.ServiceInstaller1 = New System.ServiceProcess.ServiceInstaller()
        '
        'ServiceProcessInstaller1
        Me.ServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.ServiceProcessInstaller1.Password = Nothing
        Me.ServiceProcessInstaller1.Username = Nothing
        '
        'ServiceInstaller1
        Me.ServiceInstaller1.StartType = ServiceProcess.ServiceStartMode.Automatic
        Me.ServiceInstaller1.ServiceName = strServiceName
        Me.ServiceInstaller1.Description = "Handles Scan Services"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceProcessInstaller1, Me.ServiceInstaller1})

    End Sub
    Friend WithEvents ServiceProcessInstaller1 As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents ServiceInstaller1 As System.ServiceProcess.ServiceInstaller

    Private Function GetConfigurationValue(key As String) As String

        Dim config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)

        If config.AppSettings.Settings(key) IsNot Nothing Then
            Return config.AppSettings.Settings(key).Value
        Else
            Return strDefaultServiceName
        End If
    End Function

End Class
