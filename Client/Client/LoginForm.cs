using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// 添加额外命名空间
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Client
{
    public partial class LoginForm : Form
    {
        // 服务器端口
        int port;
        // 定义变量
        //private UdpClient sendUdpClient;
        //private UdpClient receiveUdpClient;
        private IPEndPoint clientIPEndPoint;
        public EndPoint clientEndPoint;
        private IPEndPoint ServerIPEndPoint;
        private TcpClient tcpClient;
        //private Thread receiveThread;
        //private NetworkStream networkStream;
        private BinaryReader binaryReader;
        private BinaryWriter binaryWriter;
        private string userListstring;
        private List<ChatFormcs> chatFormList = new List<ChatFormcs>();
        private List<Room_Info> roomList = new List<Room_Info>();
        private Form_Search dialogSearch;
        private string Index = "1";
        private string tempIndex = "1";
        public bool tcpConnect = false;
        //private System.Timers.Timer timer_Connect;
        private InitFiles settingFile;//配置文件
        private List<Control> Room_list = new List<Control>();
        private Client.IOCPClient.client m_client;
        /*
        private static Client.IOCPClient.SocketManager smanager = null;
        //private static UserInfoModel userInfo = null;

        //定义事件与委托  
        public delegate void ReceiveData(object message);
        public delegate void ServerClosed();
        public static event ReceiveData OnReceiveData;
        public static event ServerClosed OnServerClosed;

        /// <summary>  
        /// 心跳定时器  
        /// </summary>  
        private static System.Timers.Timer heartTimer = null;
        /// <summary>  
        /// 心跳包  
        /// </summary>  
        //private static ApiResponse heartRes = null;

        /// <summary>  
        /// 判断是否已连接  
        /// </summary>  
        public static bool Connected
        {
            get { return smanager != null && smanager.Connected; }
        }

        /// <summary>  
        /// 已登录的用户信息  
        /// </summary>  
        //public static UserInfoModel UserInfo
        //{
            //get { return userInfo; }
        //}  
        */

        public LoginForm()
        {
            InitializeComponent();
            IPAddress[] localIP = Dns.GetHostAddresses("192.168.3.69");//192.168.3.69
            txtserverIP.Text = localIP[0].ToString();
            txtLocalIP.Text = localIP[0].ToString();
            // 随机指定本地端口
            //settingFile = new IniFiles(Application.StartupPath + "\\setting.ini");
            
            Random random = new Random();
            port = random.Next(1024,65500);
            txtlocalport.Text = port.ToString();
            txtServerport.Text = "7088";
            string ip = txtLocalIP.Text;
            //m_client = new Client.IOCPClient.client(ip, 7088);

            // 随机生成用户名
            Random random2 = new Random((int)DateTime.Now.Ticks);
            txtusername.Text = "user" + random2.Next(100, 999);
            monthCalendarAdv1.DisplayMonth = DateTime.Now;
            btnLogout.Enabled = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            //panel_1.Visible = true;
            panel_1.BringToFront();
            panel_2.SendToBack();
            panel_3.SendToBack();
            panel_4.SendToBack();
            //panel_2.Hide();
            //panel_3.Hide();
            //panel_4.Hide();
            
            reflectionLabel_time.Text = DateTime.Now.ToString("HH:mm:ss");//<b><font size="+6"><i>当前时间：</i><font color="#B02B2C"> 19:19 </font></font></b>
            timer_T.Start();
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            //timer_Connect = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒； 
            //timer_Connect.Elapsed += new System.Timers.ElapsedEventHandler(OrderTimer_Tick);//到时间的时候执行事件； 
            //timer_Connect.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            //timer_Connect.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
            
            
        }

        // 登录服务器
        private void btnlogin_Click(object sender, EventArgs e)
        {
            // 创建接受套接字
            IPAddress clientIP = IPAddress.Parse(txtLocalIP.Text);
            clientIPEndPoint = new IPEndPoint(clientIP, int.Parse(txtlocalport.Text));
            //receiveUdpClient = new UdpClient(clientIPEndPoint);
            string ip = txtserverIP.Text;
            //IPAddress ServerIp = IPAddress.Parse(txtserverIP.Text);
            //ServerIPEndPoint = new IPEndPoint(ServerIp, int.Parse(txtServerport.Text));
            // 启动接收线程
            //receiveThread = new Thread(ReceiveMessage);
            //receiveThread.Start();
            //SocketError error = Connect();
            //if (error == SocketError.Success)
                //tcpConnect = true;
            m_client = new Client.IOCPClient.client(ip, 7088);
            m_client.Start(this);
            Thread.Sleep(1000);
            clientIPEndPoint = (IPEndPoint)clientEndPoint;
            /*
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(ServerIPEndPoint.Address, 8088);
                if (tcpClient.Connected)
                {
                    // 表示连接成功
                    networkStream = tcpClient.GetStream();
                    binaryReader = new BinaryReader(networkStream);
                    binaryWriter = new BinaryWriter(networkStream);
                    tcpConnect = true;

                    Thread receiveThread = new Thread(receiveMessage);
                    receiveThread.Start();
                    //MessageBox.Show("连接成功", "提示");
                   
                }
            }
            catch
            {
                tcpConnect = false;
                MessageBox.Show("连接失败,请检测网络的连接情况！", "异常");
                return;
            }
             */ 
            // 匿名发送
            //sendUdpClient = new UdpClient(0);
            // 启动发送线程
            if (tcpConnect)
            {
                Init_room();
                Thread sendThread = new Thread(ClientSendMessage);
                Thread.Sleep(200);
                sendThread.Start(string.Format("login,{0},{1},{2}", txtusername.Text, clientIPEndPoint, (comboBox3.SelectedIndex + 1).ToString()));
                //timer_Connect.Enabled = true;
                //timer_Connect.Start();
                timer_Date.Enabled = true;
                timer_Date.Start();
            }
            if (!tcpConnect)
            {
                tcpConnect = false;
                MessageBox.Show("连接失败,请检测网络的连接情况！", "异常");
                return;
            }
            btnlogin.Enabled = false;
            btnLogout.Enabled = true;
            button_setting.Text = "刷新";
            //this.Text = txtusername.Text;
        }
        /*
        /// <summary>  
        /// 连接到服务器  
        /// </summary>  
        /// <returns></returns>  
        public SocketError Connect()
        {
            if (Connected) return SocketError.Success;
            //我这里是读取配置,   
            string ip = "192.168.3.69";//Config.ReadConfigString("socket", "server", "");
            int port = 7088;//Config.ReadConfigInt("socket", "port", 13909);
            //if (string.IsNullOrWhiteSpace(ip) || port <= 1000) return SocketError.Fault;

            //创建连接对象, 连接到服务器  
            smanager = new Client.IOCPClient.SocketManager(ip, port);
            SocketError error = smanager.Connect();
            if (error == SocketError.Success)
            {
                //连接成功后,就注册事件. 最好在成功后再注册.  
                smanager.ServerDataHandler += OnReceivedServerData;
                smanager.ServerStopEvent += OnServerStopEvent;
            }
            return error;
        }

        /// <summary>  
        /// 断开连接  
        /// </summary>  
        public static void Disconnect()
        {
            try
            {
                smanager.Disconnect();
            }
            catch (Exception) { }
        }

        /// <summary>  
        /// 发送请求  
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>  
        //public static bool Send(ApiResponse request)
        //{
            //return Send(JsonConvert.SerializeObject(request));
        //}

        /// <summary>  
        /// 发送消息  
        /// </summary>  
        /// <param name="message">消息实体</param>  
        /// <returns>True.已发送; False.未发送</returns>  
        public static bool Send(string message)
        {
            if (!Connected) return false;

            byte[] buff = Encoding.Default.GetBytes(message);
            //加密,根据自己的需要可以考虑把消息加密  
            //buff = AESEncrypt.Encrypt(buff, m_aesKey);  
            smanager.Send(buff);
            return true;
        }

        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="buff"></param>  
        private void OnReceivedServerData(byte[] buff)
        {
            //To do something  
            //你要处理的代码,可以实现把buff转化成你具体的对象, 再传给前台  

            string msg = Encoding.Default.GetString(buff);
            newReceiveData(msg);
            if (OnReceiveData != null)
            {
                OnReceiveData(buff);
            }
             //OnReceiveData(buff);
        }

        /// <summary>  
        /// 服务器已断开  
        /// </summary>  
        private void OnServerStopEvent()
        {
            if (OnServerClosed != null)
                OnServerClosed();
        }  
        */
        /*
        // 客户端接受服务器回应消息 
        /// <summary>
        /// UDP 的协议传输
        /// </summary>
        private void ReceiveMessage()
        {
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any,0);
            while (true)
            {
                try
                {
                    // 关闭receiveUdpClient时会产生异常
                    byte[] receiveBytes = receiveUdpClient.Receive(ref remoteIPEndPoint);
                    string message = Encoding.Unicode.GetString(receiveBytes,0,receiveBytes.Length);

                    Console.WriteLine(message);
                    // 处理消息
                    string[] splitstring = message.Split(',');

                    switch (splitstring[0])
                    {
                        case "Accept":
                            try
                            {
                                tcpClient = new TcpClient();
                                tcpClient.Connect(remoteIPEndPoint.Address, int.Parse(splitstring[1]));
                                if (tcpClient != null)
                                {
                                    // 表示连接成功
                                    networkStream = tcpClient.GetStream();
                                    binaryReader = new BinaryReader(networkStream);
                                    binaryWriter = new BinaryWriter(networkStream);
                                    tcpConnect = true;

                                    //Thread receiveThread = new Thread(receiveMessage);
                                    //receiveThread.Start();
                                    //MessageBox.Show("连接成功", "提示");
                                }
                            }
                            catch
                            {
                                tcpConnect = false;
                                MessageBox.Show("连接失败", "异常");
                                return;
                            }
                            //Thread getUserListThread = new Thread(GetUserList);
                            //getUserListThread.Start();
                            break;
                        case "login":
                            string userItem = splitstring[1] + "," + splitstring[2];
                            AddItemToListView(userItem);
                            break;
                        case "logout":
                            RemoveItemFromListView(splitstring[1]);
                            break;
                        case "talk":
                            for (int i = 0; i < chatFormList.Count; i++)
                            {
                                if (chatFormList[i].Text == splitstring[2])
                                {
                                    chatFormList[i].ShowTalkInfo(splitstring[2], splitstring[1], splitstring[3]);
                                }
                            }
                            break;
                        case "addin":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            ChangeViewInfoRoom(message,true);
                            break;
                        case "clearroom":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            ChangeViewInfoRoom(message, false);
                            break;
                        case "reservate":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            SaveSchedulInfoRoom(message);
                            break;
                        case "check":
                            //RemoveItemFromListView(splitstring[1]);
                            Console.WriteLine(message);
                            CheckHistoryInfoRoom(message);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    break;
                }
            }
        }
        */
        // 从服务器获取在线用户列表
        private void GetUserList()
        {
            //userListstring = "connection start";
            //binaryWriter.Write(userListstring);
            while (true)
            {
                userListstring = null;
                try
                {
                    userListstring = binaryReader.ReadString();
                    if (userListstring.EndsWith("end"))
                    {
                        string[] splitstring = userListstring.Split(';');
                        for (int i = 0; i < splitstring.Length - 1; i++)
                        {
                            //AddItemToListView(splitstring[i]);
                        }
                        string message = splitstring[0];
                        splitstring = message.Split(',');
                        Console.WriteLine("+++++" + message);
                        switch (splitstring[0])
                        {
                            case "login":
                                string userItem = splitstring[1] + "," + splitstring[2];
                                AddItemToListView(userItem);
                                break;
                            case "logout":
                                RemoveItemFromListView(splitstring[1]);
                                break;
                            case "talk":
                                for (int i = 0; i < chatFormList.Count; i++)
                                {
                                    if (chatFormList[i].Text == splitstring[2])
                                    {
                                        chatFormList[i].ShowTalkInfo(splitstring[2], splitstring[1], splitstring[3]);
                                    }
                                }
                                break;
                            case "addin":
                                //RemoveItemFromListView(splitstring[1]);
                                //Console.WriteLine(message);
                                //ChangeViewInfoRoom(message, true);
                                break;
                            case "clearroom":
                                //RemoveItemFromListView(splitstring[1]);
                                //Console.WriteLine(message);
                                //ChangeViewInfoRoom(message, false);
                                break;
                            case "reservate":
                                //RemoveItemFromListView(splitstring[1]);
                                //Console.WriteLine(message);
                                SaveSchedulInfoRoom(message);
                                break;
                            case "check":
                                //RemoveItemFromListView(splitstring[1]);
                                Console.WriteLine(message);
                                CheckHistoryInfoRoom(message);
                                break;
                            default:
                                break;
                        }
                        //binaryReader.Close();
                        //tcpClient.Close();
                        //break;
                    }
                }
                catch
                {
                    if (binaryReader != null)
                    {
                        binaryReader.Close();
                    }
                    if (tcpClient != null)
                    {
                        tcpClient.Close();
                    }
                    break;
                }
            }
            //timer_Connect.Enabled = true;
            //timer_Connect.Start();
            //Console.WriteLine("start===");
        }
        /// <summary>
        /// 接收服务端信息，TCP的协议连接
        /// </summary>
        private void receiveMessage()
        {
            while (true)
            {
                userListstring = null;
                try
                {
                    userListstring = binaryReader.ReadString();
                    if (userListstring.EndsWith("end"))
                    {
                        //Send_OK = false;
                        string[] splitstring = userListstring.Split(';');
                        //Console.WriteLine(userListstring);
                        if (splitstring[0].Contains("update"))
                        {
                            for (int i = 1; i < splitstring.Length - 1; i++)
                            {
                                AddItemToListView(splitstring[i]);
                                //Console.WriteLine(splitstring[i]);
                            }
                        }
                        else
                        {
                            string message = splitstring[0];
                            splitstring = message.Split(',');
                            //Console.WriteLine("+++++" + message);
                            switch (splitstring[0])
                            {
                                case "status":
                                    //string userItem = splitstring[1] + "," + splitstring[2];
                                    //AddItemToListView(userItem);
                                    Console.WriteLine("+++++online");
                                    break;
                                case "login":
                                    string userItem = splitstring[1] + "," + splitstring[2];
                                    AddItemToListView(userItem);
                                    Console.WriteLine("Login");
                                    break;
                                case "logout":
                                    RemoveItemFromListView(splitstring[1]);
                                    break;
                                case "talk":
                                    for (int i = 0; i < chatFormList.Count; i++)
                                    {
                                        if (chatFormList[i].Text == splitstring[2])
                                        {
                                            chatFormList[i].ShowTalkInfo(splitstring[2], splitstring[1], splitstring[3]);
                                        }
                                    }
                                    break;
                                case "addin":
                                    //RemoveItemFromListView(splitstring[1]);
                                    //Console.WriteLine(message);
                                    //ChangeViewInfoRoom(message, true);
                                    break;
                                case "clearroom":
                                    //RemoveItemFromListView(splitstring[1]);
                                    Console.WriteLine(message);
                                    //ChangeViewInfoRoom(message, false);
                                    break;
                                case "reservate":
                                    //RemoveItemFromListView(splitstring[1]);
                                    //Console.WriteLine(message);
                                    //SaveSchedulInfoRoom(message);
                                    break;
                                case "check":
                                    //RemoveItemFromListView(splitstring[1]);
                                    Console.WriteLine(message);
                                    CheckHistoryInfoRoom(message);
                                    break;
                                case "notice":
                                    //RemoveItemFromListView(splitstring[1]);
                                    //Console.WriteLine(message);
                                    NoticeInfoRoom(message);
                                    //CheckHistoryInfoRoom(message);
                                    break;
                                default:
                                    break;
                            }
                        }
                        //binaryReader.Close();
                        //tcpClient.Close();
                        //break;
                    }

                }
                catch
                {
                    if (binaryReader != null)
                    {
                        binaryReader.Close();
                    }
                    if (tcpClient != null)
                    {
                        tcpClient.Close();
                    }
                    tcpConnect = false;
                    return;
                }
                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// 初始化界面房间列表
        /// </summary>
        private void Init_room()
        {
            Room_list.Clear();
            foreach (Control Control in panel_view.Controls)
            {
                if (Control is Panel)
                {
                    foreach (Control control in Control.Controls)
                    {
                        if (control is GroupBox)
                        {
                            Room_list.Add(control);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 设置会议室空闲显示
        /// </summary>
        private void Set_room()
        {
            foreach (Control control in Room_list)
            {
                //Console.WriteLine(control.Text);
                foreach (Control c in control.Controls)
                {
                    if (c is Button)
                    {
                        c.Invoke(new MethodInvoker(delegate()
                        {
                            c.Parent.BackgroundImage = global::Client.Properties.Resources.空闲;
                            c.Parent.BackgroundImageLayout = ImageLayout.Center;
                            c.Text = "安排房间";
                            c.BackColor = Color.LightCoral;
                            c.Dock = DockStyle.Bottom;
                        }));
                    }
                    if (c is RichTextBox)
                    {
                        c.Invoke(new MethodInvoker(delegate()
                        {
                            c.Text = "";
                            c.Visible = false;
                        }));
                    }
                    if (c is Label)
                    {
                        c.Invoke(new MethodInvoker(delegate()
                        {
                            c.Visible = true;
                        }));
                    }
                    if (c is PictureBox)
                    {
                        c.Invoke(new MethodInvoker(delegate()
                        {
                            c.Visible = false;
                        }));
                    }
                }
            }
        }

        private bool ReceiveData_flag = false;

        /// <summary>
        /// new 接收服务端信息
        /// </summary>
        /// <param name="userListstring"></param>
        public void newReceiveData(string userListstring)
        {
            if (userListstring.EndsWith("end"))
            {
                //Send_OK = false;
                string[] splitstring = userListstring.Split(';');
                //Console.WriteLine(userListstring);
                if (splitstring[0].Contains("update"))
                {
                    for (int i = 1; i < splitstring.Length - 1; i++)
                    {
                        AddItemToListView(splitstring[i]);
                        //Console.WriteLine(splitstring[i]);
                    }
                }
                else
                {
                    string message = splitstring[0];
                    splitstring = message.Split(',');
                    //Console.WriteLine("+++++" + message);
                    switch (splitstring[0])
                    {
                        case "status":
                            //string userItem = splitstring[1] + "," + splitstring[2];
                            //AddItemToListView(userItem);
                            //Console.WriteLine("+++" + message);
                            break;
                        case "login":
                            string userItem = splitstring[1] + "," + splitstring[2];
                            AddItemToListView(userItem);
                            //Console.WriteLine("Login");
                            break;
                        case "logout":
                            RemoveItemFromListView(splitstring[1]);
                            break;
                        case "talk":
                            for (int i = 0; i < chatFormList.Count; i++)
                            {
                                if (chatFormList[i].Text == splitstring[2])
                                {
                                    chatFormList[i].ShowTalkInfo(splitstring[2], splitstring[1], splitstring[3]);
                                }
                            }
                            break;
                        case "addin":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            ChangeViewInfoRoom(message, true);
                            //Thread thread1 = new Thread(ChangeRoom);
                            //thread1.Start(message);
                            break;
                        case "endaddin":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            ChangeViewInfoRoom(message, true);
                            //Thread thread1 = new Thread(ChangeRoom);
                            //thread1.Start(message);
                            break;
                        case "clearroom":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            ChangeViewInfoRoom(message, false);
                            break;
                        case "reservate":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            SaveSchedulInfoRoom(message);
                            break;
                        case "check":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            CheckHistoryInfoRoom(message);
                            break;
                        case "notice":
                            //RemoveItemFromListView(splitstring[1]);
                            //Console.WriteLine(message);
                            NoticeInfoRoom(message);
                            //CheckHistoryInfoRoom(message);
                            break;
                        case "delete":
                            Thread DeleteThread = new Thread(DeleteInfoFromTable);
                            DeleteThread.Start(message);
                            break;
                        case "receive":
                            ReceiveData_flag = true;
                            break;
                        case "endreceive":
                            ReceiveData_flag = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // 用委托机制来操作界面上控件
        private delegate void AddItemToListViewDelegate(string str);

        /// <summary>
        /// 在ListView中追加用户信息
        /// </summary>
        /// <param name="userinfo">要追加的信息</param>
        private void AddItemToListView(string userinfo)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                AddItemToListViewDelegate adddelegate = AddItemToListView;
                lstviewOnlineUser.Invoke(adddelegate, userinfo);
            }
            else
            {
                string[] splitstring = userinfo.Split(',');
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(splitstring[0]);
                item.SubItems.Add(splitstring[1]);
                lstviewOnlineUser.Items.Add(item);
            }
        }

        private delegate void RemoveItemFromListViewDelegate(string str);

        /// <summary>
        /// 从ListView中删除用户信息
        /// </summary>
        /// <param name="str">要删除的信息</param>
        private void RemoveItemFromListView(string str)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                RemoveItemFromListViewDelegate removedelegate = RemoveItemFromListView;
                lstviewOnlineUser.Invoke(removedelegate, str);
            }
            else
            {
                for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
                {
                    if (lstviewOnlineUser.Items[i].SubItems[1].Text == str)
                    {
                        lstviewOnlineUser.Items[i].Remove();
                    }
                }
            }
        }

        private void ChangeRoom(Object obj)
        {
            string message = (string)obj;
            ChangeViewInfoRoomDelegate CVIR = new ChangeViewInfoRoomDelegate(ChangeViewInfoRoom);
            Invoke(CVIR, message);
        }

        private delegate void ChangeViewInfoRoomDelegate(string str, bool f);
        /// <summary>
        /// 动态改变界面显示
        /// </summary>
        /// <param name="str"></param>
        private void ChangeViewInfoRoom(string str, bool f)
        {
            string[] splitstring;
            if(str.Contains(";"))
                splitstring = str.Split(';')[0].Split(',');
            else
                splitstring = str.Split(',');
            Panel panel;
            if (splitstring[3].Contains("1"))
                panel = panel_1;
            else if (splitstring[3].Contains("2"))
                panel = panel_2;
            else if (splitstring[3].Contains("3"))
                panel = panel_3;
            else
                panel = panel_1;
            //Console.WriteLine(splitstring.Length);
            foreach (Control control in panel.Controls)
            {
                if (control is GroupBox && control.Text == splitstring[4])
                {
                    if (control.InvokeRequired)
                    {
                        ChangeViewInfoRoomDelegate changedelegate = ChangeViewInfoRoom;
                        control.Invoke(changedelegate, str, f);
                    }
                    else
                    {
                        Room_list.Remove(control);
                        //Console.WriteLine(control.Text + "=,=" + splitstring[4]);
                        foreach (Control c in control.Controls)
                        {
                            //Console.WriteLine(c.Name);
                            if (c is Button)
                            {
                                if (f)
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                    {
                                        //Console.WriteLine(c.Name);
                                        c.Text = "清除房间";
                                        c.BackColor = Color.Firebrick;
                                    }));
                                }
                                else
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                    {
                                        c.Parent.BackgroundImage = global::Client.Properties.Resources.空闲;
                                        c.Parent.BackgroundImageLayout = ImageLayout.Center;
                                        c.Text = "安排房间";
                                        c.BackColor = Color.LightCoral;
                                    }));
                                }
                                c.Dock = DockStyle.Bottom;
                            }
                            if (c is RichTextBox)
                            {
                                if (f)
                                {
                                    try
                                    {
                                        c.Invoke(new MethodInvoker(delegate()
                                        {
                                            //Console.WriteLine(c.Name);
                                            c.Text = "";
                                            c.Text += "参会人数： " + splitstring[5] + "\r";
                                            c.Text += "宾  客： " + splitstring[6] + "\r";
                                            c.Text += "预定人： " + splitstring[7] + "\r";
                                            c.Text += "会议开始时间： " + splitstring[8] + "\r";
                                            c.Text += "会议结束时间： " + splitstring[9] + "\r";
                                            c.Text += "备注： " + splitstring[10];
                                            c.Visible = true;
                                        }));
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                   {
                                       c.Text = "";
                                       c.Visible = false;
                                   }));
                                }
                            }
                            if (c is Label)
                            {
                                if (f)
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                    {
                                        c.Visible = false;
                                    }));
                                }
                                else
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                    {
                                        c.Visible = true;
                                    }));
                                }
                            }
                            if (c is PictureBox)
                            {
                                if (f)
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                    {
                                        c.Visible = true;
                                    }));
                                }
                                else
                                {
                                    c.Invoke(new MethodInvoker(delegate()
                                    {
                                        c.Visible = false;
                                    }));
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// 同步记录预约的信息
        /// </summary>
        /// <param name="str"></param>
        private void SaveSchedulInfoRoom(string message)
        {
            string[] splitstring = message.Split(',');
            //Console.WriteLine(message + ":" + splitstring.Count());
            int n = splitstring.Count();
            string[] str = new string[n - 3];
            for (int i = 1; i < n - 3; i++)
            {
                str[i] = splitstring[i + 3];
            }
            str[0] = splitstring[3] + "_" + splitstring[4];
            AccessFunction.InsertClientProtocol(str);
        }


        // 用委托机制来操作界面上控件
        private delegate void CheckHistoryInfoRoomDelegate(string str);
        /// <summary>
        /// 把查询搜索的历史记录显示
        /// </summary>
        /// <param name="message"></param>
        private void CheckHistoryInfoRoom(string message)
        {
            if (dataGridView_history.InvokeRequired)
            {
                CheckHistoryInfoRoomDelegate checkdelegate = CheckHistoryInfoRoom;
                dataGridView_history.Invoke(checkdelegate, message);
            }
            else
            {
                string[] splitstring = message.Split(',');
                if (splitstring[1] == "endcheck")
                {
                    dialogSearch.ChangeStateInfo("查询结束！");
                    dialogSearch.CloseForm();
                }
                else
                {
                    try
                    {
                        dataGridView_history.Invoke(new MethodInvoker(delegate()
                        {
                            int index = dataGridView_history.Rows.Add();
                            dataGridView_history.Rows[index].Cells[0].Value = splitstring[1];
                            dataGridView_history.Rows[index].Cells[1].Value = splitstring[2];
                            dataGridView_history.Rows[index].Cells[2].Value = splitstring[3];
                            dataGridView_history.Rows[index].Cells[3].Value = splitstring[4];
                            dataGridView_history.Rows[index].Cells[4].Value = splitstring[5];
                            dataGridView_history.Rows[index].Cells[5].Value = splitstring[6];
                            dataGridView_history.Rows[index].Cells[6].Value = splitstring[7];
                        }));
                    }
                    catch
                    {
                        dialogSearch.CloseForm();
                        MessageBox.Show("查询出错！");
                    }
                }
            }
        }

        // 用委托机制来操作界面上控件
        private delegate void NoticeInfoRoomDelegate(string str);
        /// <summary>
        /// 终端通知信息显示
        /// </summary>
        /// <param name="message"></param>
        private void NoticeInfoRoom(string message)
        {
            //MessageBox.Show(message,"Tips");
            if (message.Contains("notice"))
            {
                this.BeginInvoke(new Action(() =>
                {
                    Form_Tips tips = new Form_Tips();
                    tips.TopLevel = true;
                    tips.SetInit(message);
                    tips.Show();
                }));
            }
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
                //AccessFunction.DeleteTerminalProtocol(s, splitstring[5], splitstring[6]);
                AccessFunction.DeleteClientProtocol(s);
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///  发送登录udp请求
        /// </summary>
        /// <param name="obj"></param>
        private void SendMessage(object obj)
        {
            /*
            RemoteAdministor rm = new RemoteAdministor();
            rm.Ip = "192.168.3.69";
            rm.password = "123456";
            rm.userName = "dwl";
            string message = JsonHelper.SerializeObject(rm);//(string)obj;
             
            string message = (string)obj;
            message += ";end";
            //Console.WriteLine(message);
            binaryWriter.Write(message);
            binaryWriter.Flush();
            */
            string message = (string)obj;
            byte[] sendbytes = Encoding.Unicode.GetBytes(message);
            IPAddress remoteIp = IPAddress.Parse(txtserverIP.Text);
            IPEndPoint remoteIPEndPoint = new IPEndPoint(remoteIp, int.Parse(txtServerport.Text));
            //sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
            //sendUdpClient.Close();
            
        }
        //private bool Send_OK = false;
        /// <summary>
        /// tcp连接数据发送
        /// </summary>
        /// <param name="obj"></param>
        private void ClientSendMessage(object obj)
        {
            if (m_client.m_socket.Connected)
            {
                //Send_OK = true;
                string message = (string)obj;
                message += ";end";
                //Console.WriteLine(message.Length);
                //while (Send_OK)
                //Send(message);
                ReceiveData_flag = false;
                for (int i = 0; i < 3; i++)
                {
                    if (!ReceiveData_flag)
                    {
                        m_client.Send(message);
                        if (message.Contains("logout"))
                            break;
                    }
                    else
                        return;
                    Thread.Sleep(3000);
                    //Console.WriteLine(ReceiveData_flag);
                }
                //Console.WriteLine(ReceiveData_flag);
            }
        }

        // 退出
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // 匿名发送
            //sendUdpClient = new UdpClient();
            //启动发送线程
            //Thread sendThread = new Thread(SendMessage);
            //timer_Connect.Stop();
            DialogResult t = MessageBox.Show(" 确定断开与服务端的通讯连接！", " 提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (t == DialogResult.Yes || t == DialogResult.OK)
            {
                timer_Date.Stop();
                ClientSendMessage(string.Format("logout,{0},{1}", txtusername.Text, clientIPEndPoint));
                Thread.Sleep(300);
                /*
                binaryReader.Close();
                binaryWriter.Close();
                tcpClient.Close();
                 */
                m_client.DisConnect();
                //Disconnect();
                tcpConnect = false;
                //receiveUdpClient.Close();
                //receiveThread.Abort();
                lstviewOnlineUser.Items.Clear();
                btnlogin.Enabled = true;
                btnLogout.Enabled = false;
                button_setting.Text = "配置连接设置";
            }
            //this.Text = "Client";
        }

        public void Disconnect()
        {
            //timer_Connect.Stop();
            timer_Date.Stop();
            tcpConnect = false;
            this.Invoke(new Action(() =>
                {
                    btnlogin.Enabled = true;
                    btnLogout.Enabled = false;
                    button_setting.Text = "配置连接设置";
                }));
            MessageBox.Show("与服务端连接断开,请检测网络连接情况！", "异常");
        }

        /// <summary>
        ///  双击打开与某个用户聊天的子窗口 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstviewOnlineUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string peerName = lstviewOnlineUser.SelectedItems[0].SubItems[1].Text;
            //Console.WriteLine(peerName);
            if (peerName == txtusername.Text)
            {
                return;
            }
            string ipEndPoint = lstviewOnlineUser.SelectedItems[0].SubItems[2].Text;
            string[] splitString = ipEndPoint.Split(':');
            IPAddress peerIP = IPAddress.Parse(splitString[0]);
            IPEndPoint peerIPEndPoint = new IPEndPoint(peerIP,int.Parse(splitString[1]));
            ChatFormcs dialogChat = new ChatFormcs();
            dialogChat.SetUserInfo(txtusername.Text, peerName, peerIPEndPoint);
            dialogChat.Text = peerName;
            chatFormList.Add(dialogChat);
            dialogChat.Show();
        }

        public void OtherSend(string str)
        {
            if (tcpConnect)
            {
                //sendUdpClient = new UdpClient();
                //str = str + Index;
                //Console.WriteLine("====" + str);
                //启动发送线程
                Thread sendThread = new Thread(ClientSendMessage);
                sendThread.Start(str);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 关闭软件退出发送
            AccessFunction.AccessCloseAll();
            timer_T.Stop();
            timer_T.Dispose();
            this.notifyIcon1.Dispose();
            if (tcpConnect)
            {
                //sendUdpClient = new UdpClient();
                //启动发送线程
                //Thread sendThread = new Thread(SendMessage);
                ClientSendMessage(string.Format("logout,{0},{1}", txtusername.Text, clientIPEndPoint));
                //receiveUdpClient.Close();
                Thread.Sleep(200);
                //m_client.DisConnect();
                //tcpClient.Close();
                //Disconnect();
                m_client.DisConnect();
                tcpConnect = false;
                lstviewOnlineUser.Items.Clear();
            }
        }
        /// <summary>
        /// 获取会议室的信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Room_Info GetRoom(string str)
        {
            foreach (Room_Info room in roomList)
            {
                if (room.GetName() == str)
                {
                    return room;
                }
            }
            return null;
        }
        /// <summary>
        /// 修改会议室列表的信息
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rm"></param>
        public void SetRoom(string str,Room_Info rm)
        {
            int i = 0;
            foreach (Room_Info room in roomList)
            {
                if (room.GetName() == str)
                {
                    i = roomList.IndexOf(room);
                     break;
                }
            }
            roomList[i] = rm;
        }
        /// <summary>
        /// 通用安排房间进行界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Arrange_room(object sender, EventArgs e)
        {
            if (!tcpConnect)
            {
                MessageBox.Show("网络未正常连接,请检测网络！", "提示");
                return;
            }
            work_Idle = false;
            /*
            Set_room();
            Init_room();
            Thread sendThread = new Thread(ClientSendMessage);
            sendThread.Start(string.Format("status,{0},{1},{2}", txtusername.Text, clientIPEndPoint, (comboBox3.SelectedIndex + 1).ToString()));
            Thread.Sleep(500);
             */ 
            //Room_Info room = new Room_Info((sender as Button).Parent.Text, "", "", "", "", "",false);
            if ((sender as Button).Text == "安排房间")
            {
                Form_Room arrange_room = new Form_Room(this);
                arrange_room.SetUserInfo((sender as Button).Parent.Text, txtusername.Text, clientIPEndPoint, Index);
                //arrange_room.Text = peerName;
                //chatFormList.Add(dialogChat);
                arrange_room.ShowDialog();
                Room_Info room = GetRoom((sender as Button).Parent.Text);
                if (room != null)
                {
                    if (room.Getstate())//查看房间状态
                    {
                        (sender as Button).Text = "清除房间";
                        (sender as Button).BackColor = Color.Firebrick;
                        (sender as Button).Dock = DockStyle.Bottom;
                        foreach (Control control in (sender as Button).Parent.Controls)
                        {
                            if (control is RichTextBox)
                            {
                                control.Text = "";
                                control.Text += "宾  客： " + room.GetCustomer() + "\r";
                                control.Text += "预定人： " + room.GetBusiness() + "\r";
                                control.Text += "参会人数： " + room.GetNember() + "\r";
                                control.Text += "会议开始时间： " + room.GetDatetime() + "\r";
                                control.Text += "会议结束时间： " + room.GetEndtime() + "\r";
                                control.Text += "备注： " + room.GetTips();
                                control.Visible = true;
                                control.Dock = DockStyle.Fill;
                                //Console.WriteLine(control.Name);
                            }
                            if (control is Label)
                                control.Visible = false;
                            if (control is PictureBox)
                                control.Visible = true;
                        }
                    }
                }
            }
            else
            {
                DialogResult t = MessageBox.Show(" 确定对选中的会议室使用状态进行清除整理！", " 提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (t == DialogResult.Yes || t == DialogResult.OK)
                {
                    Room_Info r = new Room_Info((sender as Button).Parent.Text, "", "", "", "", "", "", false);
                    SetRoom((sender as Button).Parent.Text, r);
                    foreach (Control control in (sender as Button).Parent.Controls)
                    {
                        if (control is RichTextBox)
                        {
                            control.Text = "";
                            control.Visible = false;
                        }
                        if (control is Label)
                            control.Visible = true;
                        if (control is PictureBox)
                            control.Visible = false;
                    }
                    OtherSend(string.Format("clearroom,{0},{1},{2},{3}", txtusername.Text, clientIPEndPoint, Index, (sender as Button).Parent.Text));
                    (sender as Button).Parent.BackgroundImage = global::Client.Properties.Resources.空闲;
                    (sender as Button).Parent.BackgroundImageLayout = ImageLayout.Center;
                    (sender as Button).Text = "安排房间";
                    (sender as Button).BackColor = Color.LightCoral;
                    (sender as Button).Dock = DockStyle.Bottom;
                }
            }
            work_Idle = true;
        }
        /// <summary>
        /// 坂田
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_2_Click(object sender, EventArgs e)
        {
            //panel_1.Visible = false;
            //panel_3.Visible = false;
            panel_2.Visible = true;
            //panel_4.Visible = false;
            panel_1.SendToBack();
            panel_3.SendToBack();
            panel_4.SendToBack();
            panel_2.BringToFront();
            panel_2.Select();
            button_1.BackgroundImage = global::Client.Properties.Resources.out2;
            button_2.BackgroundImage = global::Client.Properties.Resources.in1;
            button_3.BackgroundImage = global::Client.Properties.Resources.oth2;
            Index = "2";
            work_Idle = true;
        }
        /// <summary>
        /// 惠州
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_3_Click(object sender, EventArgs e)
        {
            //panel_1.Visible = false;
            //panel_2.Visible = false;
            panel_3.Visible = true;
            //panel_4.Visible = false;
            panel_2.SendToBack();
            panel_1.SendToBack();
            panel_4.SendToBack();
            panel_3.BringToFront();
            panel_3.Select();
            button_1.BackgroundImage = global::Client.Properties.Resources.out2;
            button_2.BackgroundImage = global::Client.Properties.Resources.in2;
            button_3.BackgroundImage = global::Client.Properties.Resources.oth1;
            Index = "3";
            work_Idle = true;
        }
        /// <summary>
        /// 内部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_1_Click(object sender, EventArgs e)
        {
            panel_1.Visible = true;
            //panel_2.Visible = false;
            //panel_3.Visible = false;
            //panel_4.Visible = false;
            panel_2.SendToBack();
            panel_3.SendToBack();
            panel_4.SendToBack();
            panel_1.BringToFront();
            panel_1.Select();
            button_1.BackgroundImage = global::Client.Properties.Resources.out1;
            button_2.BackgroundImage = global::Client.Properties.Resources.in2;
            button_3.BackgroundImage = global::Client.Properties.Resources.oth2;
            Index = "1";
            work_Idle = true;
        }
        /// <summary>
        /// 记录查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_4_Click(object sender, EventArgs e)
        {
            work_Idle = false;
            panel_4.Visible = true;
            panel_2.SendToBack();
            panel_3.SendToBack();
            panel_1.SendToBack();
            panel_4.BringToFront();
            panel_4.Select();
            button_1.BackgroundImage = global::Client.Properties.Resources.out2;
            button_2.BackgroundImage = global::Client.Properties.Resources.in2;
            button_3.BackgroundImage = global::Client.Properties.Resources.oth2;
        }
        /// <summary>
        /// 选择会议室地点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox2.Items.Clear();
                foreach (Control control in panel_1.Controls)
                {
                    if (control is GroupBox)
                    {
                        comboBox2.Items.Add(control.Text);
                    }
                }
                comboBox2.SelectedIndex = 0;
                tempIndex = "1";
            }
            else if (comboBox1.SelectedIndex == 1)
            { 
                comboBox2.Items.Clear();
                foreach (Control control in panel_2.Controls)
                {
                    if (control is GroupBox)
                    {
                        comboBox2.Items.Add(control.Text);
                    }
                }
                comboBox2.SelectedIndex = 0;
                tempIndex = "2";
            }
            else if (comboBox1.SelectedIndex == 2)
            { 
                comboBox2.Items.Clear();
                foreach (Control control in panel_3.Controls)
                {
                    if (control is GroupBox)
                    {
                        if (control.Enabled)
                            comboBox2.Items.Add(control.Text);
                        //Console.WriteLine(control.Name);
                    }
                }
                comboBox2.SelectedIndex = 0;
                tempIndex = "3";
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            dataGridView_history.Columns[0].HeaderCell.Value = "会议室名称";
            dataGridView_history.Columns[1].HeaderCell.Value = "参会人数";
            dataGridView_history.Columns[2].HeaderCell.Value = "宾客";
            dataGridView_history.Columns[3].HeaderCell.Value = "预约人";
            dataGridView_history.Columns[4].HeaderCell.Value = "开始时间";
            dataGridView_history.Columns[5].HeaderCell.Value = "结束时间";
            dataGridView_history.Columns[6].HeaderCell.Value = "备注";

            AccessFunction.AccessOpen();

            settingFile = new InitFiles(Application.StartupPath + "\\setting.ini");
            txtserverIP.Text = settingFile.ReadString("SETTING", "SERVERIP", txtserverIP.Text);
            txtLocalIP.Text = settingFile.ReadString("SETTING", "LOCALIP", txtLocalIP.Text);
            // 随机指定本地端口
            comboBox3.SelectedIndex = settingFile.ReadInteger("SETTING", "INDEX", 0);
            //初始化配置信息
            Init_FormFile();

            foreach (Control Control in panel_view.Controls)
            {
                if (Control is Panel)
                {
                    foreach (Control control in Control.Controls)
                    {
                        if (control is GroupBox)
                        {
                            //Console.WriteLine(control.Text);
                            Room_Info r = new Room_Info(control.Text, "", "", "", "", "", "", false);
                            roomList.Add(r);
                            Room_list.Add(control);
                        }
                    }
                }
            }
            if (settingFile.ReadBool("SETTING", "AUTORUN", false))
            {
                try
                {
                    btnlogin_Click(null, null);
                }
                catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init_FormFile()
        {
            this.Text = settingFile.ReadString("SETTING", "TITLE", "KTC会客管理系统");
            this.groupBox3.Text = settingFile.ReadString("FORM1", "NAME1", "研发会议室一");
            this.groupBox2.Text = settingFile.ReadString("FORM1", "NAME2", "会议室1");
            this.groupBox4.Text = settingFile.ReadString("FORM1", "NAME3", "会议室3");
            this.groupBox5.Text = settingFile.ReadString("FORM1", "NAME4", "电气部对面");
            this.groupBox6.Text = settingFile.ReadString("FORM1", "NAME5", "软件部");
            this.groupBox7.Text = settingFile.ReadString("FORM1", "NAME6", "结构部");
            this.groupBox8.Text = settingFile.ReadString("FORM1", "NAME7", "皓丽部");
            this.groupBox9.Text = settingFile.ReadString("FORM1", "NAME8", "会议室2");
            this.groupBox10.Text = settingFile.ReadString("FORM1", "NAME9", "大客户六部");
            this.groupBox11.Text = settingFile.ReadString("FORM1", "NAME10", "业务部");
            this.groupBox12.Text = settingFile.ReadString("FORM1", "NAME11", "电气部");
            this.groupBox49.Text = settingFile.ReadString("FORM1", "NAME12", "财务部");
            this.groupBox54.Text = settingFile.ReadString("FORM1", "NAME13", "研发会议室二");

            this.groupBox22.Text = settingFile.ReadString("FORM2", "NAME1", "1号");
            this.groupBox23.Text = settingFile.ReadString("FORM2", "NAME2", "2号");
            this.groupBox21.Text = settingFile.ReadString("FORM2", "NAME3", "3号");
            this.groupBox26.Text = settingFile.ReadString("FORM2", "NAMEC", "C号");
            this.groupBox27.Text = settingFile.ReadString("FORM2", "NAME5", "5号");
            this.groupBox25.Text = settingFile.ReadString("FORM2", "NAME6", "6号");
            this.groupBox29.Text = settingFile.ReadString("FORM2", "NAMEA", "会议室A");
            this.groupBox30.Text = settingFile.ReadString("FORM2", "NAMED", "多功能厅");
            this.groupBox20.Text = settingFile.ReadString("FORM2", "NAME7", "7号");
            this.groupBox19.Text = settingFile.ReadString("FORM2", "NAME8", "8号");
            this.groupBox18.Text = settingFile.ReadString("FORM2", "NAME9", "9号");
            this.groupBox17.Text = settingFile.ReadString("FORM2", "NAME10", "10号");
            this.groupBox16.Text = settingFile.ReadString("FORM2", "NAME11", "11号");
            this.groupBox15.Text = settingFile.ReadString("FORM2", "NAMEB", "会议室B");
            this.groupBox14.Text = settingFile.ReadString("FORM2", "NAME12", "12号");
            this.groupBox13.Text = settingFile.ReadString("FORM2", "NAME13", "13号");
            this.groupBox24.Text = settingFile.ReadString("FORM2", "NAME14", "14号");
            this.groupBox33.Text = settingFile.ReadString("FORM2", "NAME15", "15号");
            this.groupBox32.Text = settingFile.ReadString("FORM2", "NAME17", "17号");
            this.groupBox31.Text = settingFile.ReadString("FORM2", "NAME18", "18号");
            this.groupBox34.Text = settingFile.ReadString("FORM2", "NAME16", "16号");
            this.groupBox35.Text = settingFile.ReadString("FORM2", "NAME19", "19号");
            this.groupBox41.Text = settingFile.ReadString("FORM2", "NAME20", "20号");
            this.groupBox53.Text = settingFile.ReadString("FORM2", "NAME21", "21号");


            this.groupBox51.Text = settingFile.ReadString("FORM3", "NAME1", "1号");
            this.groupBox52.Text = settingFile.ReadString("FORM3", "NAME2", "2号");
            this.groupBox50.Text = settingFile.ReadString("FORM3", "NAME3", "3号");
            this.groupBox39.Text = settingFile.ReadString("FORM3", "NAME5", "5号");
            this.groupBox40.Text = settingFile.ReadString("FORM3", "NAME6", "6号");
            this.groupBox38.Text = settingFile.ReadString("FORM3", "NAME7", "7号");
            this.groupBox48.Text = settingFile.ReadString("FORM3", "NAME9", "9号");
            this.groupBox47.Text = settingFile.ReadString("FORM3", "NAME10", "10号");
            this.groupBox45.Text = settingFile.ReadString("FORM3", "NAME12", "12号");
            this.groupBox44.Text = settingFile.ReadString("FORM3", "NAME13", "13号");
            this.groupBox43.Text = settingFile.ReadString("FORM3", "NAME15", "15号");
            this.groupBox42.Text = settingFile.ReadString("FORM3", "NAMED", "多功能厅");
            this.groupBox36.Text = settingFile.ReadString("FORM3", "NAMEA", "会议室A");

            this.groupBox37.Text = settingFile.ReadString("FORM3", "NAMEJ", "技术部会议室");
            this.groupBox46.Text = settingFile.ReadString("FORM3", "NAME11", "会议室11");

        }

        /// <summary>
        /// 查询历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_check_Click(object sender, EventArgs e)
        {
            if (tcpConnect)
            {
                //while (dataGridView_history.Rows.Count > 1)
                {
                    dataGridView_history.Rows.Clear();
                }
                string s = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                OtherSend(string.Format("check,{0},{1},{2},{3},{4}", txtusername.Text, clientIPEndPoint, tempIndex, comboBox2.Text, s));
                dialogSearch = new Form_Search();
                dialogSearch.SetSearchInfo("正在进行查询！");
                int x = panel_main.Size.Width - panel_data.Size.Width;
                int y = panel_main.Size.Height - panel_data.Size.Height;
                int x1 = this.Location.X + dataGridView_history.Size.Width / 2 - 110;
                int y1 = this.Location.Y + dataGridView_history.Size.Height / 2 - 60;
                //Console.WriteLine(this.Location.X);
                dialogSearch.Location = new Point(x + x1, y + y1);
                dialogSearch.Show();
            }
            else
            {
                MessageBox.Show("网络连接异常,请检测网络！", "提示");
                return;
            }
        }
        //public Dictionary<string, bool> myDictionary = newDictionary<string, bool>();
        public bool[] Dictionary = new bool[100];
        /// <summary>
        /// 预约日程提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Date_Tick(object sender, EventArgs e)
        {
            DataTable tables = AccessFunction.GetTables();
            if (tables != null && tables.Rows.Count > 0)
            {
                timer_Date.Stop();
                for (int i = 0; i < tables.Rows.Count; i++)
                {
                    string s = tables.Rows[i][5].ToString();
                    //Console.WriteLine("sss=" + s);
                    DateTime dd = DateTime.Parse(s);
                    DateTime d = DateTime.Parse(s).AddMinutes(-10);//DateTime.Now.Date.ToString("yyyy-MM-dd");
                    //Console.WriteLine("ddd=" + d.TimeOfDay);
                    DateTime b = DateTime.Parse(s).AddMinutes(-5);
                    if (DateTime.Now.Date == d.Date && DateTime.Now.TimeOfDay >= d.TimeOfDay)
                    {
                        if (DateTime.Now.Date == b.Date && DateTime.Now.TimeOfDay <= b.TimeOfDay)
                        {
                            string room = tables.Rows[i][1].ToString();
                            string customer = tables.Rows[i][3].ToString();
                            string who = tables.Rows[i][4].ToString();
                            string end = DateTime.Parse(tables.Rows[i][6].ToString()).ToString("HH:mm");
                            string start = dd.ToString("HH:mm");
                            string t = dd.Subtract(DateTime.Now).Minutes.ToString();
                            string index = tables.Rows[i][8].ToString();
                            //Dictionary[i] = false;
                            //Console.WriteLine(i + "=sssssdd=" + Dictionary[i]);
                            //AccessFunction.DeleteClientHistory(s);
                            this.BeginInvoke(new Action(() =>
                            {
                                string message = "";
                                if (index.Contains("1"))
                                    message = t + " 分钟后  " + who + "  已经预约 【 内部会议室： " + room + "】   (" + start + "-" + end + ")" + "的会议, " + " 客户：  " + customer + "  ,请及时进行安排！";
                                else if (index.Contains("2"))
                                    message = t + " 分钟后  " + who + "  已经预约 【 坂田会议室： " + room + "】   (" + start + "-" + end + ")" + "的会议, " + " 客户：  " + customer + "  ,请及时进行安排！";
                                else if (index.Contains("3"))
                                    message = t + " 分钟后  " + who + "  已经预约 【 惠南会议室： " + room + "】   (" + start + "-" + end + ")" + "的会议, " + " 客户：  " + customer + "  ,请及时进行安排！";
                                if (!Dictionary[i])
                                {
                                    Form_Timing tips = new Form_Timing(this);
                                    tips.TopLevel = true;
                                    tips.SetInit(message, i);
                                    tips.Show();
                                }
                            }));
                        }
                    }
                }
                Thread.Sleep(1000);
            }
            timer_Date.Interval = 60000;
            timer_Date.Start();
        }

        private void button_reservate_Click(object sender, EventArgs e)
        {
            if (tcpConnect)
            {
                work_Idle = false;
                timer_Date.Stop();
                Form_reservate reservate_room = new Form_reservate(this);
                reservate_room.SetUserInfo(txtusername.Text, clientIPEndPoint, Index);
                reservate_room.ShowDialog();
            }
            else
                MessageBox.Show("网络连接异常,请检测网络！", "提示");
            work_Idle = true;
            timer_Date.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private static int ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return (int)intResult;
        }        
        /// <summary>
        /// 定时查询TCP的连接情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                int k = ConvertDateTimeInt(DateTime.Now);
                //Console.WriteLine("test Connect===" + k);
                //tcpClient = new TcpClient();
                //tcpClient.Connect(ServerIPEndPoint.Address, 8086);
                if (m_client.m_socket.Connected)
                {
                    // 表示连接成功
                    //networkStream = tcpClient.GetStream();
                    //binaryReader = new BinaryReader(networkStream);
                    //binaryWriter = new BinaryWriter(networkStream);
                    tcpConnect = true;
                    //tcpClient.Close();
                    //ClientSendMessage(string.Format("status,{0},{1}", txtusername.Text, clientIPEndPoint));
                    Random random = new Random();
                    //timer_Connect.Interval = random.Next(5000, 10000);
                    //Console.WriteLine("test ok===" + m_client.m_socket.Connected);
                    //MessageBox.Show("连接成功", "提示");
                }
                else
                {
                    tcpConnect = false;
                    //timer_Connect.Enabled = false;
                    //timer_Connect.Stop();
                    MessageBox.Show("与服务端连接断开,请检测网络连接情况！", "异常");
                }
            }
            catch
            {
                tcpConnect = false;
                //timer_Connect.Enabled = false;
                //timer_Connect.Stop();
                MessageBox.Show("连接服务端异常,请检测网络连接情况！", "异常");
                return;
            }
        }
        private int time_count = 0;
        private bool work_Idle = true;
        private void timer_T_Tick(object sender, EventArgs e)
        {
            reflectionLabel_time.Text = DateTime.Now.ToString("HH:mm:ss");
            if (tcpConnect && work_Idle)
            {
                if (time_count >= 60)
                {
                    //Init_room();
                    Set_room();
                    Init_room();
                    //Thread sendThread = new Thread(ClientSendMessage);
                    ClientSendMessage(string.Format("status,{0},{1},{2}", txtusername.Text, clientIPEndPoint, (comboBox3.SelectedIndex + 1).ToString()));
                    time_count = 0;
                }
                else
                    time_count++;
            }
        }

        private void button_setting_Click(object sender, EventArgs e)
        {
            if (!tcpConnect)
            {
                Form_setCon Set_room = new Form_setCon();
                DialogResult dia = Set_room.ShowDialog();

                Thread.Sleep(200);
                if (dia == DialogResult.OK)
                {
                    //settingFile = new InitFiles(Application.StartupPath + "\\setting.ini");
                    txtserverIP.Text = settingFile.ReadString("SETTING", "SERVERIP", txtserverIP.Text);
                    txtLocalIP.Text = settingFile.ReadString("SETTING", "LOCALIP", txtLocalIP.Text);
                    // 随机指定本地端口
                    comboBox3.SelectedIndex = settingFile.ReadInteger("SETTING", "INDEX", 0);
                }
            }
            else
            {
                Init_room();
                time_count = 0;
                //Thread sendThread = new Thread(ClientSendMessage);
                ClientSendMessage(string.Format("status,{0},{1},{2}", txtusername.Text, clientIPEndPoint, (comboBox3.SelectedIndex + 1).ToString()));
                Thread.Sleep(2000);
                Set_room();
            }
        }

        private void panel_1_MouseEnter(object sender, EventArgs e)
        {
            //this.panel_1.AutoScroll = true;
            panel_1.Select();
        }

        private void panel_3_MouseEnter(object sender, EventArgs e)
        {
            panel_3.Select();
        }

        private void panel_2_MouseEnter(object sender, EventArgs e)
        {
            panel_2.Select();
        }
        private int w = 1095;
        private int w1;
        //private int W_S = 180;
        //private int H_S = 170;
        private void panel_1_SizeChanged(object sender, EventArgs e)
        {
            int ws = panel_1.Size.Width - w;
            //ws -= backw;
            //Console.WriteLine("===" + panel_1.Size.Width);
            //if (ws >= 0)
            if (ws < 0)
                ws = 0;
            {
                foreach (Control Control in panel_1.Controls)
                {
                    if (Control == groupBox3 || Control == groupBox5 || Control == groupBox10 || Control == groupBox37)
                    {
                        w1 = 20;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //Control.Width = W_S + ws / 3;
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox2)
                    {
                        w1 = 554;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox4)
                    {
                        w1 = 821;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox54)
                    {
                        w1 = 287;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox6 || Control == groupBox11)
                    {
                        w1 = 237;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox7 || Control == groupBox12 || Control == groupBox46)
                    {
                        w1 = 454;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox8 || Control == groupBox49)
                    {
                        w1 = 674;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox9)
                    {
                        w1 = 888;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control is Label)
                    {
                        if (Control.Name.Equals(label70.Name))
                            w1 = 741;
                        else
                            w1 = 20;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                    }
                }
            }
        }

        private void panel_3_SizeChanged(object sender, EventArgs e)
        {
            int ws = panel_3.Size.Width - w;
            //ws -= backw;
            //Console.WriteLine("===" + ws);
            //if (ws >= 0)
            if (ws < 0)
                ws = 0;
            {
                foreach (Control Control in panel_3.Controls)
                {
                    if (Control == groupBox51 || Control == groupBox48 || Control == groupBox36)
                    {
                        w1 = 20;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox52 || Control == groupBox47)
                    {
                        w1 = 194;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox50 || Control == groupBox45 || Control == groupBox42)
                    {
                        w1 = 368;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox39 || Control == groupBox44)
                    {
                        w1 = 542;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox40 || Control == groupBox43)
                    {
                        w1 = 716;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox38)
                    {
                        w1 = 890;
                        Control.Location = new Point(w1 + (ws / 2), Control.Location.Y);
                        //backw = ws / 4;
                    }
                    if (Control is Label)
                    {
                        w1 = 20;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                    }
                }
            }
        }

        private void panel_2_SizeChanged(object sender, EventArgs e)
        {
            int ws = panel_2.Size.Width - w;
            //ws -= backw;
            //Console.WriteLine("===" + ws);
            //if (ws >= 0)
            if (ws < 0)
                ws = 0;
            {
                foreach (Control Control in panel_2.Controls)
                {
                    if (Control == groupBox22 || Control == groupBox29 || Control == groupBox20 || Control == groupBox14 || Control == groupBox34)
                    {
                        w1 = 20;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox23 || Control == groupBox19 || Control == groupBox13 || Control == groupBox35)
                    {
                        w1 = 194;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox21 || Control == groupBox30 || Control == groupBox18 || Control == groupBox24 || Control == groupBox41)
                    {
                        w1 = 368;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox26 || Control == groupBox17 || Control == groupBox33 || Control == groupBox53)
                    {
                        w1 = 542;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox27 || Control == groupBox16 || Control == groupBox32)
                    {
                        w1 = 716;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == groupBox25 || Control == groupBox15 || Control == groupBox31)
                    {
                        w1 = 890;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    if (Control is Label)
                    {
                        w1 = 20;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                    }
                }
            }
        }

        private void panel_top_SizeChanged(object sender, EventArgs e)
        {
            int ws = panel_top.Size.Width - 1306;
            //ws -= backw;
            //Console.WriteLine("===" + ws);
            //if (ws >= 0)
            if (ws < 0)
                ws = 0;
            {
                foreach (Control Control in panel_top.Controls)
                {
                    if (Control == button_1)
                    {
                        w1 = 434;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == button_2)
                    {
                        w1 = 610;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == button_3)
                    {
                        w1 = 786;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                    else if (Control == button_4)
                    {
                        w1 = 962;
                        Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                        //backw = ws / 4;
                    }
                }
            }
        }

        private void dataGridView_history_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            for (int i = 0; i < this.dataGridView_history.Rows.Count; i++)
            {
                DataGridViewRow r = this.dataGridView_history.Rows[i];
                r.HeaderCell.Value = string.Format("{0}", i + 1);
            }
            this.dataGridView_history.Refresh();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            //this.Visible = true;
            this.ShowInTaskbar = true;
            //this.notifyIcon1.Visible = false;
            this.Show();
            this.Activate();
            work_Idle = true;
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon1.Visible = true;//在通知区显示Form的Icon
                //this.WindowState = FormWindowState.Minimized;
                //this.Visible = false;
                work_Idle = false;
                this.ShowInTaskbar = false;//使Form不在任务栏上显示
                this.notifyIcon1.Icon = this.Icon;
                this.Hide();
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                this.notifyIcon1.Visible = false;
            }
            //Console.WriteLine("====" + this.WindowState);
        }

        private void groupBox28_SizeChanged(object sender, EventArgs e)
        {
            int ws = groupBox28.Size.Width - w;
            if (ws < 0)
                ws = 0;
            {
                foreach (Control Control in groupBox28.Controls)
                {
                    if (Control == label26)
                        w1 = 78;
                    else if (Control == comboBox1)
                        w1 = 129;
                    else if (Control == comboBox2)
                        w1 = 426;
                    else if (Control == label27)
                        w1 = 333;
                    else if (Control == label28)
                        w1 = 625;
                    else if (Control == dateTimePicker1)
                        w1 = 712;
                    else if (Control == button_check)
                        w1 = 892;
                    Control.Location = new Point(w1 + ws / 2, Control.Location.Y);
                    //backw = ws / 4;
                }
            }
        }


        private void LoginForm_SizeChanged(object sender, EventArgs e)
        {
            //panel_1.Scale();//Refresh();
            //Console.WriteLine(panel_1.Width + "==" + panel_1.Height);
        }

    }
}
