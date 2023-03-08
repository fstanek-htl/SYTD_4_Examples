/*
 * Einfacher TCP client
 * Liest Text zeilenweise ein und schickt ihn via TCP an das angegebenen Ziel.
 */

using System.Net.Sockets;
using System.Text;

// TCP Verbindung zum angebenen Host und Port
// Bei leerem Konstruktor kann alternativ mittels Connect(...) verbunden werden
var tcpClient = new TcpClient("localhost", 8085);

// Networkstream = bidirektionaler Kanal zum Datenaustausch zwischen Client und Server
// Er kann nur Bytes lesen und schreiben.
using var stream = tcpClient.GetStream();

// Wir verwenden StreamWriter, weil er Strings in Bytes übersetzt und diese zeilenweise auf den Stream schreiben kann
// ACHTUNG: Client und Server müssen selbes Encoding (siehe Konstruktor) verwenden
using var writer = new StreamWriter(stream);
// new StreamWriter(stream, Encoding.Default);
// new StreamWriter(stream, Encoding.ASCII);
// new StreamWriter(stream, Encoding.UTF8);
// usw.

while (true)
{
    var line = Console.ReadLine();

    // Text wird in Bytes umgewandelt und in Puffer geschrieben
    writer.WriteLine(line);

    // Schreibt die Daten vom Puffer in den Stream (ähnlich Commit bei Git), erst dann wird übers Netzwerk gesendet
    writer.Flush();
}