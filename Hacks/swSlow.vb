Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class swSlow
    ' SW Slow
    Public Shared Sub Add()
        If listUtility.isSelected("SW No Slow") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                newFile = Folder & "\SWSlow.AIO"
                count = 1
                currentLine = ""
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim Effect As String = ""
                Dim effLine As Integer = 0
                Dim Code As Integer = 0
                Dim SWSlow As String = File.ReadAllText(Folder & "\swNoSlow.txt")

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Slow") Then
                            effLine = Count
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Count = effLine + 1 Then
                            Effect = CurrentLine
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Dim Temp As String = Effect.Split(","c)(2).TrimStart(" """.ToCharArray)
                Effect = Temp.Substring(0, Temp.IndexOf(""""c))

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("pushscope") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Effect) Then
                            Exit Do
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

                            If Count = Code + 1 Then
                                sw.WriteLine(SWSlow)
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
        Info.WriteLine("Disable SW Slow Effect:")
        Info.WriteLine("Line: " & Code)
        Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = File.ReadAllLines(Folder & "\swNoSlow.txt").Length
        hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
End Class
