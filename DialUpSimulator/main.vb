Imports System.Management
Public Class main

    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each a In My.Application.CommandLineArgs
            If a.Contains("-disable") = True Then

                Try
                    For Each i In NetworkAdapter.GetAllNetworkAdapter
                        If i.Name = My.Settings.myNetAdapterName And i.DeviceId = My.Settings.myNetAdapterDeviceId Then
                            i.EnableOrDisableNetworkAdapter("Disable")
                        End If
                    Next
                    mainForm_connect()
                Catch ex As Exception
                End Try
            End If
            'Do it silent?
            If a.Contains("-disablesilent") = True Then
                Me.WindowState = FormWindowState.Minimized
                Me.ShowInTaskbar = False
                Try
                    For Each i In NetworkAdapter.GetAllNetworkAdapter
                        If i.Name = My.Settings.myNetAdapterName And i.DeviceId = My.Settings.myNetAdapterDeviceId Then
                            i.EnableOrDisableNetworkAdapter("Disable")
                        End If
                    Next
                    Debug.Print("silent works")
                Catch ex As Exception
                    Debug.Print("silent error")
                End Try
                Me.Dispose()
                Me.Close()
            End If
            '##############
        Next
        
        If My.Settings.myNetAdapterName = "None" Then
            Label1.Text = "T-Online (first start)"
        Else
            Label1.Text = My.Settings.myNetAdapterName
            If checkNetState(My.Settings.myNetAdapterDeviceId) >= 0 Then
                Button2.Text = "Disc&onnect"
                mainForm_disconnect()
            Else
                Button2.Text = "C&onnect"
                mainForm_connect()
            End If
        End If
        TextBox1.Text = My.Settings.myUsername
        TextBox2.Text = My.Settings.myPassword
        Randomize()
        Dim rndnumber As Integer = CInt(Int((999999 * Rnd()) + 10000))
        TextBox3.Text = "02432" & rndnumber.ToString
        ComboBox1.SelectedIndex = 0
        Me.Text = myappfullname

        Try
            For Each i In NetworkAdapter.GetAllNetworkAdapter
                netAdapters.Add(i.Name & " (" & i.DeviceId & ")")
            Next
        Catch ex As Exception
            netAdapters.Add("None found")
            netAdapters.Add("Internal error")
        End Try


        'resetMySettings()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        config_form.ShowDialog()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'With TextBox3
        '    If IsNumeric(.Text) Then
        '        If InStr(1, .Text, ".") Or InStr(1, .Text, ",") Then
        '            If Val(.Text) > 0 Then
        '                Debug.Print("is positive floating point number")
        '            ElseIf Val(.Text) < 0 Then
        '                Debug.Print("is negative floating point number")
        '            Else
        '                Debug.Print("is zero")
        '            End If
        '        Else
        '            If Val(.Text) > 0 Then
        '                Debug.Print("is positive fixed point number")
        '            ElseIf Val(.Text) < 0 Then
        '                Debug.Print("is negative fixed point number")
        '            Else
        '                Debug.Print("is zero")
        '            End If
        '        End If
        '    End If
        'End With
        With TextBox3
            If IsNumeric(.Text) Then
                If InStr(1, .Text, ".") Or InStr(1, .Text, ",") Then
                    TextBox3.ForeColor = Color.DarkRed
                Else
                    If Val(.Text) > 0 Then
                        TextBox3.ForeColor = Color.Black
                    ElseIf Val(.Text) < 0 Then
                        TextBox3.ForeColor = Color.DarkRed
                    Else
                        TextBox3.ForeColor = Color.DarkRed
                    End If
                End If
                TextBox3.ForeColor = Color.Black
            Else
                TextBox3.ForeColor = Color.DarkRed
            End If
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Select Case Button2.Text
            Case "C&onnect"
                mainForm_disconnect()
                Button2.Text = "Connecting..."
                Button3.Text = "Please Wait"
                connection_form.ShowDialog()
                'Connect
                Try
                    For Each i In NetworkAdapter.GetAllNetworkAdapter
                        If i.Name = My.Settings.myNetAdapterName And i.DeviceId = My.Settings.myNetAdapterDeviceId Then
                            i.EnableOrDisableNetworkAdapter("Enable")
                        End If
                    Next
                    mainForm_disconnect()
                Catch ex As Exception
                    Label1.Text = "Error!"
                End Try
            Case "Disc&onnect"
                Try
                    For Each i In NetworkAdapter.GetAllNetworkAdapter
                        If i.Name = My.Settings.myNetAdapterName And i.DeviceId = My.Settings.myNetAdapterDeviceId Then
                            i.EnableOrDisableNetworkAdapter("Disable")
                        End If
                    Next
                    mainForm_connect()
                Catch ex As Exception
                End Try
        End Select
        
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        infobox.ShowDialog()
    End Sub
End Class
