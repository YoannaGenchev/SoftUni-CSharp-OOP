using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;

namespace TheContentDepartment.Repositories.Contracts
{
    public class MemberRepository : IRepository<ITeamMember>
    {
        private List<ITeamMember> _models;
        public IReadOnlyCollection<ITeamMember> Models
        {
            get
            {
                return _models.AsReadOnly();
            }
        }

        public MemberRepository()
        {
            this._models = new List<ITeamMember>();
        }

        public void Add(ITeamMember model)
        {
            this._models.Add(model);
        }

        public ITeamMember TakeOne(string modelName)
        {
            return _models.FirstOrDefault(m => m.Name == modelName);
        }
    }
}
