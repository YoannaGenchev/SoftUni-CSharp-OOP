using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheContentDepartment.Models
{
    public class ContentMember : TeamMember
    {
        private static readonly HashSet<string> allowedPaths = new() { "CSharp", "JavaScript", "Python", "Java" };

        public ContentMember(string name, string path) : base(name, path)
        {
            if (false == allowedPaths.Contains(path))
            {
                throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.PathIncorrect, path));
            }
        }

        public override string ToString()
        {
            return $"{Name} - {Path} path. Currently working on {InProgress.Count} tasks.";
        }
    }
}
