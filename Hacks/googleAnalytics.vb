Imports System
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class googleAnalytics
    ' Disable all requests to Google Analytics
    ' MuhFreedom
    Public Shared Sub Add()
        If listUtility.isSelected("Disable Google Analytics") = True Then
            Try
                myFile = Folder & "\client-1\com\google\analytics\GATracker.class.asasm"
                newFile = Folder & "\googleAnalytics.AIO"
                count = 1
                currentLine = ""

                Dim trackEvent As Boolean = False
                Dim trackEventLine As Integer

                Dim trackPageView As Boolean = False
                Dim trackPageViewLine As Integer

                count = 1

                Using sr As StreamReader = New StreamReader(myFile)
                    Using sw As StreamWriter = New StreamWriter(newFile)
                        currentLine = sr.ReadLine
                        Do While (Not currentLine Is Nothing)
                            lineToWrite = currentLine
                            If trackEvent = True Then
                                If currentLine.Contains("getlocal0") Then
                                    sw.WriteLine("pushfalse")
                                    sw.WriteLine("returnvalue")
                                    trackEventLine = count
                                    trackEvent = False
                                End If
                            End If

                            If currentLine.Contains("/trackEvent") Then
                                trackEvent = True
                            End If

                            If trackPageView = True Then
                                If currentLine.Contains("getlocal0") Then
                                    sw.WriteLine("returnvoid")
                                    trackPageViewLine = count
                                    trackPageView = False
                                End If
                            End If

                            If currentLine.Contains("/trackPageview") Then
                                trackPageView = True
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
                Info.WriteLine("Disable Google Analytics:")
                Info.WriteLine("Line: " & trackEventLine)
                Info.WriteLine("Line: " & trackPageViewLine)
                Info.WriteLine("client-1\com\google\analytics\GATracker.class.asasm")
                Info.WriteLine("")
                Info.Flush()
                Info.Close()

                ' Fill Hacks Array
                hackFile = File.AppendText(HackTemp)
                hackFile.WriteLine("client-1\com\google\analytics\GATracker.class.asasm" & "," & trackEventLine & "," & 2)
                hackFile.WriteLine("client-1\com\google\analytics\GATracker.class.asasm" & "," & trackPageView & "," & 1)
                hackFile.Flush()
                hackFile.Close()

            Catch
            End Try
        End If
    End Sub
End Class

