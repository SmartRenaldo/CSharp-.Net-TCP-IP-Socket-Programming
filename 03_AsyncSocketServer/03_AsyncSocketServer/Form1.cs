using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RenaldoSocketAsync;

namespace _03_AsyncSocketServer
{
    public partial class Form1 : Form
    {
        RenaldoSocketServer renaldoServer;

        public Form1()
        {
            InitializeComponent();
            renaldoServer = new RenaldoSocketServer();
            renaldoServer.RaiseClientConnectedEvent += HandleClientConnected;
            renaldoServer.RaiseTextReceivedEvent += HandleTextReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAcceptIncomingAsync_Click(object sender, EventArgs e)
        {
            renaldoServer.StartListeningFromIncomingConnection();
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            renaldoServer.SendToAll(messageBox.Text.Trim());
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            renaldoServer.StopServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            renaldoServer.StopServer();
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs ccea)
        {
            txtCounsel.AppendText(String.Format("{0} - New client connected: {1}{2}",
                DateTime.Now, ccea.NewClient, Environment.NewLine));
        }

        void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            txtCounsel.AppendText(String.Format("{0} - Received message from {1}: {2}{3}",
                DateTime.Now, trea.Client, trea.TextReceived, Environment.NewLine));
        }
    }
}
