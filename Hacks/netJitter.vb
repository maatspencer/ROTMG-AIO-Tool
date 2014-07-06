Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class netJitter
    ' Trim Net Jitter
    Public Shared Sub Add()
        If listUtility.isSelected("Round Net Jitter") = True Then
            Try
                FILE_NAME = Folder & "\Jitter.bat"
                Array.Resize(arytext, 2)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m :JitterWatcher *class.asasm >results9.txt"
                batchProcess.start(arytext, FILE_NAME)

                myFile = File.ReadAllText(Folder & "\results9.txt")
                MyFile = MyFile.Replace(Environment.NewLine, "")
                newFile = Folder & "\Jitter.AIO"
                count = 1
                currentLine = ""
                Dim Roundjitter As String = File.ReadAllText(Folder & "\Roundjitter.txt")
                Dim jitter As Boolean = False
                Dim RetVal As Integer = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("/cinit") Then
                            Exit Do
                        End If

                        If CurrentLine.Contains("/jitter") Then
                            jitter = True
                        End If

                        If CurrentLine.Contains("returnvalue") And jitter = True Then
                            RetVal = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = RetVal Then
                                sw.WriteLine(Roundjitter)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, Folder & "\" & MyFile, True)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("Round Net Jitter:")
                Info.WriteLine("Line: " & RetVal - 1)
                MyFile = MyFile.Replace("client-1\", "")
                Info.WriteLine(MyFile)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\Roundjitter.txt").Length
                hackFile.WriteLine(MyFile & "," & RetVal - 1 & "," & Length)
                hackFile.Flush()
                hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
