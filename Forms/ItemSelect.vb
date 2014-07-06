Imports System.Net

Public Class ItemSelect
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Coding.Show()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        MessageBox.Show("MPGH does not allow you to link to a forum post")
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Dim fullPath As String = Application.StartupPath
        MessageBox.Show(fullPath)
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        ClientLauncher.Show()
        Me.Close()
    End Sub
    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        AddHacks.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click_2(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        proxyUtility.Show()
        Me.Close()
    End Sub
End Class
