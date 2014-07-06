Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class numericalHpMpFame
    ' Numerical HP/MP/Fame
    Public Shared Sub Add()
        If listUtility.isSelected("Numerical HP/MP/Fame") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\ui\StatusBar.class.asasm"
                newFile = Folder & "\Numeric.AIO"
                count = 1
                currentLine = ""
                Dim Numeric As String = File.ReadAllText(Folder & "\numericHpMp.txt")
                Dim Line As Integer = 0
                Dim Fal As Integer = 0
                Dim Method As String = ""

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("False()") Then
                            Method = CurrentLine
                            Fal = Count
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Dim Temp As String = Method.Split(","c)(2).TrimStart(" """.ToCharArray)
                Method = Temp.Substring(0, Temp.IndexOf(""""c))

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains(Method) Then
                            If Count > Fal Then
                                Line = Count
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

                            If Count = Line + 1 Then
                                sw.WriteLine(Numeric)
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
        Info.WriteLine("Numerical HP/MP/Fame:")
        Info.WriteLine("Line: " & Line)
        Info.WriteLine("com\company\assembleegameclient\ui\StatusBar.class.asasm")
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = File.ReadAllLines(Folder & "\numericHpMp.txt").Length
        hackFile.WriteLine("com\company\assembleegameclient\ui\StatusBar.class.asasm" & "," & Line & "," & Length)
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
End Class
