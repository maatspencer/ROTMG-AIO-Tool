Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class noQuestDelay
    ' No Quest Delay
    Public Shared Sub Add()
        If listUtility.isSelected("No Quest Delay") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\map\Quest.class.asasm"
                newFile = Folder & "\QNote.AIO"
                count = 1
                currentLine = ""
                Dim LineNum As String = ""
                Dim GetObj As Integer = 0
                Dim Line As Integer = 0
                Dim PushScope As Integer = 0
                Dim Method As String = ""


                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("/getObject") Then
                            GetObj = Count
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains(":") And CurrentLine.IndexOf("L") = 0 Then
                            If Count > GetObj Then
                                LineNum = CurrentLine
                            End If
                        End If

                        If Count > GetObj Then
                            If CurrentLine.Contains("end") Then
                                Exit Do
                            End If
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using
                LineNum = LineNum.Replace(":", "")
                LineNum = "      jump                " & LineNum

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Count > GetObj Then
                            If CurrentLine.Contains("pushscope") Then
                                PushScope = Count
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

                            If Count = PushScope + 1 Then
                                sw.WriteLine(LineNum)
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
        Info.WriteLine("No Quest Delay:")
        Info.WriteLine("Line: " & PushScope)
        Info.WriteLine("com\company\assembleegameclient\map\Quest.class.asasm")
        Info.WriteLine("")
        Info.WriteLine("Updated Code:")
        Info.WriteLine(LineNum)
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = 1
        hackFile.WriteLine("com\company\assembleegameclient\map\Quest.class.asasm" & "," & PushScope & "," & Length)
        hackFile.Flush()
        hackFile.Close()
            Catch
        End Try
        End If

    End Sub
End Class
