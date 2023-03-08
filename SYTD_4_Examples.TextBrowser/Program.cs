using System.Net.Sockets;

Console.Write("Url: ");
var url = Console.ReadLine();

using var tcpClient = new TcpClient();
tcpClient.Connect(url, 80);

using var writer = new StreamWriter(tcpClient.GetStream());
writer.WriteLine("GET / HTTP/1.1");
writer.WriteLine();
writer.Flush();

using var reader = new StreamReader(tcpClient.GetStream());

while (!reader.EndOfStream)
    Console.WriteLine(reader.ReadLine());