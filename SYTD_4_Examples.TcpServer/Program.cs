/*
 * Einfacher TCP Server
 * Wartet auf TCP Verbindungen, liest empfangene Textzeilen und gibt diese in der Konsole aus
 */

using System.Net;
using System.Net.Sockets;

var tcpListener = new TcpListener(IPAddress.Loopback, 8085);
tcpListener.Start();

var tcpClient = tcpListener.AcceptTcpClient();
var stream = tcpClient.GetStream();

var reader = new StreamReader(stream);

while(!reader.EndOfStream)
{
    var line = reader.ReadLine();
    Console.WriteLine(line);
}

Console.WriteLine("-------------------");
Console.ReadLine();