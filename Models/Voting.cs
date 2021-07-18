using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bot.DataBase;
using Bot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Models
{
    public class Voting : IVoting
    {
        public Voting(int leaderId)
        {
            Id = GetId();
            Team = DB.GetTeamByLeaderId(leaderId).Result;
            Students = DB.GetStudentsByTeamId(DB.GetTeamByLeaderId(leaderId).Result.UniqueId).Result;
            Messages = new List<Message>();
            CounterYes = 0;
            CounterNo = 0;
        }

        public int Id { get; init; }
        public ITeam Team { get; set; }
        public List<IStudent> Students { get; set; }
        private List<Message> Messages { get; set; }
        
        public int CounterYes { get; set; }
        public int CounterNo { get; set; }
        
        private static int CounterOfVoting = 0;

        private static int GetId()
        {
            return CounterOfVoting++;
        }

        private async Task<bool> DeleteAllMessage()
        {
            foreach (var message in Messages)
            {
                await Program.TryDeleteMessageAsync(message);
            }
            return true;
        }
        
        public async Task<bool> StartVote(int studentUniqueId)
        {
            var deleteStudent = Students.FirstOrDefault(student => student.UniqueId == studentUniqueId);
            Settings.Votings.Add(this);
            
            foreach (var student in Students)
            {
                if (student.UniqueId != studentUniqueId)
                {
                    Messages.Add(
                            await Program.bot.SendTextMessageAsync(student.TelegramId,
                                "Капітан команди запустив <b>процедуру видалення гравця із вашої команди</b>." +
                                "\n\nІнформація про гравця, якого хочуть видалити: " +
                                $"\nІм'я - <b>{deleteStudent?.Name}</b>" +
                                $"\nУнікальний ідентифікатор - <b>{deleteStudent?.UniqueId}</b>" +
                                $"\nТелеграм username - <b>{deleteStudent?.TelegramUsername}</b>" +
                                $"\n\n\n<b>Чи згідні ви видалити цього гравця з команди?</b>", ParseMode.Html,
                                replyMarkup:Keyboards.Voting(this.Id))
                        );
                }
            }
            
            Thread.Sleep(60 * 60 * 100); // sleep 1 min
            
            await DeleteAllMessage();
            Settings.Votings.RemoveAll(voting => voting.Id == this.Id);

            if (CounterYes > CounterNo)
            {
                await Program.bot.SendTextMessageAsync(this.Team.Leader, "Команда прийняла вашу пропозицію!",
                    replyMarkup: Keyboards.DeleteThisMessage());

                await DB.DeleteStudentFromTeam(deleteStudent.TelegramId);
                await Program.TryEditMessage(deleteStudent.TelegramId, deleteStudent.MainMessageId,
                    "Для вас гру <b>завершено!</b>", ParseMode.Html);
            }
            else
            {
                await Program.bot.SendTextMessageAsync(this.Team.Leader, "Команда не прийняла вашу пропозицію!",
                    replyMarkup: Keyboards.DeleteThisMessage());
            }
            
            return CounterNo < CounterYes;
        }
    }
}