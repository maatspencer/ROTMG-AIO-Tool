Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class alliedProj
    ' Disable Ally Projectiles
    Public Shared Sub Add()
        If listUtility.isSelected("No Ally Projectiles") = True Then
            Try
                MyFile = Folder & "\client-1\com\company\assembleegameclient\objects\Projectile.class.asasm"
                NewFile = Folder & "\AllyProj.AIO"
                count = 1
                CurrentLine = ""

                Dim ownBullet As String = ""
                Dim Check As Boolean = False
                Dim noAlly As String = File.ReadAllText(Folder & "\noAlly.txt")

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Check = True Then
                            ownBullet = CurrentLine
                            Exit Do
                        End If

                        If CurrentLine.Contains("bulletType_") Then
                            Check = True
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Dim Temp As String = ownBullet.Split(",")(2).TrimStart(" """.ToCharArray)
                ownBullet = Temp.Substring(0, Temp.IndexOf(""""))
                noAlly = noAlly.Replace("Change1", ownBullet)

                Count = 1

                Check = False
                Dim Draw As Integer = 1
                Dim DrawShadow As Integer = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Check = True And CurrentLine.Contains("pushscope") Then
                            Draw = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("/draw" & Quote) Then
                            Check = True
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

                            If Count = Draw + 2 Then
                                sw.WriteLine(noAlly)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, MyFile, True)
                Check = False
                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Check = True And CurrentLine.Contains("pushscope") Then
                            DrawShadow = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("/drawShadow" & Quote) Then
                            Check = True
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
                            Dim LinetoWrite = CurrentLine

                            If Count = DrawShadow + 2 Then
                                sw.WriteLine(noAlly)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                Dim Info As StreamWriter
                Info = File.AppendText(LinesAndFiles)
                Info.WriteLine("No Ally Projectiles:")
                Info.WriteLine("Line: " & Draw + 1)
                Info.WriteLine("Line: " & DrawShadow - 17)
                Info.WriteLine("com\company\assembleegameclient\objects\Projectile.class.asasm")
                Info.WriteLine("")
                Info.WriteLine("Updated Code: ")
                Info.WriteLine("#set ownBullet       " & ownBullet)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                HackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\noAlly.txt").Length
                HackFile.WriteLine("com\company\assembleegameclient\objects\Projectile.class.asasm" & "," & Draw + 1 & "," & Length)
                HackFile.WriteLine("com\company\assembleegameclient\objects\Projectile.class.asasm" & "," & DrawShadow - 17 & "," & Length)
                HackFile.Flush()
                HackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
