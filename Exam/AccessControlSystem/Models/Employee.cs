using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public abstract class Employee : IEmployee
    {
        private string name;
        private IDepartment department;
        private int securityId;

        public Employee(string name, int securityId)
        {
            Name = name;
            SecurityId = securityId;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEmployeeName);
                }
                name = value;
            }
        }

        public IDepartment Department
        {
            get => department;
            private set => department = value;
        }

        public int SecurityId
        {
            get => securityId;
            private set
            {
                if (value < 100 || value > 999)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidSecurityId);
                }
                securityId = value;
            }
        }

        public void AssignToDepartment(IDepartment department)
        {
            Department = department;
        }

        public override string ToString()
        {
            return $"Employee: {Name}, Department: {Department.GetType().Name}, Security ID: {SecurityId}";
        }
    }
}
