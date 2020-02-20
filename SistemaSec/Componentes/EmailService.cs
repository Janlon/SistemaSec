namespace SistemaSec
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte o seu serviço de email aqui para enviar um email.
            return Task.FromResult(0);
        }
    }
}