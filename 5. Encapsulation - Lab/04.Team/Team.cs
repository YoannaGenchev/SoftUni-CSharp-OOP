using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsInfo
{
    public class Team
    {
        public Team(string name)
        {
            this.name = name;
            firstTeam = new List<Person>();
            reserveTeam = new List<Person>();
        }

        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public IReadOnlyCollection<Person> FirstTeam
        {
            get { return this.firstTeam.AsReadOnly(); }
        }

        public IReadOnlyCollection<Person> ReserveTeam
        {
            get { return this.reserveTeam.AsReadOnly(); }
        }

        public void AddPlayer(Person person)
        {
            if (person.Age < 40)
            {
                this.firstTeam.Add(person);
            }
            else
            {
                this.reserveTeam.Add(person);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"First team has {this.firstTeam.Count} players.");
            sb.Append($"Reserve team has {this.reserveTeam.Count} players.");

            return sb.ToString();
        }
    }
}
