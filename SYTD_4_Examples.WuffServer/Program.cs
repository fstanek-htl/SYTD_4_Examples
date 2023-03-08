using System.Net;
using System.Net.Sockets;


var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
socket.Bind(new IPEndPoint(IPAddress.Loopback, 8118));

// Anlegen eines Puffers für eingehende Nachrichten
var buffer = new byte[2];

Console.WriteLine("Wuff server started...");
Console.WriteLine("----------------------");

while (true)
{
    var messageSize = socket.Receive(buffer);

    if (messageSize < 2)
    {
        Console.WriteLine("Invalid message.");
        continue;
    }

    var count = buffer[0];
    var volume = buffer[1];

    for (int i = 0; i < count; i++)
    {
        if (volume > 100)
            Console.Write("WUFF ");
        else
            Console.Write("wuff ");
    }

    Console.WriteLine();
}