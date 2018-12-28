using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using inz.Services;

namespace inz.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potwierdzenie maila",
                $"Proszê o potwierdzenie stworzenia konta przez klikniêcie w link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
