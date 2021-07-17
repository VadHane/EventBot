using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    public abstract class Keyboards
    {
        /// <summary>
        /// Генерує клавішу для реєстрації капітана команди.
        /// </summary>
        /// <returns>Повертає силку на екземпляр цієї клавіши.</returns>
        public static InlineKeyboardMarkup ImLeader()
        {
            return new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Я капітан", "ImLeader"),
            });
        }

        /// <summary>
        /// Генерує клавішу для надсилання в чат слова "Назад".
        /// </summary>
        /// <returns>Повертає силку на екземпляр цієї клавіши.</returns>
        public static ReplyKeyboardMarkup Back()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton("Назад"));
        }

        /// <summary>
        /// Генерує клавіатуру управління командою для капітанів.
        /// </summary>
        /// <returns>Повертає силку на екземпляр цієї клавіатури.</returns>
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
        
        /// <summary>
        /// Генерує клавішу для видалення повідомлення до якого вона буде прив'язана.
        /// </summary>
        /// <returns>Силку на екземпляр клавіши.</returns>
        public static InlineKeyboardMarkup DeleteThisMessage()
        {
            return new InlineKeyboardMarkup(new []
            {
                InlineKeyboardButton.WithCallbackData("Зрозуміло!", "DeleteThisMessage"), 
            });
        }

        /// <summary>
        /// Генерує клавіатуру для опитування студентів.
        /// </summary>
        /// <param name="votingId">Унікальний ідентифікатор опитування.</param>
        /// <returns>Силку на екземпляр кравіатури.</returns>
        public static InlineKeyboardMarkup Voting(int votingId)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Згідний!", $"Voting:Yes:{votingId}"), 
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Не згідний!", $"Voting:No:{votingId}"), 
                }
            });
        }
    }
}