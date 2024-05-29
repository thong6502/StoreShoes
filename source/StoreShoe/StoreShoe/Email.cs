using System.Net;
using System.Net.Mail;

namespace shoe_store_manager
{
    public class Email
    {
        public static string Address = "minh02227227@gmail.com"; //Địa chỉ email của bạn
        public static string Password = "vwrjqfwgkwtkfusw"; //Mật khẩu ứng dụng


        public void Send(string sendTo, string subject, string message)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(Address, Password);
                smtp.Send(Address, sendTo, subject, message);
            }
        }

    }
}
