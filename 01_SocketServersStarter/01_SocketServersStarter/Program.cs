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
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = IPAddress.Any;

            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 33000);

            socket.Bind(iPEndPoint);

            socket.Listen(5);

            socket.Accept();
        }
    }
}
