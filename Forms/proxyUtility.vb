Imports System.Net
Imports System
Imports System.IO

Public Class proxyUtility
    Dim fileLocation As String = Application.StartupPath & "\Proxy"

    ' Load Event
    Private Sub Form4_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ItemSelect.Close()
        If My.Computer.FileSystem.DirectoryExists(fileLocation) Then
            My.Computer.FileSystem.MoveDirectory(fileLocation, Application.StartupPath & "\Archive\Proxy\" & hackVariables.Vars.today & "_" & DateAndTime.Now.Hour & "." & DateAndTime.Now.Minute)
        Else
            My.Computer.FileSystem.CreateDirectory(fileLocation)
        End If
        Button5.Enabled = False
        Label2.Text = ""
        Application.DoEvents()
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
                Button5.Enabled = True
            Else
                MessageBox.Show("You have to pick a .swf silly...")
            End If
        End If
    End Sub
    ' Back Button
    Private Sub back(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        ItemSelect.Show()
        Me.Close()
    End Sub
    ' Grab Latest Client
    Private Sub latest(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim clientVersion As String = getLatest.Grab()
        TextBox2.Text = Application.StartupPath & "\Clients\Fresh_Client_" & clientVersion & ".swf"
        Button5.Enabled = True
    End Sub
    ' Run the utility to get all of the strings
    Private Sub runUtility(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        ' Change Cursor
        Me.Cursor = Cursors.WaitCursor

        ' Copy over RABCDasm
        My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\RABCDasm", fileLocation)
        My.Computer.FileSystem.CopyFile(TextBox2.Text, fileLocation & "\client.swf", True)

        ' Create a batchfile for decompile process
        Label2.Text = "Decompiling..."
        Application.DoEvents()
        decompile.start(fileLocation)

        ' Deobfuscate Files
        Label2.Text = "Removing Junk Code..."
        Application.DoEvents()
        deobfuscate.Deobf(fileLocation)

        ' Get Packet ID's
        Label2.Text = "Mapping Packets..."
        Application.DoEvents()
        packetIDS.mapPackets(fileLocation)

        ' Get Symmetric Encryption Keys
        Label2.Text = "Finding RC4 Keys..."
        Application.DoEvents()
        encryptionKeys.getRC4(fileLocation)

        ' Find Packet Classes
        Label2.Text = "Bulding Packet Information..."
        Application.DoEvents()
        packetClasses.find(fileLocation)

        ' Move IDS and Keys to temp folder
        Label2.Text = "Cleaning Up..."
        Application.DoEvents()
        My.Computer.FileSystem.MoveFile(fileLocation & "/packetID.txt", Application.StartupPath & "/temp/packetID.txt", True)
        My.Computer.FileSystem.MoveFile(fileLocation & "/RC4keys.txt", Application.StartupPath & "/temp/RC4keys.txt", True)

        ' Delete Proxy Folder
        My.Computer.FileSystem.DeleteDirectory(fileLocation, FileIO.DeleteDirectoryOption.DeleteAllContents)

        ' Move temp to Proxy
        My.Computer.FileSystem.RenameDirectory(Application.StartupPath & "/temp", "Proxy")

        ' Format Packets File
        packetIDS.formatPackets(fileLocation)

        ' Open Proxy Folder
        Process.Start(fileLocation)
        Me.Close()
    End Sub
    ' Open Setting
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        proxyUtilitySettings.Show()
        Me.Close()
    End Sub
End Class