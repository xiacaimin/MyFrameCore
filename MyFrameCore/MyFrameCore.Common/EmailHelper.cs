using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Xml.Linq;

#region 邮件发送
/// <summary>
/// 邮箱配置
/// </summary>
public class EmailConfigDto
{
    /// <summary>
    /// 配置节点的名称
    /// </summary>
    public string ConfigName { set; get; }

    /// <summary>
    /// 邮件的发件人
    /// </summary>
    public string From { set; get; }

    /// <summary>
    /// 邮箱服务地址
    /// </summary>
    public string Host { set; get; }

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { set; get; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { set; get; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { set; get; }
}

/// <summary>
/// 邮箱类型
/// </summary>
public enum EmailTypeEnum
{
    /// <summary>
    /// 发给用户的邮箱配置
    /// </summary>
    UserEmail,
    /// <summary>
    /// 给管理员发送邮箱配置
    /// </summary>
    AdminEmail
}

/// <summary>
/// 邮件发送类
/// </summary>
public static class EmailHelper
{
    private static void ErrorLog(string emailTitle, string errorMsg)
    {
        System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
        {
            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH时mm分ss秒");
            var datetDay = DateTime.Now.ToString("yyyy-MM-dd");
            var url = AppDomain.CurrentDomain.BaseDirectory + string.Format("LogInfo\\{0}\\", datetDay);
            var urlFiName = url + string.Format("{0}.txt", datetDay);
            if (!Directory.Exists(url))
                Directory.CreateDirectory(url);
            if (!File.Exists(urlFiName))
                File.CreateText(urlFiName).Dispose();
            using (var sws = File.AppendText(urlFiName))
            {
                sws.WriteLine("-------------------{0}-----------------发送失败", datetime);
                sws.WriteLine("邮件标题：{0}", emailTitle);
                sws.WriteLine("原因：{0}", errorMsg);
                sws.WriteLine("-------------------------------------------------------");
            }
        });
        task.Start();
    }

    private static void Log(string emailTo, string emailTitle, string CC, EmailConfigDto config)
    {
        System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
        {
            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH时mm分ss秒");
            var datetDay = DateTime.Now.ToString("yyyy-MM-dd");
            var url = AppDomain.CurrentDomain.BaseDirectory + string.Format("LogInfo\\{0}\\", datetDay);
            var urlFiName = url + string.Format("{0}.txt", datetDay);
            if (!Directory.Exists(url))
                Directory.CreateDirectory(url);
            if (!File.Exists(urlFiName))
                File.CreateText(urlFiName).Dispose();
            using (var sws = File.AppendText(urlFiName))
            {
                sws.WriteLine("-------------------{0}-----------------", datetime);
                sws.WriteLine("邮件标题：{0}", emailTitle);
                sws.WriteLine("发送人：{0}", config.UserName);
                sws.WriteLine("收件人：{0}", emailTo);
                sws.WriteLine("抄送人：{0}", CC ?? "");
                sws.WriteLine("-------------------------------------------------------");
            }
        });
        task.Start();
    }


    /// <summary>
    /// 邮件发送
    /// </summary>
    /// <param name="emailType">邮件类型</param>
    /// <param name="emailTo">收件人的电子邮件地址。使用分号 (;) 分隔多名收件人。</param>
    /// <param name="emailTitle">电子邮件的主题行。</param>
    /// <param name="emailContent">电子邮件的正文。如果 isBodyHtml 为 true，则将正文中的 HTML 解释为标记。</param>
    /// <param name="filePath">（可选）文件名的集合，用于指定要附加到电子邮件中的文件；如果没有要附加的文件，则为 null。默认值为 null。</param>
    /// <param name="additionalHeaders">（可选）标头的集合，可添加到此电子邮件包含的正常 SMTP 标头中；如果不发送其他标头，则为 null。默认值为 null。</param>
    /// <param name="CC">邮件抄送人</param>
    public static bool SendMail(EmailTypeEnum emailType, string emailTo, string emailTitle,
        string emailContent, IEnumerable<string> filePath = null, IEnumerable<string> additionalHeaders = null, string CC = "")
    {

        if (string.IsNullOrEmpty(emailTitle))
        {
            ErrorLog("暂无标题", "标题不能为空");
            return false;
        }
        if (string.IsNullOrEmpty(emailContent))
        {
            ErrorLog(emailTitle, "邮件内容不能为空");
            return false;
        }
        var config = EmailConfigs.FirstOrDefault(s => s.ConfigName == emailType.ToString());
        if (config == null)
        {
            ErrorLog(emailTitle, "无法获取有效的邮件配置信息");
            return false;
        }

        var smtp = new SmtpClient
        {
            UseDefaultCredentials = true,
            Credentials = new NetworkCredential(config.UserName, config.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Port = config.Port,
            Host = config.Host,
            EnableSsl = true
        };

        var mail = new MailMessage { From = new MailAddress(config.From) };
        if (!string.IsNullOrEmpty(emailTo))
        {
            emailTo = emailTo.Replace("；", ";").Trim(';');
            foreach (var mailTo in emailTo.Split(';'))
            {
                if (RegEmail(mailTo))
                {
                    mail.To.Add(new MailAddress(mailTo));
                }
            }
        }

        if (!string.IsNullOrEmpty(CC))
        {
            CC = CC.Replace("；", ";").Trim(';');
            foreach (var item in CC.Split(';'))
            {
                if (RegEmail(item))
                {
                    mail.CC.Add(new MailAddress(item));
                }
            }
        }
        if (mail.To.Count == 0 && mail.CC.Count == 0)
        {
            ErrorLog(emailTitle, "收件人和抄送人不能同时为空");
            return false;
        }
        mail.Subject = emailTitle;
        mail.Body = emailContent;
        mail.IsBodyHtml = true;
        if (filePath != null)
        {
            foreach (var item in filePath)
            {
                if (Directory.Exists(item))
                {
                    mail.Attachments.Add(new Attachment(item));
                }
            }
        }

        if (additionalHeaders != null)
        {
            foreach (var item in additionalHeaders)
            {
                try
                {
                    mail.Headers.Add(null, item);
                }
                catch (Exception ex)
                {

                }
            }
        }

        try
        {
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            ErrorLog(emailTitle, ex.Message);
            return false;
        }
        #region 发送邮件日志
        Log(emailTo, emailTitle, CC, config);
        #endregion
        return true;
    }



    /// <summary>
    /// 验证邮箱格式是否正确
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private static bool RegEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
        return r.IsMatch(email);
    }
    /// <summary>
    /// 邮箱配置节点
    /// </summary>
    private static readonly List<EmailConfigDto> EmailConfigs =
        XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "\\ConfigFiles\\EmailSettings.config")
            .Descendants("emailSetting")
            .Select(x => new EmailConfigDto
            {
                ConfigName = x.Element("configName").Value,
                From = x.Element("from").Value,
                Host = x.Element("host").Value,
                Port = int.Parse(x.Element("port").Value),
                UserName = x.Element("userName").Value,
                Password = x.Element("password").Value
            }).ToList();
}
#endregion
