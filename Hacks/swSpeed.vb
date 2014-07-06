Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class swSpeed
    ' SW Speed Multiplier
    Public Shared Sub Add()
        If listUtility.isSelected("SW Speed Hack") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\objects\Player.class.asasm"
                newFile = Folder & "\SWSpeed.AIO"
                count = 1
                currentLine = ""
                Dim PreviousLine As String = ""
                Dim Check As Boolean = False
                Dim Init As String = ""
                Dim Props As String = ""
                Dim Speed As String = ""
                Dim SWSpeed As String = File.ReadAllText(Folder & "\swSpeed.txt")

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("speed_") And PreviousLine.Contains("props_") Then
                            Check = True
                        End If

                        If CurrentLine.Contains("initproperty") And Check = True Then
                            Init = CurrentLine
                            Exit Do
                        End If

                        PreviousLine = CurrentLine
                        CurrentLine = sr.ReadLine

                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Dim Temp As String = Init.Split("(")(2).TrimStart("""".ToCharArray)
                Props = Temp.Substring(0, Temp.IndexOf(""""c))
                SWSpeed = SWSpeed.Replace("Change1", Props)

                Temp = Init.Split(","c)(1).TrimStart(" """.ToCharArray)
                Speed = Temp.Substring(0, Temp.IndexOf(""""c))
                SWSpeed = SWSpeed.Replace("Change2", Speed)

                Count = 1
                Check = False
                Dim Line As Integer = 0

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("dy_") Then
                            Line = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1
                Dim NewLine As Integer = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Count > Line And CurrentLine.Contains("update") Then
                            Exit Do
                        End If

                        If Count > Line And CurrentLine.Contains("multiply") Then
                            NewLine = Count
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

                            If Count = NewLine + 1 Then
                                sw.WriteLine(SWSpeed)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                If listBox.GetSelected(4) = True Then
                    NewLine = NewLine - 42
                End If
                If listBox.GetSelected(13) = True Then
                    NewLine = NewLine - 2
                End If

                My.Computer.FileSystem.CopyFile(NewFile, MyFile, True)


                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("Modify Speed in SW:")
                Info.WriteLine("Line: " & NewLine)
                Info.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm")
                Info.WriteLine("")
                Info.WriteLine("Update Code:")
                Info.WriteLine("#set Props    " & Props)
                Info.WriteLine("#set Speed    " & Speed)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\swSpeed.txt").Length
                hackFile.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm" & "," & NewLine & "," & Length)
                hackFile.Flush()
                hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
