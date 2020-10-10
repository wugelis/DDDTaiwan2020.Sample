using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTaiwan2020.Application.Users.Commands.NewUser
{
    /// <summary>
    /// 
    /// </summary>
    public class NewUserCommandHandler : IRequestHandler<NewUserCommand, bool>
    {
        public Task<bool> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            // 實作儲存到資料庫的內容

            // 回傳
            return Task.FromResult(true);
        }
    }
}
