using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    public class Keyboards
    {
        public static InlineKeyboardMarkup ImLeader()
        {
            return new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Я капітан", "ImLeader"),
            });
        }
    }
}