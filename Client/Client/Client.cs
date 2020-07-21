using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private bool connected = false;
        private Thread client = null;
        private struct MyClient
        {
            public TcpClient client;
            public NetworkStream stream;
            public byte[] buffer;
            public StringBuilder data;
            public EventWaitHandle handle;
        };
        private MyClient obj;
        private Task send = null;
        private bool exit = false;

        public Client()
        {
            InitializeComponent();
        }

        private void LogWrite(string msg = null)
        {
            if (!exit)
            {
                logTextBox.Invoke((MethodInvoker)delegate
                {
                    if (msg == null)
                    {
                        logTextBox.Clear();
                    }
                    else
                    {
                        if (logTextBox.Text.Length > 0)
                        {
                            logTextBox.AppendText(Environment.NewLine);
                        }
                        logTextBox.AppendText(DateTime.Now.ToString("HH:mm") + " " + msg);
                    }
                });
            }
        }

        private void Connected(bool status)
        {
            if (!exit)
            {
                connectButton.Invoke((MethodInvoker)delegate
                {
                    connected = status;
                    if (status)
                    {
                        connectButton.Text = "断开";
                        LogWrite("[/ 你已连接 /]");
                    }
                    else
                    {
                        connectButton.Text = "连接";
                        LogWrite("[/ 你已断开 /]");
                    }
                });
            }
        }

        private void Read(IAsyncResult result)
        {
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    LogWrite(string.Format("[/ {0} /]", ex.Message));
                }
            }
            if (bytes > 0)
            {
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                    }
                    else
                    {
                        LogWrite(obj.data.ToString());
                        obj.data.Clear();
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    obj.data.Clear();
                    LogWrite(string.Format("[/ {0} /]", ex.Message));
                    obj.handle.Set();
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
            }
        }

        private void Connection(IPAddress localaddr, int port)
        {
            try
            {
                obj = new MyClient();
                obj.client = new TcpClient();
                obj.client.Connect(localaddr, port);
                obj.stream = obj.client.GetStream();
                obj.buffer = new byte[obj.client.ReceiveBufferSize];
                obj.data = new StringBuilder();
                obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                Connected(true);
                while (obj.client.Connected)
                {
                    try
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), null);
                        obj.handle.WaitOne();
                    }
                    catch (Exception ex)
                    {
                        LogWrite(string.Format("[/ {0} /]", ex.Message));
                    }
                }
                obj.client.Close();
                Connected(false);
            }
            catch (Exception ex)
            {
                LogWrite(string.Format("[/ {0} /]", ex.Message));
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                obj.client.Close();
            }
            else if (client == null || !client.IsAlive)
            {
                bool localaddrResult = IPAddress.TryParse(localaddrMaskedTextBox.Text, out IPAddress localaddr);
                if (!localaddrResult)
                {
                    LogWrite("[/ IP 地址无效，请重新填写 /]");
                }
                bool portResult = int.TryParse(portTextBox.Text, out int port);
                if (!portResult)
                {
                    LogWrite("[/ 端口无效，请重新填写 /]");
                }
                else if (port < 0 || port > 65535)
                {
                    portResult = false;
                    LogWrite("[/ 端口超过正常范围，请重新填写 /]");
                }
                if (localaddrResult && portResult)
                {
                    client = new Thread(() => Connection(localaddr, port))
                    {
                        IsBackground = true
                    };
                    client.Start();
                }
            }
        }

        private void Write(IAsyncResult result)
        {
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.EndWrite(result);
                }
                catch (Exception ex)
                {
                    LogWrite(string.Format("[/ {0} /]", ex.Message));
                }
            }
        }

        private void Send(string msg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), null);
                }
                catch (Exception ex)
                {
                    LogWrite(string.Format("[/ {0} /]", ex.Message));
                }
            }
        }

        private void TaskSend(string msg)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => Send(msg));
            }
            else
            {
                send.ContinueWith(antecendent => Send(msg));
            }
        }

        private void SendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (sendTextBox.Text.Length > 0)
                {
                    string msg = sendTextBox.Text;
                    sendTextBox.Clear();
                    LogWrite("<- 你 -> " + msg);
                    if (connected)
                    {
                        TaskSend(msg);
                    }
                }
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected)
            {
                exit = true;
                obj.client.Close();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            LogWrite();
        }
    }
}
