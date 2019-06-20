using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JewelryStore.Models;
using JewelryStore.UI;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace JewelryStore.UI.Emails
{   
        public class Emails
    {       
        public void EmailSending(Order order)
        {       
            
            using (MailMessage mail = new MailMessage())
            {                
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));
                                
                StringBuilder body = new StringBuilder()
                .AppendLine("Ваш заказ принят")
                .AppendLine("---")
                .AppendLine("Товар:");
                .AppendLine("Стоимость");                
                body.AppendFormat("Стоимость заказа: {0:c}", order.Price)
                  .AppendLine("---")
                  //.AppendLine(order.)
                  .AppendLine(order.FirstName)
                  .AppendLine(order.LastName)                  
                  .AppendLine(order.Delivery)
                  .AppendLine(order.Date.ToString())
                  .AppendLine(order.Phone)
                  .AppendLine("---");

                mail.From = new MailAddress("Zeferod1@gmail.com");
                mail.To.Add(order.Email);
                mail.Subject = "Интернет заказ";
                mail.Body = body.ToString();
                mail.IsBodyHtml = false;
                
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("Zeferod1@gmail.com", "edmondantes");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }

        }

    }
}