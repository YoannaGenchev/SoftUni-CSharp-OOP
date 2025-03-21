using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private double rating;
        private List<Player> players;

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
            rating = 0;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }

        public IReadOnlyCollection<Player> Players
        {
            get
            {
                return players.AsReadOnly();
            }
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
            rating = players.Average(p => p.GetSkillLevel());
        }

        public void RemovePlayer(string playerName)
        {
            var player = players.FirstOrDefault(p => p.Name == playerName);
            if (player != null)
            {
                players.Remove(player);
                if (players.Count > 0)
                {
                    rating = players.Average(p => p.GetSkillLevel());
                }
                else
                {
                    rating = 0.0;
                }
            }
            else
            {
                throw new ArgumentException($"Player {playerName} is not in {Name} team.");
            }
        }

        public string GetRating()
        {
            return $"{Name} - {rating:F0}";
        }
    }
}
