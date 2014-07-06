Imports System.Net
Imports System
Imports System.IO
Imports System.Text.RegularExpressions

Public Class Coding
    ' Load Event
    Private Sub Form4_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ItemSelect.Close()
        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Clients") Then
        Else
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Clients")
        End If

        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Coding") Then
            My.Computer.FileSystem.MoveDirectory(Application.StartupPath & "\Coding", Application.StartupPath & "\Archive\Coding\" & hackVariables.Vars.today & "_" & DateAndTime.Now.Hour & "." & DateAndTime.Now.Minute)
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Coding")
        Else
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Coding")
        End If

        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Using wc As New WebClient()
            Dim version = wc.DownloadString("http://www.realmofthemadgod.com/version.txt")
            Dim swf = "http://www.realmofthemadgod.com/AssembleeGameClient" + version + ".swf"
            wc.DownloadFile(swf, Application.StartupPath & "\Clients\Fresh_Client_" & version & ".swf")
            MessageBox.Show("You now have a current assembly game client!")
        End Using
    End Sub
    ' Decompile
    Private Sub decompileSWF(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Me.Cursor = Cursors.WaitCursor
        Dim Folder = Application.StartupPath & "/Coding"
        My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\RABCDasm", Folder, True)
        My.Computer.FileSystem.CopyFile(TextBox2.Text, Folder & "\client.swf", True)
        decompile.start(Folder)

        Me.Cursor = Cursors.Default
        MessageBox.Show("Your Source has been decompiled!")
        Process.Start(Folder)
    End Sub
    ' Folder Dialog
    Private Sub Open_Folder(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFileName As String
        Dim Folder = Application.StartupPath & "\Clients"
        fd.Title = "Open File Dialog"
        fd.InitialDirectory = Folder
        fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            strFileName = fd.FileName
            If InStr(strFileName, ".swf") <> 0 Then
                TextBox2.Text = strFileName
                Button6.Enabled = True
                Button5.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = True
            Else
                MessageBox.Show("You have to pick a .swf silly...")
            End If
        End If
    End Sub
    ' Recompile
    Private Sub recompileSWF(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim Folder = Application.StartupPath & "\Coding"
        If My.Computer.FileSystem.DirectoryExists(Folder & "\client-1") Then
            If My.Computer.FileSystem.FileExists(Folder & "\client.swf") Then
                Me.Cursor = Cursors.WaitCursor
                recompile.start(Folder)
                Me.Cursor = Cursors.Default
                MessageBox.Show("Your Source has been recompiled!")
            Else
                MessageBox.Show("What are you you even trying to recompile? Decompile first")
            End If
        Else
            MessageBox.Show("What are you you even trying to recompile? Decompile first")
        End If
    End Sub
    ' Get Bin Files
    Private Sub binExport(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If TextBox2.Text = "" Then
            MessageBox.Show("You have to select a .swf!")
        Else
            Dim Folder = Application.StartupPath & "\Coding"
            My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\RABCDasm", Folder, True)
            My.Computer.FileSystem.CopyFile(TextBox2.Text, Folder & "\client.swf", True)

            Me.Cursor = Cursors.WaitCursor

            Dim FILE_NAME As String = Folder & "\XML.bat"
            Dim aryText(4) As String
            aryText(0) = "cd " & Folder
            aryText(1) = "swfdecompress client.swf"
            aryText(2) = "swfbinexport client.swf"
            aryText(3) = "rabcdasm client-1.abc"
            batchProcess.start(aryText, FILE_NAME)

            Me.Cursor = Cursors.Default
            MessageBox.Show("Have fun with the XML files!")
            Process.Start(Folder)
        End If
    End Sub
    ' Replace binFile
    Private Sub binReplace(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If TextBox2.Text = "" Then
            MessageBox.Show("You have to select a .swf!")
        Else
            Dim Folder = Application.StartupPath & "\Coding"
            Dim message, title As String
            Dim Val As Object
            message = "Please Input the number(only) of the bin file you want to replace"
            title = "XML Replace"
            Val = InputBox(message, title)

            Dim Check As Boolean = My.Computer.FileSystem.FileExists(Folder & "\client-" & Val & ".bin")
            If Check = False Then
                MessageBox.Show("The bin file you requested does not exist.")
            Else
                Me.Cursor = Cursors.WaitCursor

                Dim FILE_NAME As String = Folder & "\XML_Recompile.bat"
                Dim aryText(2) As String
                aryText(0) = "cd " & Folder
                aryText(1) = "swfbinreplace client.swf " & Val & " client-" & Val & ".bin"
                batchProcess.start(aryText, FILE_NAME)

                Me.Cursor = Cursors.WaitCursor

                MessageBox.Show("Bin File Successfully Replaced")
            End If
        End If
    End Sub
    ' Back Button
    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        ItemSelect.Show()
        Me.Close()
    End Sub
End Class