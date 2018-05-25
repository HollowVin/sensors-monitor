
Imports System.Net
Imports System.Net.Sockets

Partial Class Monitor
    Inherits System.Web.UI.Page

    Private Listener As TcpListener

    Private Sub Monitor_Load(sender As Object, e As EventArgs) Handles Me.Load
        EstablishListener()

        While True
            HandleConnections()
        End While
    End Sub

    Private Sub Monitor_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Try
            Listener.Stop()
        Catch SException As SocketException
        End Try
    End Sub

    Private Sub EstablishListener()
        Dim ipHostInfo As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
        Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
        Dim localEndPoint As New IPEndPoint(ipAddress, 11000)

        Listener = New TcpListener(ipAddress, 100)
        Listener.Start()
    End Sub

    Private Sub HandleConnections()

    End Sub

End Class
