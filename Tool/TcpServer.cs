
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Tool
{
    /// <summary>
    /// 提供Scoket服务
    /// 提供ip 端口 来开启指定端口的监听
    /// 给ProcessDataEvent事件添加行为来处理接收的数据
    /// </summary>

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;

        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class TcpServer
    {
        //声明一个用于处理收到的数据的委托
        public delegate void ProcessData(string data, int port);
        //用委托实例化事件
        public static event ProcessData ProcessDataEvent;

        //  private string ip; // = "192.168.168.96";

        //本地测试监测ip
        //发布使用ip
        //172.18.55.16
        //private static readonly string ip = "172.18.55.16";

        //此时的falseb表明WaitOne()阻塞进程
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void StartListening(string ip, int port)
        {
            try
            {
                byte[] bytes = new Byte[1024];
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

                //启动tcp类型的监听
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //绑定指定的额ip地址和端口
                listener.Bind(localEndPoint);
                //最多同时连接100台设备
                listener.Listen(100);
                while (true)
                {
                    allDone.Reset();
                    string info = "启动成功" + localEndPoint.Address.ToString() + ":" + localEndPoint.Port.ToString() + "等待连接...";
                    FileOperation.WriteAppenFile(info);

                    //开启监听
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                string info = e.Message;
                FileOperation.WriteAppenFile("1" + info);
            }
            Console.Read();
        }

        //成功连接到客户端
        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                //把非终止状态改为终止状态
                allDone.Set();

                //从AsyncState中获取监听对象
                Socket listener = (Socket)ar.AsyncState;

                //客户端Socket
                Socket handler = listener.EndAccept(ar);

                StateObject state = new StateObject();
                state.workSocket = handler;
                FileOperation.WriteAppenFile(string.Format("连接成功，{0} 等待接收数据", listener.LocalEndPoint));
                //开始接收客户端的数据 将数据存储到state对象的buffer中
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("2" + ex.Message);
            }
        }

        //成功接收到数据
        public static void ReadCallback(IAsyncResult ar)
        {
            try
            {
                String content = String.Empty;

                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    string data = Encoding.Default.GetString(state.buffer, 0, bytesRead);
                    state.sb.Append(data);
                    content = state.sb.ToString();
                    //  Send(handler, "recive success");

                    string port = handler.LocalEndPoint.ToString().Split(':')[1];
                    //DataProcessCommon dataProcessCommon = new DataProcessCommon();
                    ////处理接收的数据
                    //dataProcessCommon.ProcessData(data, port);

                    //调用事件来处理收到的数据
                    ProcessDataEvent(data, int.Parse(port));

                    //继续监听
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("3" + ex.Message);
            }
        }

        private static void Send(Socket handler, String data)
        {
            try
            {
                byte[] byteData = Encoding.Default.GetBytes(data);
                IAsyncResult res = handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("4" + ex.Message);
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("5" + e.Message);
            }
        }

        //开启端口
        public static bool StartListenPoret(string ip, int port)
        {
            try
            {
                Thread thread1 = new Thread(() => TcpServer.StartListening(ip, port));
                thread1.Start();
                return true;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("开启端口{0}出错 {1}", port.ToString(), ex.Message));
                throw ex;
            }
        }
    }
}
