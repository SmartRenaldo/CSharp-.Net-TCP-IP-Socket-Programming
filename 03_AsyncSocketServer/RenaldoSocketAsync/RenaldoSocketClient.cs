using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RenaldoSocketAsync
{
    public class RenaldoSocketClient
    {
        IPAddress serverIpAddress;
        int serverPort;
        TcpClient TcpClient;

        public RenaldoSocketClient()
        {
            this.serverIpAddress = null;
            this.serverPort = -1;
            this.TcpClient = null;
        }

        public IPAddress ServerIpAddress
        {
            get { return this.serverIpAddress; }
        }

        public int ServerPort
        {
            get { return this.serverPort; }
        }

        public bool SetServerIpAddress(string _IpAddressServer)
        {
            IPAddress _IpAddress = null;

            if (!IPAddress.TryParse(_IpAddressServer, out _IpAddress))
            {
                Console.WriteLine("Invalid server IP supplied...");

                return false;
            }

            serverIpAddress = _IpAddress;

            return true;
        }

        public bool SetPort(string _ServerPort)
        {
            int portNum = 0;

            if(!int.TryParse(_ServerPort, out portNum) || portNum < 0 || portNum > 65535)
            {
                Console.WriteLine("Invalid port number supplied...");

                return false;
            }

            serverPort = portNum;

            return true;
        }

        public async Task ConnectToServer()
        {
            if(TcpClient == null)
            {
                TcpClient = new TcpClient();
            }

            try
            {
                await TcpClient.ConnectAsync(ServerIpAddress, ServerPort);
                Console.WriteLine(string.Format("Connected to server IP / PORT: {0} / {1}", serverIpAddress, serverPort));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
