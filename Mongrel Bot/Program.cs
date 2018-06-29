using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace Mongrel_Bot
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public readonly DiscordSocketClient client;
        private readonly IServiceCollection map = new ServiceCollection();
        private readonly CommandService commands = new CommandService();

        private Program()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,

                // If you or another service needs to do anything with messages
                // (eg. checking Reactions, checking the content of edited/deleted messages),
                // you must set the MessageCacheSize. You may adjust the number as needed.
                //MessageCacheSize = 50,
            });
            client.Log += Log;
            commands.Log += Log;
        }

        private async Task MainAsync()
        {
            client.MessageReceived += HandleCommandAsync;

            await commands.AddModuleAsync<CommunicationModule>(); // Pour créer un module
            await commands.AddModuleAsync<DisplayImage>();
            // Login and connect.
            await client.LoginAsync(TokenType.Bot, File.ReadAllText("token.txt"));
            await client.StartAsync();

            // Wait infinitely so your bot actually stays connected.
            await Task.Delay(-1);
        }

        public async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            if (msg.HasMentionPrefix(client.CurrentUser, ref pos))
            {
                // Create a Command Context.
                var context = new SocketCommandContext(client, msg);
                // Execute the command. (result does not indicate a return value,
                // rather an object stating if the command executed succesfully).
                var result = await commands.ExecuteAsync(context, pos, null);
            }
        }

        private Task Log(LogMessage msg)
        {
            var cc = Console.ForegroundColor;
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            //Console.WriteLine($"{DateTime.Now,-19} [{msg.Severity,8}] {msg.Source}: {msg.Message}");
            Console.WriteLine(msg);
            Console.ForegroundColor = cc;

            return Task.CompletedTask;
        }
    }
}