using En_Luna.Settings;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.NetworkInformation;

namespace En_Luna.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailConfig;

        public EmailSender(EmailSettings emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Name, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            //var client = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port)
            //{
            //    Credentials = new NetworkCredential(_emailConfig.UserName, "********2e70"),
            //    EnableSsl = true
            //};
            //client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
            //Console.WriteLine("Sent");
            //Console.ReadLine();

            using (var client = new SmtpClient(new ProtocolLogger("smtp.log")))
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
