Public Class Interface_tab
    Dim header, icon, logo, splash As String
    Public Shared imgThumb As Image = Nothing
    Public Shared icoThumb As Image = Nothing
    Public Shared logoThumb As Image = Nothing
    Public Shared splashThumb As Image = Nothing
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
      
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                TextBox1.Text = OpenFileDialog1.FileName
                header = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
            End If


            Try
                Dim image As Image = Nothing
                ' Check if textbox has a value
                If TextBox1.Text <> String.Empty Then
                    image = image.FromFile(TextBox1.Text)
                End If
                ' Check if image exists
                If Not image Is Nothing Then
                    imgThumb = image.GetThumbnailImage(348, 54, Nothing, New IntPtr())
                    Me.Refresh()
                End If

            Catch
                '  MessageBox.Show("You didn't select anything! Default will be used.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If OpenFileDialog2.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            TextBox2.Text = OpenFileDialog2.FileName
            icon = System.IO.Path.GetFileName(OpenFileDialog2.FileName)

        End If
        Try
            Dim image As Image = Nothing
            ' Check if textbox has a value
            If TextBox2.Text <> String.Empty Then
                image = image.FromFile(TextBox2.Text)
            End If
            ' Check if image exists
            If Not image Is Nothing Then
                icoThumb = image.GetThumbnailImage(25, 25, Nothing, New IntPtr())
                Me.Refresh()
            End If
          
          
        Catch
            '  MessageBox.Show("You didn't select anything! Default will be used.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub Interface_tab_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyClass.Paint

        If Not imgThumb Is Nothing Then
            e.Graphics.DrawImage(imgThumb, 13, 42, imgThumb.Width, imgThumb.Height)
        End If
        If Not icoThumb Is Nothing Then
            e.Graphics.DrawImage(icoThumb, 323, 111, icoThumb.Width, icoThumb.Height)
        End If
        If Not logoThumb Is Nothing Then
            e.Graphics.DrawImage(logoThumb, 13, 154, logoThumb.Width, logoThumb.Height)
        End If
        If Not splashThumb Is Nothing Then
            e.Graphics.DrawImage(splashThumb, 142, 274, splashThumb.Width, splashThumb.Height)
        End If

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        TextBox1.Clear()
        imgThumb = Nothing
        Me.Refresh()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        TextBox2.Clear()
        icoThumb = Nothing
        Me.Refresh()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If OpenFileDialog3.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            TextBox3.Text = OpenFileDialog3.FileName
            logo = System.IO.Path.GetFileName(OpenFileDialog3.FileName)

        End If
        Try
            Dim image As Image = Nothing
            ' Check if textbox has a value
            If TextBox3.Text <> String.Empty Then
                image = image.FromFile(TextBox3.Text)
            End If
            ' Check if image exists
            If Not image Is Nothing Then
                logoThumb = image.GetThumbnailImage(89, 218, Nothing, New IntPtr())
                Me.Refresh()
            End If


        Catch

        End Try
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        TextBox3.Clear()
        logoThumb = Nothing
        Me.Refresh()
    End Sub

    Private Sub Interface_tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = Form1.PictureBox12.Right

        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        RadioButton2.Checked = True
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        TextBox1.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label2.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label1.Font = CustomFont.GetInstance(10, FontStyle.Regular)
        TextBox2.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox3.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label3.Font = CustomFont.GetInstance(10, FontStyle.Regular)
        Label8.Font = CustomFont.GetInstance(10, FontStyle.Regular)
        Label7.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        RadioButton1.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        RadioButton2.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label10.Font = CustomFont.GetInstance(10, FontStyle.Regular)
        Label9.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label4.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label5.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label6.Font = CustomFont.GetInstance(9, FontStyle.Regular)

        If Form1.bottompart.modification = True Then
            Try
                TextBox1.Text = Form1.bottompart.header


                Dim image As Image = Nothing
                ' Check if textbox has a value
                If Form1.bottompart.header <> String.Empty Then
                    image = image.FromFile(Form1.bottompart.header)
                End If
                ' Check if image exists
                If Not image Is Nothing Then
                    imgThumb = image.GetThumbnailImage(348, 54, Nothing, New IntPtr())
                    Me.Refresh()
                End If

                TextBox3.Text = Form1.bottompart.logo

                Dim image1 As Image = Nothing
                ' Check if textbox has a value
                If Form1.bottompart.logo <> String.Empty Then
                    image1 = image.FromFile(Form1.bottompart.logo)
                End If
                ' Check if image exists
                If Not image1 Is Nothing Then
                    logoThumb = image1.GetThumbnailImage(89, 218, Nothing, New IntPtr())
                    Me.Refresh()
                End If

                If Not Form1.bottompart.splash = Nothing Then
                    RadioButton1.Checked = True
                    TextBox4.Text = Form1.bottompart.splash
                    Try
                        Dim imagex As Image = Nothing
                        ' Check if textbox has a value
                        If TextBox4.Text <> String.Empty Then
                            imagex = image.FromFile(TextBox4.Text)
                        End If
                        ' Check if image exists
                        If Not imagex Is Nothing Then
                            splashThumb = imagex.GetThumbnailImage(198, 98, Nothing, New IntPtr())
                            Me.Refresh()
                        End If
                    Catch ex As Exception

                    End Try

                End If

                TextBox2.Text = Form1.bottompart.ico
                Dim image11 As Image = Nothing
                ' Check if textbox has a value
                If TextBox2.Text <> String.Empty Then
                    image11 = image.FromFile(TextBox2.Text)
                End If
                ' Check if image exists
                If Not image11 Is Nothing Then
                    icoThumb = image11.GetThumbnailImage(25, 25, Nothing, New IntPtr())
                    Me.Refresh()
                End If


            Catch ex As Exception

            End Try



        End If


    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Label10.Visible = True
        '  TextBox4.Visible = True

        PictureBox8.Visible = True
        Label9.Visible = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Label10.Visible = False
        TextBox4.Visible = False

        PictureBox8.Visible = False
        Label9.Visible = False
        TextBox4.Clear()
        splashThumb = Nothing
        Me.Refresh()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        If OpenFileDialog4.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            TextBox4.Text = OpenFileDialog4.FileName
            splash = System.IO.Path.GetFileName(OpenFileDialog4.FileName)

        End If
        Try
            Dim image As Image = Nothing
            ' Check if textbox has a value
            If TextBox4.Text <> String.Empty Then
                image = image.FromFile(TextBox4.Text)
            End If
            ' Check if image exists
            If Not image Is Nothing Then
                splashThumb = image.GetThumbnailImage(198, 98, Nothing, New IntPtr())
                Me.Refresh()
            End If

        Catch

        End Try
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)

    End Sub
End Class
