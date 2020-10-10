using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDDTaiwan2020.Infrastructure.Services.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public static class IdentityExtensionMethods
    {
        /* (暫時註解) 必須參考 Application 層後再將其還原
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
        */
    }
}
