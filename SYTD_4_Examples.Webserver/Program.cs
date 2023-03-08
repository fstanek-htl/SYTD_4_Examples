using System.Net;
using System.Net.Sockets;

const int ServerPort = 80;
const string ServerDirectory = @"C:\temp\www";

var tcpListener = new TcpListener(IPAddress.Loopback, ServerPort);
tcpListener.Start();

Console.WriteLine("Server started...");

while (true)
{
    using var tcpClient = tcpListener.AcceptTcpClient();
    Console.WriteLine($"[Request received from {tcpClient.Client.RemoteEndPoint}]");

    using var stream = tcpClient.GetStream();

    var url = ReceiveRequest(stream);
    var fileName = ServerDirectory + url;

    if (File.Exists(fileName))
    {
        var html = File.ReadAllText(fileName);
        SendResponse(stream, 600, "OK", html);
    }
    else
    {
        SendResponse(stream, 404, "Not found", "<p>Error</p>");
    }

    Console.WriteLine();
}

string ReceiveRequest(Stream stream)
{
    var reader = new StreamReader(stream);

    // e.g. GET /images/logo.png HTTP/1.1
    var requestLine = reader.ReadLine();
    Console.WriteLine(requestLine);

    var url = requestLine.Split(' ')[1];

    if (url == "/")
        url += "index.html";

    // TODO read headers

    return url;
}

void SendResponse(Stream stream, int code, string message, string content)
{
    var writer = new StreamWriter(stream);

    // status line
    writer.WriteLine($"HTTP/1.1 {code} {message}");

    // headers
    writer.WriteLine("Content-Type: text/html");
    writer.WriteLine($"Content-Length: {content.Length}");
    writer.WriteLine();

    // body
    writer.WriteLine(content);
    writer.WriteLine();
    writer.WriteLine();

    // Spülen nicht vergessen!
    writer.Flush();
}