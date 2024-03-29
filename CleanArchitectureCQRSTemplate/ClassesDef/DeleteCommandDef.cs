﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.ClassesDef
{
    public class DeleteCommandDef
    {
        /// <summary>
        /// 取得 CQRS Delete Command 的程式碼樣板
        /// </summary>
        public static string GetClassTemplate
        {
            get => @"using $(NAMESPACE_DEF)$.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace $(NAMESPACE_DEF)$.$(CREATE_COMMAND_NAME)$.Commands
{
    /// <summary>
    /// CQRS $(CREATE_COMMAND_NAME)$ 的 Delete Command 命令
    /// </summary>
    public class Delete$(CREATE_COMMAND_NAME)$Command: IRequest<int>
    {
        $(CLASS_PROPERTIES_DEF)$

        public class Delete$(CREATE_COMMAND_NAME)$CommandHandler : IRequestHandler<Delete$(CREATE_COMMAND_NAME)$Command, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public Delete$(CREATE_COMMAND_NAME)$CommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<int> Handle(Delete$(CREATE_COMMAND_NAME)$Command request, CancellationToken cancellationToken)
            {
                /*
                $(CREATE_COMMAND_NAME)$Ent entity = new $(CREATE_COMMAND_NAME)$Ent();

                // 設定資料到 Entity
                _applicationDbContext.$(CREATE_COMMAND_NAME)$.Remove(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                */

                int result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return result;
            }
        }
    }
}
";
        }
    }
}
