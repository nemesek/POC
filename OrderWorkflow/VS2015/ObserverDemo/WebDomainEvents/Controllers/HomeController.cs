﻿using System;
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

            RegisterFuncs();
            var order = new Order();
            await order.CreateOrderAsync(async e => await DomainEvents.DomainEvents.RaiseAsync(e));
            DomainEvents.DomainEvents.ClearCallbacks();
            return "Hello Domain Event2";
        }

        private void RegisterActions()
        {
            DomainEvents.DomainEvents.Register<OrderCreatedEvent>(async e => await DoSomething());
            DomainEvents.DomainEvents.Register<OrderCreatedEvent>(async e => await DoSomethingElse());
            DomainEvents.DomainEvents.Register<OrderCreatedEvent>(_ => DoSomething());
            DomainEvents.DomainEvents.Register<OrderCreatedEvent>(_ => DoSomethingElse());
        }

        private void RegisterFuncs()
        {
            DomainEvents.DomainEvents.Register(e => true);
            DomainEvents.DomainEvents.Register(e =>
            {
                Task.Delay(100);
                return true;
            });
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