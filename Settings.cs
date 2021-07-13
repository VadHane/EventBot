using System.Collections.Generic;

namespace Bot
{
    public class Settings
    {
        
        public static bool DEBUG = true;
        public static string TOKEN = DEBUG ? "1808452087:AAGRxFDe4r5mXsBemwy089p9NWmyz5TecSs" : "";

        public static List<long> Students = new List<long>();
    }
}