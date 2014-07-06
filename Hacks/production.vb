Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class production
    ' Connect to Production
    Public Shared Sub Add()
        If listUtility.isSelected("Connect to Production") = True Then
            Try
                FILE_NAME = Folder & "\production.bat"
                Array.Resize(arytext, 4)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "Desktop" & Quote & " *class.asasm >results1.txt"
                arytext(2) = "findstr /m /f:results1.txt " & Quote & "Production" & Quote & " *class.asasm >results2.txt"
                arytext(3) = "findstr /m /f:results2.txt " & Quote & "Capabilities" & Quote & " *class.asasm >results3.txt"
                batchProcess.start(arytext, FILE_NAME)

                myFile = File.ReadAllText(Folder & "\results3.txt")
                MyFile = MyFile.Replace(Environment.NewLine, "")
                newFile = Folder & "\Production.AIO"
                count = 1
                currentLine = ""
                Dim RetVal As Integer = 0


                'Read for the last instance of returnvalue before Capabilities
                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Capabilities") Then
                            Exit Do
                        End If

                        If CurrentLine.Contains("returnvalue") Then
                            RetVal = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                ' Write The Hack lines in
                Using sr As StreamReader = New StreamReader(Folder & "/" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = RetVal Then
                                sw.WriteLine("      pop")
                                sw.WriteLine("      pushtrue")
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, Folder & "/" & MyFile, True)

        ' Write the updated code information
        Dim Info As StreamWriter
        Info = File.AppendText(linesAndFiles)
        Info.WriteLine("Connect to Production:")
        Info.WriteLine("Line: " & RetVal - 1)
        myFile = myFile.Replace("client-1\", "")
        Info.WriteLine(myFile)
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        hackFile.WriteLine(myFile & "," & RetVal - 1 & "," & "2")
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
End Class
