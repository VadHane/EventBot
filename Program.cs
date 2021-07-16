using System;
using System.Threading;
using Bot.DataBase;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Task = System.Threading.Tasks.Task;

namespace Bot
{
    class Program
    {
        
        public static ITelegramBotClient bot;
        static void Main(string[] args)
        {
            bot = new TelegramBotClient(Settings.TOKEN);
            
            // DataBase
            
            

            Console.WriteLine($"[LOG] Bot is running. Password for leader - {Settings.PasswordForLeaders}");


            bot.OnMessage += Handler.OnMessage;
            bot.OnCallbackQuery += Handler.OnCallback;
            bot.OnUpdate += Handler.OnUpdate;
            bot.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        /// <summary>
        /// Функція для безпечного видалення повідемлень при діалозі з користувачем.
        /// </summary>
        /// <param name="msg">Силка на екземпляр повідомлення, яке потрібно видалити.</param>
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

        /// <summary>
        /// Функція для безпечного редагування повідомлення при діалозі з користувачем.
        /// </summary>
        /// <param name="chatId">Ідентифікатор чату в якому відбувається діалог.</param>
        /// <param name="messageId">Ідентифікатор повідомлення, яке потрібно відредагувати.</param>
        /// <param name="message">Новий текст цього повідомлення.</param>
        /// <param name="parseMode">Режим перетворення тексту повідомлення.</param>
        /// <param name="replyMarkup">Силка на екземпляр клавіатури.</param>
        public static async Task TryEditMessage(ChatId chatId, int messageId, string message,
            ParseMode parseMode = ParseMode.Default, InlineKeyboardMarkup replyMarkup = null)
        {
            try
            {
                await bot.EditMessageTextAsync(chatId, messageId, message, parseMode, replyMarkup: replyMarkup);
            }
            catch
            {
                Console.WriteLine($"[EXEPTION] I cant edit message. [Message: '{message}';]");
                var msg = await bot.SendTextMessageAsync(chatId, message, parseMode, replyMarkup: replyMarkup);
                
                // Запит в БД на зміну id основного повідемлення для цього користувача
                await DB.EditMainMessageId(msg.MessageId, msg.From.Id);
            }
        }
    }
}