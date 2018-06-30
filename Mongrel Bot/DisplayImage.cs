using Discord.Commands;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mongrel_Bot
{
    public class DisplayImage : ModuleBase
    {
        private string Addrand(string link, params string[] ag)
        {
            using (WebClient wc = new WebClient())
            {
                string pj = wc.DownloadString(link + "tags=" + String.Join("+", ag));
                string[] post = pj.Split('"');
                int max = Convert.ToInt32(post[5]);
                int rand = Program.rand.Next(max);
                string jp = wc.DownloadString(link + "pid=" + rand + "&tags=" + String.Join("+", ag));
                return (jp);
            }
        }

        [Command("RandImage", RunMode = RunMode.Async), Summary("Display an image")]
        public async Task GetImage(params string[] ag)
        {
            if (ag.Length > 0)
            {
                string link;
                string prefix;

                if (ag[0] == "safebooru")
                {
                    prefix = "https:";
                    link = "http://safebooru.org/index.php?page=dapi&s=post&q=index&limit=1&";
                }
                else if (ag[0] == "gelbooru")
                {
                    prefix = "";
                    link = "https://gelbooru.com/index.php?page=dapi&s=post&q=index&limit=1&";
                }
                else
                {
                    await ReplyAsync("tch, can't ya read da help :\nTHAT TAG DOESN'T EXIST, MONGREL!!\n");                    
                        await ReplyAsync("http://safebooru.org//images/2456/c2f12a9827acb699e35badd3e6de7a5c77de6015.png?2557389");
                    return;
                }
                ag = ag.ToList().Skip(1).ToArray();
                string FinalString = Addrand(link, ag);
                string[] str = FinalString.Split(new string[] { "file_url" }, StringSplitOptions.RemoveEmptyEntries);
                string[] str2 = str[1].Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                string str3 = prefix + str2[1];

                await ReplyAsync("Here goes nothing");
                await ReplyAsync(str3);
            }
            else
            {
                int death = Program.rand.Next(2);
                await ReplyAsync("pff, Can't ya even read:\nNEED A TAG HERE MONGREL!!\n...Ever considered suicide ?\nYou:");
                if (death == 0)
                    await ReplyAsync("http://safebooru.org//images/2412/0893f0faaceec7b333651b3b0767c0ca02a24871.png?2512533");
                else if (death == 1)
                    await ReplyAsync("https://cdn.discordapp.com/attachments/462008136887304192/462754719916621825/Dfgmv0j.png");
                return;
            }
        }
    }
}