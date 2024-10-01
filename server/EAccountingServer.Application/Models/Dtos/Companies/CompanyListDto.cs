using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Application.Models.Dtos.Companies
{
    public class CompanyListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string TaxDepartment { get; set; }
        public string TaxNumber { get; set; }
        //public Database Database { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);
    }
}
