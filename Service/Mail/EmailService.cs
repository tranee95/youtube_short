using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Service.Mail
{
	public class EmailService : IEmailService
	{
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress("viTouch", "viTouchAdmin@admin.ru"));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(TextFormat.Html)
			                    {
				                    Text = message
			                    };

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync("smtp", 25, false);
				await client.AuthenticateAsync("admin@admin", "password");
				await client.SendAsync(emailMessage);

				await client.DisconnectAsync(true);
			}
		}
	}
}