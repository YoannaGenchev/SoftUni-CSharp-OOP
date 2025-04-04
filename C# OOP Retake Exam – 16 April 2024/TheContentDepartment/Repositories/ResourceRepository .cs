using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;

namespace TheContentDepartment.Repositories.Contracts
{
    public class ResourceRepository : IRepository<IResource>
    {
        private List<IResource> _models;
        public IReadOnlyCollection<IResource> Models
        {
            get
            {
                return _models.AsReadOnly();
            }
        }

        public ResourceRepository()
        {
            this._models = new List<IResource>();
        }

        public void Add(IResource model)
        {
            this._models.Add(model);
        }

        public IResource TakeOne(string modelName)
        {
            return _models.FirstOrDefault(r => r.Name == modelName);
        }
    }
}
