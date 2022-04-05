using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyViewModel(Company company)
        {
            Id = company.Id;
            Name = company.Name;
        }
    }
}
