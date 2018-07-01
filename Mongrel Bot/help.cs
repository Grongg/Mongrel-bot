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
                await ReplyAsync("To have random image u can type:\nRandImage ```fix\n gelbooru/safebooru``` with tags for more precision\n\nFor exemple:\n```fix\n@mongrelBot RandImage\n@mongrelBot Randimage safebooru\n@mongrelBot Randimage gelbooru(nsfw only)\n```Of course u can use the tag help to see this message again");
        }
    }
}
