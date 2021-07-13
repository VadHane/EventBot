using Bot.Models;
using Telegram.Bot.Types;

namespace Bot.Models
{
    public class Student
    {
        public Student(string name, User user)
        {
            this.Name = name;
            Telegram_name = user.FirstName + ' ' + user.LastName;
            Telegram_id = user.Id;
            Telegram_username = user.Username;
            Unique_id = GetUniqueId();
            CanJoinToTeam = true;
            Team = null;
        }
        
        
        public string Name { get; set; }
        public string Telegram_name { get; set; }
        public string Telegram_username { get; set; }
        public long Telegram_id { get; set; }
        public long Unique_id { get; set; }
        public bool CanJoinToTeam { get; set; }
        public Team Team { get; set; }
        
        
        private static long CounterUniqueId;

        private long GetUniqueId()
        {
            CounterUniqueId++;
            return CounterUniqueId - 1;
        }
    }
}