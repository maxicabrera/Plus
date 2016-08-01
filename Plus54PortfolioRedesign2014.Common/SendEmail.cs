using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;


namespace Plus54PortfolioRedesign2014.Common
{
    /// <summary>
    /// Provides an interface to send emails
    /// </summary>
    /// <remarks></remarks>
    public class SendMail
    {

        private string strSmtpServer;
        private string strUserSMTP;
        private string strPasswordSMTP;
        private int iPortSmtp;
        private bool bUseAuthentication;

        private bool bUseSSL;

        /// <summary>
        /// Initializes the email settings
        /// </summary>
        /// <remarks></remarks>
        public SendMail(string smtpServer, int smtpPort, string smtpUser, string smtpPassword, bool smtpUseSsl)
        {
            strSmtpServer = smtpServer;
            iPortSmtp = smtpPort;
            strUserSMTP = smtpUser;
            strPasswordSMTP = smtpPassword;
            bUseAuthentication = string.IsNullOrEmpty(smtpUser) ? false : true;
            bUseSSL = smtpUseSsl;
        }

        /// <summary>
        /// Sends the email
        /// </summary>
        /// <param name="nameFrom">Name of the person who sends the email</param>
        /// <param name="mailFrom">Email address of the sender</param>
        /// <param name="mailTo">Email address where the email will be sent to</param>
        /// <param name="mailBcc">BCC address for the email</param>
        /// <param name="mailReply">Reply address for the email</param>
        /// <param name="mailSubject">Subject of the email</param>
        /// <param name="mailBody">Content of the email</param>
        /// <param name="htmlFormat">HTML Format (TRUE or FALSE)</param>
        /// <param name="highPriority">High Priority (TRUE or FALSE)</param>
        /// <param name="sendAsync">Indicates if the mail must be sent with or without async</param>
        /// <param name="pathToFileAttach">Location of the file that will be attached to the message</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DoSend(string nameFrom, string mailFrom, string mailto, string mailBcc, string mailReply, string mailSubject, string mailBody, bool htmlFormat, bool highPriority, bool sendAsync,
        string pathToFileAttach)
        {

            bool bResult = true;

            if (sendAsync)
            {
                SendMailAsync objAsync = null;
                objAsync = new SendMailAsync(Send);

                //Dim objResult As IAsyncResult = 
                objAsync.BeginInvoke(nameFrom, mailFrom, mailto, mailBcc, mailReply, mailSubject, mailBody, htmlFormat, highPriority, false,
                pathToFileAttach, null, null);
            }
            else
            {
                bResult = Send(nameFrom, mailFrom, mailto, mailBcc, mailReply, mailSubject, mailBody, htmlFormat, highPriority, false,
                pathToFileAttach);
            }

            return bResult;
        }

        /// <summary>
        /// Sends the email asyncronous
        /// </summary>
        /// <param name="nameFrom">Name of the person who sends the email</param>
        /// <param name="mailFrom">Email address of the sender</param>
        /// <param name="mailTo">Email address where the email will be sent to</param>
        /// <param name="mailBcc">BCC address for the email</param>
        /// <param name="mailReply">Reply address for the email</param>
        /// <param name="mailSubject">Subject of the email</param>
        /// <param name="mailBody">Content of the email</param>
        /// <param name="htmlFormat">HTML Format (TRUE or FALSE)</param>
        /// <param name="highPriority">High Priority (TRUE or FALSE)</param>
        /// <param name="sendAsync">Indicates if the mail must be sent with or without async</param>
        /// <param name="pathToFileAttach">Location of the file that will be attached to the message</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private delegate bool SendMailAsync(string nameFrom, string mailFrom, string mailTo, string mailBcc, string mailReply, string mailSubject, string mailBody, bool htmlFormat, bool highPriority, bool sendAsync,
        string pathToFileAttach);

        /// <summary>
        /// Sends an email message
        /// </summary>
        /// <param name="nameFrom">Name of the person who sends the email</param>
        /// <param name="mailFrom">Email address of the sender</param>
        /// <param name="mailTo">Email address where the email will be sent to</param>
        /// <param name="mailBcc">BCC address for the email</param>
        /// <param name="mailReply">Reply address for the email</param>
        /// <param name="mailSubject">Subject of the email</param>
        /// <param name="mailBody">Content of the email</param>
        /// <param name="htmlFormat">HTML Format (TRUE or FALSE)</param>
        /// <param name="highPriority">High Priority (TRUE or FALSE)</param>
        /// <param name="sendAsync">Indicates if the mail must be sent with or without async</param>
        /// <param name="pathToFileAttach">Location of the file that will be attached to the message</param>
        /// <returns>TRUE if the email is sent, FALSE if an error occurs</returns>
        /// <remarks></remarks>
        private bool Send(string nameFrom,
                                string mailFrom,
                                string mailTo,
                                string mailBcc,
                                string mailReply,
                                string mailSubject,
                                string mailBody,
                                bool htmlFormat,
                                bool highPriority,
                                bool sendAsync,
                                string pathToFileAttach)
        {

            using (MailMessage aMessage = new MailMessage())
            {
                bool bResult = true;

                aMessage.From = new MailAddress(mailFrom, nameFrom);

                //If Not strMailTo Is Nothing Then aMessage.To.Add(strMailTo)
                if ((mailTo != null))
                {
                    if (mailTo.IndexOf(";", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        string[] arrEmailTo = mailTo.Split(';');

                        for (int iRowCount = 0; iRowCount <= arrEmailTo.Length - 1; iRowCount++)
                            aMessage.To.Add(arrEmailTo[iRowCount]);
                    }
                    else
                        aMessage.To.Add(mailTo);
                }

                if ((mailBcc != null))
                {
                    if (mailBcc.IndexOf("|", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        string[] arrEmailBCC = mailBcc.Split(';');

                        for (int iRowCount = 0; iRowCount <= arrEmailBCC.Length - 1; iRowCount++)
                            aMessage.Bcc.Add(arrEmailBCC[iRowCount]);
                    }
                    else
                        aMessage.Bcc.Add(mailBcc);
                }

                if ((mailReply != null))
                {
                    //aMessage.ReplyTo = new MailAddress(mailReply, nameFrom);
                    aMessage.ReplyToList.Add(new MailAddress(mailReply, nameFrom));
                }

                aMessage.Subject = mailSubject;
                aMessage.Body = mailBody;

                if ((htmlFormat))
                    aMessage.IsBodyHtml = true;
                if ((highPriority))
                    aMessage.Priority = MailPriority.High;

                if ((pathToFileAttach != null))
                {
                    Attachment oAttch = new Attachment(pathToFileAttach);
                    aMessage.Attachments.Add(oAttch);
                }

                using (SmtpClient aClient = new SmtpClient())
                {
                    aClient.Host = strSmtpServer;
                    aClient.Port = iPortSmtp;
                    aClient.EnableSsl = bUseSSL;

                    if (bUseAuthentication)
                        aClient.Credentials = new NetworkCredential(strUserSMTP, strPasswordSMTP);

                    try
                    {
                        if (sendAsync)
                        {
                            aClient.SendCompleted += SendCompletedCallback;
                            string userState = "Mail Sent!";
                            aClient.SendAsync(aMessage, userState);
                        }
                        else
                            aClient.Send(aMessage);
                    }
                    catch (TimeoutException)
                    {
                        bResult = false;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        bResult = false;
                    }
                    catch (NotSupportedException)
                    {
                        bResult = false;
                    }
                }

                return bResult;
            }
        }

        /// <summary>
        /// Sends the email using an async process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {

            if (e.Cancelled)
            {
            }


            if (e.Error != null)
            {

            }
            else
            {
            }
        }
    }
}