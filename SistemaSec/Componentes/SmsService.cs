namespace SistemaSec
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte seu serviço de SMS aqui para enviar uma mensagem de texto.
            return Task.FromResult(0);
        }
    }
}