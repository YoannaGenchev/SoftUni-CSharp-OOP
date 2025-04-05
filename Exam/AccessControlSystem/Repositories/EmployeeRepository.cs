using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Repositories
{
    public class EmployeeRepository : IRepository<IEmployee>
    {
        private List<IEmployee> models;

        public EmployeeRepository()
        {
            models = new List<IEmployee>();
        }

        public IReadOnlyCollection<IEmployee> Models => models.AsReadOnly();

        public void AddNew(IEmployee model)
        {
            models.Add(model);
        }

        public IEmployee GetByName(string modelName)
        {
            return models.FirstOrDefault(m => m.Name == modelName);
        }

        public int SecurityCheck(string modelName)
        {
            var employee = GetByName(modelName);
            if (employee.Department == null)
            {
                return 0;
            }

            return employee.Department.SecurityLevel;
        }
    }
}
