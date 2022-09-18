using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RenaldoSocketAsync
{
    public class RenaldoSocketServer
    {
        IPAddress iPAddress;
        int port;
        TcpListener listener;
        bool continuing { get; set; }
        List<TcpClient> clients;

        public RenaldoSocketServer()
        {
            clients = new List<TcpClient>();
        }

        public async void StartListeningFromIncomingConnection(IPAddress iPAddress = null, int port = 33000)
        {
            if (iPAddress == null)
            {
                iPAddress = IPAddress.Any;
            }

            if (port < 0 || port > 65535)
            {
                port = 33000;
            }

            this.iPAddress = iPAddress;
            this.port = port;

            Debug.WriteLine(string.Format("IP Address: {0}; Port: {1}", iPAddress.ToString(), port.ToString()));

            listener = new TcpListener(iPAddress, port);
        
            try
            {
                listener.Start();
                continuing = true;

                while (continuing)
                {
                    var returnedByAccept = await listener.AcceptTcpClientAsync();
                    clients.Add(returnedByAccept);
                    Debug.WriteLine(String.Format("Client connected successfully, count: {0} - {1}",
                        clients.Count, returnedByAccept.Client.RemoteEndPoint));
                    TakeCareOfTcpClient(returnedByAccept);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private async void TakeCareOfTcpClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buffer = new char[64];

                while(continuing)
                {
                    Debug.WriteLine("Ready to read...");
                    int num = await reader.ReadAsync(buffer, 0, buffer.Length);

                    if (num == 0)
                    {
                        RemoveClient(paramClient);
                        Debug.WriteLine("Socket disconnected...");
                        break;
                    }

                    string text = new string(buffer);

                    Debug.WriteLine("Received text: " + text);

                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            catch (Exception e)
            {
                RemoveClient(paramClient);
                Debug.WriteLine(e.ToString());
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if(clients.Contains(paramClient))
            {
                clients.Remove(paramClient);
                Debug.WriteLine(String.Format("Client removed. count: {0}", clients.Count));
            }
        }

        public async void SendToAll(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            
            try
            {
                byte[] bufferMsg = Encoding.ASCII.GetBytes(message);

                foreach(TcpClient client in clients)
                {
                    client.GetStream().WriteAsync(bufferMsg, 0, bufferMsg.Length);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                if (listener != null)
                {
                    listener.Stop();
                }

                foreach (TcpClient client in clients)
                {
                    client.Close();
                }

                clients.Clear();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
