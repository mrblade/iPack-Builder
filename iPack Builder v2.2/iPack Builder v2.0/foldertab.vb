Imports System.IO
Public Class folder_tab

    Dim exist As String

    Private Sub foldertab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = Form1.PictureBox11.Right

        ListBox1.Font = CustomFont.GetInstance(10, FontStyle.Regular)
        Label1.Font = CustomFont.GetInstance(11, FontStyle.Regular)
        Label4.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label5.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label6.Font = CustomFont.GetInstance(9, FontStyle.Regular)

      

    End Sub
    Private Sub ListBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListBox1.DragDrop
        exist = "no"
        Dim Files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        For Each FileName As String In Files
            Form1.aa = FileName
            If Directory.Exists(FileName) Then
                Dim f() As String
                f = Directory.GetFiles(FileName, "*.res", SearchOption.TopDirectoryOnly)
                For Each Fi As String In f
                    ListBox1.Items.Add(FileName)
                Next
            Else

            End If


        Next

        ListBox1.Sorted = True
        ListBox1.Refresh()
        Dim index As Integer
        Dim itemcount As Integer = ListBox1.Items.Count

        If itemcount > 1 Then
            Dim lastitem As String = ListBox1.Items(itemcount - 1)


            For index = itemcount - 2 To 0 Step -1
                If ListBox1.Items(index) = lastitem Then
                    ListBox1.Items.RemoveAt(index)
                    '   MessageBox.Show("File aready exist. Delete it first!", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    exist = "yes"


                Else

                    lastitem = ListBox1.Items(index)

                End If
            Next
        End If
        '   If exist = "yes" Then
        '   MessageBox.Show("Some file(s) already exist, delete them first.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        '  End If
      

    End Sub

    Private Sub ListBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListBox1.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox2.Image = My.Resources.rem2
        Dim lst As New List(Of Object)
        For Each a As Object In ListBox1.SelectedItems
            lst.Add(a)
        Next
        For Each a As Object In lst
            ListBox1.Items.Remove(a)
        Next
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.Image = My.Resources.rem1
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.rem1
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        PictureBox1.Image = My.Resources.rema2
        ListBox1.Items.Clear()
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.rema1
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.rema1
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
End Class
