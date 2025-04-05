using AccessControlSystem.Core.Contracts;
using AccessControlSystem.Models;
using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Repositories;
using AccessControlSystem.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Core
{
    public class Controller : IController
    {
        private ICollection<IDepartment> departments;
        private SecurityZoneRepository securityZones;
        private EmployeeRepository employees;

        public Controller()
        {
            departments = new List<IDepartment>();
            securityZones = new SecurityZoneRepository();
            employees = new EmployeeRepository();
        }

        public string AddDepartment(string departmentTypeName)
        {
            if (departmentTypeName != nameof(ITDepartment) &&
                departmentTypeName != nameof(HRDepartment) &&
                departmentTypeName != nameof(FinanceDepartment))
            {
                return string.Format(OutputMessages.InvalidDepartmentType, departmentTypeName);
            }

            if (departments.Any(d => d.GetType().Name == departmentTypeName))
            {
                return string.Format(OutputMessages.DepartmentExists, departmentTypeName);
            }

            IDepartment department;
            if (departmentTypeName == nameof(ITDepartment))
            {
                department = new ITDepartment();
            }
            else if (departmentTypeName == nameof(HRDepartment))
            {
                department = new HRDepartment();
            }
            else
            {
                department = new FinanceDepartment();
            }

            departments.Add(department);
            return string.Format(OutputMessages.DepartmentAdded, departmentTypeName);
        }

        public string AddEmployeeToApplication(string employeeName, string employeeTypeName, int securityId)
        {
            if (employeeTypeName != nameof(GeneralEmployee) &&
                employeeTypeName != nameof(ITSpecialist))
            {
                return string.Format(OutputMessages.InvalidEmployeeType, employeeTypeName);
            }

            if (employees.Models.Any(m => m.Name == employeeName))
            {
                return string.Format(OutputMessages.EmployeeExistsInApplication, employeeName);
            }

            if (employees.Models.Any(m => m.SecurityId == securityId))
            {
                return string.Format(OutputMessages.SecurityIdExists, securityId);
            }

            IEmployee employee;
            if (employeeTypeName == nameof(GeneralEmployee))
            {
                employee = new GeneralEmployee(employeeName, securityId);
            }
            else
            {
                employee = new ITSpecialist(employeeName, securityId);
            }

            employees.AddNew(employee);
            return string.Format(OutputMessages.EmployeeAddedToApplication, employeeName);
        }

        public string AddEmployeeToDepartment(string employeeName, string departmentTypeName)
        {
            IEmployee employee = employees.GetByName(employeeName);
            if (employee == null)
            {
                return string.Format(OutputMessages.EmployeeNotInApplication, employeeName);
            }

            if (departmentTypeName != nameof(ITDepartment) &&
                departmentTypeName != nameof(HRDepartment) &&
                departmentTypeName != nameof(FinanceDepartment))
            {
                return string.Format(OutputMessages.InvalidDepartmentType, departmentTypeName);
            }

            if ((employee.GetType().Name == nameof(ITSpecialist) && departmentTypeName != nameof(ITDepartment)) ||
                (employee.GetType().Name == nameof(GeneralEmployee) && departmentTypeName == nameof(ITDepartment)))
            {
                return string.Format(OutputMessages.ContractNotAllowed, employee.GetType().Name, departmentTypeName);
            }

            IDepartment department = departments.FirstOrDefault(d => d.GetType().Name == departmentTypeName);
            if (department == null)
            {
                return string.Format(OutputMessages.DepartmentIsNotAvailable, departmentTypeName);
            }

            if (employee.Department != null)
            {
                return string.Format(OutputMessages.EmployeeExistsInDepartment, employeeName);
            }

            if (department.Employees.Count == department.MaxEmployeesCount)
            {
                return string.Format(OutputMessages.DepartmentFull, departmentTypeName);
            }

            department.ContractEmployee(employeeName);
            employee.AssignToDepartment(department);
            return string.Format(OutputMessages.EmployeeAddedToDepartment, employee.GetType().Name, departmentTypeName);
        }

        public string AddSecurityZone(string securityZoneName, int accessLevelRequired)
        {
            if (securityZones.Models.Any(m => m.Name == securityZoneName))
            {
                return string.Format(OutputMessages.SecurityZoneExists, securityZoneName);
            }

            var securityZone = new SecurityZone(securityZoneName, accessLevelRequired);
            securityZones.AddNew(securityZone);
            return string.Format(OutputMessages.SecurityZoneAdded, securityZoneName);
        }

        public string AuthorizeAccess(string securityZoneName, string employeeName)
        {
            ISecurityZone securityZone = securityZones.GetByName(securityZoneName);
            if (securityZone == null)
            {
                return string.Format(OutputMessages.SecurityZoneNotFound, securityZoneName);
            }

            IEmployee employee = employees.GetByName(employeeName);
            if (employee == null)
            {
                return string.Format(OutputMessages.EmployeeNotInApplication, employeeName);
            }

            if (employee.Department == null || employee.Department.SecurityLevel < securityZone.AccessLevelRequired)
            {
                return string.Format(OutputMessages.AccessDenied, employeeName, securityZoneName);
            }

            if (securityZone.AccessLog.Any(a => a == employee.SecurityId))
            {
                return string.Format(OutputMessages.EmployeeAlreadyAuthorized, employeeName, securityZoneName);
            }

            securityZone.LogAccessKey(employee.SecurityId);
            return string.Format(OutputMessages.EmployeeAuthorized, employeeName, securityZoneName);
        }

        public string SecurityReport()
        {
            var sb = new StringBuilder();
            var orderedSecurityZones = securityZones.Models.OrderByDescending(m => m.AccessLevelRequired).ThenBy(p => p.Name);

            sb.AppendLine("Security Report:");
            foreach (var securityZone in orderedSecurityZones)
            {
                sb.AppendLine($"-{securityZone.Name} (Access level required: {securityZone.AccessLevelRequired})");
                foreach(var employeeId in securityZone.AccessLog)
                {
                    var employee = employees.Models.FirstOrDefault(m => m.SecurityId == employeeId);
                    sb.AppendLine($"--{employee!.ToString()}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
