Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class noLoading
    ' Remove Loading Screen
    Public Shared Sub Add()
        If listUtility.isSelected("Remove Loading Screen") = True Then
            Try
                FILE_NAME = Folder & "\NoLoading.bat"
                Array.Resize(arytext, 2)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "indicator_" & Quote & " *class.asasm >results6.txt"
                batchProcess.start(arytext, FILE_NAME)

                myFile = File.ReadAllText(Folder & "\results6.txt")
                MyFile = MyFile.Replace(Environment.NewLine, "")
                newFile = Folder & "\NoLoading.AIO"
                count = 1
                currentLine = ""
                Dim TraitMethod As Boolean = False
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("trait method") Then
                            TraitMethod = True
                        End If

                        If CurrentLine.Contains("pushfalse") And TraitMethod = True Then
                            Code = Count
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "/" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = Code + 1 Then
                                sw.WriteLine("")
                                sw.WriteLine("      returnvoid")
                                sw.WriteLine("")
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, Folder & "/" & MyFile, True)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("Remove Loading Screen:")
                Info.WriteLine("Line: " & Code)
                MyFile = MyFile.Replace("client-1\", "")
                Info.WriteLine(MyFile)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = 3
                hackFile.WriteLine(MyFile & "," & Code & "," & Length)
                hackFile.Flush()
                hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
