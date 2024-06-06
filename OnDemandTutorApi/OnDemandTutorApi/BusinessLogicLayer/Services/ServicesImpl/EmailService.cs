using MimeKit;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using MailKit.Net.Smtp;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig) 
        {
            _emailConfig = emailConfig;
        }

        public ResponseApiDTO SendEmail(EmailDTO emailDTO)
        {
            var emailMessage = CreateEmailMessage(emailDTO);
            Send(emailMessage);

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Email sent successfully"
            };
        }

        private MimeMessage CreateEmailMessage(EmailDTO emailDTO)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("TamNguyenDev", _emailConfig.From));
            emailMessage.To.AddRange(emailDTO.To);
            emailMessage.Subject = emailDTO.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailDTO.Body };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending the email: " + ex.Message, ex);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
