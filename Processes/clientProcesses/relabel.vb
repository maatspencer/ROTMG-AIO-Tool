Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class relabel
    Public Shared Sub client()
        ' Define Cleint Name
        Dim clientName As String = "<font color='#3399FF'>AIO Tool </font>"

        ' Create you findstr batch file
        FILE_NAME = Folder & "\relabel.bat"
        Array.Resize(arytext, 2)
        arytext(0) = "cd " & Folder
        arytext(1) = "findstr /s /m #{VERSION}.{MINOR} *class.asasm >results1.txt"

        ' Run you batch file process
        batchProcess.start(arytext, FILE_NAME)

        ' File to modify read from results1.txt
        myFile = File.ReadAllText(Folder & "\results1.txt")
        MyFile = MyFile.Replace(Environment.NewLine, "")

        ' Reader/Writer Variables
        newFile = Folder & "\Relabel.AIO"
        count = 1
        currentLine = ""
        Dim Line As Integer = 1

        ' Reader/Writer Loop
        Using sr As StreamReader = New StreamReader(Folder & "/" & MyFile)
            CurrentLine = sr.ReadLine
            Do While (Not CurrentLine Is Nothing)

                If CurrentLine.Contains("trait const") Then
                    Line = Count
                End If

                Count = Count + 1
                CurrentLine = sr.ReadLine
            Loop
        End Using

        Count = 1

        Using sr As StreamReader = New StreamReader(Folder & "/" & MyFile)
            Using sw As StreamWriter = New StreamWriter(NewFile)
                CurrentLine = sr.ReadLine
                Do While (Not CurrentLine Is Nothing)
                    Dim LinetoWrite = CurrentLine

                    If Count = Line Then
                        sw.WriteLine(currentLine.Replace(currentLine.Split("(")(5), Quote & clientName & " #{VERSION}.{MINOR}" & Quote & ") end"))
                    Else
                        sw.WriteLine(LinetoWrite)
                    End If

                    Count = Count + 1
                    CurrentLine = sr.ReadLine
                Loop
            End Using
        End Using

        ' Copy Temp File Back in
        My.Computer.FileSystem.CopyFile(newFile, Folder & "/" & myFile, True)
        My.Computer.FileSystem.DeleteFile(Folder & "\results1.txt")
    End Sub
End Class
