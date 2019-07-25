using System.Text;

/// <summary>
/// Page 的摘要说明
/// </summary>
public class Page
{
    //nowPage 当前页
    // pageSize 每页大小
    // totalNumber查询结果数
    public static string GeneratePage(string targetUrl, int nowPage, int pageSize, long totalNumber, string para)
    {
        //总页数
        long totlaPage = totalNumber % pageSize == 0 ? totalNumber / pageSize : totalNumber / pageSize + 1;
        StringBuilder sbText = new StringBuilder();

        if (totlaPage == 0)
            return "未查询到结果";
        else
        {

            //天添加首页和第一页链接
            if (nowPage == 1)
            {
                sbText.Append("<li><a class='disabled'>首页</a></li>");
                sbText.Append("<li><a class='disabled'>上一页</a></li>");
            }
            else
            {
                sbText.Append("<li><a href = '" + targetUrl + "?pageNumber=1&" + para + "'>首页</a></li>");

                sbText.Append("<li><a href='" + targetUrl + "?pageNumber=" + (nowPage - 1) + "&" + para + "'>上一页</a></li>");
            }

            //产生中间的链接

            for (int i = nowPage - 2; i < totlaPage + 2; i++)
            {
                if (i < 1 || i > totlaPage)
                    continue;
                else
                {
                    if (i == nowPage)
                    {
                        //当前页链接
                        sbText.Append("<li><a class='active'>" + i + "</a></li>");
                    }
                    else
                    {
                        //第i页的链接
                        sbText.Append("<li><a href='" + targetUrl + "?pageNumber=" + i + "&" + para + "'>" + i + "</a></li>");
                    }
                }
            }

            if (nowPage == totlaPage)
            {
                sbText.Append("<li><a class='disabled'>下一页</a></li>");
                sbText.Append("<li><a class='disabled'>末页</a></li>");
            }
            else
            {
                sbText.Append("<li><a href='" + targetUrl + "?pageNumber=" + (nowPage + 1) + "&" + para + "'>下一页</a></li>");
                sbText.Append("<li><a href='" + targetUrl + "?pageNumber=" + totlaPage + "&" + para + "'>末页</a></li>");
            }

            return sbText.ToString();
        }
    }

    public static string GenerateAjaxPage(int nowPage, int pageSize, long totalNumber)
    {
        //总页数
        long totlaPage = totalNumber % pageSize == 0 ? totalNumber / pageSize : totalNumber / pageSize + 1;
        StringBuilder sbText = new StringBuilder();

        if (totlaPage == 0)
            return "未查询到结果";
        else
        {

            //天添加首页和第一页链接
            if (nowPage == 1)
            {
                sbText.Append("<li  class='disabled'>首页</li>");
                sbText.Append("<li class='disabled'>上一页</li>");
            }
            else
            {
                sbText.Append("<li onclick='GetDataByPageNumber(1)'>首页</li>");

                sbText.Append("<li onclick='GetDataByPageNumber(" + (nowPage - 1) + ")'>上一页</li>");
            }

            //产生中间的链接

            for (int i = nowPage - 2; i < totlaPage + 2; i++)
            {
                if (i < 1 || i > totlaPage)
                    continue;
                else
                {
                    if (i == nowPage)
                    {
                        //当前页链接 
                        sbText.Append("<li  class='active'>" + i + "</li>");
                    }
                    else
                    {
                        //第i页的链接
                        sbText.Append("<li onclick='GetDataByPageNumber(" + i + ")' >" + i + "</li>");
                    }
                }
            }

            if (nowPage == totlaPage)
            {
                sbText.Append("<li  class='disabled'>下一页</li>");
                sbText.Append("<li  class='disabled'>末页</li>");
            }
            else
            {
                sbText.Append("<li onclick='GetDataByPageNumber(" + (nowPage + 1) + ")'>下一页</li>");
                sbText.Append("<li onclick='GetDataByPageNumber(" + totlaPage + ")'>末页</li>");
            }

            return sbText.ToString();


        }

    }
}