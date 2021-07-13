using System;
using System.Threading;
using Telegram.Bot;

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
    }
}