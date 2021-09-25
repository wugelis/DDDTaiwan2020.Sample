using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace $safeprojectname$.Users.Commands.NewUser
{
    public class NewUserCommand: IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
