Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class swTile
    ' SW Tile Hack
    Public Shared Sub Add()
        If listUtility.isSelected("SW Tile Hack") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\objects\Player.class.asasm"
                newFile = Folder & "\SWTile.AIO"
                currentLine = ""
                count = 1
                Dim Tile As String = File.ReadAllText(Folder & "\tileHack.txt")
                Dim push As Integer = 0

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("push_") Then
                            push = Count
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
                            Dim LinetoWrite = CurrentLine

                            If Count = push + 1 Then
                                sw.WriteLine(Tile)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using
                'Check Success
                    My.Computer.FileSystem.CopyFile(NewFile, MyFile, True)


                    Dim Info As StreamWriter
                    Info = File.AppendText(linesAndFiles)
                    Info.WriteLine("SW Tile Hack:")
                If listBox.GetSelected(4) = True Then
                    push = push - 42
                End If
                    Info.WriteLine("Line: " & push)
                    Info.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm")
                    Info.WriteLine("")
                    Info.Flush()
                    Info.Close()

                    ' Fill Hacks Array
                    hackFile = File.AppendText(HackTemp)
                    Dim Length As Integer = File.ReadAllLines(Folder & "\tileHack.txt").Length
                    hackFile.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm" & "," & push & "," & Length)
                    hackFile.Flush()
                    hackFile.Close()

            Catch
            End Try
        End If
    End Sub
End Class
