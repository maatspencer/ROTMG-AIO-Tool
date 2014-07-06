Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class localHost
    ' Add Local Host to Server List
    Public Shared Sub Add()
        If listUtility.isSelected("Add Proxy Server") = True Then
            Try
                ' #set fullfunctionname
                FILE_NAME = Folder & "\FullFunction.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "Servers" & Quote & " *class.asasm >results20.txt"
                arytext(2) = "findstr /m /f:results20.txt " & Quote & "setIsAdminOnly" & Quote & " *class.asasm >results21.txt"
                batchProcess.start(arytext, FILE_NAME)

                Dim FullFunction As String = File.ReadAllText(Folder & "\results21.txt")
                FullFunction = FullFunction.Replace(Environment.NewLine, "")
                FullFunction = FullFunction.Split("\")(1) & ":" & FullFunction.Split("\")(2).Split(".")(0) & "/LocalhostServer"

                ' #set PortalNS
                FILE_NAME = Folder & "\PortalNS.bat"
                Array.Resize(arytext, 3)
                arytext(0) = "cd " & Folder
                arytext(1) = "findstr /s /m " & Quote & "setName" & Quote & " *class.asasm >results22.txt"
                arytext(2) = "findstr /m /f:results22.txt :Server *class.asasm >results23.txt"
                batchProcess.start(arytext, FILE_NAME)

                Dim PortalNS As String = File.ReadAllText(Folder & "\results23.txt")
                PortalNS = PortalNS.Replace(Environment.NewLine, "")
                PortalNS = PortalNS.Split("\")(1)

                ' #set portProperty
                count = 1
                currentLine = ""
                myFile = Folder & "\client-1\com\company\assembleegameclient\parameters\Parameters.class.asasm"
                Dim Bool As Boolean = False
                Dim portProperty As String = ""

                Using sr As StreamReader = New StreamReader(MyFile)
                    Currentline = sr.ReadLine
                    Do While (Not Currentline Is Nothing)

                        If Bool = True Then
                            portProperty = Currentline
                            Exit Do
                        End If

                        If Currentline.Contains("2050") Then
                            Bool = True
                        End If

                        Currentline = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                portProperty = portProperty.Split(Quote)(5)

                ' Replace Function.txt
                Dim ProxyFunction As String = File.ReadAllText(Folder & "\proxyServer_func.txt")
                ProxyFunction = ProxyFunction.Replace("CHANGE1", FullFunction)
                ProxyFunction = ProxyFunction.Replace("CHANGE2", PortalNS)
                ProxyFunction = ProxyFunction.Replace("CHANGE3", portProperty)

                ' Inject Function.txt
                MyFile = File.ReadAllText(Folder & "\results21.txt")
                MyFile = MyFile.Replace(Environment.NewLine, "")
                newFile = Folder & "/ProxyServer.AIO"
                Dim Trait As Integer = 0

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    Currentline = sr.ReadLine
                    Do While (Not Currentline Is Nothing)

                        If Currentline.Contains("end ; trait") Then
                            Trait = Count
                        End If

                        Currentline = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        Currentline = sr.ReadLine
                        Do While (Not Currentline Is Nothing)
                            lineToWrite = currentLine

                            If Count = Trait + 1 Then
                                sw.WriteLine(ProxyFunction)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            Currentline = sr.ReadLine
                        Loop
                    End Using
                End Using

                My.Computer.FileSystem.CopyFile(NewFile, Folder & "\" & MyFile, True)


                ' Inject Add.txt
                Count = 1
                Dim ProxyAdd As String = File.ReadAllText(Folder & "\proxyServer_add.txt")
                Dim Kill As Integer = 0

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    Currentline = sr.ReadLine
                    Do While (Not Currentline Is Nothing)

                        If Currentline.Contains("kill") Then
                            Kill = Count
                        End If

                        Currentline = sr.ReadLine
                        Count = Count + 1
                    Loop
                End Using

                Count = 1

                Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
                    Using sw As StreamWriter = New StreamWriter(NewFile)
                        Currentline = sr.ReadLine
                        Do While (Not Currentline Is Nothing)
                            lineToWrite = currentLine

                            If Count = Kill + 1 Then
                                sw.WriteLine(ProxyAdd)
                            End If

                            sw.WriteLine(LinetoWrite)
                            Count = Count + 1
                            Currentline = sr.ReadLine
                        Loop
                    End Using
                End Using

                ' Check Success
                    My.Computer.FileSystem.CopyFile(NewFile, Folder & "\" & MyFile, True)


                    Dim Info As StreamWriter
                    Info = File.AppendText(linesAndFiles)
                    Info.WriteLine("Add Local Host to Server List:")
                    Info.WriteLine("")
                    Info.WriteLine("Function:")
                    Info.WriteLine("#set fullfunctionname: " & FullFunction)
                    Info.WriteLine("#set ServerNS: " & PortalNS)
                    Info.WriteLine("#set portProperty: " & portProperty)
                    MyFile = MyFile.Replace("client-1\", "")
                    Info.WriteLine("File2mod: " & MyFile)
                    Info.WriteLine("Line: " & Trait)
                    Info.WriteLine("")
                    Info.WriteLine("Add:")
                    Info.WriteLine("File2mod: " & MyFile)
                    Info.WriteLine("Line: " & Kill)
                    Info.WriteLine("")
                    Info.Flush()
                    Info.Close()

                    ' Fill Hacks Array
                    hackFile = File.AppendText(HackTemp)
                    Dim Length As Integer = File.ReadAllLines(Folder & "\proxyServer_func.txt").Length
                    hackFile.WriteLine(MyFile & "," & Trait & "," & Length)
                    Length = File.ReadAllLines(Folder & "\proxyServer_add.txt").Length
                    hackFile.WriteLine(MyFile & "," & Kill & "," & Length)
                    hackFile.Flush()
                    hackFile.Close()
            Catch
            End Try
        End If
    End Sub
End Class
