using IKEA.DAL.Models.Users;

namespace session03_MVC_.PL.Controllers.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true
            };
            Client.Credentials = new System.Net.NetworkCredential("saiflotfy30@gmail.com", "uzhh oonj atqs hknb"); //app passowerd
            Client.Send("saiflotfy30@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
