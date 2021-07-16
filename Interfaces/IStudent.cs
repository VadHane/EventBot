using Telegram.Bot.Types;

namespace Bot.Interfaces
{
    public interface IStudent
    {
        /// <summary>
        /// Ім'я студента, яке він вкаже при реєстрації
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Ім'я користувача із телеграм акаунта
        /// </summary>
        public string TelegramName { get; set; }
        
        /// <summary>
        /// Унікальне ім'я користувача в телеграмі
        /// </summary>
        public string TelegramUsername { get; set; }
        
        /// <summary>
        /// Унікальний ідентифікатор користувача в телеграмі
        /// </summary>
        public long TelegramId { get; set; }
        
        /// <summary>
        /// Унікальний порядковий номер студента в базі даних
        /// </summary>
        public long UniqueId { get; set; }
        
        /// <summary>
        /// Змінна, що вказує, чи має право цей студент приєднуватись до команд
        /// </summary>
        public bool CanJoinToTeam { get; set; }
        
        /// <summary>
        /// Унікальний порядковий номер команди цього студента
        /// </summary>
        public long TeamId { get; set; }
        
        /// <summary>
        /// Унікальний id повідомлення, яке буде редагуватись в процесі діалогу з користувачем
        /// </summary>
        public int MainMessageId { get; set; }
    }
}