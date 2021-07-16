using Bot.Interfaces;
using Telegram.Bot.Types;

namespace Bot.Models
{

    struct DescriptionOfTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int value  { get; set; }
        public string EasyHint { get; set; }
        public string MediumHint { get; set; }
        public string HardHint { get; set; }
        public string ExtraHint { get; set; }

        public Location Location { get; set; }
        public decimal radius { get; set; }
        
    }
    
    public class Task : ITask
    {
        public long UniqueId { get; set; }
    }
}