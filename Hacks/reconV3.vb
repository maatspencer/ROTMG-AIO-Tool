Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class reconV3
    ' Reconnect with P
    Public Shared Sub Add()
        If listUtility.isSelected("Recon V3") = True Then
            Try
                ' Find Mouse Class
                FILE_NAME = Folder & "\MouseClass.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "setEnablePlayerInput" & Quote & " *class.asasm >results13.txt"
                arytext(2) = "findstr /m /f:results13.txt " & Quote & "onAddedToStage" & Quote & " *class.asasm >results14.txt"
                batchProcess.start(arytext, FILE_NAME)

                Dim MouseClass As String = File.ReadAllText(Folder & "\results14.txt")
                MouseClass = MouseClass.Replace(Environment.NewLine, "")

                ' Find Reconnect Class
                FILE_NAME = Folder & "\Recon.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "RECONNECT" & Quote & " *class.asasm >results15.txt"
                arytext(2) = "findstr /m /f:results15.txt " & Quote & "connected" & Quote & " *class.asasm >results16.txt"
                batchProcess.start(arytext, FILE_NAME)

                Dim ReconClass As String = File.ReadAllText(Folder & "\results16.txt")
                ReconClass = ReconClass.Replace(Environment.NewLine, "")

                ' Find Dispatch Class
                FILE_NAME = Folder & "\DispatchClass.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "NUMBER_0" & Quote & " *class.asasm >results17.txt"
                arytext(2) = "findstr /m /f:results17.txt " & Quote & "Win" & Quote & " *class.asasm >results18.txt"
                batchProcess.start(arytext, FILE_NAME)

                Dim DispatClass As String = File.ReadAllText(Folder & "\results18.txt")
                DispatClass = DispatClass.Replace(Environment.NewLine, "")

                ' Find Reconnect Event Class
                FILE_NAME = Folder & "\ReconEvent.bat"
                Array.Resize(arytext, 2)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "RECONNECT_EVENT" & Quote & " *class.asasm >results19.txt"
                batchProcess.start(arytext, FILE_NAME)

                Dim ReconEvent As String = File.ReadAllText(Folder & "\results19.txt")
                ReconEvent = ReconEvent.Replace(Environment.NewLine, "")

                ' Dispatch.txt
                Dim keyCodes As String = DispatClass.Split("\")(4).Split(".")(0)
                Dim DispatchTxt As String = File.ReadAllText(Folder & "/drv2_dispatch.txt")
                DispatchTxt = DispatchTxt.Replace("CHANGE1", keyCodes)
                DispatchTxt = DispatchTxt.Replace("CHANGE2", My.Settings.Recon)
                DispatchTxt = DispatchTxt.Replace("CHANGE3", My.Settings.Drecon)

                ' Save.txt
                Dim hotkeyClass As String = MouseClass.Split("\")(5).Split(".")(0)
                Dim SaveTxt As String = File.ReadAllText(Folder & "/drv2_save.txt")
                SaveTxt = SaveTxt.Replace("CHANGE1", hotkeyClass)

                ' Var.txt
                Dim reconevent1 As String = ReconEvent.Split("\")(1)
                Dim reconevent2 As String = ReconEvent.Split("\")(2).Split(".")(0)
                Dim VarTxt As String = File.ReadAllText(Folder & "/drv2_var.txt")
                VarTxt = VarTxt.Replace("CHANGE1", reconevent1)
                VarTxt = VarTxt.Replace("CHANGE2", reconevent2)

                ' Inject Var.txt
                currentLine = ""
                count = 1
                Dim TSlot As Integer = 1
                Dim Recon1 As String = Folder & "\Recon1.AIO"

                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("trait slot") Then
                            TSlot = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    Using sw As StreamWriter = New StreamWriter(Recon1)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = TSlot + 1 Then
                                sw.WriteLine(VarTxt)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(Recon1, Folder & "\" & MouseClass, True)

                ' Inject Dispatch Txt
                Dim TestTwo As Integer = 1
                Dim Line As String = ""
                Dim LineJump As Integer = 1

                Count = 1


                ' Read down to testTwo
                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("testTwo") Then
                            TestTwo = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                ' Read to the jump right before testTwo
                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Count < TestTwo Then
                            If CurrentLine.Contains("jump") Then
                                Line = CurrentLine
                            End If
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                ' Get line to be jumped to by itself
                Line = "L" & Line.Split("L")(1) & ":"

                Count = 1

                ' Find Line number 
                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains(Line) Then
                            LineJump = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MouseClass)
                    Using sw As StreamWriter = New StreamWriter(Recon1)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = LineJump + 1 Then
                                sw.WriteLine(DispatchTxt)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(Recon1, Folder & "\" & MouseClass, True)

        ' Inject Save Txt
        Dim Arena As Integer = 1
        Dim gs_ As Integer = 1
        Dim Recon2 As String = Folder & "\Recon2.AIO"

        count = 1

        ' Read down to last instance of isFromArena_
        Using sr As StreamReader = New StreamReader(Folder & "\" & ReconClass)
            currentLine = sr.ReadLine
            Do While (Not currentLine Is Nothing)

                If currentLine.Contains("isFromArena_") Then
                    Arena = count
                End If

                currentLine = sr.ReadLine
                count = count + 1
            Loop
        End Using

        count = 1

        ' Read down to gs_ after isFromArena_
        Using sr As StreamReader = New StreamReader(Folder & "\" & ReconClass)
            currentLine = sr.ReadLine
            Do While (Not currentLine Is Nothing)

                If count > Arena Then
                    If currentLine.Contains("gs_") Then
                        gs_ = count
                        Exit Do
                    End If
                End If

                currentLine = sr.ReadLine
                count = count + 1
            Loop
        End Using

        count = 1

        Using sr As StreamReader = New StreamReader(Folder & "\" & ReconClass)
            Using sw As StreamWriter = New StreamWriter(Recon2)
                currentLine = sr.ReadLine
                Do While (Not currentLine Is Nothing)
                            lineToWrite = currentLine

                    If count = gs_ + 1 Then
                        sw.WriteLine(SaveTxt)
                    End If

                    sw.WriteLine(LinetoWrite)
                    count = count + 1
                    currentLine = sr.ReadLine
                Loop
            End Using
        End Using

                My.Computer.FileSystem.CopyFile(Recon2, Folder & "\" & ReconClass, True)

        Dim Info As StreamWriter
        Info = File.AppendText(linesAndFiles)
        Info.WriteLine("Reconnect with P:")
        Info.WriteLine("")
        Info.WriteLine("Dispatch:")
        Info.WriteLine("#set keyCodes: " & keyCodes)
        MouseClass = MouseClass.Replace("client-1\", "")
        Info.WriteLine("File2Mod: " & MouseClass)
        Info.WriteLine("Line: " & LineJump)
        Info.WriteLine("")
        Info.WriteLine("Var:")
        Info.WriteLine("#set recon_event1: " & reconevent1)
        Info.WriteLine("#set recon_event2: " & reconevent2)
        Info.WriteLine("File2Mod: " & MouseClass)
        Info.WriteLine("Line: " & TSlot)
        Info.WriteLine("")
        Info.WriteLine("Save:")
        Info.WriteLine("#set hotkeyClass: " & hotkeyClass)
        ReconClass = ReconClass.Replace("client-1\", "")
        Info.WriteLine("File2Mod: " & ReconClass)
        If listBox.GetSelected(14) = True Then
            gs_ = gs_ - 1
        End If
        Info.WriteLine("Line: " & gs_)
        Info.WriteLine("")
        Info.Flush()
        Info.Close()

        ' Fill Hacks Array
        hackFile = File.AppendText(HackTemp)
        Dim Length As Integer = File.ReadAllLines(Folder & "\drv2_dispatch.txt").Length
        hackFile.WriteLine(MouseClass & "," & LineJump & "," & Length)
        Length = File.ReadAllLines(Folder & "\drv2_var.txt").Length
        hackFile.WriteLine(MouseClass & "," & TSlot & "," & Length)
        Length = File.ReadAllLines(Folder & "\drv2_save.txt").Length
        hackFile.WriteLine(ReconClass & "," & gs_ & "," & Length)
        hackFile.Flush()
        hackFile.Close()

            Catch
        End Try
        End If
    End Sub
End Class
