Public Class ClientLauncher
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Clients") Then
        Else
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Clients")
        End If
        For Each file As String In IO.Directory.GetFiles(Application.StartupPath & "\Clients", "*.swf")
            Me.ComboBox1.Items.Add(file)
        Next
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        My.Computer.FileSystem.CopyFile(ComboBox1.Text, Application.StartupPath & "\Play\client.swf", True)

        Dim FILE_NAME As String = Application.StartupPath & "\Play\Run.bat"
        Dim aryText(3) As String
        aryText(0) = "cd " & Application.StartupPath & "\Play"
        aryText(1) = "flashplayer_11_sa.exe client.swf"
        aryText(2) = "exit"
        batchProcess.start(aryText, FILE_NAME)

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs)
        ItemSelect.Show()
        Me.Close()
    End Sub
End Class