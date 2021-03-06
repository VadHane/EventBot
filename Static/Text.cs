using Bot.Interfaces;
using Bot.Models;

namespace Bot.Static
{
    public abstract class Text
    {
        /// <summary>
        /// Генерує стрічку, що використовується для діологу з користувачем після його реєстрації
        /// та в момент очікування запрошення в команду
        /// </summary>
        /// <param name="student">Силка на екземпляр студента з яким відбувається діалог.</param>
        /// <returns>Повертає стрічку з інформацією про цього студента.</returns>
        public static string StartMessageFromAllStudents(IStudent student)
        {
            return $"<b>Чудово!</b> Я радий познайомитись з тобою, <b><em>{student.Name}</em>!</b>" +
                   $"\nОчікуй запрошення в команду від <b>свого <em>капітана</em> команди</b>!" +
                   $"\nТвій <b><em>унікальний порядковий номер</em></b> - <b>{student.UniqueId}</b>" +
                   $"\n\nЯкщо тебе обрали капітаном команди - натисни на кнопку '<b><em>Я капітан</em></b>' " +
                   $"та очікуй на код доступа від організатора.";
        }

        /// <summary>
        /// Генерує стрічку, що використовується в діалозі з капітанами команд після реєстрації команди.
        /// </summary>
        /// <param name="team">Силка на екземпляр команди, капітаном якої є цей користувач.</param>
        /// <returns>Повертає стрічку з інформацією про цю команду.</returns>
        public static string Team(ITeam team)
        {
            return $"<b>Команда: </b>\n" +
                   $"<b>Назва - <em>{team.Name}</em> \n" +
                   $"Група - <em>{team.Group}</em> \n" +
                   $"Кількість балів - <em>{team.Score}</em>\n\n\n" +
                   $"Виберіть дію: </b>";
        }
        
    }
}