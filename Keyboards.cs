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

        public static ReplyKeyboardMarkup Back()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton("Назад"));
        }

        public static InlineKeyboardMarkup TeamLeader()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Добавити учасника команди", "Team:AddMember"), 
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Видалити учасника команди", "Team:DeleteMember"), 
                }
            });
        }
    }
}