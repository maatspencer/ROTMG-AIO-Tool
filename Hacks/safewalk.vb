Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class safewalk
    ' Jnoobs safeWalk Hack
    Public Shared Sub Add()
        If listUtility.isSelected("JNoob's Safe Walk Hack") = True Then
            Try
                Dim FILE_NAME As String = Folder & "\safeWalk.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "useAltWeapon" & Quote & " *class.asasm >results10.txt"
                arytext(2) = "findstr /m /f:results10.txt onMouseDown *class.asasm >results11.txt"
                batchProcess.start(arytext, FILE_NAME)


                myFile = Folder & "\client-1\com\company\assembleegameclient\objects\Player.class.asasm"
                newFile = Folder & "\LavaWalls.AIO"
                count = 1
                currentLine = ""
                Dim LavaWalls As String = File.ReadAllText(Folder & "\safeWalk.txt")
                Dim GetSquare As Integer = 0
                Dim Check As Boolean = False

                Dim OnMouseDown As String = File.ReadAllText(Folder & "\results11.txt")
                OnMouseDown = OnMouseDown.Replace(Environment.NewLine, "")
                Dim MouseClass As String = OnMouseDown
                Dim Clicked As String = ""
                OnMouseDown = OnMouseDown.Split("\")(5).Replace(".class.asasm", "")

                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("/onMouseDown") Then
                            Check = True
                        End If

                        If Check = True And CurrentLine.Contains("initproperty") Then
                            Clicked = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Dim Temp As String = Clicked.Split(","c)(2).TrimStart(" """.ToCharArray)
                Clicked = Temp.Substring(0, Temp.IndexOf(""""c))

                LavaWalls = LavaWalls.Replace("Change1", OnMouseDown)
                LavaWalls = LavaWalls.Replace("Change2", Clicked)

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("getSquare") Then
                            GetSquare = Count
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

                            If Count = GetSquare + 3 Then
                                sw.WriteLine(LavaWalls)
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
                Info.WriteLine("Safe Walk:")
        Info.WriteLine("Line: " & GetSquare + 3)
        Info.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm")
        Info.WriteLine("")
        Info.WriteLine("Code Added:")
                Info.WriteLine("#set mouseClass    " & OnMouseDown)
        Info.WriteLine("#set mouseClicked   " & Clicked)
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = File.ReadAllLines(Folder & "\lavawalls.txt").Length
        hackFile.WriteLine("com\company\assembleegameclient\objects\Player.class.asasm" & "," & GetSquare + 3 & "," & Length)
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
End Class
