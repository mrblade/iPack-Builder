Public Class themetab
    Public themepath As String
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath = "" Then
            Label2.Text = "None"
        ElseIf My.Computer.FileSystem.FileExists(FolderBrowserDialog1.SelectedPath & "\Theme.xml") = True Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            themepath = FolderBrowserDialog1.SelectedPath
            Dim root As XElement = XDocument.Load(themepath & "\Theme.xml").Root
            Dim val As String
            Try
                val = (root.Element("name").Value)
                If Not val = Nothing Then
                    Label2.Text = val
                Else
                    Label2.Text = "None"
                End If
            Catch ex As Exception

            End Try

        Else
            MessageBox.Show("Theme.xml missing in the selected folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Label2.Text = "None"
        End If
    End Sub

  
    Private Sub FolderBrowserDialog1_HelpRequest(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.HelpRequest

    End Sub

    Public Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        themepath = ""
        TextBox1.Text = ""
        Label2.Text = "None"
    End Sub

    Private Sub themetab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = Form1.PictureBox19.Right
        If Form1.bottompart.modification = True Then
            Try
                If My.Computer.FileSystem.FileExists(Form1.bottompart.path & "\Setup files-iPack\Theme\Theme.xml") Then
                    TextBox1.Text = (Form1.bottompart.path & "\Setup files-iPack\Theme")
                    themepath = (Form1.bottompart.path & "\Setup files-iPack\Theme")
                    Dim root As XElement = XDocument.Load(themepath & "\Theme.xml").Root
                    Dim val As String
                    Try
                        val = (root.Element("name").Value)
                        If Not val = Nothing Then
                            Label2.Text = val
                        Else
                            Label2.Text = "None"
                        End If
                    Catch ex As Exception

                    End Try


                End If
            Catch ex As Exception

            End Try

        End If
    End Sub
End Class
