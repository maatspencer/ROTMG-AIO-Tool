Imports System.IO

Public Class getVersion
    ' Get File Version
    Public Shared Function GetVersion() As String
        Dim Folder As String = Application.StartupPath & "/AddHacks"
        Dim Version As String

        Dim VersionLine As String = ""
        Dim VersionFile As String = Folder & "\client-1\com\company\assembleegameclient\parameters\Parameters.class.asasm"
        Dim VersionCheck As Boolean = False

        Using sr As StreamReader = New StreamReader(VersionFile)
            VersionLine = sr.ReadLine
            Do While (Not VersionLine Is Nothing)

                If VersionLine.Contains("/init") Then
                    VersionCheck = True
                End If

                If VersionLine.Contains("pushstring") And VersionCheck = True Then
                    Version = VersionLine
                    Exit Do
                End If

                VersionLine = sr.ReadLine
            Loop
        End Using

        Version = Version.Split("""")(1)

        Return Version
    End Function
End Class
