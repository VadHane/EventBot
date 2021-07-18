using System.Collections.Generic;

namespace Bot.Interfaces
{
    public interface IVoting
    {
        public int Id { get; init; }
        public ITeam Team { get; set; }
        public List<IStudent> Students { get; set; }

        public int CounterYes { get; set; }
        public int CounterNo { get; set; }
    }
}