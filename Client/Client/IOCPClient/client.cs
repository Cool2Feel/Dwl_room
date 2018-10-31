using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client.IOCPClient
{
    class client
    {
        #region Members
        public Socket m_socket;
        IPEndPoint m_endPoint;
        private SocketAsyncEventArgs m_connectSAEA;
        private SocketAsyncEventArgs m_sendSAEA;
        private LoginForm lm;
        private string temp = "";
        #endregion

        public client(string ip, int port)
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //m_socket.Bind(LocalIP);
            IPAddress iPAddress = IPAddress.Parse(ip);
            m_endPoint = new IPEndPoint(iPAddress, port);
            m_connectSAEA = new SocketAsyncEventArgs { RemoteEndPoint = m_endPoint };
        }

        public void Start(LoginForm lm)
        {
            this.lm = lm;
            m_connectSAEA.Completed += OnConnectedCompleted;
            bool con = m_socket.ConnectAsync(m_connectSAEA);
            //Console.WriteLine(con);
            lm.clientEndPoint = m_socket.LocalEndPoint;
            //Console.WriteLine(m_socket.LocalEndPoint.ToString());
            /*
            if (con)
                lm.tcpConnect = true;
            else
            {
                lm.tcpConnect = false;
                return;
            }
            Thread.Sleep(5000);
            Send("Hello");
            while (true)
            {
                Console.Write("send>");
                string msg = Console.ReadLine();
                if (msg == "exit")
                    break;
                Send(msg);
            }
            DisConnect();
            //Console.ReadLine();
            */
            //按任意键终止服务器进程....
            //Console.WriteLine("Client : Press any key to terminate the server process....");
            //Console.ReadKey();
        }

        public void OnConnectedCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                lm.tcpConnect = false;
                return;
            }
            Socket socket = sender as Socket;
            string iPRemote = socket.RemoteEndPoint.ToString();
            lm.tcpConnect = true;
            //Console.WriteLine("Client : 连接服务器 [ {0} ] 成功",iPRemote);

            SocketAsyncEventArgs receiveSAEA = new SocketAsyncEventArgs();
            byte[] receiveBuffer = new byte[1024 * 4];
            receiveSAEA.SetBuffer(receiveBuffer, 0, receiveBuffer.Length);
            receiveSAEA.Completed += OnReceiveCompleted;
            receiveSAEA.RemoteEndPoint = m_endPoint;
            socket.ReceiveAsync(receiveSAEA);
        }

        private void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.OperationAborted) return;

            Socket socket = sender as Socket;

            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                string ipAdress = socket.RemoteEndPoint.ToString();
                int lengthBuffer = e.BytesTransferred;
                byte[] receiveBuffer = e.Buffer;
                byte[] buffer = new byte[lengthBuffer];
                Buffer.BlockCopy(receiveBuffer, 0, buffer, 0, lengthBuffer);

                string msg = Encoding.Default.GetString(buffer);
                //Console.WriteLine("Client : receive message[ {0} ],from Server[ {1} ]", msg, ipAdress);
                //char[] c = "end".ToCharArray();
                //lm.newReceiveData(msg);
                if (msg.Contains("addin"))
                {
                    if (!temp.Equals(""))
                    {
                        msg = temp + msg;
                    }
                    string[] message = msg.Split(';');
                    //if (message.Length > 10)
                    {
                        for (int i = 0; i < message.Length; i++)
                        {
                            if (message[i].Split(',').Length > 10)
                            {
                                lm.newReceiveData(message[i] + ";end");
                                temp = "";
                            }
                            else
                            {
                                temp = message[i];
                                //Console.WriteLine(message[i]);
                            }
                        }
                    }
                }
                else
                    lm.newReceiveData(msg);
                socket.ReceiveAsync(e);
            }
            else if (e.SocketError == SocketError.ConnectionReset || e.BytesTransferred == 0)
            {
                //Console.WriteLine("Client: 服务器断开连接 ");
                lm.Disconnect();
                //DisConnect();
            }
            else
            {
                return;
            }
        }

        public void Send(string msg)
        {
            byte[] sendBuffer = Encoding.Default.GetBytes(msg);
            if (m_sendSAEA == null)
            {
                m_sendSAEA = new SocketAsyncEventArgs();
                m_sendSAEA.Completed += OnSendCompleted;
            }

            m_sendSAEA.SetBuffer(sendBuffer, 0, sendBuffer.Length);
            if (m_socket != null)
            {
                m_socket.SendAsync(m_sendSAEA);
            }
        }

        private void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success) return;
            Socket socket = sender as Socket;
            byte[] sendBuffer = e.Buffer;

            string sendMsg = Encoding.Default.GetString(sendBuffer);

            //Console.WriteLine("Client : Send message [ {0} ] to Serer[ {1} ]", sendMsg, socket.RemoteEndPoint.ToString());
        }

        public void DisConnect()
        {
            if (m_socket != null)
            {
                try
                {
                    m_socket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException se)
                {
                    throw se;
                }
                finally
                {
                    m_socket.Close();
                }
            }
        }
    }
}
