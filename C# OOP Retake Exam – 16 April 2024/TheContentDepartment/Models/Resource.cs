using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;

namespace TheContentDepartment.Models
{
    public abstract class Resource : IResource
    {
        private string name;
        private string creator;
        private readonly int priority;
        private bool isTested;
        private bool isApproved;

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

        public string Creator
        {
            get
            {
                return creator;
            }
            private set
            {
                this.creator = value;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
        }

        public bool IsTested
        {
            get
            {
                return isTested;
            }
        }

        public bool IsApproved
        {
            get
            {
                return isApproved;
            }
        }

        public Resource(string name, string creator, int priority)
        {
            Name = name;
            Creator = creator;
            this.priority = priority;

            isTested = false;
            isApproved = false;
        }

        public void Test()
        {
            isTested = !isTested;
        }

        public void Approve()
        {
            isApproved = true;
        }

        public override string ToString()
        {
            return $"{Name} ({GetType().Name}), Created By: {Creator}";
        }
    }
}
