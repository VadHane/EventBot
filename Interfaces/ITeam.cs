namespace Bot.Interfaces
{
    public interface ITeam
    {
        /// <summary>
        /// Назва команди, що вказується при реєстрації групи
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Номер групи, що представляє цю команду
        /// </summary>
        public string Group { get; set; }
        
        /// <summary>
        /// Унікальний ідентифікатор команди
        /// </summary>
        public long UniqueId { get; set; }
        
        // Members and leader
        
        /// <summary>
        /// Телеграм ідентифікатор капітана цієї команди
        /// </summary>
        public long Leader { get; set; }
        
        
        // Score
        
        /// <summary>
        /// Кількість балів, що заробила ця команда
        /// </summary>
        public long Score { get; }
        
        // Tasks 
        
        /// <summary>
        /// Поточне завдання групи
        /// </summary>
        public ITask Task { get; set; }
    }
}