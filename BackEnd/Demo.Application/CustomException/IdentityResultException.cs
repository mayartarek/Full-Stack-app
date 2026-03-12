using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo.Application.CustomException
{
    public class IdentityResultException : Exception
    {
        public List<string> IdentityErrors { get; set; }

        public IdentityResultException(IdentityResult identityResult)
        {
            IdentityErrors = new List<string>();

            foreach (var item in identityResult.Errors)
            {
                IdentityErrors.Add(item.Description);
            }
        }
    }
}