using Experimental.System.Messaging;
using System.Net;
using System.Net.Mail;

namespace FundooModel
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        private string recieverEmail;
        private string recieverName;

        public void SendMessage(string token,string emailID,string name)
        {
            recieverEmail = emailID;
            recieverName= name;
            messageQueue.Path = @".\private$\Fundoo";

            try
            {
                if(!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] {typeof(string)});
                messageQueue.ReceiveCompleted += MessageQueue_RecieveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MessageQueue_RecieveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
               string token = msg.Body.ToString();
                MailMessage mailMessage= new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl= true,
                    Credentials = new NetworkCredential("waghmaremahesh012@gmail.com", "jjrobbwrsckadqcs")
                };
                mailMessage.From = new MailAddress("waghmaremahesh012@gmail.com");
                mailMessage.To.Add(new MailAddress(recieverEmail));
                string mailBody = $"<|DOCTYPE html>" +
                                  $"<html>" +
                                  $" <style>" +
                                  $".blink" +
                                  $"</style>" +
                                    $"<body style = \"background-color:#FFFFFF;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#660066; border-bottom: 3px solid #000000; margin-top: 5px;\"> Dear <b>{recieverName}</b> </h1>\n" +
                                    $"<h3 style = \"color:#330000;\"> For Resetting Password The Below Link Is Issued</h3>" +
                                    $"<h3 style = \"color:#993366;\"> Please Click The Link Below To Reset Your Password</h3>" +
                                    $"<a style = \"color:#666633; text-decoration: none; font-size:20px;\" href='http://localhost:4200/resetpassword/{token}'>Click here to Reset Password</a>\n" +
                                    $"<h3 style = \"color:#000033;margin-bottom:5px;\"><blink>This Token will be Valid For Next 1 Hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";

                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml= true;
                mailMessage.Subject = "Fundoo Note Reset Password Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
