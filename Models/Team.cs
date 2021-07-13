using System;
using System.Collections.Generic;
using Bot.Interfaces;

namespace Bot.Models
{
    public class Team : ITeam
    {
        public Team(string name, string group, long leader)
        {
            this.Name = name;
            this.Group = group;
            UniqueId = GetUniqueId();

            this.Leader = leader;
            Members = new List<long>();

            Score = 0;
            
            
        }
        
        // Description of group
        public string Name { get; set; }
        public string Group { get; set; }
        public long UniqueId { get; set; }
        
        // Members and leader
        public long Leader { get; set; }
        public List<long> Members { get; set; }
        
        // Score
        public long Score { get; }
        
        // Tasks 
        public ITask Task { get; set; }


        
        private static long CounterUniqueId;

        private List<ITask> setListOfTask(List<ITask> tasks)
        {
            for (int i = 0; i < new Random().Next(0, 10); i++)
            {
                for (int j = 0; j < tasks.Count; j++)
                {
                    int randomNumber = new Random().Next(0, tasks.Count - 1);
                    var task = tasks[randomNumber];
                    tasks[randomNumber] = tasks[j];
                    tasks[j] = task;
                }
            }

            return tasks;
        }

        private long GetUniqueId()
        {
            CounterUniqueId++;
            return CounterUniqueId - 1;
        }
    }
}