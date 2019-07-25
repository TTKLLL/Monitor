namespace DAL
{
    /// <summary>
    /// 不同项目的数据库
    /// </summary>
    public class LinkAttr
    {
        //数据库用户名
        public string uid { set; get; }
        //密码
        public string pwd { set; get; }
        public string LinkAttrName { set; get; }
        public string server { set; get; }
        public int port { set; get; }
        public string xmno { set; get; }
        //数据库名称
        public string database { set; get; }
        public string driver { set; get; }
    }
}

