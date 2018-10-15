Public Class infobox
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Dim fadephase As Integer = 1
    Private Sub infobox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim transval As Integer
        Label2.Text = current_copyright("2014", True, myappname)
        Me.Size = New Size(414, 251)
        transval = Me.Opacity.ToString * 100
        HScrollBar1.Value = transval
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "More infoz »" Then
            Button2.Text = "« Less infoz"
            Me.Size = New Size(414, 566)
        Else
            Button2.Text = "More infoz »"
            Me.Size = New Size(414, 251)
        End If
    End Sub
    Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll
        Me.Opacity = HScrollBar1.Value.ToString / 100
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=503249")
    End Sub
    Private Sub me_mousedown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, Label2.MouseDown, PictureBox1.MouseDown, RichTextBox1.MouseDown, Label1.MouseDown, Label3.MouseDown, PictureBox2.MouseDown, Button1.MouseDown, Button2.MouseDown
        If e.Button = MouseButtons.Left Then

            IsFormBeingDragged = True

            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub
    Private Sub me_mouseup(sender As Object, e As MouseEventArgs) Handles Me.MouseUp, Label2.MouseUp, PictureBox1.MouseUp, RichTextBox1.MouseUp, Label1.MouseUp, Label3.MouseUp, PictureBox2.MouseUp, Button1.MouseUp, Button2.MouseUp
        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, Label2.MouseMove, PictureBox1.MouseMove, RichTextBox1.MouseMove, Label1.MouseMove, Label3.MouseMove, PictureBox2.MouseMove, Button1.MouseMove, Button2.MouseMove
        If IsFormBeingDragged Then
            Dim temp As Point = New Point()
            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim k = Me.Opacity.ToString
        Dim transval As Integer
        If fadephase = 1 Then
            If k >= 1 Then
                fadephase = 0
            End If
            Me.Opacity = k + 0.01
        End If
        If fadephase = 0 Then
            If k <= 0.8 Then
                fadephase = 1
            End If
            Me.Opacity = k - 0.01
        End If
        transval = Me.Opacity.ToString * 100
        HScrollBar1.Value = transval
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Select Case CheckBox1.CheckState
            Case CheckState.Checked
                Timer1.Start()
            Case CheckState.Unchecked
                Timer1.Stop()
        End Select
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start(LinkLabel1.Text)
    End Sub
End Class