Imports System
Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Threading.Tasks

Public Class deobfuscate
    ' Remove Junk Code
    Public Shared Sub Deobf(location As String)
        ' Create an Array containing all of the files in the */client-1 directory
        Dim Classes As String() = Directory.GetFiles(location, "*.class.asasm", SearchOption.AllDirectories)
        Dim Processed As Integer = 0
        Dim Total As Integer = Classes.Length
        Parallel.For(0, Classes.Length, Sub(i)
                                            ' Loop Through Every Single file from top to bottom
                                            ' Create a Streamreader to read through each file

                                            '' Path to current file in loop
                                            Dim MyFile As String = Classes(i)

                                            '' Create Dummy file to be filed, copied in, and deleted
                                            Dim NewFile As String = Path.GetDirectoryName(MyFile) & "/Temp" & i & ".class.asasm"

                                            '' Loop Variables
                                            Dim CurrentLine As String = ""
                                            Dim LinetoWrite As String = ""
                                            Dim Count As Integer = 0

                                            '' Deobf Logic Variables
                                            Dim BoolArray(2) As String
                                            Dim LocalArray(2) As String
                                            Dim InCodeBlock As Boolean = False
                                            Dim TwoSetLocals As Integer = 0
                                            Dim HitaLocal As Boolean = False
                                            Dim LocalHit As Integer = 0
                                            Dim JumpLine As String = "EMPTY"

                                            Dim RndFile As String = Path.GetDirectoryName(MyFile) & "/RndFile" & i & ".class.asasm"
                                            Dim RndWriter As StreamWriter
                                            Dim RndJump As Boolean = False
                                            Dim RndLine As String = "EMPTY"

                                            '' Readerwriter Combo to read and write lines as it goes

                                            '' Loop 1: Check for Random Jumps
                                            Using sr As StreamReader = New StreamReader(MyFile)
                                                Using sw As StreamWriter = New StreamWriter(NewFile, False)
                                                    CurrentLine = sr.ReadLine
                                                    Do While (Not CurrentLine Is Nothing)
                                                        LinetoWrite = CurrentLine

                                                        If CurrentLine.Contains(" code") Then
                                                            ' Begining of Code Body
                                                            If CurrentLine.Contains("tracking") Then
                                                                sw.WriteLine(LinetoWrite)

                                                            ElseIf Not CurrentLine.Contains("end") Then
                                                                sw.WriteLine(LinetoWrite)
                                                                InCodeBlock = True
                                                                Count = 0

                                                                ' End of Code Block
                                                            Else
                                                                InCodeBlock = False
                                                                If RndJump = True Then
                                                                    RndWriter.Close()
                                                                    sw.WriteLine(File.ReadAllText(RndFile))
                                                                    RndLine = "EMPTY"
                                                                    RndJump = False
                                                                End If
                                                                sw.WriteLine(LinetoWrite)
                                                            End If

                                                            ' Check to See if you are in a code block
                                                        ElseIf InCodeBlock = True Then
                                                            ' Check to see if you are in a jump segment
                                                            If RndJump = True Then
                                                                ' Check to see if your jumpline is hit
                                                                '' Check first to see if you made it all the way to the jump without hitting another LXX:
                                                                If CurrentLine.Contains(RndLine) Then
                                                                    RndJump = False
                                                                    RndLine = "EMPTY"
                                                                    RndWriter.Close()
                                                                    sw.WriteLine(LinetoWrite)

                                                                    '' Check to see if the jump is not junk
                                                                ElseIf CurrentLine.Contains(":") Then
                                                                    RndWriter.WriteLine(CurrentLine)
                                                                    RndWriter.Close()
                                                                    sw.WriteLine(File.ReadAllText(RndFile))
                                                                    RndLine = "EMPTY"
                                                                    RndJump = False

                                                                    '' Check for sequential real/fake jumps
                                                                ElseIf CurrentLine.Contains("jump") Then
                                                                    RndWriter.Close()
                                                                    sw.WriteLine(File.ReadAllText(RndFile))
                                                                    RndLine = "L" & CurrentLine.Split("L")(1) & ":"
                                                                    RndWriter = New StreamWriter(RndFile, False)
                                                                    RndWriter.WriteLine(CurrentLine)
                                                                Else
                                                                    '' If neither continue to fill your streamwriter just in case
                                                                    RndWriter.WriteLine(CurrentLine)
                                                                End If


                                                                ' Check to see if a new jump segment is initiated	
                                                            ElseIf CurrentLine.Contains("jump") Then
                                                                RndJump = True
                                                                RndLine = "L" & CurrentLine.Split("L")(1) & ":"
                                                                RndWriter = New StreamWriter(RndFile, False)
                                                                RndWriter.WriteLine(CurrentLine)

                                                                ' Not in a jump Segment or starting a new one
                                                            Else
                                                                sw.WriteLine(LinetoWrite)
                                                            End If

                                                        Else
                                                            ' Write Currentline back if you are not in a code block
                                                            sw.WriteLine(LinetoWrite)
                                                        End If

                                                        Count = Count + 1
                                                        CurrentLine = sr.ReadLine
                                                    Loop
                                                End Using
                                            End Using

                                            ' Copy File Back into client-1 Directory
                                            My.Computer.FileSystem.DeleteFile(MyFile)
                                            My.Computer.FileSystem.RenameFile(NewFile, Path.GetFileNameWithoutExtension(MyFile) & ".asasm")

                                            Count = 0

                                            '' Loop 2: all other deobfuscation
                                            Using sr As StreamReader = New StreamReader(MyFile)
                                                Using sw As StreamWriter = New StreamWriter(NewFile, False)
                                                    CurrentLine = sr.ReadLine
                                                    Do While (Not CurrentLine Is Nothing)
                                                        LinetoWrite = CurrentLine

                                                        If CurrentLine.Contains(" code") Then
                                                            ' Begining of Code Body
                                                            If CurrentLine.Contains("tracking") Then
                                                                sw.WriteLine(LinetoWrite)
                                                            ElseIf Not CurrentLine.Contains("end") Then
                                                                sw.WriteLine(LinetoWrite)
                                                                InCodeBlock = True
                                                                ' Reset Count
                                                                Count = 0
                                                                TwoSetLocals = 0

                                                                ' End of Code Block
                                                            Else
                                                                InCodeBlock = False
                                                            End If
                                                        End If

                                                        ' Check to See if you are in a code block
                                                        If InCodeBlock = True Then
                                                            ' Write first two line of each code block to the Boolean Array
                                                            If Count = 1 Then
                                                                If Not CurrentLine.Contains("push") Then
                                                                    InCodeBlock = False
                                                                    sw.WriteLine(LinetoWrite)
                                                                Else
                                                                    BoolArray(0) = CurrentLine
                                                                End If

                                                            ElseIf Count = 2 Then
                                                                BoolArray(1) = CurrentLine

                                                            ElseIf Not TwoSetLocals = 2 Then
                                                                ' Check for a swap
                                                                If CurrentLine.Contains("swap") Then
                                                                    Dim Temp As String = BoolArray(0)
                                                                    BoolArray(0) = BoolArray(1)
                                                                    BoolArray(1) = Temp
                                                                End If
                                                                ' Write first two set locals to an array
                                                                If CurrentLine.Contains("set") Then
                                                                    TwoSetLocals = TwoSetLocals + 1
                                                                    LocalArray(TwoSetLocals - 1) = CurrentLine.Replace("set", "get")
                                                                End If

                                                            ElseIf HitaLocal = True Then
                                                                ' Check to see if its an iffalse jump
                                                                If CurrentLine.Contains("iffalse") Then
                                                                    ' Check for a match
                                                                    If BoolArray(LocalHit).Contains("false") Then
                                                                        CurrentLine.Replace("false", "")
                                                                        JumpLine = "L" & CurrentLine.Split("L")(1) & ":"
                                                                    Else
                                                                        JumpLine = "L" & CurrentLine.Split("L")(1) & ":"
                                                                        HitaLocal = False
                                                                    End If
                                                                End If
                                                                ' Check to see if its an iffalse jump
                                                                If CurrentLine.Contains("iftrue") Then
                                                                    ' Check for a match
                                                                    If BoolArray(LocalHit).Contains("true") Then
                                                                        JumpLine = "L" & CurrentLine.Split("L")(1) & ":"
                                                                    Else
                                                                        JumpLine = "L" & CurrentLine.Split("L")(1) & ":"
                                                                        HitaLocal = False
                                                                    End If
                                                                End If

                                                                'Check to see if first register is checked against
                                                            ElseIf CurrentLine.Contains(LocalArray(0)) Then
                                                                HitaLocal = True
                                                                LocalHit = 1

                                                                ' Check to see if Second Register is checked against
                                                            ElseIf CurrentLine.Contains(LocalArray(1)) Then
                                                                HitaLocal = True
                                                                LocalHit = 0

                                                                ' Check for label from past if*** check
                                                            ElseIf CurrentLine.Contains(JumpLine) Then
                                                                HitaLocal = False
                                                                sw.WriteLine(LinetoWrite)

                                                                ' Write Good Code
                                                            Else
                                                                sw.WriteLine(LinetoWrite)
                                                            End If

                                                        Else
                                                            ' Write Currentline back if you are not in a code block
                                                            sw.WriteLine(LinetoWrite)
                                                        End If

                                                        Count = Count + 1
                                                        CurrentLine = sr.ReadLine
                                                    Loop
                                                End Using
                                            End Using
                                            ' Copy File Back into client-1 Directory
                                            My.Computer.FileSystem.DeleteFile(MyFile)
                                            My.Computer.FileSystem.RenameFile(NewFile, Path.GetFileNameWithoutExtension(MyFile) & ".asasm")
                                        End Sub)

        ' Recompile to remove extra lines and labels
        recompile.start(location)

        ' Clean up files
        My.Computer.FileSystem.DeleteDirectory(location & "/client-1", FileIO.DeleteDirectoryOption.DeleteAllContents)
        My.Computer.FileSystem.DeleteFile(location & "/client-1.abc")


        ' Decompile Again
        decompile.start(location)
    End Sub
End Class
