using System;
using System.Threading;
using Bot.Interfaces;
using Bot.Models;
using Bot.Static;
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
                    
                    await Program.TryEditMessage(ChatId, student.MainMessageId, 
                        Text.StartMessageFromAllStudents(student), ParseMode.Html, replyMarkup: Keyboards.ImLeader());
                    
                    Settings.Students.Add(student.TelegramId);
                    
                    // DB
                    await Program.DB.AddStudent(student);
                }
            };

            Program.bot.OnMessage += add;
            wait.WaitOne();
            Program.bot.OnMessage -= add;
        }

        public static async System.Threading.Tasks.Task Team(long ChatId, int msgId)
        {
            int step = 1;
            var wait = new ManualResetEvent(false);
            string name = null;

            EventHandler<MessageEventArgs> add = async (object sender, MessageEventArgs e) =>
            {
                if (step == 1)
                {
                    step++;
                    int code;
                    int.TryParse(e.Message.Text, out code);
                    
                    if (code == Settings.PasswordForLeaders)
                    {
                        await Program.TryEditMessage(ChatId, msgId, 
                            "<b>Вірно!</b> \n\n<b>Введіть <em>назву вашої команди</em>!</b>", ParseMode.Html);
                    }
                    else
                    {
                        var student = Program.DB.GetStudentByTelegramId(ChatId).Result;
                        await Program.TryEditMessage(ChatId, msgId, 
                            Text.StartMessageFromAllStudents(student), ParseMode.Html, Keyboards.ImLeader());
                        step = 0;
                    }
                }
                else if (step == 2)
                {
                    step++;
                    name = e.Message.Text;

                    await Program.TryEditMessage(ChatId, msgId, 
                        "Чудово! " +
                        "\n\n<b> Введіть номер вашої групи </b>" +
                        "\n<b><em>Комп'ютерні науки - 111 група \n" +
                        "Прикладна математика - 102 група \n" +
                        "Середня освіта (математика) - 106 група \n" +
                        "Середня освіта (інформатика) - група \n" +
                        "Системний аналіз - група </em></b>", ParseMode.Html);
                } 
                else if (step == 3)
                {
                    step++;
                    var team = new Team(name, e.Message.Text, e.Message.From.Id);

                    await Program.DB.AddTeam(team);

                    await Program.TryEditMessage(ChatId, msgId, Text.Team(team), ParseMode.Html,
                        Keyboards.TeamLeader());
                }
            };

            await Program.TryEditMessage(ChatId, msgId,
                "<b> Введіть код доступу для створення команди! </b> " +
                "\n<b><em>Його можно отримати у організаторів! </em></b> " +
                "\n<b> Введіть будь-яке число для повернення назад </b>", ParseMode.Html);
            
            
            
            Program.bot.OnMessage += add;
            wait.WaitOne();
            Program.bot.OnMessage -= add;
        }
    }
}