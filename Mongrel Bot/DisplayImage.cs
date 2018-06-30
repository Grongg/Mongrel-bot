using Discord.Commands;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mongrel_Bot
{
    public class DisplayImage : ModuleBase
    {
        private string Addrand(params string[] ag)
        {
            using (WebClient wc = new WebClient())
            {
                string pj = wc.DownloadString("http://safebooru.org/index.php?page=dapi&s=post&q=index&limit=1&tags=" + String.Join("+", ag));
                string[] post = pj.Split('"');
                int max = Convert.ToInt32(post[5]);
                int rand = Program.rand.Next(max);
                string jp = wc.DownloadString("http://safebooru.org/index.php?page=dapi&s=post&q=index&limit=1&pid=" + rand + "&tags=" + String.Join("+", ag));
                return (jp);
            }
        }

        [Command("RandImage", RunMode = RunMode.Async), Summary("Display an image")]
        public async Task GetImage(params string[] ag)
        {
                string FinalString = Addrand(ag);
                string[] str = FinalString.Split(new string[] { "file_url" }, StringSplitOptions.RemoveEmptyEntries);
                string[] str2 = str[1].Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                string str3 = "https:" + str2[1];

                await ReplyAsync("Here goes nothing");
                await ReplyAsync(str3);
        }
    }
}