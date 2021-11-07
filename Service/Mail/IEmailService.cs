using System.Threading.Tasks;

namespace Service.Mail
{
	public interface IEmailService
	{
		Task SendEmailAsync(string email, string subject, string message);
	} 
}
