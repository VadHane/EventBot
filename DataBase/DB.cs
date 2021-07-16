using System;
using System.Threading.Tasks;
using Bot.Interfaces;
using Bot.Models;
using Npgsql;
using Telegram.Bot.Types;

namespace Bot.DataBase
{
    public abstract class DB
    {

        /// <summary>
        /// Витягує з БД інформацію про студента по його телеграм-ідентифікатору.
        /// </summary>
        /// <param name="telegramId">Телеграм-ідентифікатор студента.</param>
        /// <returns>Силка на екземпляр класа студента.</returns>
        public static async Task<IStudent> GetStudentByTelegramId(long telegramId)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Settings.ConnectionString);
            await conn.OpenAsync();

            IStudent student = null;
            string script = $"SELECT * FROM students WHERE telegramid = {telegramId};";
            var command = new NpgsqlCommand(script, conn);
            var reader = command.ExecuteReaderAsync().Result;
            if (reader.Read())
            {
                User user = new User()
                {
                    FirstName = reader.GetString(6),
                    Username = reader.GetString(7),
                    Id = reader.GetInt32(1),
                };
                student = new Student(reader.GetString(5), user, reader.GetInt32(4))
                {
                    CanJoinToTeam = reader.GetBoolean(2),
                    UniqueId = reader.GetInt64(0),
                    TeamId = reader.GetInt64(3)
                };
            }

            await conn.CloseAsync();
            return student;
        }

        /// <summary>
        /// Добавляє в БД інформацію про нового студента.
        /// </summary>
        /// <param name="student">Силка на екземпляр студента.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task AddStudent(IStudent student)
        {
            var conn = new NpgsqlConnection(Settings.ConnectionString);
            await conn.OpenAsync();

            string script = $"INSERT INTO students " +
                            $"VALUES ({student.UniqueId}, {student.TelegramId}, {student.CanJoinToTeam}," +
                            $" {student.TeamId}, {student.MainMessageId}, '{student.Name}', '{student.TelegramName}'," +
                            $" '{student.TelegramUsername}');";

            var command = new NpgsqlCommand(script, conn);
            await command.ExecuteNonQueryAsync();
            
            Console.WriteLine($"[New User] New user was added into data base. [Name - {student.Name}, " +
                              $"Telegram username - {student.TelegramUsername}, Unique id - {student.UniqueId}]");
            
            await conn.CloseAsync();
        }
        
        /// <summary>
        /// Добавляє в БД інформацію про команду.
        /// </summary>
        /// <param name="team">Силка на екземпляр команди.</param>
        public static async System.Threading.Tasks.Task AddTeam(ITeam team)
        {
            var conn = new NpgsqlConnection(Settings.ConnectionString);
            await conn.OpenAsync();

            string script = $"INSERT INTO teams " +
                            $"VALUES ('{team.Name}', '{team.Group}', {team.UniqueId}, {team.Leader}, {team.Score}," +
                            $" {team.Task.UniqueId});";

            var command = new NpgsqlCommand(script, conn);
            await command.ExecuteNonQueryAsync();
            
            Console.WriteLine($"[New Team] New team was added into data base. [Name - {team.Name}, " +
                              $"Team group - {team.Group}, Unique id - {team.UniqueId}]");
            
            await conn.CloseAsync();
        }

        /// <summary>
        /// Змінює ідентифікатор головного повідомлення для студента.
        /// </summary>
        /// <param name="msgId">Новий ідентифікатор повідомлення.</param>
        /// <param name="studentId">Ідентифікатор студента.</param>
        public static async System.Threading.Tasks.Task EditMainMessageId(int msgId, int studentId)
        {
            var conn = new NpgsqlConnection(Settings.ConnectionString);
            await conn.OpenAsync();

            string script = $"UPDATE students SET telegramid = {studentId} WHERE mainmessageid = {msgId};";
            var command = new NpgsqlCommand(script, conn);
            await command.ExecuteNonQueryAsync();

            await conn.CloseAsync();
        }
    }
}