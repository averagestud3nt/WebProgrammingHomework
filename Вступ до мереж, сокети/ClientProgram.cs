using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpClient tcpClient = new TcpClient("127.0.0.1",4000);

        NetworkStream networkStream = tcpClient.GetStream();

        Console.WriteLine("Tell the server what to return");

        string message = Console.ReadLine();

        byte[] buffer = Encoding.ASCII.GetBytes(message);

        networkStream.Write(buffer);

        buffer = new byte[512];

        StringBuilder recievedMessage = new StringBuilder();

        do
        {
            recievedMessage.Append(Encoding.ASCII.GetString(buffer, 0, networkStream.Read(buffer, 0, buffer.Length)));
        } while (networkStream.DataAvailable);

        Console.WriteLine(recievedMessage);
    }
}