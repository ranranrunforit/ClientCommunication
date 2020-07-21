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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Timers;
using System.IO;
using System.Web.Hosting;
using System.Reflection;

namespace Client
{
    /*
     A simpler version of the Logger class which does the thing, in completely Threadsafe way.
    One main thing to notice here is, no TextWriter.Synchronized is required for thread safety, 
    as we are writing the file within a proper lock.
    */
    public static class Logger
    {
        static readonly object _locker = new object();

        public static void Log(string logMessage)
        {
            try
            {
                /*
                var logFilePath = Path.Combine(@"C:\Users\zhouc\Desktop\", "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                WriteToLog(logMessage, logFilePath);
                */
                
                //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                //yourapp_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                //for example，in my testing app is 
                //C:\Users\zhouc\Desktop\C#\chatchat\TestingDLL\WindowsFormsApp1\bin\Debug\Logs\Log_2020-07-07.txt

                string FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Logs\");
                if (!Directory.Exists(FilePath))
                {
                    try
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                    }
                }
                string logFile = "Log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string logFilePath = Path.Combine(FilePath, logFile);
                WriteToLog(logMessage, logFilePath);
                
            }
            catch (Exception ex)
            {
                //log log-exception somewhere else if required!
                Console.WriteLine(string.Format("[** {0} **]", ex.Message));
            }
        }

        static void WriteToLog(string logMessage, string logFilePath)
        {
            // only one thread can own this lock, so other threads
            // entering this method will wait here until lock is
            // available.
            lock (_locker)
            {
                
                File.AppendAllText(logFilePath,
                        string.Format("Logged on: {1} at: {2}{0}Message: {3}{0}--------------------{0}",
                        Environment.NewLine, DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(), logMessage));
                
                /*
                StreamWriter sw;
                sw = File.AppendText(logFilePath);
                sw.WriteLine(string.Format("Logged on: {1} at: {2}{0}Message: {3}{0}--------------------{0}",
                        Environment.NewLine, DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(), logMessage));
                sw.Close();

                sw.Dispose();
                */
            }
        }
    }

    //handle when connected to Server
    public class ConnectedEventArgs : EventArgs
    {
        public bool Connect { get; set; }
    }

    //handle when server side order changed
    public class ServersideEventArgs : EventArgs
    {
        public string Serverside { get; set; }
    }

    public class ClientsideEventArgs : EventArgs
    {
        public string Clientside { get; set; }
    }
    public partial class client
    {
        public bool connected = false;
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
        private string _DataAvg = "";//original data after average data
        private string data = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";//post-process average data
        private string _grade = "00";
        private string _quali = "00";//qualification
        private bool Ready = false;
        private string order = "";//don't acces this variable: it's just for storing the value in it
        private string serverside = "";
        private string clientside = "";
        /* below is for timer && stopwatch*/
        private readonly Stopwatch stopwatch = new Stopwatch();
        private TimeSpan ts;
        private static System.Timers.Timer aTimer;
        public bool spark = false;
        public bool newStart = false;
        public bool RequireData = false;
        public string state = "";
        public string ctrlSamp = "";
        public string SampleNum = "";

        // A read-write instance property:
        public string DataAvg
        {
            get => _DataAvg;
            set => _DataAvg = value;
        }

        public string grade
        {
            get => _grade;
            set => _grade = value;
        }
        public string quali
        {
            get => _quali;
            set => _quali = value;
        }

        //replyOrder property contains Ready.
        //reply Order when it's ready.
        //once replied the server side，start a timer and a stopwatch
        public bool replyOrder //this variable should be used by all your code
        {
            get { return Ready; }
            set
            {
                Ready = value;
                if (Ready == true) 
                {
                    Reply(order);
                    Ready = false;
                    stopwatch.Reset();
                    stopwatch.Start();
                    ts = stopwatch.Elapsed;

                    // Create a timer and set a five second interval.
                    aTimer = new System.Timers.Timer();
                    aTimer.Interval = 120000;

                    // Hook up the Elapsed event for the timer. 
                    aTimer.Elapsed += OnTimedEvent;

                    // Have the timer fire repeated events (true is the default)
                    //**set it to false if you don't want it fire events after first 2 minutes**
                    aTimer.AutoReset = false;

                    // Start the timer
                    aTimer.Enabled = true;

                }
            }
        }

        // the Elapsed event for the timer.
        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            //Console.WriteLine("REACHED THE 30 SECONDS AT {0}", e.SignalTime);
            Console.WriteLine("REACHED THE TWO MINUTES AT {0}", e.SignalTime);
            Console.WriteLine("斑点激发错误，需要重新加工。");
            Logger.Log( e.SignalTime + "  已经2分钟未收到回复。");
            Logger.Log("斑点激发错误，需要重新加工。");
        }

        //handle when Client side order changed
        protected virtual void OnClientsideChanged(ClientsideEventArgs e)
        {
            EventHandler<ClientsideEventArgs> handler = ClientsideChanged;
            if (handler != null)
            {
                handler(this, e);
            }
            
        }

        public event EventHandler<ClientsideEventArgs> ClientsideChanged;

        //handle when server side order changed
        protected virtual void OnServersideChanged(ServersideEventArgs e)
        {
            EventHandler<ServersideEventArgs> handler = ServersideChanged;
            if (handler != null)
            {
                handler(this, e);
                
            }

        }

        public event EventHandler<ServersideEventArgs> ServersideChanged;

        //handle when connected to Server
        protected virtual void OnConnected(ConnectedEventArgs e)
        {
            EventHandler<ConnectedEventArgs> handler = Connected;
            if (handler != null)
            {
                handler(this, e);
            }
            /*
            if (e.Connect == true)
            {
                Logger.Log("[** 客户端(你) 已连接 OnConnected **]");
            }
            else
            {
                Logger.Log("[** 客户端(你) 已断开 OnConnected **]");
            }
            */
        }

        public event EventHandler<ConnectedEventArgs> Connected;

        public client()
        {
            InitializeComponent();
        }

        //calculate checksum
        public static string CalculateChecksum(string dataToCalculate)
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

            for (int i = 0; i < length; i += 1)
            {
                Result.Append(HexAlphabet[(int)(Bytes[i] >> 4)]);
                Result.Append(HexAlphabet[(int)(Bytes[i] & 0xF)]);
                Result.Append(' ');
            }
            Console.WriteLine("ByteArrayToHexString result ： " + Result.ToString());
            return Result.ToString();

        }

        //convert string to HEX Byte Array
        //use it when sending the message
        public static byte[] StringToByteArray(String Hex)
        {
            string[] hexValuesSplit = Hex.Split(' ');
            byte[] bytes = new byte[hexValuesSplit.Length];

            for (int i = 0; i < hexValuesSplit.Length; i += 1)
            {

                bytes[i] = Convert.ToByte(hexValuesSplit[i], 16);
            }
            return bytes;
        }

        //convert large integer string to (split)4-Bytes HEX string
        public static string IntStringToHEXString(String s = null)
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
            String sampNum = "";
            foreach (string samp in sampSplit)
            {
                sampNum = sampNum + " " + IntStringToHEXString(samp);
            }
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
                { "19", "9个指令(bytes)" },{ "26", "38个指令(bytes)" },
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
                { "81", "20号控样" },{ "82", "25号控样" },
                { "8A", "品类为A级" },{ "8B", "品类为B级" },{ "8C", "品类为C级" },
                { "8a", "品类为A级" },{ "8b", "品类为B级" },{ "8c", "品类为C级" },
                { "91", "本次激发/分析合格" },{ "92", "本次激发/分析不合格，需要重新激发" },
                { "00", "无数据" },
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
                value = "26";
            }
            else
            {
                value = "19";
            }
            return value;
        }
        
        /*Read data from server
         when server send data to client:
         Read function will process the data   */
        public void Read(IAsyncResult result)
        {
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    //read the last byte
                    bytes = obj.stream.EndRead(result);
                    //Console.WriteLine("Read EndRead bytes ： " + bytes.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                    Logger.Log(string.Format("[** {0} **]", ex.Message));
                }
            }
            if (bytes > 0)
            {
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        //actually start to reading and keep reading incase these still left
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                        //Console.WriteLine("Read BeginRead obj.buffer ： " + ByteArrayToHexString(obj.buffer, bytes));
                    }
                    else
                    {
                        //write down what you recieved from server
                        Console.WriteLine("[** 服务器 --> 客户端(你) **]：" + ByteArrayToHexString(obj.buffer, bytes));
                        Logger.Log("[** 服务器 --> 客户端(你) **]：" + ByteArrayToHexString(obj.buffer, bytes));

                        order = ByteArrayToHexString(obj.buffer, bytes);

                        Console.WriteLine("[** 服务器 --> 客户端(你) **] GetOrder(order)：" + GetOrder(order));
                        

                        //GetOrder(ByteArrayToHexString(obj.buffer, bytes)
                        //Reply(ByteArrayToHexString(obj.buffer, bytes));
                        //Console.WriteLine("Read logWrite obj.buffer ： " + ByteArrayToHexString(obj.buffer, bytes));
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                    Logger.Log(string.Format("[** {0} **]", ex.Message));
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
        public void Connection(IPAddress localaddr, int port)
        {
            try
            {
                obj = new MyClient();
                obj.client = new TcpClient();
                obj.client.Connect(localaddr, port);
                obj.stream = obj.client.GetStream();
                obj.buffer = new byte[obj.client.ReceiveBufferSize];
                obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                connected = true;
                ConnectedEventArgs args = new ConnectedEventArgs();
                args.Connect = connected;
                OnConnected(args);
                clientside = "[** 客户端(你) 已连接 **]";
                Console.WriteLine("[** 客户端(你) 已连接 **]");
                Logger.Log("[** 客户端(你) 已连接 **]");
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
                        Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                        Logger.Log(string.Format("[** {0} **]", ex.Message));
                    }
                }

                connected = false;
                exit = true;
                obj.client.Close();
                
                ConnectedEventArgs argsf = new ConnectedEventArgs();
                argsf.Connect = connected;
                OnConnected(argsf);

                clientside = "[** 客户端(你) 已断开 **]";
                Logger.Log("[** 客户端(你) 已断开 **]");
                Console.WriteLine("[** 客户端(你) 已断开 **]");

                //clientside = "[** 客户端(你) 已断开 Connection **]";
                //Logger.Log("[** 客户端(你) 已断开 Connection **]");
                //Console.WriteLine("[** 客户端(你) 已断开 Connection **]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                Logger.Log(string.Format("[** {0} **]", ex.Message));
            }
        }


        /*ConnectButton_Click event*/
        public void Connect_Click(String ipaddr, String iport)
        {
            if (connected)
            {
                exit = true;
                obj.client.Close();

                //Logger.Log("[** 客户端(你) 已断开Connect_Click **]");
                //Console.WriteLine("[** 客户端(你) 已断开Connect_Click **]");
            }
            else if (Tclient == null || !Tclient.IsAlive)
            {
                bool localaddrResult = IPAddress.TryParse(ipaddr, out IPAddress localaddr);
                if (!localaddrResult)
                {
                    Logger.Log("[ IP 地址无效，请重新填写  ]");
                    Console.WriteLine(("[ IP 地址无效，请重新填写  ]"));
                }
                bool portResult = int.TryParse(iport, out int port);
                if (!portResult)
                {
                    Logger.Log("[  端口无效，请重新填写  ]");
                    Console.WriteLine(("[  端口无效，请重新填写  ]"));
                }
                else if (port < 0 || port > 65535)
                {
                    portResult = false;
                    Logger.Log("[  端口超过正常范围(0，65535)，请重新填写  ]");
                    Console.WriteLine(("[  端口超过正常范围(0，65535)，请重新填写  ]"));
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
        public void Write(IAsyncResult result)
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
                    Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                    Logger.Log(string.Format("[** {0} **]", ex.Message));
                }
            }
        }

        /*Send message*/
        public void Send(byte[] msg)
        {
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginWrite(msg, 0, msg.Length, new AsyncCallback(Write), null);
                    
                    //Console.WriteLine("Send BeginWrite msg ： " + ByteArrayToHexString(msg, msg.Length));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("[** {0} **]", ex.Message));
                    Logger.Log(string.Format("[** {0} **]", ex.Message));
                }
            }
        }
        /*Send the whole task message*/
        public void TaskSend(byte[] msg)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => Send(msg));
                //Console.WriteLine("TaskSend if msg ： " + ByteArrayToHexString(msg, msg.Length));
            }
            else
            {
                send.ContinueWith(antecendent => Send(msg));
                //Console.WriteLine("TaskSend else msg ： " + ByteArrayToHexString(msg, msg.Length));
            }
        }

        /*Closed the client side*/
        public void Client_Closing()
        {
            if (connected)
            {
                exit = true;
                obj.client.Close();

                //Logger.Log("[** 客户端(你) 已断开 Client_Closing**]");
                //Console.WriteLine("[** 客户端(你) 已断开 Client_Closing**]");
            }
            
        }

        //write out string in the show box
        public string GetOrder(string msg = null)
        {
            if (connected)
            {
                if (msg != null)
                {
                    //used the stopwatch to stop the timer
                    if (stopwatch != null) 
                    {
                        stopwatch.Stop();
                        ts = stopwatch.Elapsed;
                        //TimeSpan ts = stopwatch.Elapsed;
                        if (ts != TimeSpan.Zero)
                        {
                            Console.WriteLine("AFTER REPLY GET MESSAGE IN {0} SECONDS" , ts.ToString("mm\\:ss\\.ff"));
                            aTimer.Stop();
                            aTimer.Dispose();
                        }

                    }

                    string[] msgSplit = msg.Split(' ');
                    string[] labels = { "", "", "", "", "", "", "", "" };
                    //Console.WriteLine("Replay msg length ： " + msgSplit.Length.ToString());
                    String Stest = CalculateChecksum(msgSplit[0] + " " + msgSplit[1] + " " + msgSplit[3] + " " + msgSplit[4] + " " + msgSplit[5] + " " + msgSplit[6] + " " + msgSplit[7] + " " + msgSplit[8] + " " + msgSplit[9] + " " + msgSplit[10]);


                    for (int i = 0; i < labels.Length; i += 1)
                    {
                        if (i == 0)
                        {
                            if (msgSplit[i] == "01" )
                            {
                                newStart = true;
                            }

                        }
                        if (i == 3)
                        {
                            labels[3] = HexToDecimal(msgSplit[3] + msgSplit[4] + msgSplit[5]);
                            SampleNum = HexToDecimal(msgSplit[3] + msgSplit[4] + msgSplit[5]);

                        }
                        else if (i == 4)
                        {
                            labels[i] = GetMeaning(msgSplit[i + 2]);

                        }
                        else if (i == 2)
                        {
                            String check = "";
                            if (Stest == msgSplit[i]) { check = "传输正常"; }
                            else { check = "传输不正常"; }
                            labels[i] = check + " " + "HEX运算" + Stest + "收到" + msgSplit[i];

                        }
                        else if (i == 5)
                        {
                            labels[i] = GetMeaning(msgSplit[i + 2]) + " " + GetMeaning(msgSplit[i + 3]);
                            state = GetMeaning(msgSplit[i + 2]);
                            if (msgSplit[i + 2] == "62" || msgSplit[i + 2] == "64") 
                            {
                                spark = true;
                            }
                            if (msgSplit[i + 2] == "63" )
                            {
                                RequireData = true;
                            }
                        }
                        else if (i > 5)
                        {
                            labels[i] = GetMeaning(msgSplit[i + 3]);
                            if (labels[i] == "81" || labels[i] == "82")
                            {
                                ctrlSamp = labels[i];
                            }

                        }
                        else
                        {
                            labels[i] = GetMeaning(msgSplit[i]);
                            
                        }

                    } //end of for loop
                    serverside = string.Join(" ", labels);
                    Logger.Log("[** 服务器 --> 客户端(你) **]serverside：" + serverside);
                    ServersideEventArgs args = new ServersideEventArgs();
                    args.Serverside = serverside;
                    OnServersideChanged(args);
                    
                    return serverside;
                }//end of if (msg != null)
                else
                {
                    return "指令为空";
                }
            }//end of if (!exit)
            else
            {
                return "你已断开,没得到指令";
            }
        }// end of private void GetOrder(string msg = null)

        //write out string in the show box
        public string Reply(string msg = null)
        {
            if (connected)
            {
                if (msg != null)
                {
                    string[] msgSplit = msg.Split(' ');

                    String step = msgSplit[0];
                    String length = GetLength(msgSplit[0], msgSplit[7]);
                    String sampNum = msgSplit[3] + " " + msgSplit[4] + " " + msgSplit[5];
                    String totalNum = msgSplit[6];
                    String cond = GetMeaning(msgSplit[0] + msgSplit[6] + msgSplit[7]);
                    String testNum = msgSplit[8];
                    String test = CalculateChecksum(step + " " + length + " " + sampNum + " " + totalNum + " " + cond + " " + testNum);
                    string clientmsg = step + " " + length + " " + test + " " + sampNum + " " + totalNum + " " + cond + " " + testNum;
                    //String NewData = "";


                    //else 

                    if (cond == "67")
                    {
                        Console.WriteLine("Replay 67 DataAvg ： " + DataAvg);
                        if (DataAvg != null)
                        {
                            data = PreHex(DataAvg.Replace(".", ""));
                        }

                        test = CalculateChecksum(step + " " + length + " " + sampNum + " " + totalNum + " " + cond + " " + testNum + " " + grade + " " + quali + " " + data);

                        clientmsg = step + " " + length + " " + test + " " + sampNum + " " + totalNum + " " + cond + " " + testNum + " " + grade + " " + quali + " " + data;
                    }
                    if (cond == "66" || cond == "62" || cond == "64")
                    {

                        test = CalculateChecksum(step + " " + length + " " + sampNum + " " + totalNum + " " + cond + " " + testNum + " " + quali);

                        clientmsg = step + " " + length + " " + test + " " + sampNum + " " + totalNum + " " + cond + " " + testNum + " " + quali;
                    }

                    TaskSend(StringToByteArray(clientmsg));
                    
                    Console.WriteLine("[** 客户端(你) --> 服务器 **]：" + clientmsg);
                    Logger.Log("[** 客户端(你) --> 服务器 **]：" + clientmsg);
                    serverside = "";
                    ServersideEventArgs args = new ServersideEventArgs();
                    args.Serverside = serverside;
                    OnServersideChanged(args);
                    clientside = GetMeaning(msgSplit[0]) + " " + GetMeaning(length) + " " + test + " " + HexToDecimal(msgSplit[3] + msgSplit[4] + msgSplit[5]) + " " + GetMeaning(msgSplit[6]) + " " + GetMeaning(cond) + " " + GetMeaning(testNum) + " " + GetMeaning(grade) + " " + GetMeaning(quali) + " " + DataAvg;
                    ClientsideEventArgs args1 = new ClientsideEventArgs();
                    args1.Clientside = clientside;
                    OnClientsideChanged(args1);
                    Logger.Log("[** 客户端(你) --> 服务器 **]clientside：" + clientside);
                    return clientside;
                    /*
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
                    */
                }//end of if (msg != null)
                else 
                {
                    return "回复消息为空";
                }
            }//end of if (!exit)
            else 
            {
                return "你已断开，无法回复";
            }

        }// end of private void Replay(string msg = null)
    }
}
