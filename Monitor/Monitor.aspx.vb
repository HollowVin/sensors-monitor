
Imports System.Net
Imports System.Net.Sockets
Imports Client

Partial Class Monitor
    Inherits System.Web.UI.Page

    Private Shared Listener As TcpListener
    Private Shared ConnectedSensors As Hashtable
    Private Shared Status As String
    Private Shared Data As String

    Private Sub Monitor_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ConnectedSensors = New Hashtable()
            Dim ListenThread = New Threading.Thread(AddressOf DoListen)
            ListenThread.Start()
        End If
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
                UpdateStatus("Temperature sensor connected")

                LabelNSensors.Text = ConnectedSensors.Values.Count

            Loop Until False
        Catch E As Exception
            System.Diagnostics.Debug.WriteLine(E.ToString())
        End Try
    End Sub

    Private Sub EstablishListener()
        Dim ipHostInfo As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
        Dim ipAddress As IPAddress = ipHostInfo.AddressList(1)
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

    Private Sub OnLineReceived(ByVal sender As Client, ByVal ReceivedData As String)
        UpdateStatus("Line:" & ReceivedData)
        Data = ReceivedData

        'Dim objClient As Client
        'Dim d As DictionaryEntry

        'For Each Sensor In ConnectedSensors
        'objClient = Sensor.Value
        'objClient.Send(Data & vbCrLf)
        'Next
    End Sub

    Private Sub UpdateStatus(ByVal Stat As String)
        Status = Stat
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LabelNSensors.Text = ConnectedSensors.Values.Count
        LabelStatus.Text = Status
        Table1.Rows(1).Cells(1).Text = Data
    End Sub
End Class
