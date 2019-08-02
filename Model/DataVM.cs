using System;

namespace Model
{
    //用于在窗口上显示监测数据
    public class DataVM
    {
        //序号
        public int number { set; get; }
        //设备号
        public string deviceId { set; get; }
        public string dataType { get; set; }

        public string pointName { get; set; }
        //模块号
        public string mkno { set; get; }
        public string tdno { set; get; }

        public DateTime time { set; get; }

        public int xmno { set; get; }

        public string port { get; set; }

        //最终的计算结果
        public double valueOne { set; get; }
        public double valueTwo { set; get; }
        public double valueThree { set; get; }

    }
}
