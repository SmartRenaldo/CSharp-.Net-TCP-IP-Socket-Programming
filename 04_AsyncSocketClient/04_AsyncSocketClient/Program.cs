using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenaldoSocketAsync;

namespace _04_AsyncSocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RenaldoSocketClient client = new RenaldoSocketClient();
            Console.WriteLine("Please type a valid server IP address and press enter:");

            string strIpAddress = Console.ReadLine();

            Console.WriteLine("Please supply a valid port number 0 - 65536 and press enter:");
            string strPort = Console.ReadLine();

            if (!client.SetServerIpAddress(strIpAddress) || !client.SetPort(strPort))
            {
                Console.WriteLine(string.Format("Wrong IP address ot port number supplied - {0} - {1} ",
                    strIpAddress, strPort));
                Console.ReadKey();

                return;
            }

            client.ConnectToServer();

            string userInput = null;
            bool isContinuing;

            do
            {
                userInput = Console.ReadLine();
                isContinuing = !userInput.Trim().ToUpper().Equals("EXIT");

                if (isContinuing)
                {
                    client.SendToServer(userInput);
                }
                else
                {
                    client.CloseAndDisconnect();
                }
            } while (isContinuing);
        }
    }
}
