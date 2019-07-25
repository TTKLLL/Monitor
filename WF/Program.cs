using System;
using System.Windows.Forms;
using Tcp;
using Tool;

namespace WF
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ////给Tcp的处理数据事件添加行为
            //TcpServer.ProcessDataEvent += new TcpServer.ProcessData(new DataProcessCommon().ProcessData);
            Form form = new Form1();
        
            Application.Run(form);

           

        }
    }
}
