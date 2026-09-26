using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any,4000);

		try
		{
			listener.Start();

			TcpClient tcpClient = listener.AcceptTcpClient();

			NetworkStream networkStream = tcpClient.GetStream();

			byte[] buffer = new byte[512];

			StringBuilder message = new StringBuilder();

            do
            {
				message.Append(Encoding.ASCII.GetString(buffer,0,networkStream.Read(buffer,0,buffer.Length)));
			} while (networkStream.DataAvailable);

			Console.WriteLine(message);

			if (message.ToString().ToLower() == "date")
			{
				networkStream.Write(Encoding.ASCII.GetBytes(DateOnly.FromDateTime(DateTime.Now).ToString()));
			}
			else if (message.ToString().ToLower() == "time")
            {
                networkStream.Write(Encoding.ASCII.GetBytes(TimeOnly.FromDateTime(DateTime.Now).ToString()));
            }

        }
		finally
		{
			listener.Stop();
		}


    }
}