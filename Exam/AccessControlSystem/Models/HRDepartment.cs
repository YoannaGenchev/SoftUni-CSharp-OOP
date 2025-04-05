using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class HRDepartment : Department
    {
        public HRDepartment() : base() { }

        public override int SecurityLevel => 3;
        public override int MaxEmployeesCount => 5;
    }
}
