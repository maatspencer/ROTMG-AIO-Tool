Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class binReplacements
    ' Odom Style SW
    Public Shared Sub Add()
        If listUtility.isSelected("SW ODOM Style") = True Then
            Try
                currentLine = ""
                count = 1

                Dim Trees As String = Folder & "\client-30.bin"
                Dim NewTrees As String = Folder & "\Trees.bin"

                FILE_NAME = Folder & "\XML.bat"
                Array.Resize(arytext, 4)
                arytext(0) = "cd " & Folder
                arytext(1) = "swfdecompress client.swf"
                arytext(2) = "swfbinexport client.swf"
                arytext(3) = "rabcdasm client-1.abc"
                batchProcess.start(arytext, FILE_NAME)

                Dim Line1 As Integer = 1
                Dim Line2 As Integer = 1
                Dim Line3 As Integer = 1
                Dim Line4 As Integer = 1
                Dim Line5 As Integer = 1
                Dim Line6 As Integer = 1
                Dim Obj As Integer = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Line1 <> 1 And CurrentLine.Contains("</Object>") Then
                            Obj = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("White Sprite Tree") Then
                            Line1 = Count
                        End If


                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    Using sw As StreamWriter = New StreamWriter(NewTrees)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If Count >= Line1 And Count <= Obj Then
                                If CurrentLine.Contains("<Static/>") Then
                                ElseIf CurrentLine.Contains("<Enemy/>") Then
                                ElseIf CurrentLine.Contains("<File>") Then
                                    sw.WriteLine("         <File>lofiChar28x8</File>")
                                ElseIf CurrentLine.Contains("<Index>") Then
                                    sw.WriteLine("         <Index>0x1a</Index>")
                                ElseIf CurrentLine.Contains("<Size>") Then
                                    sw.WriteLine("      <Size>40</Size>")
                                Else
                                    sw.WriteLine(LinetoWrite)
                                End If
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If

                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTrees, Trees, True)
                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Line2 > 1 And CurrentLine.Contains("</Object>") Then
                            Obj = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("Green Sprite Tree") Then
                            Line2 = Count
                        End If


                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    Using sw As StreamWriter = New StreamWriter(NewTrees)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If Count >= Line2 And Count <= Obj Then
                                If CurrentLine.Contains("<Static/>") Then
                                ElseIf CurrentLine.Contains("<Enemy/>") Then
                                ElseIf CurrentLine.Contains("<File>") Then
                                    sw.WriteLine("         <File>lofiChar28x8</File>")
                                ElseIf CurrentLine.Contains("<Index>") Then
                                    sw.WriteLine("         <Index>0x1a</Index>")
                                ElseIf CurrentLine.Contains("<Size>") Then
                                    sw.WriteLine("      <Size>40</Size>")
                                Else
                                    sw.WriteLine(LinetoWrite)
                                End If
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTrees, Trees, True)
                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Line3 > 1 And CurrentLine.Contains("</Object>") Then
                            Obj = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("Yellow Sprite Tree") Then
                            Line3 = Count
                        End If


                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    Using sw As StreamWriter = New StreamWriter(NewTrees)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If Count >= Line3 And Count <= Obj Then
                                If CurrentLine.Contains("<Static/>") Then
                                ElseIf CurrentLine.Contains("<Enemy/>") Then
                                ElseIf CurrentLine.Contains("<File>") Then
                                    sw.WriteLine("         <File>lofiChar28x8</File>")
                                ElseIf CurrentLine.Contains("<Index>") Then
                                    sw.WriteLine("         <Index>0x1a</Index>")
                                ElseIf CurrentLine.Contains("<Size>") Then
                                    sw.WriteLine("      <Size>40</Size>")
                                Else
                                    sw.WriteLine(LinetoWrite)
                                End If
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTrees, Trees, True)
                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Line4 > 1 And CurrentLine.Contains("</Object>") Then
                            Obj = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("Purple Sprite Tree") Then
                            Line4 = Count
                        End If


                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    Using sw As StreamWriter = New StreamWriter(NewTrees)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If Count >= Line4 And Count <= Obj Then
                                If CurrentLine.Contains("<Static/>") Then
                                ElseIf CurrentLine.Contains("<Enemy/>") Then
                                ElseIf CurrentLine.Contains("<File>") Then
                                    sw.WriteLine("         <File>lofiChar28x8</File>")
                                ElseIf CurrentLine.Contains("<Index>") Then
                                    sw.WriteLine("         <Index>0x1a</Index>")
                                ElseIf CurrentLine.Contains("<Size>") Then
                                    sw.WriteLine("      <Size>40</Size>")
                                Else
                                    sw.WriteLine(LinetoWrite)
                                End If
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTrees, Trees, True)
                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Line5 > 1 And CurrentLine.Contains("</Object>") Then
                            Obj = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("Red Sprite Tree") Then
                            Line5 = Count
                        End If


                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    Using sw As StreamWriter = New StreamWriter(NewTrees)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If Count >= Line5 And Count <= Obj Then
                                If CurrentLine.Contains("<Static/>") Then
                                ElseIf CurrentLine.Contains("<Enemy/>") Then
                                ElseIf CurrentLine.Contains("<File>") Then
                                    sw.WriteLine("         <File>lofiChar28x8</File>")
                                ElseIf CurrentLine.Contains("<Index>") Then
                                    sw.WriteLine("         <Index>0x1a</Index>")
                                ElseIf CurrentLine.Contains("<Size>") Then
                                    sw.WriteLine("      <Size>40</Size>")
                                Else
                                    sw.WriteLine(LinetoWrite)
                                End If
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTrees, Trees, True)
                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If Line6 > 1 And CurrentLine.Contains("</Object>") Then
                            Obj = Count
                            Exit Do
                        End If

                        If CurrentLine.Contains("Blue Sprite Tree") Then
                            Line6 = Count
                        End If


                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Trees)
                    Using sw As StreamWriter = New StreamWriter(NewTrees)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If Count >= Line6 And Count <= Obj Then
                                If CurrentLine.Contains("<Static/>") Then
                                ElseIf CurrentLine.Contains("<Enemy/>") Then
                                ElseIf CurrentLine.Contains("<File>") Then
                                    sw.WriteLine("         <File>lofiChar28x8</File>")
                                ElseIf CurrentLine.Contains("<Index>") Then
                                    sw.WriteLine("         <Index>0x1a</Index>")
                                ElseIf CurrentLine.Contains("<Size>") Then
                                    sw.WriteLine("      <Size>40</Size>")
                                Else
                                    sw.WriteLine(LinetoWrite)
                                End If
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTrees, Trees, True)

                Dim Tiles As String = Folder & "\client-27.bin"
                Dim NewTiles As String = Folder & "\NewTiles.bin"
                Dim Check As Boolean = False

                Count = 1

                Using sr As StreamReader = New StreamReader(Tiles)
                    Using sw As StreamWriter = New StreamWriter(NewTiles)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            Dim LinetoWrite = CurrentLine

                            If CurrentLine.Contains("White Alpha Square") Then
                                Check = True
                            End If
                            If Check = True And CurrentLine.Contains("<Index>0x31</Index>") Then
                                sw.WriteLine("         <Index>0x30</Index>")
                            ElseIf Check = True And CurrentLine.Contains("<Index>0x32</Index>") Then
                                sw.WriteLine("         <Index>0x30</Index>")
                            ElseIf Check = True And CurrentLine.Contains("<Index>0x33</Index>") Then
                                sw.WriteLine("         <Index>0x30</Index>")
                            ElseIf Check = True And CurrentLine.Contains("<Index>0x34</Index>") Then
                                sw.WriteLine("         <Index>0x30</Index>")
                            ElseIf Check = True And CurrentLine.Contains("<Index>0x35</Index>") Then
                                sw.WriteLine("         <Index>0x30</Index>")
                            ElseIf Check = True And CurrentLine.Contains("</Animate>") Then
                                ' Do nothing and skip line
                            ElseIf CurrentLine.Contains("0xad") Then
                                sw.WriteLine(CurrentLine)
                                Check = False
                            Else
                                sw.WriteLine(LinetoWrite)
                            End If

                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewTiles, Tiles, True)

                FILE_NAME = Folder & "\XML_Recompile.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "swfbinreplace client.swf 30 client-30.bin"
                arytext(2) = "swfbinreplace client.swf 27 client-27.bin"
                batchProcess.start(arytext, FILE_NAME)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("All Tiles SW Tiles are White and the trees have been replaced with Valentines")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()
            Catch
        End Try
        End If
    End Sub
End Class
