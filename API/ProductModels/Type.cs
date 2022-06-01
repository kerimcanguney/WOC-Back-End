using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ProductModels
{
    public class Type
    {
        public string Name { get; set; }
        public IList<Info> Info { get; set; }
    }
}
