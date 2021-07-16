using Bot.Interfaces;

namespace Bot.Static
{
    public class Text
    {
        public static string StartMessageFromAllStudents(IStudent student)
        {
            return $"<b>Чудово!</b> Я радий познайомитись з тобою, <b><em>{student.Name}</em>!</b>" +
                   $"\nОчікуй запрошення в команду від <b>свого <em>капітана</em> команди</b>!" +
                   $"\nТвій <b><em>унікальний порядковий номер</em></b> - <b>{student.UniqueId}</b>" +
                   $"\n\nЯкщо тебе обрали капітаном команди - натисни на кнопку '<b><em>Я капітан</em></b>' " +
                   $"та очікуй на код доступа від організатора.";
        }

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