Imports System.IO
Imports System.Net
'Imports Newtonsoft.Json.Linq

Public Class DockerClient
    Private Sub Exceute(batFileName As String)
        Dim procc As New Process
        procc.StartInfo.FileName = batFileName
        procc.StartInfo.Verb = "runas"
        procc.StartInfo.WorkingDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName
        procc.StartInfo.CreateNoWindow = True
        procc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        procc.StartInfo.UseShellExecute = False
        procc.StartInfo.RedirectStandardError = True
        procc.StartInfo.RedirectStandardOutput = True
        Dim resultvalue As String = procc.StandardOutput.ReadToEnd
        procc.Start()
        procc.BeginOutputReadLine()
        procc.WaitForExit()
        procc.Close()
        IO.File.Delete(batFileName)
    End Sub

    Public Sub CreateFile(CredentialIP As String, Username As String, password As String, filename As String)
        If System.IO.File.Exists("CreateFile.txt") Then
            System.IO.File.Delete("CreateFile.txt")
        End If
        If System.IO.File.Exists("Build.bat") Then
            System.IO.File.Delete("Build.bat")
        End If

        If Not System.IO.File.Exists("CreateFile.txt") Then
            IO.File.WriteAllText("CreateFile.txt", "echo 'my linux data' >> " & filename)
        End If
        If Not System.IO.File.Exists("Build.bat") Then
            IO.File.WriteAllText("Build.bat", "putty.exe -ssh " & CredentialIP & " -l " & Username & " -pw " & password & " -m CreateFile.txt")
        End If
        Exceute("Build.bat")
    End Sub

    Public Sub CreateContainer(CredentialIP As String, Username As String, password As String, templateimagepath As String, newContainername As String, CASSENDRAPORTNO As String, ByVal Nginxportno As String, ByVal POSTGREPORTNO As String)
        Dim puttyreferencefilename As String = "CreateContainer.txt"
        If System.IO.File.Exists(puttyreferencefilename) Then
            System.IO.File.Delete(puttyreferencefilename)
        End If
        If System.IO.File.Exists("Build.bat") Then
            System.IO.File.Delete("Build.bat")
        End If

        Dim cassendraportvalue As String = ""
        If CASSENDRAPORTNO <> "" Then
            cassendraportvalue = " -p " & CASSENDRAPORTNO & ":9042"
        End If

        Dim nginxportvalue As String = ""
        If Nginxportno <> "" Then
            nginxportvalue = " -p  " & Nginxportno & ":80"
        End If

        Dim postgreportvalue As String = ""
        If POSTGREPORTNO <> "" Then
            postgreportvalue = " -p " & POSTGREPORTNO & ":5432"
        End If



        If Not System.IO.File.Exists(puttyreferencefilename) Then
            'cat core_clt_usr_cas.tar | docker import - clt_usr_cas_client1
            '   IO.File.WriteAllText(puttyreferencefilename, " echo " & DOCKER_FILE_CONTENT & " >> /HOME/" & templateimagepath & "/DOCKERFILE")
            '  IO.File.WriteAllText(puttyreferencefilename, "cd home")
            ' IO.File.WriteAllText(puttyreferencefilename, "cd " & templateimagepath)
            IO.File.WriteAllText(puttyreferencefilename, " docker build -t " & newContainername & " -f" & templateimagepath & ".")
            IO.File.WriteAllText(puttyreferencefilename, "docker run -i " & cassendraportvalue & nginxportvalue & postgreportvalue & " " & newContainername & " --name=" & newContainername)
        End If
        If Not System.IO.File.Exists("Build.bat") Then
            IO.File.WriteAllText("Build.bat", "putty.exe -ssh " & CredentialIP & " -l " & Username & " -pw " & password & " -m " & puttyreferencefilename)
        End If
        Exceute("Build.bat")
    End Sub

    Private Function GetContainerID(CredentialIP As String, imagename As String, ByVal portno As String) As String
        'Dim wc2 As New WebClient
        'Dim urivalue As String = "Http://" & CredentialIP & ":" & portno & "/images/" & imagename & "/json"
        'Dim uric As New Uri(urivalue)
        'Dim result As String = wc2.DownloadString(uric)
        'Dim jobj As JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(result)
        'Dim configdata As JObject = jobj("ContainerConfig")
        'Dim containerid As String = configdata("Hostname")
        'Return containerid
    End Function


    Public Sub RunNGINX(CredentialIP As String, Username As String, password As String, client_container_name As String, ByVal NGINXContent As String)
        Dim puttyreferencefilename As String = "RunNGINX.txt"
        If System.IO.File.Exists(puttyreferencefilename) Then
            System.IO.File.Delete(puttyreferencefilename)
        End If
        If System.IO.File.Exists("Build.bat") Then
            System.IO.File.Delete("Build.bat")
        End If

        If Not System.IO.File.Exists(puttyreferencefilename) Then
            'cat core_clt_usr_cas.tar | docker import - clt_usr_cas_client1
            IO.File.WriteAllText(puttyreferencefilename, " echo " & NGINXContent & " >> nginx.conf")
            IO.File.WriteAllText(puttyreferencefilename, "cat nginx.conf | docker exec -i " & client_container_name & " sh -c 'cat > /etc/nginx/nginx.conf'")
            IO.File.WriteAllText(puttyreferencefilename, "docker exec -it  " & client_container_name & "/bin/bash")
            IO.File.WriteAllText(puttyreferencefilename, "service nginx restart")
            IO.File.WriteAllText(puttyreferencefilename, "exit")
            ' IO.File.WriteAllText(puttyreferencefilename, "docker commit " & client_container_name & "  " & client_container_name)
        End If

        If Not System.IO.File.Exists("Build.bat") Then
            IO.File.WriteAllText("Build.bat", "putty.exe -ssh " & CredentialIP & " -l " & Username & " -pw " & password & " -m " & puttyreferencefilename)
        End If
        Exceute("Build.bat")
    End Sub

End Class
