using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using inz.Models;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace inz.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView();
            else
                return View();
        }

        public async Task<IActionResult> Execute(string mail, string subject, string name, string content)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(mail, "OpenPlayer"),
                Subject = subject,
                HtmlContent = content + "<br/> <br/> <b>" + name + "</b>"
            };
            msg.AddTo(new EmailAddress("cobrra95@gmail.com"));
            var response = await client.SendEmailAsync(msg);

            return RedirectToAction("Contact");
        }

        public IActionResult About()
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjax)
                return PartialView();
            else
                return View();
        }

        public IActionResult Contact()
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjax)
                return PartialView();
            else
                return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
