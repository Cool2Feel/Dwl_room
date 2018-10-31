using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
// 添加额外的命名空间
using System.Net;
using System.Net.Sockets;
//using System.Threading;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace Server
{
   
    public partial class ServerForm : Form
    {
        #region Members
        // 保存登录的所有用户
        private List<User> userList = new List<User>();

        //private List<TcpClient> userClientList = new List<TcpClient>();
        // 服务器端口
        int port;
        private bool changedIP = false;
        // 监听端口
        //int tcpPort;
        // 定义变量
        //private UdpClient sendUdpClient;
        //private UdpClient receiveUdpClient;
        private TcpClient newClient = null;
        private TcpClient newTerminal = null;
        private IPEndPoint serverIPEndPoint;
        //private TcpListener tcpListener, tcpListener1;
        private IPAddress serverIp;
        private NetworkStream networkStream;
        private BinaryWriter binaryWriter;
        //private BinaryReader binaryReader;
        //private string userListstring;
        private User user = null;
        //private NetworkStream networkStream = null;
        private BinaryReader reader;
        private BinaryWriter writer;

        //private Thread listenThread = null;
        //private Thread listenThread1 = null;

        //private User backUser = null;
        //private TcpClient backClient = null;
        private string[] tabel_name = new string[] { "history_table_0", "history_table_1", "history_table_2" };
        //TOP
        private static string[] province = new string[] { "空", "内部会议室", "坂田工厂", "惠南工厂" };
        //middle
        private static string[ , ] city = {{"",""},{"坂田内部", "惠南内部"},{"前台左侧", "前台右侧"},{"前台左侧", "前台右侧"}};
        
        //low
        private static string[,] room_1 = new string[2, 11]
        {
            //内部会议室-坂田
            {"研发会议室一","会议室1","会议室3","电气部对面","软件部","结构部","皓丽部","会议室2","大客户六部","业务部","电气部"},
            //内部会议室-惠南
            {"技术部会议室","会议室11","","","","","","","","",""}
        };

        private static string[,] room_2 = new string[2, 16]
        {
            //坂田工厂-前台左侧
            {"1号","2号","3号","C号","5号","6号","会议室A","多功能厅","","","","","","","",""},
            //坂田工厂-前台右侧
            {"7号","8号","9号","10号","11号","会议室B","12号","13号","14号","15号","17号","18号","16号","19号","20号","21号"}
        };

        //low
        private static string[,] room_3 = new string[2, 13]
        {
           //惠南工厂-前台右侧
            //{"8号","9号","10号","11号","12号","15号","多功能厅","13号"}
            {"1号","2号","3号","5号","6号","7号","9号","10号","12号","13号","15号","多功能厅","会议室A"},
           //惠南工厂-前台左侧
            {"","","","","","","","","","","","",""}
        };
        private IOCPServer ClientServer;
        //private IOCPServer TerminalServer;
        #endregion

        #region Server Form
        public ServerForm()
        {
            InitializeComponent();
            // 初始化窗口
            IPAddress[] serverIPs = Dns.GetHostAddresses("");
            txbServerIP.Text = serverIPs[0].ToString();
            //for (int i = 0; i < 9; i++)
                //Console.WriteLine(serverIPs[i].ToString());

            txbServerIP.Text = GetConnectionStringsConfig("IP");
            port = 7088;
            txbServerport.Text = port.ToString();
            serverIp = IPAddress.Parse(txbServerIP.Text);
            serverIPEndPoint = new IPEndPoint(serverIp, 7088);
            ClientServer = new IOCPServer(serverIp,7088, 1024);
            //TerminalServer = new IOCPServer(8086,1024);
            //Random random = new Random();
            btnStop.Enabled = false;
            timer_auto.Enabled = true;
            timer_auto.Interval = 5000;
            timer_auto.Start();
            //Console.WriteLine("1111111");
        }

        // 启动服务器
        // 客户端先向服务器发送登录请求，然后通过服务器返回的端口号
        // 再与服务器建立连接
        // 所以启动服务按钮事件中有两个套接字：一个是接收客户端信息套接字和
        // 监听客户端连接套接字
        private void btnStart_Click(object sender, EventArgs e)
        {
            // 创建接收套接字
            //serverIp = IPAddress.Parse(txbServerIP.Text);
            //serverIPEndPoint = new IPEndPoint(serverIp, int.Parse(txbServerport.Text));
            //receiveUdpClient = new UdpClient(serverIPEndPoint);
            // 启动接收线程
            //Thread receiveThread = new Thread(ReceiveMessage);
            //receiveThread.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            serverIp = IPAddress.Parse(txbServerIP.Text);
            ClientServer = new IOCPServer(serverIp, 7088, 1024);
            // 随机指定监听端口
            //Random random = new Random();
            //tcpPort = random.Next(port + 1, 65536);
            ClientServer.Start(this);
            if (changedIP)
                UpdateConnectionStringsConfig("IP", txbServerIP.Text);
            //TerminalServer.Start(this);
            //IOCPServer TerminalServer = new IOCPServer(8086, 1024);
            //TerminalServer.Start();
            // 创建监听套接字
            /*
            tcpListener1 = new TcpListener(serverIp, 8086);
            tcpListener1.Start(100);

            // 启动监听线程
            listenThread1 = new Thread(ListenTerminalConnect);
            listenThread1.Start();

            // 创建监听套接字
            tcpListener = new TcpListener(serverIp, 7088);
            tcpListener.Start(50);

            // 启动监听线程
            listenThread = new Thread(ListenClientConnect);
            listenThread.Start();
             */ 
            //AddItemToListBox(string.Format("服务器线程{0}启动，监听端口{1},{2}", serverIPEndPoint, 8086,7088));
        }

        // 接收客户端发来的信息
        /*
        private void ReceiveMessage()
        {
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    // 关闭receiveUdpClient时下面一行代码会产生异常
                    byte[] receiveBytes = receiveUdpClient.Receive(ref remoteIPEndPoint);
                    string message = Encoding.Unicode.GetString(receiveBytes, 0, receiveBytes.Length);

                    //message = message.Trim();
                    Console.WriteLine(message);
                    //RemoteAdministor rm = JsonHelper.DeserializeJsonToObject<RemoteAdministor>(message);

                    //Console.WriteLine(rm.Ip + "," +rm.password + "," + rm.userName);
                    // 显示消息内容
                    AddItemToListBox(string.Format("{0}:{1}",remoteIPEndPoint,message));

                    // 处理消息数据
                    // 根据协议的设计部分，从客户端发送来的消息是具有一定格式的
                    // 服务器接收消息后要对消息做处理
                    string[] splitstring = message.Split(',');
                    // 解析用户端地址
                    string[] splitsubstring = splitstring[2].Split(':');
                    IPEndPoint clientIPEndPoint = new IPEndPoint(IPAddress.Parse(splitsubstring[0]), int.Parse(splitsubstring[1]));
                    switch (splitstring[0])
                    {
                        case "status":
                            User use = new User(splitstring[1], clientIPEndPoint);
                            string String = "Online," + tcpPort.ToString();
                            // 向客户端发送应答消息
                            SendtoClient(use, String);
                            break;
                        // 如果是登录信息，向客户端发送应答消息和广播有新用户登录消息
                        case "login":
                            User user = new User(splitstring[1], clientIPEndPoint);
                            // 往在线的用户列表添加新成员
                            userList.Add(user);
                            AddItemToListBox(string.Format("用户{0}({1})加入", user.GetName(), user.GetIPEndPoint()));
                            string sendString = "Accept," + tcpPort.ToString();
                            // 向客户端发送应答消息
                            SendtoClient(user, sendString);
                            //SendInfoFromTable(user, message);//连接进行显示同步
                            AddItemToListBox(string.Format("向{0}({1})发出：[{2}]", user.GetName(), user.GetIPEndPoint(), sendString));
                            for (int i = 0; i < userList.Count; i++)
                            {
                                if (userList[i].GetName() != user.GetName())
                                {
                                    // 给在线的其他用户发送广播消息
                                    // 通知有新用户加入
                                    SendtoClient(userList[i], message);
                                    //Console.WriteLine("message :" + message);
                                }
                            }

                            AddItemToListBox(string.Format("in广播：[{0}]", message));
                            break;
                        case "logout":
                            for (int i = 0; i < userList.Count; i++)
                            {
                                if (userList[i].GetName() == splitstring[1])
                                {
                                    AddItemToListBox(string.Format("用户{0}({1})退出",userList[i].GetName(),userList[i].GetIPEndPoint()));
                                    userList.RemoveAt(i); // 移除用户
                                }
                            }
                            for (int i = 0; i < userList.Count; i++)
                            {
                                // 广播注销消息
                                SendtoClient(userList[i], message);
                            }
                            AddItemToListBox(string.Format("out广播:[{0}]", message));
                            break;
                        case "addin":
                            //Console.WriteLine(message);
                            user = new User(splitstring[1], clientIPEndPoint);
                            for (int i = 0; i < userList.Count; i++)
                            {
                                if (userList[i].GetName() != user.GetName())
                                {
                                    // 给在线的其他用户发送广播消息
                                    // 通知有新用户加入
                                    SendtoClient(userList[i], message);
                                }
                            }
                            Thread WriteThread = new Thread(WritetoTable);
                            WriteThread.Start(message);
                            //WritetoTable(message);//保存到数据表中
                            //AddItemToListBox(string.Format("广播:[{0}]", message));
                            break;
                        case "clearroom":
                            //Console.WriteLine(message);
                            user = new User(splitstring[1], clientIPEndPoint);
                            for (int i = 0; i < userList.Count; i++)
                            {
                                if (userList[i].GetName() != user.GetName())
                                {
                                    // 给在线的其他用户发送广播消息
                                    // 通知其他用户更新
                                    SendtoClient(userList[i], message);
                                }
                            }//先通知用户再删除
                            //ClearTable(message);//从数据表中删除
                            Thread ClearThread = new Thread(ClearTable);
                            ClearThread.Start(message);
                            AddItemToListBox(string.Format("clear广播:[{0}]", message));
                            break;
                        case "reservate":
                            //Console.WriteLine(message);
                            user = new User(splitstring[1], clientIPEndPoint);
                            for (int i = 0; i < userList.Count; i++)
                            {
                                if (userList[i].GetName() != user.GetName())
                                {
                                    // 给在线的其他用户发送广播消息
                                    // 通知有新用户加入
                                    SendtoClient(userList[i], message);
                                }
                            }
                            //AddItemToListBox(string.Format("广播:[{0}]", message));
                            break;
                        case "check":
                            //Console.WriteLine(message);
                            user = new User(splitstring[1], clientIPEndPoint);
                            backUser = user;
                            //CheckInfoFromTable(user, message);//从数据中查询
                            Thread CheckThread = new Thread(CheckInfoFromTable);
                            CheckThread.Start(message);
                            break;
                    }
                }
                catch
                {
                    // 发送异常退出循环
                    break;
                }
            }
            AddItemToListBox(string.Format("服务线程{0}终止", serverIPEndPoint));
        }
         */

        public static string GetConnectionStringsConfig(string connectionName)
        {
            //指定config文件读取
            string file = System.Windows.Forms.Application.ExecutablePath;
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string connectionString = config.AppSettings.Settings[connectionName].Value.ToString();
            return connectionString;
        }

        ///<summary> 
        ///更新连接字符串  
        ///</summary> 
        ///<param name="newName">连接字符串名称</param> 
        ///<param name="newConString">连接字符串内容</param> 
        ///<param name="newProviderName">数据提供程序名称</param> 
        public static void UpdateConnectionStringsConfig(string newName, string newConString)
        {
            //指定config文件读取
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);

            bool exist = false; //记录该连接串是否已经存在  
            //如果要更改的连接串已经存在  
            if (config.AppSettings.Settings[newName] != null)
            {
                exist = true;
            }
            // 如果连接串已存在，首先删除它  
            if (exist)
            {
                config.AppSettings.Settings.Remove(newName);
            }
            //新建一个连接字符串实例  
            //ConnectionStringSettings mySettings = new ConnectionStringSettings(newName, newConString, newProviderName);
            // 将新的连接串添加到配置文件中.  
            //config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            config.AppSettings.Settings.Add(newName,newConString);
            // 保存对配置文件所作的更改  
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节  
            ConfigurationManager.RefreshSection("appSettings");
        }

        private Object locker = new Object();
        /// <summary>
        /// TCP向客户端发送消息
        /// </summary>
        /// <param name="newUserClient"></param>
        /// <param name="message"></param>
        private void TcpSendtoClient(Socket s, string message)
        {
            //TcpClient newUserClient = (TcpClient)userClient;  
            //lock (locker)
            {
                try
                {
                    message += ";end";

                    byte[] bf = System.Text.Encoding.Default.GetBytes(message);
                    ClientServer.Send(s, bf, 0, bf.Length, 10);
                    //networkStream = newUserClient.GetStream();
                    //binaryWriter = new BinaryWriter(networkStream);
                    //binaryReader = new BinaryReader(networkStream);
                    /*
                    if (newUserClient.Connected)
                    {
                        binaryWriter.Write(message);
                        binaryWriter.Flush();
                        Thread.Sleep(200);
                    }
                    else
                    {
                        for (int i = 0; i < userList.Count; i++)
                        {
                            if (userList[i].GetTcpClient() == newUserClient)
                            {
                                //AddItemToListBox(string.Format("用户{0}({1})退出", userList[i].GetName(), userList[i].GetIPEndPoint()));
                                //AddItemToListBox(string.Format("用户{0}退出", userClientList[i].Client.RemoteEndPoint));
                                userList.RemoveAt(i);// 移除用户
                            }
                        }
                    }
                     */ 
                }
                catch
                {
                    return;
                }
            }
        }

        private void TcpSendtoClient(SocketAsyncEventArgs e, string message)
        {
            //TcpClient newUserClient = (TcpClient)userClient;  
            //lock (locker)
            {
                try
                {
                    message += ";end";

                    byte[] bf = System.Text.Encoding.Default.GetBytes(message);
                    ClientServer.Send(e, bf);
                }
                catch
                {
                    return;
                }
            }
        }


        private void TcpSendtoClient(TcpClient userClient, string message)
        {

        }

        #endregion

        #region DateTable
        /// <summary>
        /// 写入数据库
        /// </summary>
        /// <param name="message"></param>
        private void WritetoTable(string obj)
        {
            string message = obj;
            try
            {
                string[] splitstring = message.Split(',');
                //Console.WriteLine(message + ":" + splitstring.Count());
                int n = splitstring.Count();
                string[] str = new string[n - 2];
                for (int i = 1; i < n - 2; i++)
                {
                    str[i] = splitstring[i + 2];
                }
                str[0] = splitstring[3] + "_" + splitstring[4];

                //DataTable ts = AccessFunction.GetClientProtocol(str[0]);
                //if (ts == null && ts.Rows.Count <= 0)
                AccessFunction.InsertClientProtocol(str, "Table_room");

                DataTable tables = AccessFunction.GetTerminalProtocol(str[0]);
                if (tables != null && tables.Rows.Count > 0)
                {
                    for (int j = 0; j < tables.Rows.Count; j++)
                    {
                        if (DateTime.Parse(tables.Rows[j][6].ToString()) == DateTime.Parse(str[6]))
                        {
                            AccessFunction.DeleteTerminalProtocol(str[0], tables.Rows[j][6].ToString(), tables.Rows[j][7].ToString());
                        }
                    }
                }
            }
            catch
            { }
        }
        /// <summary>
        /// 写入预约记录表
        /// </summary>
        /// <param name="obj"></param>
        private void ReservatetoTable(object obj)
        {
            string message = (string)obj;
            try
            {
                string[] splitstring = message.Split(',');
                //Console.WriteLine(message + ":" + splitstring.Count());
                int n = splitstring.Count();
                string[] str = new string[n - 2];
                for (int i = 1; i < n - 2; i++)
                {
                    str[i] = splitstring[i + 2];
                }
                str[0] = splitstring[3] + "_" + splitstring[4];

                AccessFunction.InsertClientProtocol(str, "Reservatel_room");
            }
            catch
            { }
        }

        /// <summary>
        /// 清除房间、从数据库中删除信息
        /// </summary>
        /// <param name="message"></param>
        private void ClearTable(string obj)
        {
            string message = obj;
            string[] splitstring = message.Split(',');
            string str = splitstring[3] + "_" + splitstring[4];
            string[] s = new string[8];
            DataTable tables = AccessFunction.GetClientProtocol(str);
            if (tables != null && tables.Rows.Count > 0)
            {
                for (int i = 2; i < 9; i++)
                {
                    s[i - 2] = tables.Rows[0][i].ToString();
                }
                s[7] = splitstring[3];
            }
            try
            {
                
                if (splitstring[3].Equals("1"))
                    AccessFunction.InsertClientProtocol(s, tabel_name[0]);//保存到历史记录表1
                else if (splitstring[3].Equals("2"))
                    AccessFunction.InsertClientProtocol(s, tabel_name[1]);//保存到历史记录表2
                else if (splitstring[3].Equals("3"))
                    AccessFunction.InsertClientProtocol(s, tabel_name[2]);//保存到历史记录表3
                else
                    return;
                Thread.Sleep(100);
                AccessFunction.DeleteClientProtocol(str);
            }
            catch
            { 
            }
        }
        /// <summary>
        /// 终端轮巡信息更新（会议室信息）
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        private Class_Room TerminalCheckTable(IpRoom.RoomId rm)
        {
            int t = rm.topID;
            int m = rm.middleID;
            int l = rm.lastID;

            Class_Room room = new Class_Room();
            room.topID = t;
            room.middleID = m;
            room.lastID = l;
            room.status = 0;
            string s = "";
            try
            {
                switch (t)
                {
                    case 1:
                        s = "1_" + room_1[m,l];
                        break;
                    case 2:
                        s = "2_" + room_2[m,l];
                        break;
                    case 3:
                        s = "3_" + room_3[m,l];
                        break;
                    default:
                        break;
                };
                //Console.WriteLine(s);
                if (!s.Equals(""))
                {
                    DataTable tables = AccessFunction.GetClientProtocol(s);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        room.status = 1;
                        Class_Room.scheduleRoom mes1 = new Class_Room.scheduleRoom();
                        //mes1.id = int.Parse(tables.Rows[0][9].ToString());
                        mes1.startTime = ConvertDateTimeInt(DateTime.Parse(tables.Rows[0][6].ToString()));
                        //Console.WriteLine(mes1.startTime);
                        mes1.endTime = ConvertDateTimeInt(DateTime.Parse(tables.Rows[0][7].ToString()));
                        mes1.OrderPerson = tables.Rows[0][5].ToString();
                        mes1.peopleNumber = tables.Rows[0][3].ToString();
                        mes1.Customer = tables.Rows[0][4].ToString();
                        mes1.Clerk = tables.Rows[0][5].ToString();
                        mes1.Aim = tables.Rows[0][8].ToString();

                        room.roomScheduleList.Add(mes1);
                    }
                    /*
                    tables = AccessFunction.GetTerminalProtocol(s);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        for (int i = 0; i < tables.Rows.Count; i++)
                        {
                            if (DateTime.Parse(tables.Rows[i][6].ToString()) >= DateTime.Now.AddHours(1) && DateTime.Parse(tables.Rows[i][6].ToString()) < DateTime.Now.AddHours(1.5))
                            {
                                Class_Room.scheduleRoom mes2 = new Class_Room.scheduleRoom();

                                mes2.peopleNumber = tables.Rows[i][3].ToString();
                                mes2.Customer = tables.Rows[i][4].ToString();
                                mes2.OrderPerson = tables.Rows[i][5].ToString();
                                mes2.startTime = ConvertDateTimeInt(DateTime.Parse(tables.Rows[i][6].ToString()));
                                mes2.endTime = ConvertDateTimeInt(DateTime.Parse(tables.Rows[i][7].ToString()));
                                mes2.Aim = tables.Rows[i][8].ToString();
                                mes2.id = int.Parse(tables.Rows[i][9].ToString());
                                //Console.WriteLine(mes1.startTime);
                                room.roomScheduleList.Add(mes2);
                                break;
                            }
                        }
                    }
                     */ 
                }
            }
            catch
            { }
            return room;
        }

        /// <summary>
        /// 连接后进行数据同步，界面的初始化显示
        /// </summary>
        /// <param name="user"></param>
        /// <param name="str"></param>
        private void SendInfoFromTable(Socket s, string str)
        {
            try
            {
                //Console.WriteLine("ssss=" + str);
                string message = "";
                if(str.Contains("login"))
                    message = str.Replace("login", "addin");
                if(str.Contains("status"))
                    message = str.Replace("status", "addin");
                if (!message.Equals(""))
                {
                    string[] ss = message.Split(',');
                    message = ss[0] + "," + ss[1] + "," + ss[2] + ",";
                    DataTable tables = AccessFunction.GetTables();
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int Count = tables.Columns.Count;
                        int Rows = tables.Rows.Count;
                        for (int r = 0; r < Rows; r++)
                        {
                            string temp = message;
                            for (int t = 1; t < Count; t++)
                            {
                                temp += tables.Rows[r][t].ToString() + ",";
                            }
                            temp += ";end";
                            //Thread.Sleep(100);
                            //Console.WriteLine(temp);
                            byte[] bf = System.Text.Encoding.Default.GetBytes(temp);
                            ClientServer.Send(s, bf, 0, bf.Length, 10);
                                /*
                            while (!ClientServer.Send(s, bf, 0, bf.Length, 10))
                            {
                                return;
                            }*/
                            Thread.Sleep(200);
                        }
                    }
                }
            }
            catch
            { 
                
            }
        }

        /// <summary>
        /// DataTable 到 string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void DataTableToString(User user,DataTable dt)
        {
            //!@&,#$%,^&*为字段的拼接字符串
            //为了防止连接字符串不在DataTable数据中存在，特意将拼接字符串写成特殊的字符！
            StringBuilder strData = new StringBuilder();
            StringWriter sw = new StringWriter();

            //DataTable 的当前数据结构以 XML 架构形式写入指定的流
            dt.WriteXmlSchema(sw);
            strData.Append(sw.ToString());
            sw.Close();
            strData.Append("@&@");
            for (int i = 0; i < dt.Rows.Count; i++)           //遍历dt的行
            {
                DataRow row = dt.Rows[i];
                if (i > 0)                                    //从第二行数据开始，加上行的连接字符串
                {
                    strData.Append("#$%");
                }
                for (int j = 0; j < dt.Columns.Count; j++)    //遍历row的列
                {
                    if (j > 0)                                //从第二个字段开始，加上字段的连接字符串
                    {
                        strData.Append("^&*");
                    }
                    strData.Append(Convert.ToString(row[j])); //取数据
                }
            }
            //SendtoClient(user, "check," + strData.ToString());
            //return strData.ToString();
        }

        /// <summary>
        /// 查询历史记录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="str"></param>
        private void CheckInfoFromTable(object obj)
        {
            //Console.WriteLine(message);
            string message = (string)obj;
            string[] splitstring = message.Split(',');
            try
            {
                string s = "";
                if (splitstring[3].Equals("1"))
                    s = tabel_name[0];//查询历史记录表1
                else if (splitstring[3].Equals("2"))
                    s = tabel_name[1];//查询历史记录表2
                else if (splitstring[3].Equals("3"))
                    s = tabel_name[2];//查询历史记录表3
                else
                    return;
                Socket backClient = null;
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i].GetName() == splitstring[1])//匹配对应的查询用户
                        backClient = userList[i].GetSocket();
                }
                if (!s.Equals(""))
                {
                    DataTable tables = AccessFunction.GetClientHistory(splitstring[4], splitstring[3],s);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int Rows = tables.Rows.Count;
                        int Count = tables.Columns.Count;
                        //DataTableToString(user,tables);
                        for (int r = 0; r < Rows; r++)
                        {
                            //Console.WriteLine(tables.Rows[r][4].ToString());
                            if (tables.Rows[r][4].ToString().Contains(splitstring[5].ToString()))//日期对应匹配
                            {
                                string temp = splitstring[0] + ",";
                                for (int t = 0; t < Count - 2; t++)
                                {
                                    temp += tables.Rows[r][t].ToString() + ",";
                                }
                                //Console.WriteLine(temp);
                                TcpSendtoClient(backClient, temp);//把查询的信息发送给对应的用户
                            }
                        }
                    }
                    TcpSendtoClient(backClient, splitstring[0] + ",endcheck");
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 删除预约的记录
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteInfoFromTable(object obj)
        {
            string message = (string)obj;
            string[] splitstring = message.Split(',');
            try
            {
                //Console.WriteLine(message);
                string s = splitstring[3] + "_" + splitstring[4];
                AccessFunction.DeleteTerminalProtocol(s, splitstring[5], splitstring[6]);
            }
            catch
            {
                return;
            }
        }

        #endregion

        #region ListenConnect
        /// <summary>
        /// 监听接受客户端的连接
        /// </summary>
        private void ListenClientConnect()
        {
            while (true)
            {
                try
                {
                    //newClient = tcpListener.AcceptTcpClient();
                    //tcpListener.BeginAcceptTcpClient(new AsyncCallback(ReceiveClientData), tcpListener);
                    //AddItemToListBox(string.Format("接受客户端{0}的TCP请求",newClient.Client.RemoteEndPoint));
                    //networkStream = newClient.GetStream();
                    //reader = new BinaryReader(networkStream);
                    //writer = new BinaryWriter(networkStream);
                    //userClientList.Add(newClient);
                    
                    //if (newClient.Connected)
                    {
                        //Thread ReceiveThread = new Thread(new ParameterizedThreadStart(ReceiveClientData));
                        //ReceiveThread.Start(newClient);
                    }
                    
                }
                catch
                {
                    //AddItemToListBox(string.Format("监听线程({0}:{1})关闭", serverIp, 8086));
                    break;
                }
                
            }
        }
        /// <summary>
        /// 监听接受终端的连接
        /// </summary>
        private void ListenTerminalConnect()
        {
            //TcpClient newClient = null;
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    //while (tcpListener1.Pending())  //跳开阻塞，用这个在关闭服务器程序时不会出现异常
                    {
                        //newTerminal = tcpListener1.AcceptTcpClient();
                        //AddItemToListBox(string.Format("接受终端{0}的TCP请求", newTerminal.Client.RemoteEndPoint));
                        if (newTerminal.Connected)
                        {
                            //networkStream = newClient.GetStream();
                            //reader = new BinaryReader(networkStream);
                            //writer = new BinaryWriter(networkStream);
                            Thread ReceiveThread = new Thread(new ParameterizedThreadStart(ReceiveTerminalData));
                            ReceiveThread.Start(newTerminal);
                            Console.WriteLine("+++111===" + newTerminal.Client.RemoteEndPoint);
                        }
                    }
                    //if (newClient.Connected)
                    //newClient.Close();
                }
                catch
                {
                    //AddItemToListBox(string.Format("监听线程({0}:{1})关闭", serverIp, 7088));
                    break;
                }
                
            }
        }

        #endregion

        #region Room Handlers
        // 向客户端发送在线用户列表信息
        // 服务器通过TCP连接把在线用户列表信息发送给客户端
        private void SendData(object userClient)
        {
            TcpClient newUserClient = (TcpClient)userClient;
            string userListstring = "update;";
            //Console.WriteLine(userList.Count);
            for (int i = 0; i < userList.Count; i++)
            {
                userListstring += userList[i].GetName() + ","
                    + userList[i].GetIPEndPoint().ToString() + ";";
            }
            userListstring += "end";
            networkStream = newUserClient.GetStream();
            binaryWriter = new BinaryWriter(networkStream);
            //binaryReader = new BinaryReader(networkStream);
            binaryWriter.Write(userListstring);
            binaryWriter.Flush();
            //AddItemToListBox(string.Format("向{0}发送[{1}]", newUserClient.Client.RemoteEndPoint, userListstring));
            //binaryWriter.Close();
            //newUserClient.Close();
            Thread.Sleep(1000);
        }
        /// <summary>
        /// 与客户端TCP连接接收线程
        /// </summary>
        /// <param name="userClient"></param>
        /*
        private void ReceiveClientData(object userClient)
        {
            //this.tcpListener = (TcpListener)iar.AsyncState;
            TcpClient newUserClient = (TcpClient)userClient;
            //string usereceive = null;
            networkStream = newUserClient.GetStream();
            reader = new BinaryReader(networkStream);
            Socket socket = newUserClient.Client;
            while (true)
            {
                bool logout = false;
                //byte[] buffer = new byte[2048];
                string userListstring = "";
                try
                {
                    //if (socket.Available > 0)
                    {
                        userListstring = reader.ReadString();
                        //userListstring = Encoding.ASCII.GetString(buffer, 0, length);
                        if (userListstring.Length > 0)
                        {
                            //userListstring = reader.ReadString();
                            Console.WriteLine(userListstring);
                            if (userListstring.EndsWith("end"))
                            {
                                string[] splitstring = userListstring.Split(';');
                                //Console.WriteLine("===== " + splitstring[0]);
                                // 解析用户端地址
                                string message = splitstring[0];
                                splitstring = message.Split(',');
                                string[] splitsubstring = splitstring[2].Split(':');
                                IPEndPoint clientIPEndPoint = new IPEndPoint(IPAddress.Parse(splitsubstring[0]), int.Parse(splitsubstring[1]));
                                switch (splitstring[0])
                                {
                                    case "status":
                                        //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        //string String = "Online," + tcpPort.ToString();
                                        // 向客户端发送应答消息
                                        //SendtoClient(user, "Online");
                                        TcpSendtoClient(newUserClient, message);
                                        break;
                                    // 如果是登录信息，向客户端发送应答消息和广播有新用户登录消息
                                    case "login":
                                        user = new User(splitstring[1], clientIPEndPoint, newUserClient, splitstring[3]);
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            if (userList[i].GetIPEndPoint() == user.GetIPEndPoint())
                                                userList.RemoveAt(i);
                                        }
                                        userList.Add(user);
                                        // 往在线的用户列表添加新成员
                                        //AddItemToListBox(string.Format("用户{0}({1})加入", user.GetName(), user.GetIPEndPoint()));
                                        //string sendString = "Accept," + tcpPort.ToString();
                                        // 向客户端发送应答消息
                                        SendData(newUserClient);
                                        //Thread sendThread = new Thread(SendData);
                                        //sendThread.Start(newClient);
                                        //SendtoClient(user, sendString);
                                        //SendInfoFromTable(newUserClient, message);//连接进行显示同步
                                        //AddItemToListBox(string.Format("向{0}({1})发出：[{2}]", user.GetName(), user.GetIPEndPoint(), sendString));
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            if (userList[i].GetName() != user.GetName())
                                            {
                                                // 给在线的其他用户发送广播消息
                                                // 通知有新用户加入
                                                //SendtoClient(userList[i], message);
                                                TcpSendtoClient(userList[i].GetTcpClient(), message);
                                                //Console.WriteLine("message :" + message);
                                            }
                                        }
                                        //AddItemToListBox(string.Format("in广播：[{0}]", message));
                                        break;
                                    case "logout":
                                        user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            if (userList[i].GetName() == splitstring[1])
                                            {
                                                //AddItemToListBox(string.Format("用户{0}({1})退出", userList[i].GetName(), userList[i].GetIPEndPoint()));
                                                //AddItemToListBox(string.Format("用户{0}退出", userClientList[i].Client.RemoteEndPoint));
                                                userList.RemoveAt(i);// 移除用户
                                            }
                                        }
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            // 广播注销消息
                                            //SendtoClient(userList[i], message);
                                            TcpSendtoClient(userList[i].GetTcpClient(), message);
                                        }
                                        logout = true;
                                        //AddItemToListBox(string.Format("out广播:[{0}]", message));
                                        break;
                                    case "addin":
                                        //Console.WriteLine(message);
                                        user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            if (userList[i].GetName() != user.GetName())
                                            {
                                                // 给在线的其他用户发送广播消息
                                                // 通知有新用户加入
                                                //SendtoClient(userList[i], message);
                                                TcpSendtoClient(userList[i].GetTcpClient(), message);
                                            }
                                        }
                                        Thread WriteThread = new Thread(WritetoTable);
                                        WriteThread.Start(message);
                                        //WritetoTable(message);//保存到数据表中
                                        //AddItemToListBox(string.Format("广播:[{0}]", message));
                                        break;
                                    case "clearroom":
                                        //Console.WriteLine(message);
                                        user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            if (userList[i].GetName() != user.GetName())
                                            {
                                                // 给在线的其他用户发送广播消息
                                                // 通知其他用户更新
                                                //SendtoClient(userList[i], message);
                                                TcpSendtoClient(userList[i].GetTcpClient(), message);
                                            }
                                        }//先通知用户再删除
                                        //ClearTable(message);//从数据表中删除
                                        Thread ClearThread = new Thread(ClearTable);
                                        ClearThread.Start(message);
                                        //AddItemToListBox(string.Format("clear广播:[{0}]", message));
                                        break;
                                    case "reservate":
                                        //Console.WriteLine(message);
                                        user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            if (userList[i].GetName() != user.GetName())
                                            {
                                                // 给在线的其他用户发送广播消息
                                                // 通知有新用户加入
                                                //SendtoClient(userList[i], message);
                                                TcpSendtoClient(userList[i].GetTcpClient(), message);
                                            }
                                        }
                                        Thread ReservateThread = new Thread(ReservatetoTable);
                                        ReservateThread.Start(message);
                                        //AddItemToListBox(string.Format("广播:[{0}]", message));
                                        break;
                                    case "check":
                                        //Console.WriteLine(newUserClient.ToString());
                                        //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        //backClient = newUserClient;
                                        //CheckInfoFromTable(user, message);//从数据中查询
                                        Thread CheckThread = new Thread(CheckInfoFromTable);
                                        CheckThread.Start(message);
                                        break;
                                    case "delete":
                                        //Console.WriteLine(message);
                                        //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                        //backUser = user;
                                        //从数据中删除
                                        Thread DeleteThread = new Thread(DeleteInfoFromTable);
                                        DeleteThread.Start(message);
                                        break;
                                }
                            }
                        }
                    }
                    //else
                    Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    Console.WriteLine("客户端接收数据失败,原因" + e.Message, "连接提示");
                    //reader.Close();
                    /*
                    if (newUserClient.Connected)
                    {
                        //networkStream = newUserClient.GetStream();
                        reader = new BinaryReader(networkStream);
                    }
                    else
                    {
                        for (int i = 0; i < userList.Count; i++)
                        {
                            if (userList[i].GetTcpClient() == newUserClient)
                            {
                                userList.RemoveAt(i);
                            }
                        }
                        Thread.Sleep(100);
                        reader.Close();
                        //writer.Close();
                        networkStream.Close();
                        newUserClient.Close();
                        break;
                    }
                }
                if (logout)
                {
                    reader.Close();
                    //writer.Close();
                    newUserClient.Close();
                    break;
                }
                if (!newUserClient.Connected)
                {
                    for (int i = 0; i < userList.Count; i++)
                    {
                        if (userList[i].GetTcpClient() == newUserClient)
                        {
                            userList.RemoveAt(i);
                        }
                    }
                    reader.Close();
                    //writer.Close();
                    networkStream.Close();
                    //newUserClient.Close();
                    //Thread.CurrentThread.Abort();
                    break;
                }
                Thread.Sleep(200);
            }
        }*/

        private void SendReceive(Socket s)
        {
            string st = "receive,;end";
            byte[] bf = System.Text.Encoding.Default.GetBytes(st);
            ClientServer.Send(s, bf, 0, bf.Length, 10);
        }

        /// <summary>
        /// newTCP连接接收线程
        /// </summary>
        /// <param name="userListstring"></param>
        /// <param name="s"></param>
        /// <param name="SockList"></param>
        public void ReceiveData(string userListstring, Socket s)
        {
            if (userListstring.Length > 0)
            {
                //userListstring = reader.ReadString();
                //Console.WriteLine(userListstring);
                try
                {
                    if (userListstring.EndsWith("end"))
                    {
                        string[] splitstring = userListstring.Split(';');
                        //Console.WriteLine("===== " + splitstring[0]);
                        // 解析用户端地址
                        string message = splitstring[0];
                        splitstring = message.Split(',');
                        string[] splitsubstring = splitstring[2].Split(':');
                        IPAddress clientIP = IPAddress.Parse(splitsubstring[0]);
                        //IPEndPoint clientIPEndPoint = new IPEndPoint(IPAddress.Parse(splitsubstring[0]), int.Parse(splitsubstring[1]));
                        switch (splitstring[0])
                        {
                            case "status":
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                //string String = "Online," + tcpPort.ToString();
                                // 向客户端发送应答消息
                                //SendtoClient(user, "Online");
                                //TcpSendtoClient(newUserClient, message);
                                Socket s_K = s;
                                SendReceive(s_K);
                                Thread.Sleep(200);
                                SendInfoFromTable(s_K, message);//连接进行显示同步
                                break;
                            // 如果是登录信息，向客户端发送应答消息和广播有新用户登录消息
                            case "login":
                                Socket s_K1 = s;
                                SendReceive(s_K1);
                                user = new User(splitstring[1], clientIP, splitstring[3], s_K1);
                                for (int i = 0; i < userList.Count; i++)
                                {
                                    if (userList[i].GetIPEndPoint() == user.GetIPEndPoint())
                                        userList.RemoveAt(i);
                                }
                                userList.Add(user);
                                SendInfoFromTable(s_K1, message);//连接进行显示同步
                                break;
                            case "logout":
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                Socket s_K2 = s;
                                SendReceive(s_K2);
                                for (int i = 0; i < userList.Count; i++)
                                {
                                    if (userList[i].GetSocket() == s_K2)
                                    {
                                        userList.RemoveAt(i);
                                        break;
                                    }
                                }
                                //Console.WriteLine(s.RemoteEndPoint.ToString());
                                //logout = true;
                                //AddItemToListBox(string.Format("out广播:[{0}]", message));
                                break;
                            case "addin":
                                //Console.WriteLine(message);
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                Socket s_K3 = s;
                                SendReceive(s_K3);
                                for (int i = 0; i < userList.Count; i++)
                                {
                                    //Console.WriteLine("add" + userList[i].GetSocket().RemoteEndPoint);
                                    if (userList[i].GetSocket() != s_K3)
                                    {
                                        // 给在线的其他用户发送广播消息
                                        // 通知其他用户更新
                                        //SendtoClient(userList[i], message);
                                        //Console.WriteLine(userList[i].GetSocket().RemoteEndPoint);
                                        TcpSendtoClient(userList[i].GetSocket(), message);
                                        //ClientServer.Send(message + ";end", userList[i].GetSocket());
                                    }
                                }
                                //Thread WriteThread = new Thread(WritetoTable);
                                //WriteThread.Start(message);
                                WritetoTable(message);//保存到数据表中
                                //AddItemToListBox(string.Format("广播:[{0}]", message));
                                break;
                            case "clearroom":
                                //Console.WriteLine(message);
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                Socket s_K4 = s;
                                SendReceive(s_K4);
                                for (int i = 0; i < userList.Count; i++)
                                {
                                    if (userList[i].GetSocket() != s_K4)
                                    {
                                        // 给在线的其他用户发送广播消息
                                        // 通知其他用户更新
                                        //SendtoClient(userList[i], message);
                                        TcpSendtoClient(userList[i].GetSocket(), message);
                                    }
                                }
                                //先通知用户再删除
                                ClearTable(message);//从数据表中删除
                                //Thread ClearThread = new Thread(ClearTable);
                                //ClearThread.Start(message);
                                //AddItemToListBox(string.Format("clear广播:[{0}]", message));
                                break;
                            case "reservate":
                                //Console.WriteLine(message);
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                Socket s_K5 = s;
                                SendReceive(s_K5);
                                for (int i = 0; i < userList.Count; i++)
                                {
                                    if (userList[i].GetSocket() != s_K5)
                                    {
                                        // 给在线的其他用户发送广播消息
                                        // 通知其他用户更新
                                        //SendtoClient(userList[i], message);
                                        TcpSendtoClient(userList[i].GetSocket(), message);
                                    }
                                }
                                Thread ReservateThread = new Thread(ReservatetoTable);
                                ReservateThread.Start(message);
                                //AddItemToListBox(string.Format("广播:[{0}]", message));
                                break;
                            case "check":
                                //Console.WriteLine(newUserClient.ToString());
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                //backClient = newUserClient;
                                //CheckInfoFromTable(user, message);//从数据中查询
                                Socket s_K7 = s;
                                SendReceive(s_K7);
                                Thread CheckThread = new Thread(CheckInfoFromTable);
                                CheckThread.Start(message);
                                break;
                            case "delete":
                                //Console.WriteLine(message);
                                //user = new User(splitstring[1], clientIPEndPoint, newUserClient);
                                //backUser = user;
                                //从数据中删除
                                Socket s_K6 = s;
                                SendReceive(s_K6);
                                for (int i = 0; i < userList.Count; i++)
                                {
                                    if (userList[i].GetSocket() != s_K6)
                                    {
                                        // 给在线的其他用户发送广播消息
                                        // 通知其他用户更新
                                        //SendtoClient(userList[i], message);
                                        TcpSendtoClient(userList[i].GetSocket(), message);
                                    }
                                }
                                Thread DeleteThread = new Thread(DeleteInfoFromTable);
                                DeleteThread.Start(message);
                                break;
                        }
                    }
                    else if (userListstring.EndsWith("}"))
                    {
                        if (userListstring.Length >= 3)
                        {
                            Class_Room room = null;
                            Socket s_T = s;
                            string strSplit = userListstring.Substring(0, 3);
                            string str1 = userListstring.Substring(3, userListstring.Length - 3);
                            //Console.WriteLine(str1);
                            switch (strSplit)
                            {
                                case "100"://轮巡请求状态
                                    //Console.WriteLine("Online");
                                    //socket.SendTo(Encoding.UTF8.GetBytes("100"), newUserClient.Client.RemoteEndPoint);
                                    //writer.Write("100");
                                    //writer.Flush();
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        IpRoom room1 = new IpRoom();
                                        room1 = JsonHelper.DeserializeJsonToObject<IpRoom>(str1);
                                        
                                        for (int i = 0; i < room1.roomIDList.Count; i++)
                                        {
                                            IpRoom.RoomId rmd = room1.roomIDList[i];
                                            Class_Room rm = TerminalCheckTable(rmd);
                                            string message1 = JsonHelper.SerializeObject(rm);
                                            message1 = "105" + message1;

                                            byte[] bf = System.Text.Encoding.UTF8.GetBytes(message1);
                                            ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                            //s.Send();
                                            Thread.Sleep(300);
                                            //Console.WriteLine(message1);
                                        }
                                    }
                                    break;
                                case "101"://连接登录 102 登录OK ，103 登录NG
                                    //Console.WriteLine("Login");
                                    //string str1 = res.Substring(3, res.Length - 3);
                                    //Console.WriteLine(str2);
                                    if (str1.EndsWith("}"))
                                    {
                                        RemoteAdministor rm = null;
                                        rm = JsonHelper.DeserializeJsonToObject<RemoteAdministor>(str1);

                                        //Console.WriteLine(rm.Ip + "," + rm.userName + "," + rm.password);
                                    }
                                    //string message = "102{}";
                                    //Console.WriteLine(message + "," + message.Length);
                                    //int k = socket.Send(Encoding.UTF8.GetBytes(message));
                                    //Console.WriteLine(k);
                                    break;
                                case "104"://请求更新房间105更新OK，106更新NG
                                    //Console.WriteLine("update room");
                                    //string str2 = res.Substring(3, res.Length - 3);
                                    //Console.WriteLine(str2);
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        IpRoom room1 = new IpRoom();
                                        room1 = JsonHelper.DeserializeJsonToObject<IpRoom>(str1);

                                        for (int i = 0; i < room1.roomIDList.Count; i++)
                                        {
                                            IpRoom.RoomId rmd1 = room1.roomIDList[i];
                                            Class_Room rm1 = TerminalCheckTable(rmd1);
                                            string message105 = JsonHelper.SerializeObject(rm1);
                                            message105 = "105" + message105;
                                            byte[] bf = System.Text.Encoding.UTF8.GetBytes(message105);
                                            ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                            Thread.Sleep(300);
                                            //Console.WriteLine(message105);
                                        }
                                    }
                                    else
                                    {
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("106{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                    }
                                    break;
                                case "105":
                                    Console.WriteLine("Logout");
                                    break;
                                case "107":
                                    //Console.WriteLine("do something");
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        if (userList.Count > 0)
                                        {
                                            room = new Class_Room();
                                            room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                            string coffe = GetRoomName(room);
                                            string message5 = "notice," + "107," + coffe + " 需要提供 : ";
                                            int chunp = room.cunpinCoffee;
                                            int onetwo = room.oneaddtwoCoffee;
                                            if (chunp > 0 || onetwo > 0)
                                            {
                                                if (chunp > 0)
                                                {
                                                    message5 += chunp + "  份醇品咖啡 ";
                                                    if (room.CunpinCoffeeAddMilk)
                                                        message5 += "(加奶) ";
                                                    if (room.CunpinCoffeeAddSugar)
                                                        message5 += "(加糖) ";
                                                }
                                                if (onetwo > 0)
                                                {
                                                    message5 += onetwo + "  份1+2咖啡  ";
                                                    if (room.OneaddtwoCoffeeAddMilk)
                                                        message5 += "(加奶) ";
                                                    if (room.OneaddtwoCoffeeAddSugar)
                                                        message5 += "(加糖) ";
                                                }
                                                message5 += "!";
                                                //Console.WriteLine(message5);
                                                for (int i = 0; i < userList.Count; i++)
                                                {
                                                    // 给在线的其他用户发送终端消息
                                                    // 通知有新用户加入
                                                    //Console.WriteLine(userList[i].GetIndex() + "," + room.topID.ToString());
                                                    if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                        TcpSendtoClient(userList[i].GetSocket(), message5);
                                                }
                                            }
                                        }
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("108{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                        //Console.WriteLine("107=====");
                                        Thread.Sleep(100);
                                    }
                                    else
                                    {
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("109{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                    }
                                    break;
                                case "110":
                                    //Console.WriteLine("do something");
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        if (userList.Count > 0)
                                        {
                                            room = new Class_Room();
                                            room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                            string clear = GetRoomName(room);
                                            string message6 = "notice," + "110," + clear + " 需要清理会议室！";
                                            for (int i = 0; i < userList.Count; i++)
                                            {
                                                // 给在线的其他用户发送终端消息
                                                // 通知有新用户加入
                                                //Console.WriteLine(userList[i].GetIndex() + "," + room.topID.ToString());
                                                if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                    TcpSendtoClient(userList[i].GetSocket(), message6);
                                            }
                                        }
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("111{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                        Thread.Sleep(100);
                                    }
                                    else
                                    {
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("112{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                    }
                                    break;
                                case "113":
                                    //Console.WriteLine("ask for fruit");
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        if (userList.Count > 0)
                                        {
                                            room = new Class_Room();
                                            room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                            string fruit = GetRoomName(room);
                                            //string message5 = "notice," + "107," + coffe + " 需要提供  ";
                                            string message7 = "notice," + "113," + fruit + " 需要瓶装水！";
                                            for (int i = 0; i < userList.Count; i++)
                                            {
                                                // 给在线的其他用户发送终端消息
                                                // 通知有新用户加入
                                                //Console.WriteLine(userList[i].GetIndex() + "," + room.topID.ToString());
                                                if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                    TcpSendtoClient(userList[i].GetSocket(), message7);
                                            }
                                        }
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("114{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                        Thread.Sleep(100);
                                    }
                                    else
                                    {
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("115{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                    }
                                    break;
                                case "116":
                                    //Console.WriteLine("ask for water");
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        if (userList.Count > 0)
                                        {
                                            room = new Class_Room();
                                            room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                            string tea = GetRoomName(room);
                                            //string message5 = "notice," + "107," + coffe + " 需要提供  ";
                                            string message8 = "notice," + "116," + tea + " 需要提供 :";

                                            int redtea = room.Blacktea;
                                            int greentea = room.Greentea;
                                            if (redtea > 0 || greentea > 0)
                                            {
                                                if (redtea > 0)
                                                {
                                                    message8 += redtea + "  份红茶 ";
                                                }
                                                if (greentea > 0)
                                                {
                                                    message8 += greentea + "  份绿茶 ";
                                                }
                                                message8 += "!";
                                                //Console.WriteLine(message8);
                                                for (int i = 0; i < userList.Count; i++)
                                                {
                                                    // 给在线的其他用户发送终端消息
                                                    // 通知有新用户加入
                                                    //Console.WriteLine(userList[i].GetIndex() + "," + room.topID.ToString());
                                                    if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                        TcpSendtoClient(userList[i].GetSocket(), message8);
                                                }
                                            }
                                        }
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("117{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                        Thread.Sleep(100);
                                    }
                                    else
                                    {
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("118{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                    }
                                    break;
                                case "120":
                                    //Console.WriteLine("日期");
                                    if (str1.Contains("}") && str1.Length > 10)
                                    {
                                        IpRoom room1 = new IpRoom();
                                        room1 = JsonHelper.DeserializeJsonToObject<IpRoom>(str1);
                                        //room = new Class_Room();
                                        string message118 = "";
                                        for (int i = 0; i < room1.roomIDList.Count; i++)
                                        {
                                            IpRoom.RoomId rmd1 = room1.roomIDList[i];
                                            //room = TerminalCheckTable(rmd1);
                                            //room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                            Class_Room new_room = CheckRoomInfo(rmd1);
                                            message118 += JsonHelper.SerializeObject(new_room);
                                            if (0 == i && room1.roomIDList.Count > 1)
                                                message118 += "|";
                                        }
                                        message118 = "121" + message118;
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes(message118);
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                        Thread.Sleep(1000);
                                        //Console.WriteLine(message118);
                                    }
                                    else
                                    {
                                        byte[] bf = System.Text.Encoding.UTF8.GetBytes("122{}");
                                        ClientServer.Send(s_T, bf, 0, bf.Length, 10);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                    return;
                }
            }
        }


        /// <summary>
        /// 获取会议室名字
        /// </summary>
        /// <returns></returns>
        private string GetRoomName(Class_Room room)
        {
            int t = room.topID;
            int m = room.middleID;
            int l = room.lastID;
            string s = "";
            try
            {
                switch (t)
                {
                    case 1:
                        s = "内部会议室：" + room_1[m, l];
                        break;
                    case 2:
                        s = "坂田会议室：" + room_2[m, l];
                        break;
                    case 3:
                        s = "惠南会议室：" + room_3[m, l];
                        break;
                    default:
                        break;
                };
            }
            catch
            { }
            return s;
        }
        /// <summary>
        /// 返回终端查询会议室记录
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        private Class_Room CheckRoomInfo(IpRoom.RoomId rm)
        {
            int t = rm.topID;
            int m = rm.middleID;
            int l = rm.lastID;

            Class_Room room = new Class_Room();
            room.topID = t;
            room.middleID = m;
            room.lastID = l;
            room.status = 0;
            string s = "";
            try
            {
                switch (t)
                {
                    case 1:
                        s = "1_" + room_1[m,l];
                        break;
                    case 2:
                        s = "2_" + room_2[m,l];
                        break;
                    case 3:
                        s = "3_" + room_3[m,l];
                        break;
                    default:
                        break;
                };
                //Console.WriteLine(s);
                if (!s.Equals(""))
                {
                    DataTable tables = AccessFunction.GetTerminalProtocol(s);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        for (int i = 0; i < tables.Rows.Count; i++)
                        {
                            if (DateTime.Parse(tables.Rows[i][6].ToString()) < DateTime.Now.AddDays(15))
                            {
                                Class_Room.scheduleRoom mes1 = new Class_Room.scheduleRoom();
                                
                                mes1.peopleNumber = tables.Rows[i][3].ToString();
                                mes1.Customer = tables.Rows[i][4].ToString();
                                mes1.OrderPerson = tables.Rows[i][5].ToString();
                                mes1.startTime = ConvertDateTimeInt(DateTime.Parse(tables.Rows[i][6].ToString()));
                                mes1.endTime = ConvertDateTimeInt(DateTime.Parse(tables.Rows[i][7].ToString()));
                                mes1.Aim = tables.Rows[i][8].ToString();
                                mes1.id = int.Parse(tables.Rows[i][9].ToString());
                                //Console.WriteLine(mes1.startTime);
                                room.roomScheduleList.Add(mes1);
                            }
                        }
                    }
                }
            }
            catch
            { }
            return room;
        }

        /// <summary>
        /// 与终端TCP连接接收线程
        /// </summary>
        /// <param name="userClient"></param>
        private void ReceiveTerminalData(object userClient)
        {
            TcpClient newUserClient = (TcpClient)userClient;
            //string usereceive = null;
            networkStream = newUserClient.GetStream();
            reader = new BinaryReader(networkStream);
            writer = new BinaryWriter(networkStream);
            Socket socket = newUserClient.Client;
            
            Class_Room room = null;
            //EndPoint p = newUserClient.Client.RemoteEndPoint;
            
            while (true)
            {
                //Console.WriteLine("usereceive===" + Thread.CurrentThread.Name);
                byte[] buffer = new byte[2048];
                int length = 0;
                try
                {
                    //usereceive = reader.ReadString();
                    //if(networkStream.DataAvailable)
                    length = socket.Receive(buffer);
                    string res = Encoding.UTF8.GetString(buffer, 0, length);
                    //int n = newUserClient.Available;
                    //networkStream.Read(buffer, 0, n);
                    //Console.WriteLine("usereceive===" + length);
                    //byte[] b = reader.ReadBytes(20);
                    //string s = Encoding.UTF8.GetString(buffer, 0, n);
                    //res = res.Trim("\0".ToCharArray());
                    //if(length > 0)
                        //Console.WriteLine(res);
                    if (res.Length >= 3)
                    {
                        string strSplit = res.Substring(0, 3);
                        string str1 = res.Substring(3, res.Length - 3);
                        Console.WriteLine(str1);
                        switch (strSplit)
                        {
                            case "100"://轮巡请求状态
                                //Console.WriteLine("Online");
                                //socket.SendTo(Encoding.UTF8.GetBytes("100"), newUserClient.Client.RemoteEndPoint);
                                //writer.Write("100");
                                //writer.Flush();
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    IpRoom room1 = new IpRoom();
                                    room1 = JsonHelper.DeserializeJsonToObject<IpRoom>(str1);
                                    //Console.WriteLine("OK");
                                    /*
                                    room.status = 1;
                                    room.isSmoking = false;
                                    
                                    Class_Room.scheduleRoom mes1 = new Class_Room.scheduleRoom();
                                    mes1.id = 123;
                                    mes1.startTime = ConvertDateTimeInt(DateTime.Now);
                                    //Console.WriteLine(mes1.startTime);
                                    mes1.endTime = ConvertDateTimeInt(DateTime.Now);
                                    mes1.Customer = "xxx";
                                    mes1.Clerk = "yyyy";
                                    mes1.Aim = "======";
                                    //room.roomScheduleList roomlist = new List<Class_Room.scheduleRoom>();
                                    //roomlist.Add(mes1);
                                    //room.roomScheduleList = roomlist;
                                    room.roomScheduleList.Add(mes1);
                                     */
                                    //Console.WriteLine("OK" + room1.roomIDList.Count);
                                    for (int i = 0; i < room1.roomIDList.Count; i++)
                                    {
                                        IpRoom.RoomId rmd = room1.roomIDList[i];
                                        Class_Room rm = TerminalCheckTable(rmd);
                                        string message1 = JsonHelper.SerializeObject(rm);
                                        message1 = "105" + message1;
                                        writer.Write(message1);
                                        writer.Flush();
                                        Thread.Sleep(300);
                                        //Console.WriteLine(message1);
                                    }
                                }
                                break;
                            case "101"://连接登录 102 登录OK ，103 登录NG
                                //Console.WriteLine("Login");
                                //string str1 = res.Substring(3, res.Length - 3);
                                //Console.WriteLine(str2);
                                if (str1.EndsWith("}"))
                                {
                                    RemoteAdministor rm = null;
                                    rm = JsonHelper.DeserializeJsonToObject<RemoteAdministor>(str1);

                                    //Console.WriteLine(rm.Ip + "," + rm.userName + "," + rm.password);
                                }
                                string message = "102{}";
                                //Console.WriteLine(message + "," + message.Length);
                                int k = socket.Send(Encoding.UTF8.GetBytes(message));
                                //Console.WriteLine(k);
                                break;
                            case "104"://请求更新房间105更新OK，106更新NG
                                //Console.WriteLine("update room");
                                //string str2 = res.Substring(3, res.Length - 3);
                                //Console.WriteLine(str2);
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    IpRoom room1 = new IpRoom();
                                    room1 = JsonHelper.DeserializeJsonToObject<IpRoom>(str1);

                                    for (int i = 0; i < room1.roomIDList.Count; i++)
                                    {
                                        IpRoom.RoomId rmd1 = room1.roomIDList[i];
                                        Class_Room rm1 = TerminalCheckTable(rmd1);
                                        string message105 = JsonHelper.SerializeObject(rm1);
                                        message105 = "105" + message105;
                                        writer.Write(message105);
                                        writer.Flush();
                                        Thread.Sleep(300);
                                        //Console.WriteLine(message105);
                                    }
                                }
                                else
                                {
                                    writer.Write("106{}");//失败
                                    writer.Flush();
                                }
                                break;
                            case "105":
                                Console.WriteLine("Logout");
                                break;
                            case "107":
                                //Console.WriteLine("do something");
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    if (userList.Count > 0)
                                    {
                                        room = new Class_Room();
                                        room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                        string coffe = GetRoomName(room);
                                        string message5 = "notice," + "107," + coffe + " 需要提供 : ";
                                        int chunp = room.cunpinCoffee;
                                        int onetwo = room.oneaddtwoCoffee;
                                        if (chunp > 0 || onetwo > 0)
                                        {
                                            if (chunp > 0)
                                            {
                                                message5 += chunp + "  份醇品咖啡 ";
                                                if (room.CunpinCoffeeAddMilk)
                                                    message5 += "(加奶) ";
                                                if(room.CunpinCoffeeAddSugar)
                                                    message5 += "(加糖) ";
                                            }
                                            if (onetwo > 0)
                                            {
                                                message5 += onetwo + "  份1+2咖啡  ";
                                                if (room.OneaddtwoCoffeeAddMilk)
                                                    message5 += "(加奶) ";
                                                if (room.OneaddtwoCoffeeAddSugar)
                                                    message5 += "(加糖) ";
                                            }
                                            message5 += "!";
                                            Console.WriteLine(message5);
                                            for (int i = 0; i < userList.Count; i++)
                                            {
                                                // 给在线的其他用户发送终端消息
                                                // 通知有新用户加入
                                                //Console.WriteLine(userList[i].GetIndex() + "," + room.topID.ToString());
                                                //if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                    //TcpSendtoClient(userList[i].GetTcpClient(), message5);
                                            }
                                        }
                                    }
                                    writer.Write("109");
                                    writer.Flush();
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    writer.Write("108");
                                    writer.Flush();
                                }
                                break;
                            case "110":
                                //Console.WriteLine("do something");
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    if (userList.Count > 0)
                                    {
                                        room = new Class_Room();
                                        room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                        string clear = GetRoomName(room);
                                        //string message5 = "notice," + "107," + coffe + " 需要提供  ";
                                        string message6 = "notice," + "110," + clear + " 需要清理会议室！";
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            // 给在线的其他用户发送终端消息
                                            // 通知有新用户加入
                                            //if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                //TcpSendtoClient(userList[i].GetTcpClient(), message6);
                                        }
                                    }
                                    writer.Write("111");
                                    writer.Flush();
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    writer.Write("112");
                                    writer.Flush();
                                }
                                break;
                            case "113":
                                //Console.WriteLine("ask for fruit");
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    if (userList.Count > 0)
                                    {
                                        room = new Class_Room();
                                        room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                        string fruit = GetRoomName(room);
                                        //string message5 = "notice," + "107," + coffe + " 需要提供  ";
                                        string message7 = "notice," + "113," + fruit + " 需要瓶装水！";
                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            // 给在线的其他用户发送终端消息
                                            // 通知有新用户加入
                                            //if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                //TcpSendtoClient(userList[i].GetTcpClient(), message7);
                                        }
                                    }
                                    writer.Write("114");
                                    writer.Flush();
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    writer.Write("115");
                                    writer.Flush(); 
                                }
                                break;
                            case "116":
                                //Console.WriteLine("ask for water");
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    if (userList.Count > 0)
                                    {
                                        room = new Class_Room();
                                        room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                        string tea = GetRoomName(room);
                                        //string message5 = "notice," + "107," + coffe + " 需要提供  ";
                                        string message8 = "notice," + "116," + tea + " 需要提供 :";

                                        int redtea = room.Blacktea;
                                        int greentea = room.Greentea;
                                        if (redtea > 0 || greentea > 0)
                                        {
                                            if (redtea > 0)
                                            {
                                                message8 += redtea + "  份红茶 ";
                                            }
                                            if (greentea > 0)
                                            {
                                                message8 += greentea + "  份绿茶 ";
                                            }
                                            message8 += "!";
                                            Console.WriteLine(message8);
                                            for (int i = 0; i < userList.Count; i++)
                                            {
                                                // 给在线的其他用户发送终端消息
                                                // 通知有新用户加入 
                                                //if (userList[i].GetIndex().Equals(room.topID.ToString()))
                                                    //TcpSendtoClient(userList[i].GetTcpClient(), message8);
                                            }
                                        }
                                    }
                                    writer.Write("117");
                                    writer.Flush();
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    writer.Write("118");
                                    writer.Flush(); 
                                }
                                break;
                            case "120":
                                //Console.WriteLine("日期");
                                if (str1.Contains("}") && str1.Length > 10)
                                {
                                    IpRoom room1 = new IpRoom();
                                    room1 = JsonHelper.DeserializeJsonToObject<IpRoom>(str1);
                                    //room = new Class_Room();
                                    string message118 = "";
                                    for (int i = 0; i < room1.roomIDList.Count; i++)
                                    {
                                        IpRoom.RoomId rmd1 = room1.roomIDList[i];
                                        //room = TerminalCheckTable(rmd1);
                                        //room = JsonHelper.DeserializeJsonToObject<Class_Room>(str1);
                                        Class_Room new_room = CheckRoomInfo(rmd1);
                                        message118 += JsonHelper.SerializeObject(new_room);
                                        if (0 == i && room1.roomIDList.Count > 1)
                                            message118 += "|";
                                    }
                                    message118 = "121" + message118;
                                    writer.Write(message118);
                                    writer.Flush();
                                    Thread.Sleep(1000);
                                    //Console.WriteLine(message118);
                                }
                                else
                                {
                                    writer.Write("122");
                                    writer.Flush();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    //Thread.Sleep(100);
                    //break;
                }
                catch (Exception e)
                {
                    if (newUserClient.Connected)
                    {
                        socket = newUserClient.Client;
                        //networkStream = newUserClient.GetStream();
                        //reader = new BinaryReader(networkStream);
                        //break;
                    }
                    else
                    {
                        Console.WriteLine("接收数据失败,原因" + e.Message, "连接提示");
                        //reader.Close();
                        //writer.Close();
                        //networkStream = newUserClient.GetStream();
                        //reader = new BinaryReader(networkStream);
                        Thread.Sleep(100);
                        socket.Close();
                        newUserClient.Close();
                        Thread.CurrentThread.Abort();
                        //Console.WriteLine("出错！");
                        break;
                    }
                }
                Thread.Sleep(100);
            }
            socket.Close();
            newUserClient.Close();
        }

        #endregion

        #region ServerFormHandlers
        /// <summary>
        /// 日期时间转换UTC
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return (int)intResult;
        }      

        // 停止服务器
        private void btnStop_Click(object sender, EventArgs e)
        {
            /*
            if (newClient != null)
            {
                reader.Close();
                //writer.Close();
                newClient.Close();
            }
            if (newTerminal != null)
            {
                newTerminal.Close();
            }
            userList.Clear();
            tcpListener1.Stop();
            tcpListener.Stop();
             */ 
            //AddItemToListBox(string.Format("服务线程{0}终止", serverIPEndPoint));
            //tcpListener1.Stop();
            //AccessFunction.AccessCloseAll();
            ClientServer.Stop();
            //receiveUdpClient.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        
        private delegate void AddItemToListBoxDelegate(string message);
        /// <summary>
        /// 向ListBox中添加状态信息
        /// </summary>
        /// <param name="message">要添加的信息字符串</param>
        private void AddItemToListBox(string message)
        {
            // InvokeRequired代表如果调用线程与创建控件的线程不在一个线程上时，则返回true
            // 否则返回false
            if (listboxStatus.InvokeRequired)
            {
                AddItemToListBoxDelegate listboxdelegate = AddItemToListBox;
                listboxStatus.Invoke(listboxdelegate, message);
            }
            else
            {
                listboxStatus.Items.Add(message);
                listboxStatus.TopIndex = listboxStatus.Items.Count - 1;
                listboxStatus.ClearSelected();
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //tcpListener.Stop();
            /*
            if (newClient != null)
            {
                reader.Close();
                //writer.Close();
                //tcpListener1.Stop();
                tcpListener.Stop();
                newClient.Close();
            }
            if (newTerminal != null)
            {
                newTerminal.Close();
                tcpListener1.Stop();
                //tcpListener.Stop();
            }
            userList.Clear();
             */
            this.notifyIcon1.Visible = false;
            this.notifyIcon1.Dispose();
            ClientServer.Stop();
            AccessFunction.AccessCloseAll();
            Thread.Sleep(500);
            //Restart();
            System.Environment.Exit(0);

            //if (MessageBox.Show("要重新启动嘛？", "提示", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question) == DialogResult.Yes)
                //System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// 周期清理记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_auto_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("======------======");
            DataTable tables = null;
            try
            {
                for (int n = 0; n < tabel_name.Length; n++)
                {
                    //Console.WriteLine(tabel_name[n]);
                    tables = AccessFunction.GetHistoryTables(tabel_name[n]);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        timer_auto.Stop();
                        for (int i = 0; i < tables.Rows.Count; i++)
                        {
                            string s = tables.Rows[i][4].ToString();
                            //Console.WriteLine("sss=" + s);
                            if (!s.Equals(""))
                            {
                                DateTime d = DateTime.Parse(s);//DateTime.Now.Date.ToString("yyyy-MM-dd");
                                //Console.WriteLine("ddd=" + d);
                                if ((DateTime.Now - d).Days > 60)
                                {
                                    //string id = tables.Rows[i][7].ToString();
                                    //Console.WriteLine("sssss=" + s);
                                    AccessFunction.DeleteClientHistory(s, tabel_name[n]);
                                }
                            }
                        }
                    }
                }
                timer_auto.Interval = 60000;
                timer_auto.Start();
            }
            catch
            { }
        }
        
        private void Restart()
        {
            Application.ExitThread();
            Thread thtmp = new Thread(new ParameterizedThreadStart(run));
            object appName = Application.ExecutablePath;
            Thread.Sleep(2000);
            thtmp.Start(appName);
        }
        private void run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            //Console.WriteLine("222222");
            // 创建接收套接字
            //receiveUdpClient = new UdpClient(serverIPEndPoint);
            // 启动接收线程
            //Thread receiveThread = new Thread(ReceiveMessage);
            //receiveThread.Start();
            AccessFunction.AccessOpen();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            // 随机指定监听端口
            //Random random = new Random();
            //tcpPort = random.Next(port + 1, 65536);
            //IOCPServer ClientServer = new IOCPServer(7088, 100);
            ClientServer.Start(this);

            string path = Application.ExecutablePath;
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.SetValue("JcShutdown", path);
            rk2.Close();
            rk.Close();
            //TerminalServer.Start(this);
            // 创建监听套接字
            /*
            tcpListener1 = new TcpListener(serverIp, 8086);
            tcpListener1.Start(500);

            // 启动监听线程
            listenThread1 = new Thread(ListenTerminalConnect);
            listenThread1.Start();
            listenThread1.IsBackground = true;

            // 创建监听套接字
            tcpListener = new TcpListener(serverIp, 7088);
            tcpListener.Start(50);

            // 启动监听线程
            listenThread = new Thread(ListenClientConnect);
            listenThread.Start();
            listenThread.IsBackground = true;
             */
            //ServerForm_Resize(null,null);
            //AddItemToListBox(string.Format("服务器线程{0}启动，监听端口{1},{2}", serverIPEndPoint, 8086, 7088));
        }

        private void txbServerIP_TextChanged(object sender, EventArgs e)
        {
            changedIP = true;
        }

        private void ServerForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon1.Visible = true;//在通知区显示Form的Icon
                //this.WindowState = FormWindowState.Minimized;
                //this.Visible = false;
                this.ShowInTaskbar = false;//使Form不在任务栏上显示
                this.notifyIcon1.Icon = this.Icon;
                this.Hide();
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                this.notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            //this.Visible = true;
            this.ShowInTaskbar = true;
            //this.notifyIcon1.Visible = false;
            this.Show();
        }

        #endregion
    }

}
