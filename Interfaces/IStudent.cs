using Telegram.Bot.Types;

namespace Bot.Interfaces
{
    public interface IStudent
    {
        // Ім'я студента, яке він вкаже при реєстрації
        public string Name { get; set; }
        
        // Ім'я користувача із телеграм акаунта
        public string TelegramName { get; set; }
        
        // Унікальне ім'я користувача в телеграмі
        public string TelegramUsername { get; set; }
        
        // Унікальний ідентифікатор користувача в телеграмі
        public long TelegramId { get; set; }
        
        // Унікальний порядковий номер студента в базі даних
        public long UniqueId { get; set; }
        
        // Змінна, що вказує, чи має право цей студент приєднуватись до команд
        public bool CanJoinToTeam { get; set; }
        
        // Унікальний порядковий номер команди цього студента
        public long TeamId { get; set; }
        
        // Унікальний id повідомлення, яке буде редагуватись в процесі діалогу з користувачем
        public int MainMessageId { get; set; }
    }
}