using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            string HexAlphabet = "0123456789abcdef";
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
            Console.WriteLine("ByteArrayToHexString result ： " + Result.ToString());
            return Result.ToString();
            //below works too, just keep it here for now
            //return BitConverter.ToString(Bytes).Replace("-", " ");
            

        }
        
        //convert string to HEX Byte Array
        //use it when sending the message
        public static byte[] StringToByteArray(String Hex)
        {
            string[] hexValuesSplit = Hex.Split(' ');
            byte[] bytes = new byte[hexValuesSplit.Length];
            /*
            for (int i = 0; i < Hex.Length; i += 3)
                bytes[i / 2] = Convert.ToByte(Hex.Substring(i, 2), 16);
            */
            for (int i = 0; i < hexValuesSplit.Length; i += 1)
            {
                bytes[i] = Convert.ToByte(hexValuesSplit[i], 16);
            }
            return bytes;
            //return Enumerable.Range(0, Hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(Hex.Substring(x, 2), 16)).ToArray();
        }

        //get meaning of Hex String
        public static string GetMeaning(String Hex)
        {
            string value = "";
            Dictionary<string, string> meaning = new Dictionary<string, string>()
            {
                { "01", "第1步" },{ "02", "第2步" },{ "03", "第3步" },{ "04", "第4步" },
                { "05", "第5步" },{ "06", "第6步" },{ "07", "第7步" },{ "19", "9个指令" },
                { "18", "8个指令" },{ "17", "7个指令" },{ "51", "共检测1次" },{ "52", "共检测2次" },
                { "53", "共检测3次" },{ "61", "服务器端准备就绪状态" },{ "62", "服务器端放样品状态" },
                { "63", "服务器端收数状态" },{ "64", "服务器端换点状态" },{ "65", "客户端准备就绪状态" },
                { "66", "客户端激发状态" },{ "67", "客户端发送数据状态" },
                { "6a", "本次为第1次检测" },{ "6b", "本次为第2次检测" },{ "6c", "本次为第3次检测" },
                { "6A", "本次为第1次检测" },{ "6B", "本次为第2次检测" },{ "6C", "本次为第3次检测" },
                { "71", "镨钕合金曲线" },{ "72", "机械手将样品放到稀土分析仪上" },
                { "73", "服务器端询问客户端是否结束分析" },{ "74", "服务器端准备接收数据" },
                { "75", "机械手进行换点操作" },{ "76", "移走样品，刷电极" },
                { "81", "20控样" },{ "82", "25控样" }

            };
            if (meaning.TryGetValue(Hex, out value))
            {
                Console.WriteLine("For key  = {0}, value = {1}.", Hex, value);
            }
            else
            {
                Console.WriteLine("Key  = {0} is not found.", Hex);
                value = Hex;
            }
            return value;
        }

        //write out string in the show box
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
                        Console.WriteLine("Read BeginRead obj.buffer ： " + Encoding.UTF8.GetString(obj.buffer,0, bytes));
                    }
                    else
                    {
                        //write down what you recieved from server
                        ServerDisplay(ByteArrayToHexString(obj.buffer, bytes));
                        LogWriteS("[** 服务器 --> 你 **]："+ ByteArrayToHexString(obj.buffer,bytes));
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (StepText.Text.Length > 0 && LengthText.Text.Length > 0 && TestingText.Text.Length > 0 && SampleNumText.Text.Length > 0 && TotalNumText.Text.Length > 0 && TestNumText.Text.Length > 0 && DataText.Text.Length > 0 && AddComText.Text.Length > 0 )
            //{
                String step = StepText.Text;
                String length = LengthText.Text;
                String test = TestingText.Text;
                String sampNum = SampleNumText.Text;
                String totalNum = TotalNumText.Text;
                String testNum = TestNumText.Text;
                String data = DataText.Text;
                String addi = AddComText.Text;

                if (connected)
                {
                    String final = "操作步骤：" + GetMeaning(step) + "\n指令总长度：" + GetMeaning(length) + "\n校验：" + GetMeaning(test) + "\n样品号：" + GetMeaning(sampNum) + "\n总检测次数：" + GetMeaning(totalNum) + "\n状态/检测次数：" + GetMeaning(testNum) + "\n分析流程/信息交流：" + GetMeaning(addi) + "\n分析数据（数据次序：Pr、Nd、Ti、Mo、W、Al、Si、Fe、C）：\n" + GetMeaning(data);
                    DialogResult result = MessageBox.Show(final, "发送信息到服务器", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        string msg = step + " " + length + " " + test + " " + sampNum + " " + totalNum + " " + testNum + " " + addi + " " + data;
                        Console.WriteLine("SendTextbox msg ： ", Encoding.UTF8.GetBytes(msg));
                        //StringToByteArray
                        //Encoding.UTF8.GetBytes
                        TaskSend(StringToByteArray(msg));
                        //clear everything
                        //StepText.Clear();
                        //LengthText.Clear();
                        //TestingText.Clear();
                        //SampleNumText.Clear();
                        //TotalNumText.Clear();
                        //TestNumText.Clear();
                        //DataText.Clear();
                        //AddComText.Clear();
                        LogWrite(Encoding.UTF8.GetBytes("[** 你 --> 服务器 **]：" + msg));

                    }

                    //this.Close();
                }
                else
                {
                    MessageBox.Show("你还未连接服务器，请先连接服务器。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //this.Close();
                    // Do something  
                }

            //}
        }

        //write out string in the show box
        private void ServerDisplay(string msg = null)
        {
            if (!exit)
            {
                SBox1.Invoke((MethodInvoker)delegate
                {
                    
                    if (msg != null)
                    {
                        string[] msgSplit = msg.Split(' ');
                        Label[] labels = { Slabel11, Slabel12, Slabel13, Slabel14, Slabel15, Slabel16, Slabel17, Slabel18 };
                        for (int i = 0; i < labels.Length; i += 1)
                        {
                            
                            if (i == 3 )
                            {
                                labels[3].Text = GetMeaning(msgSplit[3] + " "+ msgSplit[4] + " " + msgSplit[5] + " " + msgSplit[6] + " " + msgSplit[7] + " " + msgSplit[8]);
                                labels[3].Refresh();
                            }
                            else if (i>=4)
                            {
                                labels[i].Text = GetMeaning(msgSplit[i+5]);
                                labels[i].Refresh();
                            }
                            else 
                            {
                                labels[i].Text = GetMeaning(msgSplit[i]);
                                labels[i].Refresh();
                            }
                                
                         
                        }

                    }
                    
                });
            }
        }

    }
}
