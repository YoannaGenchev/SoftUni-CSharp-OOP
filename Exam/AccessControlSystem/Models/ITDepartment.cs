using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class ITDepartment : Department
    {
        public ITDepartment() : base() { }

        public override int SecurityLevel => 5;
        public override int MaxEmployeesCount => 8;
    }
}
