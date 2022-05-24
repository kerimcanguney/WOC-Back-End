﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] StoredSalt { get; set; }
        public Role Role { get; set; }
        public IList<AccountWorkspace> Workspaces { get; set; }
    }
}
