using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Repositories
{
    internal class SecurityZoneRepository : IRepository<ISecurityZone>
    {
        private List<ISecurityZone> models;

        public SecurityZoneRepository()
        {
            models = new List<ISecurityZone>();
        }

        public IReadOnlyCollection<ISecurityZone> Models => models.AsReadOnly();

        public void AddNew(ISecurityZone model)
        {
            models.Add(model);
        }

        public ISecurityZone GetByName(string modelName)
        {
            return models.FirstOrDefault(m => m.Name == modelName);
        }

        public int SecurityCheck(string modelName)
        {
            var securityZone = GetByName(modelName);
            return securityZone.AccessLevelRequired;
        }
    }
}
