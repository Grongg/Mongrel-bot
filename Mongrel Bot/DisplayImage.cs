using Discord.Commands;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mongrel_Bot
{
    public class DisplayImage : ModuleBase
    {
        [Command("image", RunMode = RunMode.Async), Summary("Display an image")]

        public async Task GetImage()
        {
            using (WebClient wc = new WebClient())
            {
                string pj = wc.DownloadString("http://safebooru.org/index.php?page=dapi&s=post&q=index&limit=1&pid=");
                string[] post = pj.Split('"');

                int max = Convert.ToInt32(post[5]);
                int rand = Program.rand.Next(max);
                string jp = wc.DownloadString("http://safebooru.org/index.php?page=dapi&s=post&q=index&limit=1&pid=" + rand);
                string[] str = jp.Split(new string[] { "file_url" }, StringSplitOptions.RemoveEmptyEntries);
                string[] str2 = str[1].Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                string str3 = "http:" + str2[1];

                await ReplyAsync("Here goes nothing");
                await ReplyAsync(str3);
            }
        }
    }
}