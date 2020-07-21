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
using System.Text.RegularExpressions;

namespace Client
{
    public partial class client : Form
    {
        private bool connected = false;
        private Thread Tclient = null;
        private struct MyClient
        {
            public TcpClient client;
            public NetworkStream stream;
            public byte[] buffer;
            public EventWaitHandle handle;
        };
        private MyClient obj;
        private Task send = null;
        private bool exit = false;
        public static string DataStore = "";
        public static string DataText = "无数据";
        public static string data = "";
        public client()
        {
            InitializeComponent();
        }

        //calculate checksum
        private string CalculateChecksum(string dataToCalculate)
        {
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);
            int checksum = 0;
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            checksum &= 0xff;
            return checksum.ToString("X2");
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

        //convert large integer string to (split)4-Bytes HEX string
        public static string IntStringToHEXString(String s)
        {
            var result = new List<byte>();
            result.Add(0);
            foreach (char c in s)
            {
                int val = (int)(c - '0');
                for (int i = 0; i < result.Count; i++)
                {
                    int digit = result[i] * 10 + val;
                    result[i] = (byte)(digit & 0x0F);
                    val = digit >> 4;
                }
                if (val != 0)
                    result.Add((byte)val);
            }

            string hex = "";
            foreach (byte b in result)
                hex = "0123456789ABCDEF"[b] + hex;
            //hex = hex.PadLeft(6, '0');
            Console.WriteLine("IntStringToHEXString hex ： " + Regex.Replace(hex.PadLeft(6, '0'), ".{2}", "$0 ").TrimEnd());
            //return hex;
            //.Trim();.TrimEnd();.TrimStart();
            //PadLeft(6, '0')
            return Regex.Replace(hex.PadLeft(6, '0'), ".{2}", "$0 ").TrimEnd();
        }

        //Pre sample number processing
        //split the sample numbers, convert to hex, then put them together as a string
        public static string PreHex(String SampleNumText)
        {
            string[] sampSplit = SampleNumText.Split(' ');
            //String sampNum0 = IntStringToHEXString(sampSplit[0]);
            //String sampNum1 = IntStringToHEXString(sampSplit[1]);
            String sampNum = "";
            foreach (string samp in sampSplit)
            {
                sampNum = sampNum + " " + IntStringToHEXString(samp);
            }
            //String sampNum = sampNum0 + " " + sampNum1;
            //String sampNum = (sampNum0 + sampNum1).TrimEnd();
            return sampNum.Trim().TrimEnd();

        }

        //convert HEX to large integer string
        public static string HexToDecimal(string hex)
        {
            List<int> dec = new List<int> { 0 };   // decimal result

            foreach (char c in hex)
            {
                int carry = Convert.ToInt32(c.ToString(), 16);
                // initially holds decimal value of current hex digit;
                // subsequently holds carry-over for multiplication

                for (int i = 0; i < dec.Count; ++i)
                {
                    int val = dec[i] * 16 + carry;
                    dec[i] = val % 10;
                    carry = val / 10;
                }

                while (carry > 0)
                {
                    dec.Add(carry % 10);
                    carry /= 10;
                }
            }

            var chars = dec.Select(d => (char)('0' + d));
            var cArr = chars.Reverse().ToArray();
            return new string(cArr);
        }

        //get meaning of Hex String
        //or get client cond
        //or get info of data
        public static string GetMeaning(String Hex)
        {
            string value = "";
            Dictionary<string, string> meaning = new Dictionary<string, string>()
            {
                { "01", "第1步" },{ "02", "第2步" },{ "03", "第3步" },{ "04", "第4步" },
                { "05", "第5步" },{ "06", "第6步" },{ "07", "第7步" },{ "08", "第8步" },{ "09", "第9步" },
                { "19", "9个指令(bytes)" },{ "24", "36个指令(bytes)" },
                { "1a", "10个指令(bytes)" },{ "1b", "11个指令(bytes)" },
                { "1A", "10个指令(bytes)" },{ "1B", "11个指令(bytes)" },
                { "51", "共检测1次" },{ "52", "共检测2次" },
                { "53", "共检测3次" },{ "61", "服务器端准备就绪状态" },{ "62", "服务器端放样品状态" },
                { "63", "服务器端收数状态" },{ "64", "服务器端换点状态" },{ "65", "客户端准备就绪状态" },
                { "66", "客户端激发状态" },{ "67", "客户端发送数据状态" },
                { "68", "客户端结束第1次分析，准备第2次检测状态" },{ "69", "客户端结束第2次分析，准备第3次检测状态" },
                { "60", "客户端结束全部检测，数据准备就绪状态" },
                { "6a", "本次为第1次检测" },{ "6b", "本次为第2次检测" },{ "6c", "本次为第3次检测" },
                { "6A", "本次为第1次检测" },{ "6B", "本次为第2次检测" },{ "6C", "本次为第3次检测" },
                { "71", "镨钕合金曲线" },{ "72", "机械手将样品放到稀土分析仪上" },
                { "7a", "服务器端询问客户端是否结束第1次分析" },{ "7b", "服务器端询问客户端是否结束第2次分析" },
                { "7c", "服务器端询问客户端是否结束第3次分析" },
                { "7A", "服务器端询问客户端是否结束第1次分析" },{ "7B", "服务器端询问客户端是否结束第2次分析" },
                { "7C", "服务器端询问客户端是否结束第3次分析" },{ "74", "服务器端准备接收数据" },
                { "75", "机械手进行换点操作" },{ "76", "移走样品，刷电极" },
                { "81", "20控样" },{ "82", "25控样" },
                { "015161", "65" },{  "025162", "66" },{  "035161", "60" },{  "045163", "67" },
                { "055161", "65"},
                { "015261", "65" },{  "025262", "66" },{  "035261", "68" },{  "045264", "66" },
                { "055261", "60"},{ "065263", "67" },{  "075261", "65" },
                { "015361", "65" },{  "025362", "66" },{  "035361", "68" },{  "045364", "66" },
                { "055361", "69"},{ "065364", "66" },{  "075361", "60" },{ "085363", "67" },
                { "095361", "65" },

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

        
        public static string GetLength(String step, String cond)
        {
            string value = "";
            if (step != "01" && step != "02" && step != "03" && cond == "63")
            {
                value = "24";
            }
            else
            {
                value = "19";
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
        private void LogWrite(byte[] msg = null)
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
                        LogWrite(Encoding.UTF8.GetBytes("[** 客户端(你)已连接 **]"));
                    }
                    else
                    {
                        connectButton.Text = "连接";
                        LogWrite(Encoding.UTF8.GetBytes("[** 客户端(你)已断开 **]"));
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
                        Console.WriteLine("Read BeginRead obj.buffer ： " + ByteArrayToHexString(obj.buffer, bytes));
                    }
                    else
                    {
                        //write down what you recieved from server
                        LogWriteS("[** 服务器 --> 客户端(你) **]：" + ByteArrayToHexString(obj.buffer, bytes));
                        ServerDisplay(ByteArrayToHexString(obj.buffer, bytes));

                        Console.WriteLine("Read logWrite obj.buffer ： " + ByteArrayToHexString(obj.buffer, bytes));
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
                        Console.WriteLine("Connection BeginRead obj.buffer ：" + ByteArrayToHexString(obj.buffer, obj.buffer.Length));
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
            else if (Tclient == null || !Tclient.IsAlive)
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
                    Tclient = new Thread(() => Connection(localaddr, port))
                    {
                        IsBackground = true
                    };
                    Tclient.Start();
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
                    Console.WriteLine("Send BeginWrite msg ： " + ByteArrayToHexString(msg, msg.Length));
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
                Console.WriteLine("TaskSend if msg ： " + ByteArrayToHexString(msg, msg.Length));
            }
            else
            {
                send.ContinueWith(antecendent => Send(msg));
                Console.WriteLine("TaskSend else msg ： " + ByteArrayToHexString(msg, msg.Length));
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
                        Console.WriteLine("ServerDisplay msg length ： " + msgSplit.Length.ToString());
                        String Stest = CalculateChecksum(msgSplit[0] + " " + msgSplit[1] + " " + msgSplit[3] + " " + msgSplit[4] + " " + msgSplit[5] + " " + msgSplit[6] + " " + msgSplit[7] + " " + msgSplit[8] + " " + msgSplit[9] + " " + msgSplit[10]);

                        for (int i = 0; i < labels.Length; i += 1)
                        {
                            if (i == 3)
                            {
                                //labels[3].Text = GetMeaning(msgSplit[3] + " "+ msgSplit[4] + " " + msgSplit[5] + " " + msgSplit[6] + " " + msgSplit[7] + " " + msgSplit[8]);
                                labels[3].Text = HexToDecimal(msgSplit[3] + msgSplit[4] + msgSplit[5]) + Environment.NewLine + "（HEX:" + msgSplit[3] + " " + msgSplit[4] + " " + msgSplit[5] + ")";
                                labels[3].Refresh();
                            }
                            else if (i == 4)
                            {
                                labels[i].Text = GetMeaning(msgSplit[i + 2]) + Environment.NewLine + "（HEX:" + msgSplit[i + 2] + ")";
                                labels[i].Refresh();
                            }
                            else if (i == 2)
                            {
                                String check = "";
                                if (Stest == msgSplit[i]) { check = "传输正常"; }
                                else { check = "传输不正常"; }
                                labels[i].Text = check + Environment.NewLine + "HEX运算:" + Stest + "收到:" + msgSplit[i];
                                labels[i].Refresh();
                            }
                            else if (i == 5)
                            {
                                labels[i].Text = GetMeaning(msgSplit[i + 2]) + Environment.NewLine + GetMeaning(msgSplit[i + 3]) + Environment.NewLine + "（HEX:" + msgSplit[i + 2] + " " + msgSplit[i + 3] + ")";
                                labels[i].Refresh();
                            }
                            else if (i > 5)
                            {
                                labels[i].Text = GetMeaning(msgSplit[i + 3]) + Environment.NewLine + "（HEX:" + msgSplit[i + 3] + ")";
                                labels[i].Refresh();
                            }
                            else
                            {
                                labels[i].Text = GetMeaning(msgSplit[i]) + Environment.NewLine + "（HEX:" + msgSplit[i] + ")";
                                labels[i].Refresh();
                            }

                        } //end of for loop

                        String step = msgSplit[0];
                        String length = GetLength(msgSplit[0], msgSplit[7]);
                        String sampNum = msgSplit[3] + " " + msgSplit[4] + " " + msgSplit[5];
                        String totalNum = msgSplit[6];
                        String cond = GetMeaning(msgSplit[0] + msgSplit[6] + msgSplit[7]);
                        String testNum = msgSplit[8];
                        String test = CalculateChecksum(step + " " + length + " " + sampNum + " " + totalNum + " " + cond + " " + testNum);
                        string clientmsg = step + " " + length + " " + test + " " + sampNum + " " + totalNum + " " + cond + " " + testNum;
                        String NewData = "";
                        String x = "";
                        

                        if (step == "05")
                        {
                            x = "第二次";
                        }
                        if (step == "07")
                        {
                            x = "第三次";
                        }

                        if (cond == "68" || cond == "69" || cond == "60")
                        {
                            //System.Threading.Thread.Sleep(10000);
                            if (cond == "60")
                            {
                                NewData = "32.4897 51.2486 37.8932 48.9751 24.8637 89.3248 97.5124 86.3789 32.4897";
                            }
                            if (cond == "68")
                            {
                                NewData = "12.4539 93.1587 28.6746 95.1234 46.7895 61.3957 45.7913 73.9628 85.7942";
                            }
                            if (cond == "69")
                            {
                                NewData = "45.2319 78.2123 90.6743 54.8472 37.1237 91.3697 84.3159 43.1467 23.0351";
                            }

                            string[] NewDataSplit = NewData.Split(' ');
                            float[] NewDataint = Array.ConvertAll(NewDataSplit, s => float.Parse(s));

                            if (DataText == "无数据")
                            {
                                DataStore = NewData;
                                DataText = "已得到第一次检测元素分析值。";
                                data = "已准备";
                                Console.WriteLine("ServerDisplay NewData DataText ： " + DataText);
                                Console.WriteLine("ServerDisplay NewData DataStore ： " + DataStore);
                            }
                            else
                            {
                                string[] DataSplit = DataStore.Split(' ');
                                float[] DataSplitint = Array.ConvertAll(DataSplit, s => float.Parse(s));
                                String avg = "";
                                for (int i = 0; i < NewDataint.Length; i++)
                                {
                                    //Math.Round(111.3547198, 4)
                                    avg = avg + " " + Math.Round(((NewDataint[i] + DataSplitint[i]) / 2), 4).ToString().PadRight(6, '0');
                                }
                                DataStore = avg.Trim().TrimEnd();
                                DataText = "已得到" + x + "检测元素分析值。";
                                data = "已准备";
                                Console.WriteLine("ServerDisplay 686960 DataText ： " + DataText);
                                Console.WriteLine("ServerDisplay 686960 DataStore ： " + DataStore);
                            }

                        }
                        else if (cond == "67")
                        {
                            Console.WriteLine("ServerDisplay 67 DataText ： " + DataText);
                            DataText = DataStore;
                            data = PreHex(DataText.Replace(".", ""));
                            test = CalculateChecksum(step + " " + length + " " + sampNum + " " + totalNum + " " + cond + " " + testNum + " " + data);

                            clientmsg = step + " " + length + " " + test + " " + sampNum + " " + totalNum + " " + cond + " " + testNum + " " + data;
                            DataStore = "";
                        }
                        else 
                        {
                            DataText = "无数据";
                            data = "";
                        }

                        Console.WriteLine("ServerDisplay clientmsg ： " + clientmsg);

                        TaskSend(StringToByteArray(clientmsg));
                        LogWrite(Encoding.UTF8.GetBytes("[** 客户端(你) --> 服务器 **]：" + clientmsg));
                        Clabel1.Text = GetMeaning(msgSplit[0]) + Environment.NewLine + "（HEX:" + msgSplit[0] + ")";
                        Clabel1.Refresh();
                        Clabel2.Text = GetMeaning(length) + Environment.NewLine + "（HEX:" + length + ")";
                        Clabel2.Refresh();
                        Clabel3.Text = HexToDecimal(msgSplit[3] + msgSplit[4] + msgSplit[5]) + Environment.NewLine + "（HEX:" + msgSplit[3] + " " + msgSplit[4] + " " + msgSplit[5] + ")";
                        Clabel3.Refresh();
                        Clabel4.Text = GetMeaning(msgSplit[6]) + Environment.NewLine + "（HEX:" + msgSplit[6] + ")";
                        Clabel4.Refresh();
                        Clabel5.Text = GetMeaning(cond) + Environment.NewLine + GetMeaning(testNum) + Environment.NewLine + "（HEX:" + cond + " " + testNum + ")";
                        Clabel5.Refresh();
                        Clabel6.Text = DataText + Environment.NewLine + "（HEX:" + data + ")";
                        Clabel6.Refresh();
                        Clabel7.Text = test;
                        Clabel7.Refresh();
                        

                    }//end of if (msg != null)
                }); //end of SBox1.Invoke((MethodInvoker)delegate  
            }//end of if (!exit)

        }// end of private void ServerDisplay(string msg = null)
    }
}
