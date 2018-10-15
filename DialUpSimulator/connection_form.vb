Imports System.IO
Public Class connection_form
    Public timeOfSound As Single = 0
    Public grpBoxAlign As Double = 0.0
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor
    End Sub

    Private Sub connection_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Location = New Point(0, 0)
        My.Computer.Audio.Play(My.Resources.dialup_snd, AudioPlayMode.Background)
        Label1.Text = "Dialing."
        Timer1.Interval = 1000
        Timer1.Start()
        Timer2.Interval = 350
        Timer2.Start()
        GroupBox1.Size = New Point(1, 120)
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.BackColor = Color.Transparent
        timeOfSound = 0
        grpBoxAlign = 0.0
        Timer3.Interval = 38
        Timer3.Start()
        
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timeOfSound += 1
        If timeOfSound >= 23 Then
            Me.Dispose()
            Me.Close()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Select Case Label1.Text
            Case "Dialing."
                Label1.Text = "Dialing.."
            Case "Dialing.."
                Label1.Text = "Dialing..."
            Case "Dialing..."
                Label1.Text = "Dialing."
        End Select
        If timeOfSound = 20 Then
            Timer2.Stop()
            Label1.Text = "Connected !"
            Label1.BackColor = Color.DarkGreen
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        grpBoxAlign += 1
        GroupBox1.Location = New Point(grpBoxAlign, 0)
        If grpBoxAlign = 425 Then
            grpBoxAlign = 0
            Timer3.Stop()
        End If
    End Sub
End Class