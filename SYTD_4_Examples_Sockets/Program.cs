using System.Net;
using System.Net.Sockets;

// Erstelle einen neuen UDP-Socket (Dgram = Datagram = Data + Telegram)
var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

// Teile dem Socket mit, auf welcher Adresse und Port gelauscht werden soll 
// TODO Umstellen auf IPAddress.Any, um Pakete aus dem Netzwerk entgegen zu nehmen (Loopback = nur localhost)
var endpoint = new IPEndPoint(IPAddress.Any, 8000);
socket.Bind(endpoint);

// Anlegen eines Puffers für eingehende Nachrichten
var buffer = new byte[1024];

Console.WriteLine("Warte auf eingehende Nachrichten...");

var udpClient = new UdpClient(endpoint);
udpClient.Send(new byte[] { 1 });

while (true)
{
    var remoteEndPoint = (EndPoint)endpoint;
    var length = socket.ReceiveFrom(buffer, ref remoteEndPoint);
    Console.WriteLine($"{length} bytes von {remoteEndPoint} empfangen.");

    Console.WriteLine(string.Join(", ", buffer.Take(length)));
    Console.WriteLine();
}
