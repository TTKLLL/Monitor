using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Config;

namespace SuperSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConfig serverConfig = new ServerConfig()
            {
                Ip = "192.168.168.96",
                Port = 8080,
                Mode = SocketMode.Tcp
            };
   
            AppServer server = new AppServer();
            if(!server.Setup(serverConfig))
            {
                Console.WriteLine("fail");
            }

            if(!server.Start())
            {
                Console.WriteLine("fail");
            }
            Console.WriteLine("success");
        }
    }
}
