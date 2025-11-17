using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Environmental_Monitoring.Controller
{
    public class EmailService
    {
        public static async Task<bool> SendEmailAsync(string toEmail, string subject, string body, string attachmentPath = null)
        {
            try
            {
                string host = ConfigurationManager.AppSettings["SmtpHost"];
                int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string user = ConfigurationManager.AppSettings["SmtpUser"];
                string pass = ConfigurationManager.AppSettings["SmtpPass"];
                bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]);
                string senderName = ConfigurationManager.AppSettings["SmtpSenderName"];

                var fromAddress = new MailAddress(user, senderName);
                var toAddress = new MailAddress(toEmail);

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, pass),
                    Timeout = 20000 
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
                    {
                        message.Attachments.Add(new Attachment(attachmentPath));
                    }

                    await smtp.SendMailAsync(message); 
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Lỗi Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}