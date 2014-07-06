Imports System
Imports System.IO
Imports AIO.hackVariables.Vars
Public Class encryptionKeys
    ' Find RC4 Keys
    Public Shared Sub getRC4(location As String)
        FILE_NAME = location & "\getRC4.bat"
        Array.Resize(arytext, 4)
        arytext(0) = "cd " & location
        arytext(1) = "findstr /s /m " & Quote & "rc4" & Quote & " *class.asasm >results4.txt"
        arytext(2) = "findstr /m /f:results1.txt " & Quote & "getCipher" & Quote & " *class.asasm >results5.txt"
        arytext(3) = "findstr /m /f:results5.txt " & Quote & "PLAYSOUND" & Quote & " *class.asasm >results6.txt"
        batchProcess.start(arytext, FILE_NAME)

        'Find the smaller of the two packet classes
        Dim MyFile As String = System.IO.File.ReadAllText(location & "\results6.txt")
        MyFile = MyFile.Replace(Environment.NewLine, "")
        Dim NewFile As String = location & "\RC4keys.txt"
        Dim Count As Integer = 1
        Dim CurrentLine As String = ""
        Dim toggle As Boolean = False
        Dim writer As StreamWriter = New StreamWriter(NewFile, False)
        Dim keyTick As Integer = 0


        'Read for the last instance of returnvalue before Capabilities
        Using sr As StreamReader = New StreamReader(location & "\" & MyFile)
            CurrentLine = sr.ReadLine
            Do While (Not CurrentLine Is Nothing)

                If toggle = True Then
                    If CurrentLine.Contains("pushstring") Then
                        If keyTick = 0 Then
                            writer.WriteLine("clientKey=" & CurrentLine.Split(Quote)(1))
                            keyTick += 1
                            toggle = False
                        Else
                            writer.WriteLine("serverKey=" & CurrentLine.Split(Quote)(1))
                            keyTick += 1
                            toggle = False
                        End If
                    End If

                    If keyTick = 2 Then
                        Exit Do
                    End If
                End If

                If CurrentLine.Contains("rc4") Then
                    toggle = True
                End If

                CurrentLine = sr.ReadLine
                Count = Count + 1
            Loop
        End Using

        writer.Flush()
        writer.Close()
    End Sub
End Class
