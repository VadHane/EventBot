using System;
using System.Threading;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Bot.Handlers
{
    public class AddNew
    {
        public static async System.Threading.Tasks.Task Student(long ChatId)
        {
            int step = 1;
            var wait = new ManualResetEvent(false);

            var msg = await Program.bot.SendTextMessageAsync(ChatId,
                "<b>Привіт!</b> \nДавай ми тебе зареєструємо, щоб ти зміг розпочати гру! \n" +
                "Надішли, будь ласка, <b><em>як я можу до тебе звертатись</em></b>.", ParseMode.Html);
            
            EventHandler<MessageEventArgs> add = async (sender, e) =>
            {
                if (step == 1)
                {
                    step++;
                    
                    IStudent student = new Student(e.Message.Text, e.Message.From, msg.MessageId);
                    
                    await Program.bot.EditMessageTextAsync(ChatId, student.MainMessageId, 
                        $"<b>Чудово!</b> Я радий познайомитись з тобою, <b><em>{student.Name}</em>!</b>" +
                        $"\nОчікуй запрошення в команду від <b>свого <em>капітана</em> команди</b>!" +
                        $"\nТвій <b><em>унікальний порядковий номер</em></b> - <b>{student.UniqueId}</b>" +
                        $"\n\nЯкщо тебе обрали капітаном команди - натисни на кнопку '<b><em>Я капітан</em></b>' " +
                        $"та очікуй на код доступа від організатора.", ParseMode.Html, replyMarkup: Keyboards.ImLeader());
                    
                    Settings.Students.Add(student.TelegramId);
                    
                    // DB


                    Console.WriteLine($"[New User] New user was added into data base. [Name - {student.Name}, " +
                                      $"Telegram username - {student.TelegramUsername}, Unique id - {student.UniqueId}]");
                }
            };

            Program.bot.OnMessage += add;
            wait.WaitOne();
            Program.bot.OnMessage -= add;
        }
    }
}