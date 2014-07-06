Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class debuffs
    ' Disable Confuse
    Public Shared Sub addConfuse()
        If listUtility.isSelected("Confuse") = True Then
            Try
                myFile = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                newFile = Folder & "\Confuse.AIO"
                count = 1
                currentLine = ""
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim Confuse As String = ""
                Dim ConfLine As Integer = 0
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Confused") Then
                            ConfLine = Count
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

                        If Count = ConfLine + 1 Then
                            Confuse = CurrentLine
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Dim Temp As String = Confuse.Split(","c)(2).TrimStart(" """.ToCharArray)
                Confuse = Temp.Substring(0, Temp.IndexOf(""""c))

                Count = 1

                Using sr As StreamReader = New StreamReader(MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("code") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Quote & Confuse & Quote) Then
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
                                sw.WriteLine("pushfalse")
                                sw.WriteLine("returnvalue")
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
                Info.WriteLine("Confuse:")
                Info.WriteLine("Line: " & Code)
                Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = 2
                hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
    ' Disable Drunk
    Public Shared Sub addDrunk()
        If listUtility.isSelected("Drunk") = True Then
            Try
                Dim MyFile As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim NewFile As String = Folder & "\Drunk.AIO"
                Dim Effect As String = ""
                Dim Count As Integer = 1
                Dim CurrentLine As String = ""
                Dim effLine As Integer = 0
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Drunk") Then
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

                        If CurrentLine.Contains("code") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Quote & Effect & Quote) Then
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

                            If Count = Code + 1 Then
                                sw.WriteLine("pushfalse")
                                sw.WriteLine("returnvalue")
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
        Info.WriteLine("Drunk:")
        Info.WriteLine("Line: " & Code)
        Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = 2
        hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
    ' Disable Blind
    Public Shared Sub addBlind()
        If listUtility.isSelected("Blind") = True Then
            Try
                Dim MyFile As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim NewFile As String = Folder & "\Blind.AIO"
                Dim Effect As String = ""
                Dim Count As Integer = 1
                Dim CurrentLine As String = ""
                Dim effLine As Integer = 0
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Blind") Then
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

                        If CurrentLine.Contains("code") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Quote & Effect & Quote) Then
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

                            If Count = Code + 1 Then
                                sw.WriteLine("pushfalse")
                                sw.WriteLine("returnvalue")
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
        Info.WriteLine("Blind:")
        Info.WriteLine("Line: " & Code)
        Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = 2
        hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
    ' Disable Hallucinating
    Public Shared Sub addHallucinating()
        If listUtility.isSelected("Hallucinating") = True Then
            Try
                Dim MyFile As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim NewFile As String = Folder & "\Hallucinating.AIO"
                Dim Effect As String = ""
                Dim Count As Integer = 1
                Dim CurrentLine As String = ""
                Dim effLine As Integer = 0
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Hallucinating") Then
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

                        If CurrentLine.Contains("code") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Quote & Effect & Quote) Then
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

                            If Count = Code + 1 Then
                                sw.WriteLine("pushfalse")
                                sw.WriteLine("returnvalue")
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
                Info.WriteLine("Hallucinating:")
                If listBox.GetSelected(7) = True Then
                    Code = Code - 2
                End If
                If listBox.GetSelected(8) = True Then
                    Code = Code - 2
                End If
                If listBox.GetSelected(9) = True Then
                    Code = Code - 2
                End If
                Info.WriteLine("Line: " & Code)
                Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = 2
                hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
    ' Disable Unstable
    Public Shared Sub addUnstable()
        If listUtility.isSelected("Unstable") = True Then
            Try
                Dim MyFile As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim NewFile As String = Folder & "\Unstable.AIO"
                Dim Effect As String = ""
                Dim Count As Integer = 1
                Dim CurrentLine As String = ""
                Dim effLine As Integer = 0
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Unstable") Then
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

                        If CurrentLine.Contains("code") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Quote & Effect & Quote) Then
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

                            If Count = Code + 1 Then
                                sw.WriteLine("pushfalse")
                                sw.WriteLine("returnvalue")
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
        Info.WriteLine("Unstable:")
        If listBox.GetSelected(7) = True Then
            Code = Code - 2
        End If
        If listBox.GetSelected(8) = True Then
            Code = Code - 2
        End If
        If listBox.GetSelected(9) = True Then
            Code = Code - 2
        End If
        If listBox.GetSelected(10) = True Then
            Code = Code - 2
        End If
        Info.WriteLine("Line: " & Code)
        Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = 2
        hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
        hackFile.Flush()
        hackFile.Close()
            Catch
        End Try
        End If

    End Sub
    ' Disable Darkness
    Public Shared Sub addDarkness()
        If listUtility.isSelected("Darkness") = True Then
            Try
                Dim MyFile As String = Folder & "\client-1\com\company\assembleegameclient\objects\GameObject.class.asasm"
                Dim CondEff As String = Folder & "\client-1\com\company\assembleegameclient\util\ConditionEffect.class.asasm"
                Dim NewFile As String = Folder & "\Darkness.AIO"
                Dim Effect As String = ""
                Dim Count As Integer = 1
                Dim CurrentLine As String = ""
                Dim effLine As Integer = 0
                Dim Code As Integer = 0

                Using sr As StreamReader = New StreamReader(CondEff)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("Darkness") Then
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

                        If CurrentLine.Contains("code") Then
                            Code = Count
                        End If

                        If CurrentLine.Contains(Quote & Effect & Quote) Then
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

                            If Count = Code + 1 Then
                                sw.WriteLine("pushfalse")
                                sw.WriteLine("returnvalue")
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
                Info.WriteLine("Darkness:")
                If listBox.GetSelected(7) = True Then
                    Code = Code - 2
                End If
                If listBox.GetSelected(8) = True Then
                    Code = Code - 2
                End If
                If listBox.GetSelected(9) = True Then
                    Code = Code - 2
                End If
                If listBox.GetSelected(10) = True Then
                    Code = Code - 2
                End If
                If listBox.GetSelected(11) = True Then
                    Code = Code - 2
                End If
                Info.WriteLine("Line: " & Code)
                Info.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = 2
                hackFile.WriteLine("com\company\assembleegameclient\objects\GameObject.class.asasm" & "," & Code & "," & Length)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
End Class
