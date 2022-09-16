﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _02_SocketClientStarter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = null;

            try
            {
                Console.WriteLine("Please type a valid server ip address and press enter:");
                string iPAddressStr = Console.ReadLine();
                Console.WriteLine("Please type a valid port number 0 - 65535 and press enter:");
                string portNumberStr = Console.ReadLine();
                int portNumber = 0;

                if (!IPAddress.TryParse(iPAddressStr.Trim(), out iPAddress))
                {
                    Console.WriteLine("Invalid server IP address.");
                    return;
                }

                if (!int.TryParse(portNumberStr.Trim(), out portNumber) || portNumber < 0 || portNumber > 65535)
                {
                    Console.WriteLine("Invalid port number.");
                    return;
                }

                System.Console.WriteLine(string.Format("IPAddress: {0} - Port: {1}", iPAddress.ToString(), portNumber.ToString()));

                client.Connect(iPAddress, portNumber);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}