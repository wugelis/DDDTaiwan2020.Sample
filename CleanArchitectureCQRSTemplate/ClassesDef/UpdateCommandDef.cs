using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.ClassesDef
{
    public class UpdateCommandDef
    {
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
    /// CQRS $(CREATE_COMMAND_NAME)$ 的 Update Command 命令
    /// </summary>
    public class Update$(CREATE_COMMAND_NAME)$Command: IRequest<int>
    {
        $(CLASS_PROPERTIES_DEF)$

        public class Update$(CREATE_COMMAND_NAME)$CommandHandler : IRequestHandler<Update$(CREATE_COMMAND_NAME)$Command, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public Update$(CREATE_COMMAND_NAME)$CommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<int> Handle(Update$(CREATE_COMMAND_NAME)$Command request, CancellationToken cancellationToken)
            {
                /*
                $(CREATE_COMMAND_NAME)$Ent entity = new $(CREATE_COMMAND_NAME)$Ent();

                // 設定資料到 Entity
                _applicationDbContext.$(CREATE_COMMAND_NAME)$.Update(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
