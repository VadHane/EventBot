using System;
using System.Linq;
using System.Threading;
using Bot.Handlers;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;


namespace Bot
{
    public class Handler
    {
        public static async void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Chat.Type == ChatType.Private) // Private chat
            {
                if (e.Message.Type == MessageType.Location)
                {
                    
                }


                switch (e.Message.Text)
                {
                    case "/start":
                        if (Settings.Students.FirstOrDefault(user => user == e.Message.From.Id) != 0)
                        {
                            await Program.TryDeleteMessageAsync(e.Message);
                            break;
                        }

                        await Program.TryDeleteMessageAsync(e.Message);

                        await AddNew.Student(e.Message.Chat.Id);
                        break;
                    default:
                        await Program.TryDeleteMessageAsync(e.Message);
                        break;
                }
                
            }
            else if (e.Message.Chat.Type == ChatType.Channel) // Channel
            {
                try
                {
                    await Program.bot.LeaveChatAsync(e.Message.Chat.Id);
                }
                catch
                {
                    Console.WriteLine($"[EXEPTION] I cant go aut from channel! " +
                                      $"[Group name - {e.Message.Chat.FirstName} {e.Message.Chat.LastName}];");
                }
            }
            else // Group and Supergroup
            {
                await Program.bot.SendTextMessageAsync(
                    e.Message.Chat.Id, "<b>Привіт!</b> <br>Я <em>не працюю</em> в <b>групах</b>, вибачте!", ParseMode.Html);
                try
                {
                    await Program.bot.LeaveChatAsync(e.Message.Chat.Id);
                }
                catch
                {
                    Console.WriteLine($"[EXEPTION] I cant go aut from group! " +
                                      $"[Group name - {e.Message.Chat.FirstName} {e.Message.Chat.LastName}];");
                }
            }
        }
        
        public static async void OnCallback(object sender, CallbackQueryEventArgs e)
        {
            
        }
        
        public static async void OnUpdate(object sender, UpdateEventArgs e)
        {
            

            
                // 48.2906952  / 48.2906647
                // 25.9361649 / 25.936182
        }
    }
}