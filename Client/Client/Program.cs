using System.Net;
using System.Net.Sockets;
using System.Text;


IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("localhost");
IPAddress ipAddress = ipHostInfo.AddressList[0];

IPEndPoint ipEndPoint = new(ipAddress, 11_000);

using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);



await client.ConnectAsync(ipEndPoint);
while (true)
{
    Console.WriteLine("Client : ");
    var message = Console.ReadLine();
    if (message == "STOP")
    {
        break;
    }
    var messageBytes = Encoding.UTF8.GetBytes(message);
    _ = await client.SendAsync(messageBytes, SocketFlags.None);
    Console.WriteLine($"Socket client sent message: \"{message}\"");
    string? stop = Console.ReadLine();



}

client.Shutdown(SocketShutdown.Both);