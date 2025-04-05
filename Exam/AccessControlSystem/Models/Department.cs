using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public abstract class Department : IDepartment
    {
        private List<string> employees;

        public Department()
        {
            employees = new List<string>();
        }

        public virtual int SecurityLevel => 0;

        public IReadOnlyCollection<string> Employees => employees.AsReadOnly();

        public virtual int MaxEmployeesCount => 1;

        public void ContractEmployee(string employeeName)
        {
            if (Employees.Count == MaxEmployeesCount)
            {
                throw new ArgumentException(ExceptionMessages.InvalidDepartmentCapacity);
            }

            if (Employees.Contains(employeeName))
            {
                throw new ArgumentException(ExceptionMessages.EmployeeAlreadyAdded);
            }

            employees.Add(employeeName);
        }
    }
}
