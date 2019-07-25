using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //if (Session["uId"] == null)
        //{
        //    Response.Write("<script>alert('请重新登录'); window.parent.parent.location='../../Login.aspx'</script>");

        //}

        //if (Request.Cookies["uId"] == null)
        //{
        //    ReLogIn();
        //}


    }

    //重新登陆
    public void ReLogIn()
    {
        Response.Write("<script>alert('请重新登录'); window.parent.parent.location='/Login.aspx'</script>");
    }

    public void alert(string str)
    {
        //  Response.Write("<script>alert('" + str + "')</script>");

        //防止弹出窗口后页面样式丢失
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('" + str + "');</script>");
    }


    public void SuAlert(string str)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>SuAlert('" + str + "');</script>");
    }

    public void errAlert(string str)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>errAlert('" + str + "');</script>");
    }



    public void infoAlert(string str)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>infoAlert('" + str + "');</script>");
    }


    public void alert(string str, string link)
    {
        Response.Write("<script>alert('" + str + "');window.location.href='" + link + "'</script>");
    }

    public void alertMes(string str)
    {
        Response.Write("<script>alert('" + str + "')</script>");
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "<script>alert('"+str+"');</script>");
    }

}