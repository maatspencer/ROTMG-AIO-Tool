Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class spamNotifications
    ' Allow Spammable Notifications
    Public Shared Sub Add()
        If listUtility.isSelected("Spammable Notifications") = True Then
            Try
                FILE_NAME = Folder & "\SpamNot.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder & "/client-1"
                arytext(1) = "findstr /s /m addQueuedText *class.asasm >results7.txt"
                arytext(2) = "findstr /m /f:results7.txt addStatusText *class.asasm >results8.txt"
                batchProcess.start(arytext, FILE_NAME)

                My.Computer.FileSystem.CopyFile(Folder & "\client-1\results8.txt", Folder & "\results8.txt", True)

                myFile = File.ReadAllText(Folder & "\results8.txt")
                MyFile = MyFile.Replace(Environment.NewLine, "")
                newFile = Folder & "\SpamNot.AIO"
                count = 1
                currentLine = ""
                Dim AddQued As Boolean = False
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(Folder & "\client-1\" & MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("/addQueuedText") Then
                            AddQued = True
                        End If

                        If CurrentLine.Contains("dup") And AddQued = True Then
                            Code = Count
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "/client-1/" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = Code + 1 Then
                                sw.WriteLine("      pop")
                                sw.WriteLine("      pushfalse")
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, Folder & "/client-1/" & MyFile, True)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("Allow Spammable Notifications:")
                Info.WriteLine("Line: " & Code)
                MyFile = MyFile.Replace("client-1\", "")
                Info.WriteLine(MyFile)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = 2
                hackFile.WriteLine(MyFile & "," & Code & "," & Length)
                hackFile.Flush()
                hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
