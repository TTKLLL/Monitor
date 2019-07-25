namespace Model
{
    public class QueryModel
    {     
        public string dataType { get; set; }
        public string xmno { set; get; }
        public string sno { get; set; }
        public string pointName { get; set; }
        public string tdno { get; set; }
        public string beginTime { set; get; }
        public string endTime { set; get; }
       

        public int nowPage { set; get; }
        public int pageSize { set; get; }
        public int totlaNumber { set; get; }
        public int totalPage { set; get; }

        public  QueryModel()
        {
            pageSize = 13;
            nowPage = 1;
        }
    }
}
