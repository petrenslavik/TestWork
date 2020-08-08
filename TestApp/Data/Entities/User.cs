﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestApp.Data.Entities
{
    public class User:IdentityUser
    {
        public IList<UserTest> UserTest { get; set; }
    }
}
