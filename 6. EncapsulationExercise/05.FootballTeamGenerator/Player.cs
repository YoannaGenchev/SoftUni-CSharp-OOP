﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Name = name;
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
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
        public int Endurance
        {
            get => endurance;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Endurance should be between 0 and 100.");
                endurance = value;
            }
        }
        public int Sprint
        {
            get => sprint;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Sprint should be between 0 and 100.");
                sprint = value;
            }
        }
        public int Dribble
        {
            get => dribble;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Dribble should be between 0 and 100.");
                dribble = value;
            }
        }
        public int Passing
        {
            get => passing;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Passing should be between 0 and 100.");
                passing = value;
            }
        }
        public int Shooting
        {
            get => shooting;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Shooting should be between 0 and 100.");
                shooting = value;
            }
        }

        public double GetSkillLevel()
        {
            return (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0;
        }
    }
}
