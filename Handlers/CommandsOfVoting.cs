using System.Linq;
using System.Threading.Tasks;
using Bot.Interfaces;
using Telegram.Bot.Types;

namespace Bot.Handlers
{
    public static class CommandsOfVoting
    {
        /// <summary>
        /// Розпаршує запит на окремі команди та виконує їх.
        /// </summary>
        /// <param name="commands">Масив із командами.</param>
        /// <param name="msg">Силка на екземпляр повідомлення.</param>
        public static async Task ParseCommands(string[] commands)
        {
            switch (commands[1])
            {
                case "Yes":
                    SetVoting(int.Parse(commands[2]), true);
                    break;
                case "No":
                    SetVoting(int.Parse(commands[2]), false);
                    break;
            }
        }

        private static void SetVoting(int votindId, bool vote)
        {
            IVoting Voting = Settings.Votings.FirstOrDefault(voting => voting.Id == votindId);
            if (Voting == null) return;
            
            if (vote)
            {
                 Voting.CounterYes++;
            }
            else
            {
                Voting.CounterNo++;
            }
        }
        
    }
}