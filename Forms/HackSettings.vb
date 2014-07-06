Public Class HackSettings
    ' Load Settings
    Private Sub HackSettings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.Nexus
        TextBox2.Text = My.Settings.FPS
        TextBox3.Text = My.Settings.Drecon
        TextBox4.Text = My.Settings.Recon
        CheckBox1.Checked = My.Settings.Debug
    End Sub
    ' On Closing Event
    Private Sub HackSettings_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Nexus = TextBox1.Text.Replace("%", "")
        My.Settings.FPS = TextBox2.Text
        My.Settings.Drecon = TextBox3.Text
        My.Settings.Recon = TextBox4.Text
        My.Settings.Debug = CheckBox1.CheckState
        My.Settings.Save()
        AddHacks.Show()
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        My.Settings.Nexus = TextBox1.Text.Replace("%", "")
        My.Settings.FPS = TextBox2.Text
        My.Settings.Drecon = TextBox3.Text
        My.Settings.Recon = TextBox4.Text
        My.Settings.Debug = CheckBox1.CheckState
        My.Settings.Save()
        AddHacks.Show()
        Me.Close()
    End Sub
End Class