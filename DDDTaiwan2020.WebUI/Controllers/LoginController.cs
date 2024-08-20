using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTaiwan2020.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Login()
        {
            return View();
        }
        /*
        [HttpPost]
        public IActionResult Login(NewUserCommand user)
        {
            return _mediator.Send(user).Result ? RedirectToAction("Index", "Home") as IActionResult : View();
        }
        */
    }
}
