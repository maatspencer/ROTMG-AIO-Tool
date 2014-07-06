Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class forceLQ
    ' Force Low Quality
    Public Shared Sub Add()
        If listUtility.isSelected("Force Low Quality") = True Then
            Try
                myFile = Folder & "\client-1\WebMain.class.asasm"
                newFile = Folder & "\LowQ.AIO"
                count = 1
                currentLine = ""
                Dim forcelq As String = File.ReadAllText(Folder & "\forcelq.txt")
                Dim Setup As Integer = 0
                Dim RetVoid As Integer = 0

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("/setup") Then
                            Setup = Count
                        End If

                        If CurrentLine.Contains("returnvoid") Then
                            If Setup <> 0 Then
                                RetVoid = Count
                                Exit Do
                            End If
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = RetVoid - 1 Then
                                sw.WriteLine(forcelq)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, MyFile, True)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("Force Low Quality:")
                Info.WriteLine("Line: " & RetVoid - 1)
                Info.WriteLine("WebMain.class.asasm")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\forcelq.txt").Length
                hackFile.WriteLine("WebMain.class.asasm" & "," & RetVoid - 1 & "," & Length)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
End Class
