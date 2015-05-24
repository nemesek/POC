using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainEvents;
using Microsoft.AspNet.Mvc;

namespace WebDomainEvents.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<string> Events()
        {
            await Task.Delay(100);
            DomainEvents.DomainEvents.Register<OrderCreatedEvent>(async e => await DoSomething());
            DomainEvents.DomainEvents.Register <OrderCreatedEvent>(async e => await DoSomethingElse());
            var order = new Order();
            order.CreateOrder();
            return "Hello Domain Event2";
        }

        private async Task<bool> DoSomething()
        {
            await Task.Delay(100);
            return true ;
        }

        private async Task<bool> DoSomethingElse()
        {
            await Task.Delay(100);
            return true;
        }
    }
}
