Imports System
Imports System.IO
Imports AIO.hackVariables.Vars
Public Class packetIDS
    ' Get Packet ID's
    Public Shared Sub mapPackets(location As String)
        ' findstr CMD for packet class
        FILE_NAME = location & "\mapPackets.bat"
        Array.Resize(arytext, 4)
        arytext(0) = "cd " & location
        arytext(1) = "findstr /s /m " & Quote & "slotid 75" & Quote & " *class.asasm >results1.txt"
        arytext(2) = "findstr /m /f:results1.txt " & Quote & "chooseName" & Quote & " *class.asasm >results2.txt"
        arytext(3) = "findstr /m /f:results2.txt " & Quote & "keyTime_" & Quote & " *class.asasm >results3.txt"
        batchProcess.start(arytext, FILE_NAME)

        'Find the smaller of the two packet classes
        Try
            If System.IO.File.ReadAllLines(location & "\results3.txt")(0).Length > System.IO.File.ReadAllLines(location & "\results3.txt")(1).Length Then
                myFile = System.IO.File.ReadAllLines(location & "\results3.txt")(1)
            Else
                myFile = System.IO.File.ReadAllLines(location & "\results3.txt")(0)
            End If

            ' Variables
            myFile = myFile.Replace(Environment.NewLine, "")
            newFile = location & "\packetID.txt"
            Dim classesFile As String = location & "\packetClasses.txt"
            count = 1
            currentLine = ""
            Dim writer As StreamWriter = New StreamWriter(newFile, False)
            Dim packetClasses As StreamWriter = New StreamWriter(classesFile, False)

            ' unnamed packets
            Dim tick As Integer = 0
            Dim tickArray(6) As String
            tickArray(0) = "SHOOT2"
            tickArray(1) = "UPDATEACK"
            tickArray(2) = "NEW_TICK"
            tickArray(3) = "SHOW_EFFECT"
            tickArray(4) = "CREATEGUILDRESULT"
            tickArray(5) = "SHOOT"
            tickArray(6) = "FILE"

            ' Write the Packets to a Text File and array list
            Using sr As StreamReader = New StreamReader(location & "\" & myFile)
                currentLine = sr.ReadLine
                Do While (Not currentLine Is Nothing)

                    If currentLine.Contains("slotid") And currentLine.Contains("Integer") Then
                        Dim name As String = currentLine.Split(Quote)(5)
                        Dim getlex As String = name
                        Dim id As String = currentLine.Split("(")(5).Replace(") end", "")
                        If name.Contains("-") And tick <> 7 Then
                            name = tickArray(tick)
                            tick += 1
                        End If

                        writer.WriteLine(name & "=" & id)
                        packetClasses.WriteLine(getlex)
                    End If

                    currentLine = sr.ReadLine
                    count = count + 1
                Loop
            End Using

            writer.Flush()
            writer.Close()
            packetClasses.Flush()
            packetClasses.Close()

            Dim checkLength As String = File.ReadAllLines(newFile)(2)
        Catch
            If System.IO.File.ReadAllLines(location & "\results3.txt")(0).Length > System.IO.File.ReadAllLines(location & "\results3.txt")(1).Length Then
                myFile = System.IO.File.ReadAllLines(location & "\results3.txt")(0)
            Else
                myFile = System.IO.File.ReadAllLines(location & "\results3.txt")(1)
            End If

            ' Variables
            myFile = myFile.Replace(Environment.NewLine, "")
            newFile = location & "\packetID.txt"
            Dim classesFile As String = location & "\packetClasses.txt"
            count = 1
            currentLine = ""
            Dim writer As StreamWriter = New StreamWriter(newFile, False)
            Dim packetClasses As StreamWriter = New StreamWriter(classesFile, False)

            ' unnamed packets
            Dim tick As Integer = 0
            Dim tickArray(6) As String
            tickArray(0) = "SHOOT2"
            tickArray(1) = "UPDATEACK"
            tickArray(2) = "NEW_TICK"
            tickArray(3) = "SHOW_EFFECT"
            tickArray(4) = "CREATEGUILDRESULT"
            tickArray(5) = "SHOOT"
            tickArray(6) = "FILE"

            ' Write the Packets to a Text File and array list
            Using sr As StreamReader = New StreamReader(location & "\" & myFile)
                currentLine = sr.ReadLine
                Do While (Not currentLine Is Nothing)

                    If currentLine.Contains("slotid") And currentLine.Contains("Integer") Then
                        Dim name As String = currentLine.Split(Quote)(5)
                        Dim getlex As String = name
                        Dim id As String = currentLine.Split("(")(5).Replace(") end", "")
                        If name.Contains("-") And tick <> 7 Then
                            name = tickArray(tick)
                            tick += 1
                        End If

                        writer.WriteLine(name & "=" & id)
                        packetClasses.WriteLine(getlex)
                    End If

                    currentLine = sr.ReadLine
                    count = count + 1
                Loop
            End Using

            writer.Flush()
            writer.Close()
            packetClasses.Flush()
            packetClasses.Close()
        End Try
    End Sub
    ' Format Packets
    Public Shared Sub formatPackets(location As String)
        ' Check to see if formatting is used
        Dim Setting As ArrayList = My.Settings.proxyUtilText
        If Setting.Count <> 8 Then
            Exit Sub
        End If

        ' Variables
        myFile = location & "\packetID.txt"
        newFile = location & "\packetTemp.txt"
        count = 1
        currentLine = ""
        Dim writer As StreamWriter = New StreamWriter(newFile, False)

        ' Write Header
        If Setting(1) <> "" Then
            writer.WriteLine(Setting(1))
        End If
        If Setting(2) <> "" Then
            writer.WriteLine(Setting(2))
        End If


        ' Write the Packets to a Text File
        Using sr As StreamReader = New StreamReader(myFile)
            currentLine = sr.ReadLine
            Do While (Not currentLine Is Nothing)
                Dim name As String = currentLine.Split("=")(0)
                Dim id As String = currentLine.Split("=")(1)
                writer.WriteLine(Setting(3).ToString & name & Setting(4).ToString & id & Setting(5).ToString)

                currentLine = sr.ReadLine
                count = count + 1
            Loop
        End Using

        'Write Footer
        If Setting(6) <> "" Then
            writer.WriteLine(Setting(6))
        End If
        If Setting(7) <> "" Then
            writer.WriteLine(Setting(7))
        End If

        writer.Flush()
        writer.Close()

        ' Rename File
        My.Computer.FileSystem.RenameFile(newFile, Setting(0).ToString)

        ' Delete old file
        My.Computer.FileSystem.DeleteFile(myFile)
    End Sub
End Class
