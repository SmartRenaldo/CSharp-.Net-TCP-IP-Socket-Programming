using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _01_SocketServersStarter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress iPAddress = IPAddress.Any;

                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 33000);

                socket.Bind(iPEndPoint);

                socket.Listen(5);

                Console.WriteLine("About to accept a connection...");

                Socket client = socket.Accept();

                Console.WriteLine("Client connected... " + client.ToString()
                    + " - IP End Point: " + client.RemoteEndPoint.ToString());

                byte[] buffer = new byte[128];

                int numOfReceivedBytes = 0;

                while (true)
                {
                    numOfReceivedBytes = client.Receive(buffer);

                    Console.WriteLine("Number of received bytes: " + numOfReceivedBytes);

                    string receivedText = Encoding.ASCII.GetString(buffer, 0, numOfReceivedBytes);

                    Console.WriteLine("Data: " + receivedText);

                    client.Send(buffer);

                    if (receivedText == "e")
                    {
                        break;
                    }

                    Array.Clear(buffer, 0, buffer.Length);

                    numOfReceivedBytes = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
