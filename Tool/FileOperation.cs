using System;
using System.IO;
using System.Text;


//文件操作类
namespace Tool
{/// <summary>
/// 用于日志文件的读写
/// </summary>
    public class FileOperation
    {
        //用于往消息查看窗口添加日志的事件
        public delegate void SendLog(string log);
        public static event SendLog Send;

        public delegate void ReLoadLogDele();
        public static event ReLoadLogDele ReLoadLog;

        //日志文件的路径
        //   C:\Monitor\Display\Log
        public static readonly string headDir = "C:\\Monitor\\Display\\Log\\";
        //public static string readonly headDir = "D:\\LCTest\\Code\\Monitor\\Display\\Log\\";
        public static string LogDir = headDir + "Log.txt";
        public static string FileDir = headDir + "Data.txt";
        public static double FileMaxSize = 500; //日志文件最大500M


        //写字符串 追加模式
        public static void WriteAppenFile(string filePath, string str)
        {
            //将字符串转码成字节数组
            byte[] strByte = System.Text.Encoding.UTF8.GetBytes(str);
            using (FileStream fs = new FileStream(@filePath, FileMode.Append))
            {
                fs.Write(Encoding.UTF8.GetBytes(str), 0, strByte.Length);
            }
        }

        //将收到的数据写入文本
        public static void WriteReceiveData(string data)
        {
            using (FileStream fs = new FileStream(FileDir, FileMode.Append))
            {
                ChargeFileSize(FileDir);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.Write(data);

                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }

        //读文件
        public static void ReadFile(string filePath)
        {
            using (FileStream fs = new FileStream(@filePath, FileMode.Open))
            {
                int fsLen = (int)fs.Length;      //获取文件大小
                byte[] wByte = new Byte[fsLen];  //按文件大小定义缓冲区
                int r = fs.Read(wByte, 0, wByte.Length); //将数据读入到缓冲区中
                string str = System.Text.Encoding.UTF8.GetString(wByte);
                Console.WriteLine(str);
                Console.ReadKey();
            }
        }

        //判断文件大小是否合理
        public static bool ChargeFileSize(string dir)
        {
            try
            {
                FileInfo info = new FileInfo(dir);
                if (info.Length / (1024 * 1024.0) >= FileMaxSize)
                {
                    ClearLog(dir);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        //写日志文件
        public static void WriteAppenFile(string str)
        {
            try
            {
                //判断文件大小是否合理
                if (ChargeFileSize(LogDir))
                    //当清空了日志后窗口重新加载日志数据
                    ReLoadLog();

                string info = DateTime.Now.ToLocalTime() + ": " + str;
                using (FileStream fs = new FileStream(LogDir, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(info);
                    //调用事件 向窗口写入最新的日志
                    Send(info);

                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //清空文件内容
        public static bool ClearLog(string dir)
        {
            try
            {
                FileStream stream = File.Open(dir, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("清空日志文件出错，因为" + ex.Message);
                return false;
            }
        }

        //读取文件后n行
        public static string ReadLastLine(int ALineCount)
        {
            if (ALineCount <= 0) return string.Empty;
            if (!File.Exists(LogDir)) return string.Empty; // 文件不存在
            //if (AEncoding == null)
            Encoding AEncoding = Encoding.UTF8;
            using (FileStream vFileStream = new FileStream(LogDir,
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (vFileStream.Length <= 0) return string.Empty; // 空文件
                byte[] vBuffer = new byte[0x1000]; // 缓冲区
                int vReadLength; // 读取到的大小
                int vLineCount = 0; // 读取的行数
                int vReadCount = 0; // 读取的次数
                int vScanCount = 0; // 扫描过的字符数
                long vOffset = 0; // 向后读取的位置
                do
                {
                    vOffset = vBuffer.Length * ++vReadCount;
                    int vSpace = 0; // 偏移超出的空间
                    if (vOffset >= vFileStream.Length) // 超出范围
                    {
                        vSpace = (int)(vOffset - vFileStream.Length);
                        vOffset = vFileStream.Length;
                    }
                    vFileStream.Seek(-vOffset, SeekOrigin.End); //“SeekOrigin.End”反方向偏移读取位置

                    vReadLength = vFileStream.Read(vBuffer, 0, vBuffer.Length - vSpace);
                    #region 所读的缓冲里有多少行
                    for (int i = vReadLength - 1; i >= 0; i--)
                    {
                        if (vBuffer[i] == 10)
                        {
                            if (vScanCount > 0) vLineCount++; // #13#10为回车换行
                            if (vLineCount >= ALineCount) break;
                        }
                        vScanCount++;
                    }
                    #endregion 所读的缓冲里有多少行
                } while (vReadLength >= vBuffer.Length && vOffset < vFileStream.Length &&
                    vLineCount < ALineCount);

                if (vReadCount > 1) // 读的次数超过一次，则需重分配缓冲区
                {
                    vBuffer = new byte[vScanCount];
                    vFileStream.Seek(-vScanCount, SeekOrigin.End);
                    vReadLength = vFileStream.Read(vBuffer, 0, vBuffer.Length);
                }
                return AEncoding.GetString(vBuffer, vReadLength - vScanCount, vScanCount);
            }
        }
    }
}
