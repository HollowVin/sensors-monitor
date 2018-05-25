
Imports System.Net
Imports System.Net.Sockets
Imports Client

Partial Class Monitor
    Inherits System.Web.UI.Page

    Private Listener As TcpListener
    Private ConnectedSensors As New Hashtable()

    Private Sub Monitor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ListenThread = New Threading.Thread(AddressOf DoListen)
        ListenThread.Start()
    End Sub

    Private Sub Monitor_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Try
            Listener.Stop()
        Catch
        End Try
    End Sub

    Private Sub DoListen()
        Try
            EstablishListener()
            Do
                Dim x As New Client(Listener.AcceptTcpClient)

                AddHandler x.Connected, AddressOf OnConnected
                AddHandler x.Disconnected, AddressOf OnDisconnected
                AddHandler x.ValueReceived, AddressOf OnLineReceived
                ConnectedSensors.Add(x.ID, x)

                LabelNSensors.Text = ConnectedSensors.Values.Count

            Loop Until False
        Catch
        End Try
    End Sub

    Private Sub EstablishListener()
        Dim ipHostInfo As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
        Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
        Dim localEndPoint As New IPEndPoint(ipAddress, 11000)
        Dim port As Integer = 23

        Listener = New TcpListener(ipAddress, port)
        Listener.Start()

        LabelIP.Text = ipAddress.ToString()
        LabelPort.Text = port
        LabelNSensors.Text = 0
    End Sub

    Private Sub OnConnected(ByVal sender As Client)
        UpdateStatus("New sensor connected")
    End Sub

    Private Sub OnDisconnected(ByVal sender As Client)
        UpdateStatus("Sensor disconnected")
        ConnectedSensors.Remove(sender.ID)
    End Sub

    Private Sub OnLineReceived(ByVal sender As Client, ByVal Data As String)
        UpdateStatus("Line:" & Data)

        'Dim objClient As Client
        'Dim d As DictionaryEntry

        'For Each Sensor In ConnectedSensors
        'objClient = Sensor.Value
        'objClient.Send(Data & vbCrLf)
        'Next
    End Sub

    Private Sub UpdateStatus(ByVal Status As String)
        TextAreaLog.InnerText += Status + "\n"
    End Sub

End Class
