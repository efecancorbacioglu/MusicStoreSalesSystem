using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Concrete
{
    public class MailBusiness
    {
        public void SendMail(string RecevierAddress, string MailTitle, string MailBody)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential("", "");
            client.Port = 587;
            client.Host = "smtp.office365.com";
            client.EnableSsl = true;

            message.To.Add(RecevierAddress);
            message.From = new MailAddress("");
            message.Subject = MailTitle;
            message.Body = MailBody;

            client.Send(message);
        }
    }
}
