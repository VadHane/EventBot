using System;
using System.Threading;
using System.Threading.Tasks;
using Bot.DataBase;
using Bot.Static;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Handlers
{
    public abstract class CommandsOfLeader
    {
        /// <summary>
        /// Розпаршує запит на окремі команди капітанів та виконує їх.
        /// </summary>
        /// <param name="commands">Масив із командами.</param>
        /// <param name="msg">Силка на екземпляр повідомлення.</param>
        public static async Task ParseCommands(string[] commands, Message msg)
        {
            switch (commands[1])
            {
                case "AddMember":
                    await AddMember(msg.From.Id, msg.MessageId);
                    break;
                case "DeleteMember":
                    await DeleteMember(msg.From.Id, msg.MessageId);
                    break;
            }
        }

        /// <summary>
        /// Добавляє нового гравця в команду.
        /// </summary>
        /// <param name="LeaderId">Унікальний ідентифікатор капітана команди.</param>
        /// <param name="msgId">Унікальний ідентифікатор головного повідомлення капітана команди.</param>
        private static async Task AddMember(int LeaderId, int msgId)
        {
            int step = 1;
            var wait = new ManualResetEvent(false);

            await Program.TryEditMessage(LeaderId, msgId, "<b>Введіть унікальний ідентифікатор студента</b>",
                ParseMode.Html);

            EventHandler<MessageEventArgs> add = async (object sender, MessageEventArgs e) =>
            {
                if (step == 1)
                {
                    step++;
                    
                    var team = await DB.GetTeamByLeaderId(LeaderId);
                    
                    if (int.TryParse(e.Message.Text, out _))
                    {
                        if (DB.GetStudentByUniqueId(int.Parse(e.Message.Text)).Result)
                        {
                            await DB.AddTeamToStudent(int.Parse(e.Message.Text), team);
                            await Program.bot.SendTextMessageAsync(e.Message.From.Id,
                                "<b>Ви успішно добавили нового учасника команди " +
                                $"з унікальним ідентифікатором <em>{e.Message.Text}</em></b>", ParseMode.Html,
                                replyMarkup: Keyboards.DeleteThisMessage());
                        }
                        else
                        {
                            await Program.bot.SendTextMessageAsync(e.Message.From.Id,
                                "<b>Я не зміг знайти студента з таким унікальним ідентифікатором!</b>", ParseMode.Html,
                                replyMarkup: Keyboards.DeleteThisMessage());
                        }
                        
                    }
                    else
                    {
                        await Program.bot.SendTextMessageAsync(e.Message.From.Id,
                            "<b>Я не зміг знайти студента з таким унікальним ідентифікатором!</b>", ParseMode.Html,
                            replyMarkup: Keyboards.DeleteThisMessage());
                    }

                    await Program.TryEditMessage(LeaderId, msgId, Text.Team(team), ParseMode.Html, 
                        Keyboards.TeamLeader());

                }
            };
            
            Program.bot.OnMessage += add;
            wait.WaitOne();
            Program.bot.OnMessage -= add;
        }
        
        /// <summary>
        /// Видаляє гравця з команди та забороняє йому приєднуватись до будь-якої іншої команди.
        /// </summary>
        /// <param name="LeaderId">Унікальний ідентифікатор капітана команди.</param>
        /// <param name="msgId">Унікальний ідентифікатор головного повідомлення капітана команди.</param>
        private static async Task DeleteMember(int LeaderId, int msgId)
        {
            
        }
    }
}