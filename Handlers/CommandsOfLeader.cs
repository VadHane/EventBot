using System;
using System.Threading;
using Bot.DataBase;
using Bot.Interfaces;
using Bot.Models;
using Bot.Static;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Task = System.Threading.Tasks.Task;

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
                    await AddMember(msg.Chat.Id, msg.MessageId);
                    break;
                case "StartDeleteMember":
                    await Program.TryEditMessage(msg.Chat.Id, msg.MessageId,
                        "Виберіть студента, якого хочете <b>видалити</b>:", ParseMode.Html, 
                        Keyboards.TeamMembers(
                            DB.GetStudentsByTeamId(DB.GetTeamByLeaderId(msg.Chat.Id).Result.UniqueId).Result
                            ));
                    break;
                case "DeleteMember":
                    IVoting voting = new Voting(msg.Chat.Id);
                    await voting.StartVote(int.Parse(commands[2]));
                    await Program.TryEditMessage(msg.Chat.Id, msg.MessageId, "Голосування запущено!");
                    Thread.Sleep(30 * 1000);
                    await Program.TryEditMessage(msg.Chat.Id, msg.MessageId,
                        Text.Team(DB.GetTeamByLeaderId(msg.Chat.Id).Result), ParseMode.Html, 
                        Keyboards.TeamLeader());
                    break;
                case "ToMainMenu":
                    await Program.TryEditMessage(msg.Chat.Id, msg.MessageId,
                        Text.Team(DB.GetTeamByLeaderId(msg.Chat.Id).Result),
                        ParseMode.Html, Keyboards.TeamLeader());
                    break;
            }
        }

        /// <summary>
        /// Добавляє нового гравця в команду.
        /// </summary>
        /// <param name="LeaderId">Унікальний ідентифікатор капітана команди.</param>
        /// <param name="msgId">Унікальний ідентифікатор головного повідомлення капітана команди.</param>
        private static async Task AddMember(long LeaderId, int msgId)
        {
            int step = 1;
            var wait = new ManualResetEvent(false);

            await Program.TryEditMessage(LeaderId, msgId, "<b>Введіть унікальний ідентифікатор студента</b>",
                ParseMode.Html);

            EventHandler<MessageEventArgs> func = async (object sender, MessageEventArgs e) =>
            {
                if (LeaderId != e.Message.Chat.Id) return;
                
                if (step == 1)
                {
                    step++;
                    
                    var team = await DB.GetTeamByLeaderId((int)LeaderId);
                    
                    await Program.TryEditMessage(LeaderId, msgId, Text.Team(team), ParseMode.Html, 
                        Keyboards.TeamLeader());
                    
                    
                    if (int.TryParse(e.Message.Text, out _))
                    {
                        var student = await DB.GetStudentByUniqueId(int.Parse(e.Message.Text));
                        
                        if (student != null)
                        {
                            if (!student.CanJoinToTeam)
                            {
                                await Program.bot.SendTextMessageAsync(e.Message.From.Id,
                                    $"Цей студент <em>не може приєднуднатись до команди!</em></b>", ParseMode.Html,
                                    replyMarkup: Keyboards.DeleteThisMessage());
                            } 
                            else if (student.TeamId != -1)
                            {
                                await Program.bot.SendTextMessageAsync(e.Message.From.Id,
                                    $"Цей студент <em>уже знаходиться в команді!</em></b>", ParseMode.Html,
                                    replyMarkup: Keyboards.DeleteThisMessage());
                            }
                            else
                            {
                                await DB.AddTeamToStudent(int.Parse(e.Message.Text), team);
                                await Program.bot.SendTextMessageAsync(e.Message.From.Id,
                                    $"<b>Ви успішно добавили нового учасника команди - {student.Name}" +
                                    $"з унікальним ідентифікатором <em>{e.Message.Text}</em></b>", ParseMode.Html,
                                    replyMarkup: Keyboards.DeleteThisMessage());
                                await Program.TryEditMessage(student.TelegramId, student.MainMessageId, Text.Team(team),
                                    ParseMode.Html);
                                await Program.bot.SendTextMessageAsync(student.TelegramId,
                                    $"Ви були добавлені в команду {team.Name}!",
                                    replyMarkup: Keyboards.DeleteThisMessage());
                            }
                            
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
                }
            };
            
            Program.bot.OnMessage += func;
            wait.WaitOne();
            Program.bot.OnMessage -= func;
        }
    }
}