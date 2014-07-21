Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class friendList
    ' FPS Cap to 60
    Public Shared Sub Add()
        If listUtility.isSelected("FPS Cap") = True Then
            Try
                myFile = Folder & "\client-1\WebMain.class.asasm"
                newFile = Folder & "\60fps.AIO"
                count = 1
                currentLine = ""
                Dim forcelq As String = File.ReadAllText(Folder & "\60fps.txt")
                forcelq = forcelq.Replace("CHANGE1", My.Settings.FPS)
                Dim Setup As Integer = 0
                Dim RetVoid As Integer = 0

                Using sr As StreamReader = New StreamReader(myFile)
                    currentLine = sr.ReadLine
                    Do While (Not currentLine Is Nothing)

                        If currentLine.Contains("/setup") Then
                            Setup = count
                        End If

                        If currentLine.Contains("returnvoid") Then
                            If Setup <> 0 Then
                                RetVoid = count
                                Exit Do
                            End If
                        End If

                        currentLine = sr.ReadLine
                        count = count + 1
                    Loop
                End Using

                count = 1

                Using sr As StreamReader = New StreamReader(myFile)
                    Using sw As StreamWriter = New StreamWriter(newFile)
                        currentLine = sr.ReadLine
                        Do While (Not currentLine Is Nothing)
                            lineToWrite = currentLine

                            If count = RetVoid - 1 Then
                                sw.WriteLine(forcelq)
                            End If

                            sw.WriteLine(lineToWrite)
                            count = count + 1
                            currentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(newFile, myFile, True)


                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("Change FPS Cap to 60fps:")
                If listBox.GetSelected(2) = True Then
                    RetVoid = RetVoid - 4
                End If
                Info.WriteLine("Line: " & RetVoid - 1)
                Info.WriteLine("WebMain.class.asasm")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = File.ReadAllLines(Folder & "\60fps.txt").Length
                hackFile.WriteLine("WebMain.class.asasm" & "," & RetVoid - 1 & "," & Length)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
End Class

