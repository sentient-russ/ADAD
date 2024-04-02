using adad.Models;
using adad.Controllers;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace adad.Services
{
    public class EmailService
    {
        string MD_Email_Pass = Environment.GetEnvironmentVariable("MD_Email_Pass");

        public String SendContactMessage(ContactDataModel complexDataIn, string MD_Email_Pass)
        {
            string _pass = MD_Email_Pass;
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("cs@magnadigi.com"));
            email.To.Add(MailboxAddress.Parse("cs@magnadigi.com"));
            email.Subject = "ADAD Contact";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<p><strong>Contact Name: </strong>" + complexDataIn.Name + "</p>" +
              "<p><strong>Contact Email: </strong>" + complexDataIn.Email + "</p>" +
              "<p><strong>Message: </strong>" + complexDataIn.Message + "</p>"
            };
            using var smtp = new SmtpClient();
            smtp.Connect("us2.smtp.mailhostbox.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("cs@magnadigi.com", MD_Email_Pass);
            var response = smtp.Send(email);
            smtp.Disconnect(true);
            return response;
        }

        public String SendWarningMessage(ContactDataModel complexDataIn)
        {
            string _pass = MD_Email_Pass;
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("cs@magnadigi.com"));
            email.To.Add(MailboxAddress.Parse("cs@magnadigi.com"));
            email.Subject = complexDataIn.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = complexDataIn.Message
            };

            using var smtp = new SmtpClient();
            smtp.Connect("us2.smtp.mailhostbox.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("cs@magnadigi.com", MD_Email_Pass);
            var response = smtp.Send(email);
            smtp.Disconnect(true);
            return response;
        }

    }
}
