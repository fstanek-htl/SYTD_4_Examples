using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using SYTD_4_Examples_JSON;

const int ServerPort = 80;

var productDb = new Dictionary<int, Product>
{
    { 123, new Product { Id = 123, Name = "Samsung Elitebook", Price = 1499.9f  } },
    { 3, new Product { Id = 3, Name = "ASUS TUF", Price = 2099.9f } },
};

var tcpListener = new TcpListener(IPAddress.Loopback, ServerPort);
tcpListener.Start();

Console.WriteLine("Server started...");

while (true)
{
    using var tcpClient = tcpListener.AcceptTcpClient();

    using var stream = tcpClient.GetStream();
    var reader = new StreamReader(stream);
    var writer = new StreamWriter(stream);

    // e.g. GET /213 HTTP/1.1
    var requestLine = reader.ReadLine();
    Console.WriteLine(requestLine);

    var path = requestLine.Split(' ')[1];
    var id = Convert.ToInt32(path.Trim('/'));

    if (productDb.TryGetValue(id, out var product))
    {
        // status line
        writer.WriteLine($"HTTP/1.1 200 OK");

        var json = JsonSerializer.Serialize(product);

        // headers
        writer.WriteLine("Content-Type: application/json");
        writer.WriteLine($"Content-Length: {json.Length}");
        writer.WriteLine();

        // body
        writer.WriteLine(json);
        writer.WriteLine();
        writer.WriteLine();

        // Spülen nicht vergessen!
        writer.Flush();
    }
    else
    {
        writer.WriteLine($"HTTP/1.1 404 NOT FOUND");
        writer.WriteLine();
;
        writer.WriteLine();
        writer.WriteLine();

        // Spülen nicht vergessen!
        writer.Flush();
    }

    Console.WriteLine();
}