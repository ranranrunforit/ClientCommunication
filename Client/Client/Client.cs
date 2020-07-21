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
            //public StringBuilder data;
            public EventWaitHandle handle;
        };
        private MyClient obj;
        private Task send = null;
        private bool exit = false;

        public Client()
        {
            InitializeComponent();
        }

        //used it when reading recieved data from server
        public static string ByteArrayToHexString(byte[] Bytes, int length)
        {
            StringBuilder Result = new StringBuilder(length * 2);
            string HexAlphabet = "0123456789ABCDEF";
            /*
             foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }
             */

            for (int i = 0; i < length; i += 1)
            {
                Result.Append(HexAlphabet[(int)(Bytes[i] >> 4)]);
                Result.Append(HexAlphabet[(int)(Bytes[i] & 0xF)]);
                Result.Append(' ');
            }

            return Result.ToString();
            //below works too, just keep it here for now
            //return BitConverter.ToString(Bytes).Replace("-", " ");
            

        }

        //convert string to HEX Byte Array
        public static byte[] StringToByteArray(String Hex)
        {
            byte[] bytes = new byte[Hex.Length / 2];
            for (int i = 0; i < Hex.Length; i += 3)
                bytes[i / 2] = Convert.ToByte(Hex.Substring(i, 2), 16);
            return bytes;
        }
        private void LogWriteS(string msg = null)
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

        /*Write conversation to the log*/
        private void LogWrite(byte[] msg =null)
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
                        logTextBox.AppendText(DateTime.Now.ToString("HH:mm") + " " + Encoding.UTF8.GetString(msg));
                    }
                });
            }
        }

        /*the connect button control*/
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
                        LogWrite(Encoding.UTF8.GetBytes("[** 你已连接 **]"));
                    }
                    else
                    {
                        connectButton.Text = "连接";
                        LogWrite(Encoding.UTF8.GetBytes("[** 你已断开 **]"));
                    }
                });
            }
        }

        /*Read data from server
         when server send data to client:
         Read function will process the data    
         */
        private void Read(IAsyncResult result)
        {
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    //read the last byte
                    bytes = obj.stream.EndRead(result);
                    Console.WriteLine("Read EndRead bytes ： " + bytes.ToString());
                }
                catch (Exception ex)
                {
                    LogWrite(Encoding.UTF8.GetBytes(string.Format("[** {0} **]", ex.Message)));
                }
            }
            if (bytes > 0)
            {
                //obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        //actually start to reading and keep reading incase these still left
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                        Console.WriteLine("Read BeginRead obj.buffer ： " + Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                    }
                    else
                    {
                        //write down what you recieved from server
                        LogWriteS(ByteArrayToHexString(obj.buffer,bytes));
                        Console.WriteLine("Read logWrite obj.buffer ： " + Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                        //Console.WriteLine("Read logWrite bytes ： " + bytes.ToString());
                        //Console.WriteLine("Read logWrite obj.buffer.Length ： " + obj.buffer.Length.ToString());
                        //obj.data.Clear();
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    //obj.data.Clear();
                    LogWrite(Encoding.UTF8.GetBytes(string.Format("[** {0} **]", ex.Message)));
                    obj.handle.Set();
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
            }
        }

        /*Create a connection with server from address
         this will read the data that send from server side*/
        private void Connection(IPAddress localaddr, int port)
        {
            try
            {
                obj = new MyClient();
                obj.client = new TcpClient();
                obj.client.Connect(localaddr, port);
                obj.stream = obj.client.GetStream();
                obj.buffer = new byte[obj.client.ReceiveBufferSize];
                //obj.data = new StringBuilder();
                obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                Connected(true);
                while (obj.client.Connected)
                {
                    try
                    {
                        //this will start to read, which call Read function
                        Console.WriteLine("Connection BeginRead obj.buffer ：" + Encoding.UTF8.GetString(obj.buffer));
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), null);
                        obj.handle.WaitOne();//for thread safe
                        
                    }
                    catch (Exception ex)
                    {
                        LogWrite(Encoding.UTF8.GetBytes(string.Format("[** {0} **]", ex.Message)));
                    }
                }
                obj.client.Close();
                Connected(false);
            }
            catch (Exception ex)
            {
                LogWrite(Encoding.UTF8.GetBytes(string.Format("[** {0} **]", ex.Message)));
            }
        }

        /*ConnectButton_Click event*/
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
                    LogWrite(Encoding.UTF8.GetBytes("[ IP 地址无效，请重新填写  ]"));
                }
                bool portResult = int.TryParse(portTextBox.Text, out int port);
                if (!portResult)
                {
                    LogWrite(Encoding.UTF8.GetBytes("[  端口无效，请重新填写  ]"));
                }
                else if (port < 0 || port > 65535)
                {
                    portResult = false;
                    LogWrite(Encoding.UTF8.GetBytes("[  端口超过正常范围(0，65535)，请重新填写  ]"));
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

        /*Write out message*/
        private void Write(IAsyncResult result)
        {
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.EndWrite(result);
                    //Console.WriteLine("Write result ： " + result.ToString());
                }
                catch (Exception ex)
                {
                    LogWrite(Encoding.UTF8.GetBytes(string.Format("[** {0} **]", ex.Message)));
                }
            }
        }

        /*Send message*/
        private void Send(byte[] msg)
        {
            //StringToByteArray
            //Encoding.UTF8.GetBytes
            //byte[] buffer = msg;
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginWrite(msg, 0, msg.Length, new AsyncCallback(Write), null);
                    Console.WriteLine("Send BeginWrite msg ： " + Encoding.UTF8.GetString(msg));
                }
                catch (Exception ex)
                {
                    LogWrite(Encoding.UTF8.GetBytes(string.Format("[** {0} **]", ex.Message)));
                }
            }
        }
        /*Send the whole task message*/
        private void TaskSend(byte[] msg)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => Send(msg));
                Console.WriteLine("TaskSend if msg ： " + Encoding.UTF8.GetString(msg));
            }
            else
            {
                send.ContinueWith(antecendent => Send(msg));
                Console.WriteLine("TaskSend else msg ： " + Encoding.UTF8.GetString(msg));
            }
        }

        /*TextBox send event*/
        private void SendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (sendTextBox.Text.Length > 0)
                {
                    string msg = sendTextBox.Text;
                    Console.WriteLine("SendTextbox msg ： " , msg);
                    sendTextBox.Clear();
                    LogWrite(Encoding.UTF8.GetBytes("[** 你 --> 服务器 **]：" + msg));
                    if (connected)
                    {
                        //StringToByteArray
                        //Encoding.UTF8.GetBytes
                        TaskSend(StringToByteArray(msg));
                    }
                }
            }
        }

        /*Closed the client side*/
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected)
            {
                exit = true;
                obj.client.Close();
            }
        }
        /*Clear button click*/
        private void ClearButton_Click(object sender, EventArgs e)
        {
            LogWrite();
        }
    }
}
