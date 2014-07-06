Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class packetClasses
    Public Shared Sub find(location As String)
        'Find the smaller of the two packet classes
        If System.IO.File.ReadAllLines(location & "\results3.txt")(0).Length > System.IO.File.ReadAllLines(location & "\results3.txt")(1).Length Then
            myFile = System.IO.File.ReadAllLines(location & "\results3.txt")(0)
        Else
            myFile = System.IO.File.ReadAllLines(location & "\results3.txt")(1)
        End If

        ' Variables
        myFile = myFile.Replace(Environment.NewLine, "")
        count = 0
        currentLine = ""


        Dim firstHit As Boolean = False
        Dim packetObf() As String = File.ReadAllLines(location & "\packetClasses.txt")
        Dim packetType() As String = File.ReadAllLines(location & "\packetID.txt")
        Dim name As String = ""
        Dim unknownCounter As Integer = 0
        Dim switch As Integer = 0

        ' Move Packet Classes to a new location
        Using sr As StreamReader = New StreamReader(location & "\" & myFile)
            currentLine = sr.ReadLine
            Do While (Not currentLine Is Nothing)

                If switch = 1 Then
                    If currentLine.Contains("getlex") Then
                        switch = 0
                        Dim fileName As String = currentLine.Split(Quote)(1) & "/" & currentLine.Split(Quote)(3) & ".class.asasm"
                        If Not name = "UNKNOWN" Then
                            My.Computer.FileSystem.MoveFile(location & "/client-1/" & fileName, Application.StartupPath & "/temp/" & name & "/fullClass.class.asasm", True)
                            ' Write out a file with just the Trait Slots
                            traitSlots(name)
                        Else
                            My.Computer.FileSystem.MoveFile(location & "/client-1/" & fileName, Application.StartupPath & "/temp/" & name & "/UNKNOWN" & unknownCounter & ".class.asasm", True)
                        End If
                        count += 1
                        If count = File.ReadAllLines(location & "/packetID.txt").Length - 2 Then
                            Exit Do
                        End If
                    End If
                Else
                    If firstHit = True Then
                        If currentLine.Contains("getlex") Then
                            switch = 1
                            If currentLine.Contains("#0") Then
                                name = currentLine.Split(Quote)(5)
                            Else
                                name = currentLine.Split(Quote)(3)
                            End If
                            For i = 0 To packetObf.Length - 1
                                If packetObf(i) = name Then
                                    name = packetType(i).Split("=")(0)
                                    If name.Contains("-") Then
                                        name = "UNKNOWN"
                                        unknownCounter += 1
                                    End If
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                If firstHit = False Then
                    If currentLine.Contains("CREATE") Then
                        firstHit = True
                        switch = 1
                        name = "CREATE"
                    End If
                End If

                currentLine = sr.ReadLine
            Loop
        End Using
    End Sub

    Private Shared Sub traitSlots(name As String)
        Using sr As StreamReader = New StreamReader(Application.StartupPath & "/temp/" & name & "/fullClass.class.asasm")
            Using sw As StreamWriter = New StreamWriter(Application.StartupPath & "/temp/" & name & "/traitSlots.class.asasm", False)
                currentLine = sr.ReadLine
                Do While (Not currentLine Is Nothing)
                    If currentLine.Contains("trait slot") Then
                        sw.WriteLine(currentLine)
                    End If
                    currentLine = sr.ReadLine
                Loop
            End Using
        End Using
    End Sub
End Class
