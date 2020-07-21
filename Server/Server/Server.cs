using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private bool active = false;
        private Thread listener = null;
        private long id = 0;
        private struct MyClient
        {
            public long id;
            public TcpClient client;
            public NetworkStream stream;
            public byte[] buffer;
            public StringBuilder data;
            public EventWaitHandle handle;
        };
        private ConcurrentDictionary<long, MyClient> list = new ConcurrentDictionary<long, MyClient>();
        private Task send = null;
        private Thread disconnect = null;
        private bool exit = false;

        public Server()
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

        private void Active(bool status)
        {
            if (!exit)
            {
                startButton.Invoke((MethodInvoker)delegate
                {
                    active = status;
                    if (status)
                    {
                        startButton.Text = "断开";
                        LogWrite("[/ 服务器 开始 /]");
                    }
                    else
                    {
                        startButton.Text = "开始";
                        LogWrite("[/ 服务器 停止 /]");
                    }
                });
            }
        }

        private void Read(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
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
                        string msg = string.Format("<- 客户端 {0} -> {1}", obj.id, obj.data);
                        LogWrite(msg);
                        TaskSend(msg, obj.id);
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

        private void Connection(MyClient obj)
        {
            list.TryAdd(obj.id, obj);
            string msg = string.Format("[/ 客户端 {0} 连接 /]", obj.id);
            LogWrite(msg);
            TaskSend(msg, obj.id);
            while (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                    obj.handle.WaitOne();
                }
                catch (Exception ex)
                {
                    LogWrite(string.Format("[/ {0} /]", ex.Message));
                }
            }
            obj.client.Close();
            msg = string.Format("[/ 客户端 {0} 断开 /]", obj.id);
            LogWrite(msg);
            TaskSend(msg, obj.id);
            list.TryRemove(obj.id, out MyClient tmp);
        }

        private void Listener(IPAddress localaddr, int port)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(localaddr, port);
                listener.Start();
                Active(true);
                while (active)
                {
                    if (listener.Pending())
                    {
                        try
                        {
                            MyClient obj = new MyClient();
                            obj.id = id;
                            obj.client = listener.AcceptTcpClient();
                            obj.stream = obj.client.GetStream();
                            obj.buffer = new byte[obj.client.ReceiveBufferSize];
                            obj.data = new StringBuilder();
                            obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                            Thread th = new Thread(() => Connection(obj))
                            {
                                IsBackground = true
                            };
                            th.Start();
                            id++;
                        }
                        catch (Exception ex)
                        {
                            LogWrite(string.Format("[/ {0} /]", ex.Message));
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
                Active(false);
            }
            catch (Exception ex)
            {
                LogWrite(string.Format("[/ {0} /]", ex.Message));
            }
            finally
            {
                if (listener != null)
                {
                    listener.Server.Close();
                }
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (active)
            {
                active = false;
            }
            else if (listener == null || !listener.IsAlive)
            {
                bool localaddrResult = IPAddress.TryParse(localaddrMaskedTextBox.Text, out IPAddress localaddr);
                if (!localaddrResult)
                {
                    LogWrite("[/ IP 地址无效，请重新填写  /]");
                }
                bool portResult = int.TryParse(portTextBox.Text, out int port);
                if (!portResult)
                {
                    LogWrite("[/ 端口无效，请重新填写  /]");
                }
                else if (port < 0 || port > 65535)
                {
                    portResult = false;
                    LogWrite("[/ 端口超过正常范围，请重新填写 /]");
                }
                if (localaddrResult && portResult)
                {
                    listener = new Thread(() => Listener(localaddr, port))
                    {
                        IsBackground = true
                    };
                    listener.Start();
                }
            }
        }

        private void Write(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
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

        private void Send(string msg, long id = -1)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            foreach (KeyValuePair<long, MyClient> obj in list)
            {
                if (id != obj.Value.id && obj.Value.client.Connected)
                {
                    try
                    {
                        obj.Value.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj.Value);
                    }
                    catch (Exception ex)
                    {
                        LogWrite(string.Format("[/ {0} /]", ex.Message));
                    }
                }
            }
        }

        private void TaskSend(string msg, long id = -1)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => Send(msg, id));
            }
            else
            {
                send.ContinueWith(antecendent => Send(msg, id));
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
                    LogWrite("<- 服务器 (你) -> " + msg);
                    TaskSend("<- 服务器 -> " + msg);
                }
            }
        }

        private void Disconnect()
        {
            foreach (KeyValuePair<long, MyClient> obj in list)
            {
                obj.Value.client.Close();
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (disconnect == null || !disconnect.IsAlive)
            {
                disconnect = new Thread(() => Disconnect())
                {
                    IsBackground = true
                };
                disconnect.Start();
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
            active = false;
            if (disconnect == null || !disconnect.IsAlive)
            {
                disconnect = new Thread(() => Disconnect())
                {
                    IsBackground = true
                };
                disconnect.Start();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            LogWrite();
        }

    }
}
