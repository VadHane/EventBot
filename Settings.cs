using System;
using System.Collections.Generic;

namespace Bot
{
    public class Settings
    {
        
        public static bool DEBUG = true;
        public static string TOKEN = DEBUG ? "1808452087:AAGRxFDe4r5mXsBemwy089p9NWmyz5TecSs" : "";

        public static int PasswordForLeaders = new Random().Next(1000, 9999);

        public static List<long> Students = new List<long>();
        
        
        
        // DataBase settings
        private const string Host = "ec2-54-74-77-126.eu-west-1.compute.amazonaws.com";
        private const int Port = 5432;
        private const string Username = "hikrpbttlttcwq";
        private const string Password = "b29d4a14c532b6a3a51930e0cfcd6df1160314534c54663a34ecb15a9a5c96b7";
        private const string Datebase = "d5jn967h0si61h";
        private const bool Pooling = true;
        private const string SSLMode = "Require";
        private const bool TrueServerCertificate = true;

        public static readonly string ConnectionString = $"host={Host};" +
                                                         $"username={Username};" +
                                                         $"password={Password};" +
                                                         $"database={Datebase};" +
                                                         $"port={Port};" +
                                                         $"Sslmode=Require;" +
                                                         $"Trust Server Certificate=true";
        
    }
}