using System;

namespace Tcp
{
    public class Model
    {
        public int xmno { set; get; }
        //设备号
        public string sno { set; get; }
        //周期
        public string cycle { set; get; }
        public DateTime time { set; get; }
        public string tdno { set; get; }
        public double valueOne { get; set; }
        public double valueTwo { get; set; }
        public string port { get; set; }
        //电压
        public double dy { get; set; }
        public string pointName { get; set; }
        //第几条记录
        public string number { get; set; }
        public string dataType { get; set; }
        //模块号
        public string mkno { set; get; }

        //最终的计算结果
        public double res { set; get; }


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}, {7}, {8}\n\r", sno, number.ToString(), cycle, time.ToString(), tdno, valueOne, valueTwo, dy, port);
        }
    }
}
