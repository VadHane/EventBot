using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot
{
    class Program
    {
        
        public static ITelegramBotClient bot;
        static void Main(string[] args)
        {
            bot = new TelegramBotClient(Settings.TOKEN);
            
            // DataBase

            Console.WriteLine($"[LOG] Bot is running");


            bot.OnMessage += Handler.OnMessage;
            bot.OnCallbackQuery += Handler.OnCallback;
            bot.OnUpdate += Handler.OnUpdate;
            bot.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        public static async Task TryDeleteMessageAsync(Message msg)
        {
            try
            {
                await bot.DeleteMessageAsync(msg.Chat.Id, msg.MessageId);
            }
            catch
            {
                Console.WriteLine($"[EXEPTION] I cant delete message. " +
                                  $"[Chat with {msg.Chat.FirstName} {msg.Chat.LastName} - {msg.Chat.Username};" +
                                  $"Message: '{msg.Text}'; Message type - {msg.Type};]");
            }
        }
    }
}