namespace Model
{
    /// <summary>
    /// 用于在窗口上显示传感器的信息
    /// </summary>
    public class DeviceVM : Device
    {
        public string mkno { set; get; }
        public string tdnos { set; get; }
        public string pointName { set; get; }
        public string xmno { set; get; }
        public string port { set; get; }
    }
}