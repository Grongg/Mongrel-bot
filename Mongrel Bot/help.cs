using Discord.Commands;
using System.Threading.Tasks;

namespace Mongrel_Bot
{
    public class HelpModule : ModuleBase
    {
        [Command("help"), Summary("Ask for help"), Alias("h")]

        public async Task GetHelp()
        {
                await ReplyAsync("Here comes the help :\n\n");
                await ReplyAsync("To have random image u can type:\nRandImage gelbooru/safebooru with/without any command for more precision\n\nOf course u can use the tag help to see this message again");
        }
    }
}
