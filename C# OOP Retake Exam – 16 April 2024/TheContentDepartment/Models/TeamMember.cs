using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;

namespace TheContentDepartment.Models
{
    public abstract class TeamMember : ITeamMember
    {
        private string name;
        private string path;
        private readonly List<string> _inProgress;

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.NameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
            protected set
            {
                path = value;
            }
        }

        public IReadOnlyCollection<string> InProgress
        {
            get
            {
                return _inProgress;
            }
        }

        protected TeamMember(string name, string path)
        {
            Name = name;
            Path = path;
            _inProgress = new List<string>();
        }

        public void WorkOnTask(string resourceName)
        {
            if (!_inProgress.Contains(resourceName))
            {
                _inProgress.Add(resourceName);
            }
        }

        public void FinishTask(string resourceName)
        {
            _inProgress.Remove(resourceName);
        }
    }
}
