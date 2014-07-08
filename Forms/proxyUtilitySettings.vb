Public Class proxyUtilitySettings
    Public Shared proxyUtilText As ArrayList

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'File Name
        TextBox1.Text = "Packets.xml"
        TextBox2.Text = "<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>"
        TextBox3.Text = "<Packets>"
        TextBox4.Text = "   <Packet id="""
        TextBox5.Text = """ type="""
        TextBox6.Text = """/>"
        TextBox7.Text = "</Packets>"
        TextBox8.Text = ""
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        proxyUtility.Show()
        Me.Close()
    End Sub

    Private Sub proxyUtilitySettings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        proxyUtilText = My.Settings.proxyUtilText
        If proxyUtilText.Count = 8 Then
            TextBox1.Text = proxyUtilText(0).ToString
            TextBox2.Text = proxyUtilText(1).ToString
            TextBox3.Text = proxyUtilText(2).ToString
            TextBox4.Text = proxyUtilText(3).ToString
            TextBox5.Text = proxyUtilText(4).ToString
            TextBox6.Text = proxyUtilText(5).ToString
            TextBox7.Text = proxyUtilText(6).ToString
            TextBox8.Text = proxyUtilText(7).ToString
        End If
    End Sub

    Private Sub proxyUtilitySettings_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        proxyUtilText.Clear()
        proxyUtilText.Add(TextBox1.Text)
        proxyUtilText.Add(TextBox2.Text)
        proxyUtilText.Add(TextBox3.Text)
        proxyUtilText.Add(TextBox4.Text)
        proxyUtilText.Add(TextBox5.Text)
        proxyUtilText.Add(TextBox6.Text)
        proxyUtilText.Add(TextBox7.Text)
        proxyUtilText.Add(TextBox8.Text)
        My.Settings.proxyUtilText = proxyUtilText
        My.Settings.Save()

    End Sub
End Class