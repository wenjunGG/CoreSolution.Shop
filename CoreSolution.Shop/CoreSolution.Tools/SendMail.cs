using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CoreSolution.Tools
{
    /// <summary>
    /// 发送邮件
    /// create by LiuCheng 2019.7.22
    /// </summary>
    public class SendMail
    {
        public static bool Send(MailModel model)
        {
            try
            {
                string user = ConfigHelper.GetSectionValue("EmailUser");//替换成你的hotmail用户名
                string password = ConfigHelper.GetSectionValue("EmailPassword");//替换成你的hotmail密码
                string host = "smtp.163.com";//设置邮件的服务器

                SmtpClient smtp = new SmtpClient(host);
                smtp.EnableSsl = true; //开启安全连接。
                smtp.Credentials = new NetworkCredential(user, password); //创建用户凭证
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //使用网络传送

                foreach (var item in model.ToAddress)
                {
                    MailMessage message = new MailMessage(user, item, model.Subject, model.Body);  //创建邮件
                    smtp.Send(message); //发送邮件   异步发送邮件 smtp.SendAsync(message, "huayingjie"); //这里简单修改下，发送邮件会变的很快。
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public class MailModel
        {
            /// <summary>
            /// 目标账户
            /// </summary>
            public IList<string> ToAddress { get; set; }

            /// <summary>
            /// 标题
            /// </summary>
            public string Subject { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            public string Body { get; set; }

            public MailModel(IList<string> toAddress, string subject, string body)
            {
                ToAddress = toAddress;
                Subject = subject;
                Body = body;
            }
        }
    }
}
