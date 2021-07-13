using Bot.Interfaces;
using Telegram.Bot.Types;

namespace Bot.Models
{
    public class Student : IStudent
    {
        public Student(string name, User user, int msgId)
        {
            this.Name = name;
            TelegramName = user.FirstName + ' ' + user.LastName;
            TelegramId = user.Id;
            TelegramUsername = user.Username;
            UniqueId = GetUniqueId();
            CanJoinToTeam = true;
            TeamId = -1;
            MainMessageId = msgId;
        }
        
        
        public string Name { get; set; }
        public string TelegramName { get; set; }
        public string TelegramUsername { get; set; }
        public long TelegramId { get; set; }
        public long UniqueId { get; set; }
        public bool CanJoinToTeam { get; set; }
        public long TeamId { get; set; }
        public int MainMessageId { get; set; }
        
        
        private static long CounterUniqueId;

        private long GetUniqueId()
        {
            CounterUniqueId++;
            return CounterUniqueId - 1;
        }
    }
}