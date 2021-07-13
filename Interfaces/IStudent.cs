using Telegram.Bot.Types;

namespace Bot.Interfaces
{
    public interface IStudent
    {
        // Ім'я студента, яке він вкаже при реєстрації
        public string Name { get; set; }
        
        // Ім'я користувача із телеграм акаунта
        public string Telegram_name { get; set; }
        
        // Унікальне ім'я користувача в телеграмі
        public string Telegram_username { get; set; }
        
        // Унікальний ідентифікатор користувача в телеграмі
        public long Telegram_id { get; set; }
        
        // Унікальний порядковий номер студента в базі даних
        public long Unique_id { get; set; }
        
        // Змінна, що вказує, чи має право цей студент приєднуватись до команд
        public bool CanJoinToTeam { get; set; }
        
        // Унікальний порядковий номер команди цього студента
        public long TeamId { get; set; }
    }
}