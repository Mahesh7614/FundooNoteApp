// <copyright file="MSMQModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooModel
{
    using System.Net;
    using System.Net.Mail;
    using Experimental.System.Messaging;

    /// <summary>
    /// MSMQModel.
    /// </summary>
    public class MSMQModel
    {
        private MessageQueue messageQueue = new MessageQueue();
        private string? recieverEmail;
        private string? recieverName;

        /// <summary>
        /// SendMessage.
        /// </summary>
        /// <param name="token">token.</param>
        /// <param name="emailID">emailID.</param>
        /// <param name="name">name.</param>
        public void SendMessage(string token, string emailID, string name)
        {
            this.recieverEmail = emailID;
            this.recieverName = name;
            this.messageQueue.Path = @".\private$\Fundoo";

            try
            {
                if (!MessageQueue.Exists(this.messageQueue.Path))
                {
                    MessageQueue.Create(this.messageQueue.Path);
                }

                this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.messageQueue.ReceiveCompleted += this.MessageQueue_RecieveCompleted;
                this.messageQueue.Send(token);
                this.messageQueue.BeginReceive();
                this.messageQueue.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void MessageQueue_RecieveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = this.messageQueue.EndReceive(e.AsyncResult);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string token = msg.Body.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("waghmaremahesh012@gmail.com", "edjmssbjwcwzpuuv"),
                };
                mailMessage.From = new MailAddress("waghmaremahesh012@gmail.com");
#pragma warning disable CS8604 // Possible null reference argument.
                mailMessage.To.Add(new MailAddress(this.recieverEmail));
#pragma warning restore CS8604 // Possible null reference argument.
                string mailBody = $"<|DOCTYPE html>" +
                                  $"<html>" +
                                  $" <style>" +
                                  $".blink" +
                                  $"</style>" +
                                    $"<body style = \"background-color:#FFFFFF;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#660066; border-bottom: 3px solid #000000; margin-top: 5px;\"> Dear <b>{this.recieverName}</b> </h1>\n" +
                                    $"<h3 style = \"color:#330000;\"> For Resetting Password The Below Link Is Issued</h3>" +
                                    $"<h3 style = \"color:#993366;\"> Please Click The Link Below To Reset Your Password</h3>" +
                                    $"<a style = \"color:#666633; text-decoration: none; font-size:20px;\" href='http://localhost:4200/resetpassword/{token}'>Click here to Reset Password</a>\n" +
                                    $"<h3 style = \"color:#000033;margin-bottom:5px;\"><blink>This Token will be Valid For Next 1 Hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";

                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Fundoo Note Reset Password Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
