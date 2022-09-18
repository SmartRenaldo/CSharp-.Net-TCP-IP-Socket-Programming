using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RenaldoSocketAsync
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public string NewClient { get; set; }

        public ClientConnectedEventArgs(string newClient)
        {
            NewClient = newClient;
        }
    }

    public class TextReceivedEventArgs : EventArgs
    {
        public string Client { get; set; }
        public string TextReceived { get; set; }

        public TextReceivedEventArgs(string client, string textReceived)
        {
            Client = client;
            TextReceived = textReceived;
        }
    }
}