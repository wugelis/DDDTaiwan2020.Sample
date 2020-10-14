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
    public class DbContextDef
    {
        public static string GetClassTemplate
        {
            get => @"using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;$(OTHER_NAMESPACE)$
using Microsoft.EntityFrameworkCore.Metadata;

namespace $(NAMESPACE_DEF)$.Persistance
{
    public partial class $(ENTITIES_DEF)$ : DbContext $(MARK_CODE)$, IApplicationDbContext
    {
        public $(ENTITIES_DEF)$()
        {
        }

        public $(ENTITIES_DEF)$(DbContextOptions<$(ENTITIES_DEF)$> options)
            : base(options)
        {
        }
		
		$(DB_SET_DEF)$
		
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(""Server=(local)\\MSSQLSERVER2017;Database=Northwind;Trusted_Connection=True;"");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public Task<int> SaveChangesAsync(bool cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

";
        }
    }
}
