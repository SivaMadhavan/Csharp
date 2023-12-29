using System.Net.Sockets;
using System.Net;
using System.Text;


IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("localhost");
IPAddress ipAddress = ipHostInfo.AddressList[0];

Console.WriteLine(ipAddress.ToString());

IPEndPoint ipEndPoint = new(ipAddress, 11_000);

using Socket listener = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

listener.Bind(ipEndPoint);
listener.Listen(100);

var handler = await listener.AcceptAsync();
while (true)
{
    var buffer = new byte[1_024];
    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
    var response = Encoding.UTF8.GetString(buffer, 0, received);
    Console.WriteLine($"Socket server received message: \"{response}");

    if (!string.IsNullOrEmpty(response))
    {
        Console.WriteLine($"Socket server received message: \"{response}");
    }
}




