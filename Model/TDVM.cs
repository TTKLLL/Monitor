namespace Model
{
    /// <summary>
    /// 用于在窗口显示模块的通道信息
    /// </summary>
    public class TDVM
    {
        //通道编号
        public string tdno { set; get; }
        //传感器编号
        public string deviceId { set; get; }
        //传感器类型
        public string type { set; get; }
        //点名
        public string pointName { set; get; }

        public string mkno { set; get; }
    }
}
