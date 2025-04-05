using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class FinanceDepartment : Department
    {
        public FinanceDepartment() : base() { }

        public override int SecurityLevel => 4;
        public override int MaxEmployeesCount => 3;
    }
}
