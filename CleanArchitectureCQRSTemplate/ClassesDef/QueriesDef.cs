using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.ClassesDef
{
    /// <summary>
    /// 
    /// </summary>
    public class QueriesDef
    {
        /// <summary>
        /// 取得 CQRS Query Command 定義 (暫時放置程式碼中，建議放置在 Resources 或 Txt 中)
        /// </summary>
        public static string GetCQRSQueryCommandTemplate
        {
            get
            {
                return @"
using $(NAMESPACE_DEF)$.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace $(NAMESPACE_DEF)$.$(QUERY_COMMAND_NAME)$.Queries
{
    /// <summary>
    /// 查詢 Northwind 的 Customers 的命令
    /// </summary>
    public class $(QUERY_COMMAND_NAME)$Query: IRequest<IEnumerable<$(QUERY_DTO)$>>
    {
        public class $(QUERY_COMMAND_NAME)$QueryHandler : IRequestHandler<$(QUERY_COMMAND_NAME)$Query, IEnumerable<$(QUERY_DTO)$>>
        {
            private readonly IApplicationDbContext _context;

            public $(QUERY_COMMAND_NAME)$QueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<$(QUERY_DTO)$>> Handle($(QUERY_COMMAND_NAME)$Query request, CancellationToken cancellationToken)
            {
                /* the Query Sample Code.
                var result = await (from customer in _context.Customers
                             select new $(QUERY_DTO)$
                             {
                                 CustomerId = customer.CustomerId
                                 // Others...

                             }).ToListAsync(cancellationToken);

                return result;
                */

                return await Task.FromResult<IEnumerable<$(QUERY_DTO)$>>(null);
            }
        }
    }
}

";
            }
        }
    }
}
