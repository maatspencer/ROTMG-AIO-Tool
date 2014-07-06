Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class necroParticles
    ' Disable Necromancer Particles
    Public Shared Sub Add()
        If listUtility.isSelected("Disable Necromancer Particles") = True Then
            Try
                FILE_NAME = Folder & "\NoLagro.bat"
                Array.Resize(arytext, 2)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "serverName" & Quote & " *class.asasm >results4.txt"
                batchProcess.start(arytext, FILE_NAME)

                myFile = File.ReadAllText(Folder & "\results4.txt")
                MyFile = MyFile.Replace(Environment.NewLine, "")
                newFile = Folder & "\NoLagro.AIO"
                count = 1
                currentLine = ""
                Dim Label As Integer = 0

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    CurrentLine = sr.ReadLine
                    Do While (Not CurrentLine Is Nothing)

                        If CurrentLine.Contains("FlowEffect") Then
                            Exit Do
                        End If

                        If CurrentLine.Contains("label") Then
                            Label = Count
                        End If

                        CurrentLine = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using
                Count = 1
                Using sr As StreamReader = New StreamReader(Folder & "/" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        CurrentLine = sr.ReadLine
                        Do While (Not CurrentLine Is Nothing)
                            lineToWrite = currentLine

                            If Count = Label + 1 Then
                                sw.WriteLine("      returnvoid")
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            CurrentLine = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, Folder & "/" & MyFile, True)

                Dim Info As StreamWriter
                Info = File.AppendText(linesAndFiles)
                Info.WriteLine("No Necromancer Particles:")
                Info.WriteLine("Line: " & Label)
                MyFile = MyFile.Replace("client-1\", "")
                Info.WriteLine(MyFile)
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                Dim Length As Integer = 1
                hackFile.WriteLine(MyFile & "," & Label & "," & Length)
                hackFile.Flush()
                hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
