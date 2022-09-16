using System;
using System.Collections.Generic;
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

            System.Diagnostics.Debug.WriteLine(string.Format("IP Address: {0}; Port: {1}", iPAddress.ToString(), port.ToString()));

            listener = new TcpListener(iPAddress, port);
            listener.Start();
            var returnedByAccept = await listener.AcceptTcpClientAsync();

            System.Diagnostics.Debug.WriteLine("Client connected successfully " + returnedByAccept.ToString());
        }
    }
}
