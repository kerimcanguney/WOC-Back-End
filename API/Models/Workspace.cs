using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Workspace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public IList<AccountWorkspace> Accounts { get; set; }
    }
}
