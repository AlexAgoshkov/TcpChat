using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Name");
            string name = Console.ReadLine();

            int port = 19539;
            string serverAddr = "127.0.0.1";
            TcpClient client = new TcpClient(serverAddr, port);
            byte[] data = Encoding.ASCII.GetBytes(name);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
           
            try
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        string message = Console.ReadLine();
                        data = Encoding.ASCII.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                    }
                    if (stream.DataAvailable)
                    {
                        var responseData = new byte[1024];
                        int bytesRead = stream.Read(responseData, 0, responseData.Length);
                        var responseMessage = Encoding.ASCII.GetString(responseData, 0, bytesRead);
                        Console.WriteLine(responseMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exp {ex}");
            }
        }
    }
}
