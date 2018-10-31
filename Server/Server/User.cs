using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 添加额外的命名空间
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace Server
{
    // 用户类
    public class User
    {
        // 私有字段
        private string username;
        private IPAddress userIPEndPoint;
        private TcpClient userTcpClient;
        private string Index;
        private Socket userSocket;
        private SocketAsyncEventArgs s_Event;
        //构造函数
        public User(string name, IPAddress ipEndPoint, string index, Socket socket)
        {
            username = name;
            userIPEndPoint = ipEndPoint;
            //userTcpClient = tcpClient;
            userSocket = socket;
            Index = index;
        }

        public User(string name, IPAddress ipEndPoint, string index, Socket socket, SocketAsyncEventArgs e)
        {
            username = name;
            userIPEndPoint = ipEndPoint;
            //userTcpClient = tcpClient;
            userSocket = socket;
            Index = index;
            s_Event = e;
        }

        /// <summary>  
        /// 通信SOKET  
        /// </summary>  
        public Socket GetSocket()
        {
            return userSocket;
        }

        public SocketAsyncEventArgs GetEvent()
        {
            return s_Event;
        }

        public User(string name, IPAddress ipEndPoint, TcpClient tcpClient, string index)
        {
            username = name;
            userIPEndPoint = ipEndPoint;
            userTcpClient = tcpClient;
            Index = index;
        }

        public User(string name, IPAddress ipEndPoint)
        {
            username = name;
            userIPEndPoint = ipEndPoint;
            //userTcpClient = tcpClient;
        }

        // 公共方法
        public string GetName()
        {
            return username;
        }

        public IPAddress GetIPEndPoint()
        {
            return userIPEndPoint;
        }

        public TcpClient GetTcpClient()
        {
            return userTcpClient;
        }

        public string GetIndex()
        {
            return Index;
        }
    }

}
