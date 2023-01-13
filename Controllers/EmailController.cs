using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace email_sender.Controllers
{
    public class EmailController : ControllerBase
    {
        [HttpPost("send-email")]
        public IActionResult SendMail(Email emailInfo)
        {
            //insert the receiver
            emailInfo.EmailTo = "xxx@gmail.com";
            MailMessage mail = new MailMessage();

            //insert the sender info
            mail.From = new MailAddress("yyy@gmail.com");
            mail.To.Add(emailInfo.EmailTo);
            mail.Subject = "Hello";
            mail.Body = "Hello world";
            //mail.Priority = MailPriority.High;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                //insert username and password of the sender, password can be generated from GMAIL
                client.Credentials = new NetworkCredential("yyy@gmail.com", "vnmmeifkjgimkuny");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(mail);
            }
            return Ok();
        }

        public class Email
        {
            public string EmailTo { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
    }
}