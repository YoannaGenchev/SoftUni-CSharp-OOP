using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Core
{
    public class Controller : IController
    {
        private ResourceRepository resources;
        private MemberRepository members;

        public Controller()
        {
            resources = new ResourceRepository();
            members = new MemberRepository();
        }

        public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
        {
            IResource resource = resources.TakeOne(resourceName);

            if (false == resource.IsTested)
            {
                return $"{resourceName} cannot be approved without being tested.";
            }

            ITeamMember teamLead = members.Models.FirstOrDefault(m => m.GetType().Name == nameof(TeamLead));
            if (isApprovedByTeamLead)
            {
                resource.Approve();
                teamLead!.FinishTask(resourceName);
                return $"{teamLead!.Name} approved {resourceName}.";
            }
            else
            {
                resource.Test();
                return $"{teamLead!.Name} returned {resourceName} for a re-test.";
            }
        }

        public string CreateResource(string resourceType, string resourceName, string path)
        {
            if (resourceType != nameof(Exam) && resourceType != nameof(Workshop) && resourceType != nameof(Presentation))
            {
                return $"{resourceType} type is not handled by Content Department.";
            }

            ITeamMember member = members.Models.FirstOrDefault(m => m.Path == path);
            if (member == null)
            {
                return $"No content member is able to create the {resourceName} resource.";
            }

            if (member.InProgress.Contains(resourceName))
            {
                return $"The {resourceName} resource is being created.";
            }

            IResource resource;
            if (resourceType == nameof(Exam))
            {
                resource = new Exam(resourceName, member.Name);
            }
            else if (resourceType == nameof(Workshop))
            {
                resource = new Workshop(resourceName, member.Name);
            }
            else
            {
                resource = new Presentation(resourceName, member.Name);
            }

            resources.Add(resource);
            member.WorkOnTask(resourceName);
            return $"{member.Name} created {resourceType} - {resourceName}.";
        }

        public string DepartmentReport()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Finished Tasks:");
            foreach (var resource in resources.Models.Where(r => r.IsApproved))
            {
                sb.AppendLine($"--{resource.ToString()}");
            }

            sb.AppendLine($"Team Report:");
            ITeamMember teamLead = members.Models.FirstOrDefault(m => m.GetType().Name == nameof(TeamLead));
            sb.AppendLine($"--{teamLead!.ToString()}");

            foreach (var member in members.Models.Where(m => m.GetType().Name != nameof(TeamLead)))
            {
                sb.AppendLine($"{member.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }

        public string JoinTeam(string memberType, string memberName, string path)
        {
            if (memberType != nameof(TeamLead) && memberType != nameof(ContentMember))
            {
                return $"{memberType} is not a valid member type.";
            }

            if (members.Models.Any(m => m.Path == path))
            {
                return "Position is occupied.";
            }

            if (members.Models.Any(m => m.Name == memberName))
            {
                return $"{memberName} has already joined the team.";
            }

            ITeamMember member;
            if (memberType == nameof(TeamLead))
            {
                member = new TeamLead(memberName, path);
            }
            else
            {
                member = new ContentMember(memberName, path);
            }

            members.Add(member);
            return $"{memberName} has joined the team. Welcome!";
        }

        public string LogTesting(string memberName)
        {
            ITeamMember member = members.TakeOne(memberName);
            if (member == null)
            {
                return "Provide the correct member name.";
            }

            var resource = resources.Models
                .Where(r => r.Creator == member.Name)
                .Where(r => false == r.IsTested)
                .OrderBy(r => r.Priority)
                .FirstOrDefault();

            if (resource == null)
            {
                return $"{memberName} has no resources for testing.";
            }

            ITeamMember teamLead = members.Models.FirstOrDefault(m => m.GetType().Name == nameof(TeamLead));
            member.FinishTask(resource.Name);
            teamLead!.WorkOnTask(resource.Name);
            resource!.Test();

            return $"{resource.Name} is tested and ready for approval.";
        }
    }
}
