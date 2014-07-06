Imports System.Net
Imports System.IO
Imports AIO.hackVariables.Vars

Public Class AddHacks
    ''' Define Public Variable
    Dim swfok As Boolean
    Dim exists As Boolean

    ' Mod information Array
    ' File , Line , Length
    Dim Hacks As Integer = 25

    ' /mods folder
    Public Mods As Integer
    Public Modid() As String
    Public Modfile() As String
    Public CodeLine() As String
    Public Codeadded() As String

    ' Load
    Private Sub Load_Event(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Form Loading Event
        '   Create a Clients folder for the completed client
        labelChange("Initializing...")
        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Clients") Then
        Else
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Clients")
        End If
        Button1.Enabled = False
        Button15.Enabled = False
        swfok = False
        exists = False

        ' Load Previously used SWF
        TextBox2.Text = My.Settings.SWF

        ' Modify items from settings
        ListBox1.Items.Item(3) = "FPS Cap to " & My.Settings.FPS
        ListBox1.Items.Item(6) = "Auto-Nexus(" & My.Settings.Nexus & "%)"

        ' Add Mod Selector hacks to listbox
        LoadMods()

        labelChange("")
    End Sub
    ' File Selector Dialog
    Private Sub File_Selector(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFileName As String
        fd.Title = "Open File Dialog"
        fd.InitialDirectory = Application.StartupPath & "\Clients"
        fd.Filter = "Shockwave Flash Objects (*.swf)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            strFileName = fd.FileName
            If InStr(strFileName, ".swf") <> 0 Then
                TextBox2.Text = strFileName
                swfok = True
            Else
                MessageBox.Show("You have to pick a .swf silly...")
            End If
        End If
    End Sub
    ' Back Button
    Private Sub Back_Button(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        ItemSelect.Show()
        Me.Close()
    End Sub
    ' Check to see if an swf is selected and RABCDasm exists
    Private Sub RABCDASM_Exists(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        ' Check to See if RABCDasm Exists
        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\RABCDasm") Then
            exists = True
            RabLBL.Text = "Yes"
            RabLBL.ForeColor = Color.Black
        Else
            MsgBox("Make sure you have the entire RABCDasm folder in the same directory as the application. Click help if you dont understand!", MsgBoxStyle.Critical)
            RabLBL.ForeColor = Color.Red
            RabLBL.Text = "No"
        End If

        If TextBox2.Text.Contains(".swf") Then
            swfok = True
        End If

        ' Check to see if the file is a .swf
        If swfok = False Then
            MessageBox.Show("You have to pick a .swf silly...")
        End If

        If exists = True And swfok = True Then
            Button15.Enabled = True
        End If
    End Sub
    ' Add Hacks Button
    Private Sub Add_Hacks(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        ' Create a Temporary Folder to build the hacks in
        If TextBox2.Text = "" Then
            MessageBox.Show("You have to pick a .swf silly...")
        Else
            ' Change Cursor
            Me.Cursor = Cursors.WaitCursor

            ' Create Folder and copy over RABCDasm
            labelChange("Creating Directory...")
            If My.Computer.FileSystem.DirectoryExists(Folder) Then
                My.Computer.FileSystem.DeleteDirectory(Folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\RABCDasm", Folder)
            My.Computer.FileSystem.CopyFile(TextBox2.Text, Folder & "\client.swf", True)

            ' Create a batchfile for decompile process
            labelChange("Decompiling Client...")
            decompile.start(Folder)

            ' Get Version Number
            labelChange("Getting the Client Version...")
            version = getVersion.GetVersion

            ' Relabel the client
            labelChange("Naming Client...")
            relabel.client()

            ' Create a new string with all of the Hack lines and files2mod
            labelChange("Building Arrays...")
            File.Create(linesAndFiles).Dispose()

            ' Create a file for Offseting the Mod Seleltor XML's
            File.Create(HackTemp).Dispose()

            ' Add Hacks
            labelChange("Adding Hacks...")
            alliedProj.Add()
            autoNexus.Add()
            binReplacements.Add()
            debuffs.addConfuse()
            debuffs.addDrunk()
            debuffs.addBlind()
            debuffs.addHallucinating()
            debuffs.addUnstable()
            debuffs.addDarkness()
            forceLQ.Add()
            hpBars.Add()
            localHost.Add()
            necroParticles.Add()
            netJitter.Add()
            noLoading.Add()
            noQuestDelay.Add()
            'numericalHpMpFame.Add()
            production.Add()
            reconV3.Add()
            safewalk.Add()
            spamNotifications.Add()
            swSlow.Add()
            swSpeed.Add()
            swTile.Add()

            'Add Selected /mods
            labelChange("Adding Mods...")
            AddMods()

            ' Recompile SWF
            labelChange("Recompiling Client...")
            recompile.start(Folder)

            ' Clean up Directory
            labelChange("Cleaning up files...")
            My.Computer.FileSystem.CopyFile(Folder & "\client.swf", Application.StartupPath & "\Clients\" & version & "_AIOtool.swf", True)
            'Check Debug Mode Setting
            If My.Settings.Debug = False Then
                My.Computer.FileSystem.DeleteDirectory(Folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            ' Display Messages
            labelChange("")
            Me.Cursor = Cursors.Default
            MessageBox.Show("Client is named: " & version & "_AIOtool.swf" & Environment.NewLine & "It contains all the hacks you selected")
            Button1.Enabled = True
            Me.Activate()
        End If
    End Sub
    ' Load Mod Selector Files to Listbox
    Private Sub LoadMods()
        ' Check to see if directory exists
        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\mods") Then
        Else
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\mods")
        End If

        ' Variables
        Dim Folder As String = Application.StartupPath & "/mods"
        Dim AllFiles As String() = Directory.GetFiles(Folder, "*.xml", SearchOption.AllDirectories)
        Dim FilNum As Integer = AllFiles.Length
        Dim Path(FilNum) As String
        Dim Filename(FilNum) As String
        Dim ModCount(FilNum) As Integer

        ' Read All Files
        '' Get names and full paths
        For i = 0 To AllFiles.Length - 1
            Filename(i) = IO.Path.GetFileName(AllFiles(i))
            Path(i) = AllFiles(i).Replace(Filename(i), "")
        Next

        ' Get Number of Mods and mods per
        Dim Currentline As String = ""
        For i = 0 To AllFiles.Length - 1
            Dim Count As Integer = 1
            ModCount(i) = 0
            Using sr As StreamReader = New StreamReader(AllFiles(i))
                Currentline = sr.ReadLine
                Do While (Not Currentline Is Nothing)

                    If Currentline.Contains("<mod") Then
                        ModCount(i) = ModCount(i) + 1
                        Mods = Mods + 1
                    End If


                    Currentline = sr.ReadLine
                    Count = Count + 1
                Loop
            End Using
        Next

        Array.Resize(Modid, Mods)
        Array.Resize(Modfile, Mods)
        Array.Resize(Codeadded, Mods)
        Array.Resize(CodeLine, Mods)

        Dim TempMods As Integer = 0

        For i = 0 To AllFiles.Length - 1
            Dim Count As Integer = 1
            ModCount(i) = 0
            Using sr As StreamReader = New StreamReader(AllFiles(i))
                Currentline = sr.ReadLine
                Do While (Not Currentline Is Nothing)

                    ' Begining of mod
                    If Currentline.Contains("<mod") Then
                        Modid(TempMods) = Currentline.Split("""")(1) ' Get Mod id
                    End If

                    ' Get Files to Mod
                    If Currentline.Contains("file2mod=") Then
                        If Modfile(TempMods) = "" Then
                            Modfile(TempMods) = Currentline.Split(""""c)(1) & "," ' File2mod
                        Else
                            Modfile(TempMods) = Modfile(TempMods) & Currentline.Split(""""c)(1) & ","
                        End If
                    End If

                    ' Get lines to inject at
                    If Currentline.Contains("line=") Then
                        If CodeLine(TempMods) = "" Then
                            CodeLine(TempMods) = Currentline.Split(""""c)(1) & "," ' Line Number
                        Else
                            CodeLine(TempMods) = CodeLine(TempMods) & Currentline.Split(""""c)(1) & ","
                        End If
                    End If

                    ' Get code to inject
                    If Currentline.Contains(".txt") Then
                        If Codeadded(TempMods) = "" Then
                            Codeadded(TempMods) = Path(i) & Currentline.TrimStart & "," ' .txt file to add
                        Else
                            Codeadded(TempMods) = Codeadded(TempMods) & Path(i) & Currentline.TrimStart & ","
                        End If
                    End If

                    ' End of mod
                    If Currentline.Contains("</mod") Then
                        TempMods = TempMods + 1
                    End If


                    Currentline = sr.ReadLine
                    Count = Count + 1
                Loop
            End Using
        Next

        ' Add Mods to Mod Selector
        For i = 0 To Mods - 1
            ListBox1.Items.Add("MOD: " & Modid(i))
        Next

    End Sub
    ' Add selected Mod files
    Private Sub AddMods()
        ' Add Hacks from /mods
        For i = 0 To Mods - 1
            If ListBox1.GetSelected(Hacks + i) = True Then
                Dim Info As StreamWriter
                Info = File.AppendText(LinesAndFiles)
                Info.WriteLine(ListBox1.Items(Hacks + i).ToString & " <-- Added")
                Info.Flush()
                Info.Close()

                Dim Count As Integer = 1
                Dim CurrentLine As String = ""
                Dim Number As Integer = Modfile(i).Split(",").Length - 1

                For k = 0 To Number - 1
                    Dim MyFile As String = Folder & "\client-1\" & Modfile(i).Split(",")(k)
                    Dim NewFile As String = Folder & "\Mod_" & i & k & ".AIO"
                    Dim Line As Integer = Convert.ToInt32(CodeLine(i).Split(",")(k))
                    Dim LineCount As Integer

                    ' Check to see if the same file was modified in previous mods
                    If i > 0 Then
                        If ListBox1.GetSelected(Hacks + i) = True Then
                            For y = 0 To i - 1
                                If ListBox1.GetSelected(Hacks + y) = True Then
                                    For z = 0 To Modfile(y).Split(",").Length - 1
                                        If Folder & "\client-1\" & Modfile(i).Split(",")(k) = Folder & "\client-1\" & Modfile(y).Split(",")(z) Then
                                            If CodeLine(i).Split(",")(k) >= CodeLine(y).Split(",")(z) Then
                                                LineCount = File.ReadAllLines(Codeadded(y).Split(",")(z)).Length
                                                Line = Line + LineCount
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If

                    ' Check to see if same file was modified in an auto update
                    If ListBox1.GetSelected(Hacks + i) = True Then
                        Using sr As StreamReader = New StreamReader(HackTemp)
                            CurrentLine = sr.ReadLine
                            Do While (Not CurrentLine Is Nothing)

                                CurrentLine = CurrentLine.Replace("\", "/")
                                If CurrentLine.Split(",")(0) = Modfile(i).Split(",")(k) Then
                                    If Line >= CurrentLine.Split(",")(1) Then
                                        Line = Line + CurrentLine.Split(",")(2)
                                    End If
                                End If
                                CurrentLine = sr.ReadLine
                            Loop
                        End Using
                    End If

                    ' Check to see if the current file was modified in the current mod already
                    If k >= 1 Then
                        For x = 0 To k - 1
                            If Folder & "\client-1\" & Modfile(i).Split(",")(k) = Folder & "\client-1\" & Modfile(i).Split(",")(x) Then
                                If CodeLine(i).Split(",")(k) >= CodeLine(i).Split(",")(x) Then
                                    LineCount = File.ReadAllLines(Codeadded(i).Split(",")(x)).Length
                                    Line = Line + LineCount
                                End If
                            End If
                        Next
                    End If
                    Count = 1
                    Using sr As StreamReader = New StreamReader(MyFile)
                        Using sw As StreamWriter = New StreamWriter(NewFile)
                            CurrentLine = sr.ReadLine
                            Do While (Not CurrentLine Is Nothing)
                                Dim LinetoWrite = CurrentLine

                                If Count = Line + 1 Then
                                    sw.WriteLine(File.ReadAllText(Codeadded(i).Split(",")(k)))
                                End If

                                sw.WriteLine(LinetoWrite)
                                Count = Count + 1
                                CurrentLine = sr.ReadLine
                            Loop
                        End Using
                    End Using
                    My.Computer.FileSystem.CopyFile(NewFile, MyFile, True)
                Next
            End If
        Next

        Dim Delete As String() = Directory.GetFiles(Folder, "*.class.asasm", SearchOption.TopDirectoryOnly)
        For i = 0 To Delete.Length - 1
            My.Computer.FileSystem.DeleteFile(Delete(i))
        Next
    End Sub
    'Show Code Updates
    Private Sub Code_Updates(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Process.Start(LinesAndFiles)
    End Sub
    ' Select All
    Private Sub SelectAll(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim ListboxCount = Hacks + Mods
        For i = 0 To ListboxCount - 1
            ListBox1.SetSelected(i, True)
        Next
    End Sub
    ' Select None
    Private Sub SelectNone(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim ListboxCount = Hacks + Mods
        For i = 0 To ListboxCount - 1
            ListBox1.SetSelected(i, False)
        Next
    End Sub
    ' Form Closing Event
    Private Sub AddHacks_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.SWF = TextBox2.Text
        My.Settings.Save()
    End Sub
    ' Setting Form
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        HackSettings.Show()
        Me.Close()
    End Sub
    ' Grab the Latest Client
    Private Sub latest(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim clientVersion As String = getLatest.Grab()
        TextBox2.Text = Application.StartupPath & "\Clients\Fresh_Client_" & clientVersion & ".swf"
    End Sub
    ' Change Label
    Private Sub labelChange(text As String)
        Label2.Text = text
        Application.DoEvents()
    End Sub
End Class