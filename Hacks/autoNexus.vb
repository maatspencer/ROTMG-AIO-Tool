Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class autoNexus
    ' AutoNexus
    Public Shared Sub Add()
        If listUtility.isSelected("AutoNexus") = True Then
            Try
                MyFile = Folder & "\client-1\com\company\assembleegameclient\objects\Player.class.asasm"
                newFile = Folder & "\AutoNexus.AIO"
                count = 1
                currentLine = ""

                Dim GameObj As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim Nexus As String = File.ReadAllText(Folder & "\autoNexus.txt")
                Nexus = Nexus.Replace("CHANGE3", My.Settings.Nexus / 100)
                Dim CharHP As Integer = 0
                Dim MethodHP As String = ""
                Dim CharMaxHP As Integer = 0
                Dim MethodMaxHP As String = ""
                Dim LastDam As Integer = 0
                Dim RetVal As Integer = 0

                ' Find Method for Current Player HP
                Using sr As StreamReader = New StreamReader(GameObj)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Integer(200)") Then
                            MethodHP = CurrentLine
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                ' Find Player Max HP
                Using sr As StreamReader = New StreamReader(GameObj)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Integer(200)") Then
                            MethodMaxHP = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                ' Trim All but Method Name
                Dim Temp As String = MethodHP.Split(","c)(2).TrimStart(" """.ToCharArray)
                MethodHP = Temp.Substring(0, Temp.IndexOf(""""c))

                Temp = MethodMaxHP.Split(","c)(2).TrimStart(" """.ToCharArray)
                MethodMaxHP = Temp.Substring(0, Temp.IndexOf(""""c))

                ' Replace Methods into code
                Nexus = Nexus.Replace("CHANGE1", MethodHP)
                Nexus = Nexus.Replace("CHANGE2", MethodMaxHP)

                Count = 1

                ' Find Line number
                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("lastDamage_") Then
                            LastDam = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                ' Find Line number
                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("returnvalue") Then
                            If Count >= LastDam Then
                                RetVal = Count
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

                            If Count = RetVal Then
                                sw.WriteLine(Nexus)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, myFile, True)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("AutoNexus:")
                If listBox.GetSelected(4) = True Then
                    RetVal = RetVal - 42
                End If
                Info.WriteLine("Line: " & RetVal - 1)
                Info.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm")
                Info.WriteLine("")
                Info.WriteLine("Updated Code:")
                Info.WriteLine("charHp:     " & MethodHP)
                Info.WriteLine("charMaxHp:  " & MethodMaxHP)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\autoNexus.txt").Length
                hackFile.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm" & "," & RetVal - 1 & "," & Length)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
End Class
