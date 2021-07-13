using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Bot
{
    public class Handler
    {
        public static async void OnMessage(object sender, MessageEventArgs e)
        {
            
        }
        
        public static async void OnCallback(object sender, CallbackQueryEventArgs e)
        {
            
        }
        
        public static async void OnUpdate(object sender, UpdateEventArgs e)
        {
            

            if (e.Update.Message.Type == MessageType.Location)
            {
                
            }
                // 48.2906952  / 48.2906647
                // 25.9361649 / 25.936182
        }
    }
}