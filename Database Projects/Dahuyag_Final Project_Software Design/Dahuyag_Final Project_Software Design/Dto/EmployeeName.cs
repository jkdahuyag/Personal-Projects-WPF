using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
{
    public class EmployeeName
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        public EmployeeName(Employee e)
        {
            EmployeeId = e.EmployeeId;
            Region = e.RegionLink.Name.Substring(e.RegionLink.Name.Length - 3, 2);
            Name = $"{e.FirstName} {e.MiddleName[0]}. {e.LastName}, {Region}";
        }
    }
}
