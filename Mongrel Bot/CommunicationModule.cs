using Discord.Commands;
using System.Threading.Tasks;

namespace Mongrel_Bot
{

    public class CommunicationModule : ModuleBase
    {
        [Command("Hi"), Summary("Answer with hi"), Alias("Hello")]
        public async Task SayHi()
        {
            await ReplyAsync("Hi");
        }
    }
}