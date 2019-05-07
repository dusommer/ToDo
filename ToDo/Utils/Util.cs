using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ToDo.Utils
{
    public static class Util
    {

        public static string Encrypt(string text)
        {
            var encoding = Encoding.Unicode;
            byte[] stringBytes = encoding.GetBytes(text);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        public static string Decrypt(string encryptedText)
        {
            var encoding = Encoding.Unicode;
            int numberChars = encryptedText.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(encryptedText.Substring(i, 2), 16);
            }
            return encoding.GetString(bytes);
        }

        internal static void SendEmail(string email, string nomeLista, string userEmail, string url)
        {
            string FROM = "vribbraprojectodo@gmail.com";
            string FROMNAME = "ToDo Project";
            string TO = email;
            string SMTP_USERNAME = "vibbraprojectodo@gmail.com";
            string SMTP_PASSWORD = "@Todoproject";
            string CONFIGSET = "ConfigSet";
            string HOST = "smtp.gmail.com";
            int PORT = 587;
            string SUBJECT = "Você recebu um link para acesso Vibbra ToDo";


            string BODY = string.Format("<h1>Vibbra ToDo</h1><p><a href='{0}'>Clique aqui</a> para acessar a lista {1}, criada pelo {2}.</p>", url, nomeLista, userEmail);

            MailMessage message = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(FROM, FROMNAME)
            };
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                client.EnableSsl = true;

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}