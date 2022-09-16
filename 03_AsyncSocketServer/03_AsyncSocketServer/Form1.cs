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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAcceptIncomingAsync_Click(object sender, EventArgs e)
        {
            renaldoServer.StartListeningFromIncomingConnection();
        }
    }
}
