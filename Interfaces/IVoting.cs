using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Interfaces
{
    public interface IVoting
    {
        public int Id { get; init; }
        public ITeam Team { get; set; }
        public List<IStudent> Students { get; set; }

        public int CounterYes { get; set; }
        public int CounterNo { get; set; }


        public Task<bool> StartVote(int studentUniqueId);
    }
}