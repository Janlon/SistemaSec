namespace Sec.Business
{
    using System.Net.Mail;

    public partial class Engine
    {
        public static bool SendEmail(string body, string subject, string fromAddress, string fromName, string toAddress, string toName)
        {
            Db.SendEmail(body, 
                subject, 
                new MailAddress(fromAddress, fromName),
                new MailAddress(toAddress, toName));
            return true;
        }
    }
}

