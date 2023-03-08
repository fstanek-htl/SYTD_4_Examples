using System.Net;
using System.Net.Sockets;

var tcpListener = new TcpListener(IPAddress.Loopback, 8085);
tcpListener.Start();

while (true)
{
    var tcpClient = tcpListener.AcceptTcpClient();

    Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} schreibt:");

    var reader = new StreamReader(tcpClient.GetStream());

    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        Console.WriteLine($"\t{line}");
    }

    Console.WriteLine("Verbindung getrennt.");
    Console.WriteLine("--------------------");
}