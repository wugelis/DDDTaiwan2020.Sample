using System;
using System.Collections.Generic;
using System.Text;

namespace DDDTaiwan2020.Domain.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
