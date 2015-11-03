using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.Utilities.Helpers
{
    public class MailHelper
    {
        public static void Send(string from, string password, string to, string subject, string body, bool isHtml, string smtpServer)
        {
            Send(from, password, to, subject, body, isHtml, smtpServer, null);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">收件人</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="mailBody">邮件内容</param>
        public static void Send(string from, string password, string to, string subject, string body, bool isHtml, string smtpServer, Dictionary<string, string> values)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                throw new Exception("From or to can't be null while sending email.");
            }

            if (values != null)
            {
                foreach (var item in values)
                {
                    body = body.Replace(item.Key, item.Value);
                }
            }

            MailMessage email = new MailMessage()
            {
                From = new System.Net.Mail.MailAddress(from),
                Subject = subject,
                Body = body,
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = isHtml
            };

            if(to.IndexOf(';') >= 0)
            {
                foreach (var str in to.Split(new char[] { ';' }))
                {
                    email.To.Add(str);
                }
            }
            else
            {
                email.To.Add(to);
            }
            
            SmtpClient client = new SmtpClient
            {
                Host = smtpServer,
                Credentials = new NetworkCredential(from, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                client.Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending email: " + ex.Message);
            }
        }
    }
}
