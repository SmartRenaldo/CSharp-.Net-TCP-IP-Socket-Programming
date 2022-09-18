using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RenaldoSocketAsync
{
    public class RenaldoSocketClient
    {
        IPAddress serverIpAddress;
        int serverPort;
        TcpClient tcpClient;

        public EventHandler<TextReceivedEventArgs> RaiseTextReceivedEvent;

        public RenaldoSocketClient()
        {
            this.serverIpAddress = null;
            this.serverPort = -1;
            this.tcpClient = null;
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
            if(tcpClient == null)
            {
                tcpClient = new TcpClient();
            }

            try
            {
                await tcpClient.ConnectAsync(ServerIpAddress, ServerPort);
                Console.WriteLine(string.Format("Connected to server IP / PORT: {0} / {1}", serverIpAddress, serverPort));

                ReadDataAsync(tcpClient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected virtual void OnRaiseTextReceivedEvent(TextReceivedEventArgs trea)
        {
            EventHandler<TextReceivedEventArgs> handler = RaiseTextReceivedEvent;

            if (handler != null)
            {
                handler(this, trea);
            }
        }

        private async Task ReadDataAsync(TcpClient tcpClient)
        {
            try
            {
                StreamReader streamReader = new StreamReader(tcpClient.GetStream());
                char[] buffer = new char[128];
                int readByteCount;

                while(true)
                {
                    readByteCount = await streamReader.ReadAsync(buffer, 0, buffer.Length);

                    if (readByteCount <= 0)
                    {
                        Console.WriteLine("Disconnected from server...");
                        tcpClient.Close();
                        break;
                    }

                    Console.WriteLine(String.Format("Received bytes: {0}, message: {1}", readByteCount, new string(buffer)));

                    OnRaiseTextReceivedEvent(new TextReceivedEventArgs(tcpClient.Client.RemoteEndPoint.ToString(), new string(buffer)));

                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public async Task SendToServer(string userInput)
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected)
                {
                    StreamWriter streamWriter = new StreamWriter(tcpClient.GetStream());
                    streamWriter.AutoFlush = true;

                    await streamWriter.WriteAsync(userInput);
                    Console.WriteLine("Data \"{0}\" sent", userInput);
                }
            }
        }

        public void CloseAndDisconnect()
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                }
            }
        }
    }
}
