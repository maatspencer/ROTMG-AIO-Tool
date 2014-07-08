Imports System.Net

Public Class getLatest
    Public Shared Function Grab() As String
        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "/Clients") Then
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "/Clients")
        End If
        Using wc As New WebClient()
            Dim clientVersion = wc.DownloadString("http://www.realmofthemadgod.com/version.txt")
            Dim swf = "http://www.realmofthemadgod.com/AssembleeGameClient" + clientVersion + ".swf"
            wc.DownloadFile(swf, Application.StartupPath & "\Clients\Fresh_Client_" & clientVersion & ".swf")
            Return clientVersion
        End Using
    End Function
End Class
