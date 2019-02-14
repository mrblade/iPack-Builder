
Public Class main_tab
    Public exist As String
    Dim filename0 As String
    Public Shared hasfiles As String
    Dim filepath() As String
    Dim f0, fullpath() As String

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
    Private Shared ReadOnly SupportedExtensions As String() = {".res"}

    Private Sub ListBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListBox1.DragDrop
        exist = "no"
        Dim Files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

        For Each FileName As String In Files

            Form1.aa = FileName

            If FileName.Contains(".res") Then

                f0 = IO.Path.GetFileName(FileName)
                If ListBox1.Items.Contains(f0) Then
                    exist = "yes"
                Else
                    ListBox1.Items.Add(f0)
                    ListBox2.Items.Add(FileName)
                    hasfiles = "yes"

                End If
            End If

        Next

        ListBox1.Sorted = True
        ListBox1.Refresh()
        ListBox2.Sorted = True
        ListBox2.Refresh()
       
        Dim index As Integer
        Dim itemcount As Integer = ListBox1.Items.Count

        If itemcount > 1 Then
            Dim lastitem As String = ListBox1.Items(itemcount - 1)


            For index = itemcount - 2 To 0 Step -1
                If ListBox1.Items(index) = lastitem Then

                    ListBox1.Items.RemoveAt(index)
                    ListBox2.Items.RemoveAt(index)
                    '   MessageBox.Show("File aready exist. Delete it first!", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    exist = "yes"
                Else

                    lastitem = ListBox1.Items(index)
                    hasfiles = "yes"
                End If
            Next
        End If
        If exist = "yes" Then
            MessageBox.Show("Some file(s) with same name already exist, delete them first.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If


    End Sub
    Public Shared Function ToStrArray(ByVal value As String) As String()
        Dim result As String() = New String(value.Length - 1) {}
        For i As Integer = 0 To value.Length - 1
            result(i) = New String(value(i), 1)
        Next
        Return result
    End Function

    Private Sub ListBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListBox1.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub main_tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Font = CustomFont.GetInstance(10, FontStyle.Regular)
        Label1.Font = CustomFont.GetInstance(11, FontStyle.Regular)
        hasfiles = "no"
        If ListBox1.Text = "" Then
            hasfiles = "no"
        End If

        If Form1.bottompart.modification = True Then
            Try
                ' Dim Files As String() = My.Computer.FileSystem.GetFiles(Form1.bottompart.path & "\Resource Files")

                For Each FileName As String In My.Computer.FileSystem.GetFiles(Form1.bottompart.path & "\Resource Files")

                    Form1.aa = FileName

                    If FileName.Contains(".res") Then

                        f0 = IO.Path.GetFileName(FileName)
                        If ListBox1.Items.Contains(f0) Then
                            exist = "yes"
                        Else
                            ListBox1.Items.Add(f0)
                            ListBox2.Items.Add(FileName)
                            hasfiles = "yes"

                        End If
                    End If

                Next
            Catch ex As Exception

            End Try
        End If
    End Sub



    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox2.Image = My.Resources.rem2
        Dim lst As New List(Of Object)
        For Each a As Object In ListBox1.SelectedItems
            lst.Add(a)
        Next
        For Each a As Object In lst
            Dim pos As Integer
            pos = ListBox1.Items.IndexOf(a)
            ListBox1.Items.Remove(a)
            ListBox2.Items.RemoveAt(pos)
          

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
        ListBox2.Items.Clear()
        ListBox1.Items.Clear()
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.rema1
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.rema1
    End Sub

    

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

    End Sub
End Class
