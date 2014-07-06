Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class hpBars
    ' HP Bars
    Public Shared Sub Add()
        If listUtility.isSelected("HP Bars") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\objects\Character.class.asasm"
                newFile = Folder & "\HPBars.AIO"
                count = 1
                currentLine = ""
                Dim HPBars As String = File.ReadAllText(Folder & "\hpBar.txt")
                Dim CharNS As String = ""
                Dim spnsGameObject As String = ""
                Dim spnsBasicObject As String = ""
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("ProtectedNamespace") Then
                            CharNS = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Dim Temp As String = CharNS.Split("(")(1).TrimStart("""")
                CharNS = Temp.Substring(0, Temp.IndexOf(""""))
                HPBars = HPBars.Replace("Change01", CharNS)
                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm")
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("ProtectedNamespace") Then
                            spnsGameObject = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Temp = spnsGameObject.Split("(")(1).TrimStart("""")
                spnsGameObject = Temp.Substring(0, Temp.IndexOf(""""))
                HPBars = HPBars.Replace("Change02", spnsGameObject)
                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\client-1\com\company\assembleegameclient\objects\BasicObject.class.asasm")
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("ProtectedNamespace") Then
                            spnsBasicObject = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Temp = spnsBasicObject.Split("(")(1).TrimStart("""")
                spnsBasicObject = Temp.Substring(0, Temp.IndexOf(""""))
                HPBars = HPBars.Replace("Change03", spnsBasicObject)
                Count = 1


                FILE_NAME = Folder & "\Util.bat"
                Array.Resize(arytext, 2)
                arytext(0) = "cd " & Folder & "\client-1\com\company\util"
                arytext(1) = "findstr /s /m " & Quote & "MOVE_TO" & Quote & " *class.asasm >results5.txt"
                batchProcess.start(arytext, FILE_NAME)

                My.Computer.FileSystem.MoveFile(Folder & "\client-1\com\company\util\results5.txt", Folder & "\results5.txt", True)

                Dim Util As String = File.ReadAllText(Folder & "\results5.txt")
                Util = Util.Replace(Environment.NewLine, "")
                Dim Utilshort As String = Util.Split(".")(0)
                HPBars = HPBars.Replace("Change04", Utilshort)

                Dim graphicUtilsCommands As String = ""
                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\client-1\com\company\util\" & Util)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("slotid 2") Then
                            graphicUtilsCommands = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Temp = graphicUtilsCommands.Split(",")(2).TrimStart(" """.ToCharArray)
                graphicUtilsCommands = Temp.Substring(0, Temp.IndexOf(""""))
                HPBars = HPBars.Replace("Change05", graphicUtilsCommands)
                Count = 1

                Dim charPos As String = ""
                Using sr As StreamReader = New StreamReader(Folder & "\client-1\com\company\assembleegameclient\objects\BasicObject.class.asasm")
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("sortVal_") Then
                            Exit Do
                        End If
                        charPos = CurrentLine

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Temp = charPos.Split(",")(2).TrimStart(" """.ToCharArray)
                charPos = Temp.Substring(0, Temp.IndexOf(""""))
                HPBars = HPBars.Replace("Change06", charPos)
                Count = 1

                Dim GameObj As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim Player As String = Folder & "\client-1\com\company\assembleegameclient\objects\Player.class.asasm"
                Dim MethodHP As String = ""
                Dim MethodMaxHP As String = ""
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
                Temp = MethodHP.Split(","c)(2).TrimStart(" """.ToCharArray)
                MethodHP = Temp.Substring(0, Temp.IndexOf(""""c))

                Temp = MethodMaxHP.Split(","c)(2).TrimStart(" """.ToCharArray)
                MethodMaxHP = Temp.Substring(0, Temp.IndexOf(""""c))

                ' Replace Methods into code
                HPBars = HPBars.Replace("Change07", MethodHP)
                HPBars = HPBars.Replace("Change08", MethodMaxHP)

                Count = 1

                Dim PlayerMana As String = ""
                Dim PlayerMaxMana As String = ""
                Dim Breath As Boolean = False

                Using sr As StreamReader = New StreamReader(Player)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("breath_") Then
                            Breath = True
                        End If
                        If CurrentLine.Contains("Integer(0)") And Breath = True Then
                            PlayerMana = CurrentLine
                            Exit Do
                        End If
                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1
                Breath = False

                Using sr As StreamReader = New StreamReader(Player)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("breath_") Then
                            Breath = True
                        End If
                        If CurrentLine.Contains("Integer(200)") And Breath = True Then
                            PlayerMaxMana = CurrentLine
                            Exit Do
                        End If
                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Temp = PlayerMana.Split(",")(2).TrimStart(" """.ToCharArray)
                PlayerMana = Temp.Substring(0, Temp.IndexOf(""""))

                Temp = PlayerMaxMana.Split(",")(2).TrimStart(" """.ToCharArray)
                PlayerMaxMana = Temp.Substring(0, Temp.IndexOf(""""))

                HPBars = HPBars.Replace("Change09", PlayerMana)
                HPBars = HPBars.Replace("Change10", PlayerMaxMana)

                Count = 1

                Dim Draw As Boolean = False
                Dim GameBoard As String = ""

                Using sr As StreamReader = New StreamReader(GameObj)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("/draw") Then
                            Draw = True
                        End If

                        If CurrentLine.Contains("map") And Draw = True Then
                            GameBoard = CurrentLine
                            Exit Do
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Temp = GameBoard.Split(",")(1).TrimStart(" """.ToCharArray)
                GameBoard = Temp.Substring(0, Temp.IndexOf(""""))

                HPBars = HPBars.Replace("Change11", GameBoard)

                Count = 1

                Dim Slot As Integer = 0

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("trait slot") Then
                            Slot = Count
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

                            If Count = Slot + 1 Then
                                sw.WriteLine(HPBars)
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
                Info.WriteLine("HP Bars:")
                Info.WriteLine("Line: " & Slot)
                Info.WriteLine("com/company/assembleegameclient/objects/Character.class.asasm")
                Info.WriteLine("")
                Info.WriteLine("Updated Code: ")

                Dim Check As Boolean = False

                Using sr As StreamReader = New StreamReader(NewFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)
                        Dim LinetoWrite = CurrentLine
                        If CurrentLine.Contains("#set") And Check = True Then
                            Info.WriteLine(CurrentLine)
                        End If
                        If CurrentLine.Contains(" Guild Hall") Then
                            Check = True
                        End If

                        Count = Count + 1
                        CurrentLine = sr.ReadLine
                    Loop
                End Using
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\hpBar.txt").Length
                hackFile.WriteLine("com/company/assembleegameclient/objects/Character.class.asasm" & "," & Slot & "," & Length)
                hackFile.Flush()
                hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
