Public Class language_select
    Public text1 As String
    Private Sub language_select_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = Form1.ComboBox1.Width
        Me.Height = Form1.ComboBox1.Height
       

        If Form1.ComboBox1.Visible = True Then
            Me.Location = New Point(Form1.ComboBox1.Location.X, Form1.ComboBox1.Location.Y)
            Form1.ComboBox1.SendToBack()
        Else

            Me.Location = New Point(Form1.Uninstaller.ComboBox1.Left, Form1.ComboBox1.Location.Y)
            Form1.Uninstaller.ComboBox1.SendToBack()
        End If

        Try
            Me.BackgroundImage = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.drop1)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub language_select_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown


        If Form1.ComboBox1.Visible = True Then
            Form1.ComboBox1.DroppedDown = True
        Else
            Form1.Uninstaller.ComboBox1.DroppedDown = True


        End If


    End Sub

    Private Sub language_select_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Try
            Me.BackgroundImage = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.drop2)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub language_select_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Try
            Me.BackgroundImage = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.drop1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As MouseEventArgs) Handles Label1.MouseDown
        'Form1.ComboBox1.DroppedDown = True
        If Form1.ComboBox1.Visible = True Then
            Form1.ComboBox1.DroppedDown = True
        Else
            Form1.Uninstaller.ComboBox1.DroppedDown = True


        End If
    End Sub

    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter
        language_select_MouseEnter(Nothing, Nothing)
    End Sub

    Private Sub Label1_MouseLeave(sender As Object, e As EventArgs) Handles Label1.MouseLeave
        language_select_MouseLeave(Nothing, Nothing)
    End Sub

   
    Private Sub Label1_Paint(sender As Object, e As PaintEventArgs) Handles Label1.Paint


    End Sub
End Class
