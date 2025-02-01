using System.Net.Mail;
using System.Net;

namespace back_end.Infastructure.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendVerificationEmailAsync(string email, string verificationLink)
        {
            try
            {
                var subject = "Verify Your Email Address";
                var body = $@"
                <p>Hi,</p>
                <p>Thank you for registering. Please verify your email address by clicking the link below:</p>
                <p><a href='{verificationLink}'>Set your password and verify your email</a></p>
                <p>If you did not request this, please ignore this email.</p>
                <p>Best regards,<br>Your App Team</p>";

                await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the verification email to {Email}", email);
                throw;
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Preparing to send email to {ToEmail} with subject {Subject}", toEmail, subject);

                var emailSettings = _configuration.GetSection("EmailSettings");

                var smtpServer = emailSettings["SmtpHost"];
                var smtpPort = int.Parse(emailSettings["SmtpPort"]);
                var smtpUser = emailSettings["Username"];
                var smtpPassword = emailSettings["Password"];
                var enableSSL = bool.Parse(emailSettings["EnableSSL"]);
                var fromEmail = emailSettings["FromEmail"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    client.EnableSsl = enableSSL;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail, "Authentication App"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                }

                _logger.LogInformation("Email successfully sent to {ToEmail}", toEmail);
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error occurred while sending email to {ToEmail}", toEmail);
                throw;
            }
            catch (FormatException formatEx)
            {
                _logger.LogError(formatEx, "Invalid email format for {ToEmail}", toEmail);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending email to {ToEmail}", toEmail);
                throw;
            }
        }
    }
}
