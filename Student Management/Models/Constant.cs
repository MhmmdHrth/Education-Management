using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Models
{
    public enum UserRole
    {
        Admin,
        Individual,
        Developer,
    }

    public static class Constant
    {
        public const string Issuer = "";
        public const string Audience = Issuer;
    }
}
