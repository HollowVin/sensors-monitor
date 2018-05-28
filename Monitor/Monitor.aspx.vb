
Imports System.Net
Imports System.Net.Sockets
Imports Client

Partial Class Monitor
    Inherits System.Web.UI.Page

    Private Shared Listener As TcpListener
    Private Shared ConnectedSensors As New Dictionary(Of Client, TableRow)
    Private Shared Status As String
    Private Shared Data As String

    Private Sub Monitor_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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
                AddHandler x.NameReceived, AddressOf OnNameReceived

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
        UpdateStatus(sender.Name + " sensor disconnected")
        ConnectedSensors.Remove(sender)
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

    Private Sub OnNameReceived(ByVal Sender As Client)
        Dim SensorTableRow As New TableRow()
        Dim SensorNameCell As New TableCell()
        Dim SensorValueCell As New TableCell()

        SensorNameCell.Text = Sender.Name
        SensorValueCell.Text = "(N/A)"

        SensorTableRow.Cells.Add(SensorNameCell)
        SensorTableRow.Cells.Add(SensorValueCell)

        ConnectedSensors.Add(Sender, SensorTableRow)
        UpdateStatus(Sender.Name + " sensor connected")
    End Sub

    Private Sub UpdateStatus(ByVal Stat As String)
        Status = Stat
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LabelNSensors.Text = ConnectedSensors.Values.Count
        LabelStatus.Text = Status

        For Each TableRow In TableSensors.Rows
            TableSensors.Rows.Remove(TableRow)
        Next

        For Each Sensor In ConnectedSensors
            Dim Index = TableSensors.Rows.Add(Sensor.Value)
            TableSensors.Rows(Index).Cells(1).Text = Sensor.Key.LastValue

            LabelStatus.Text = Sensor.Key.LastValue
        Next

    End Sub
End Class
