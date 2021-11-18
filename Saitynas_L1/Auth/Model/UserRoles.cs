using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Auth.Model
{
    public static class UserRoles
    {
        public const string User = nameof(User);
        public const string Worker = nameof(Worker);
        public const string Admin = nameof(Admin);

        public static readonly IReadOnlyCollection<string> All = new[] { User, Worker, Admin };
    }
}
