using System;
using System.Linq;
using System.IO;
using System.Net;

namespace GameLogicTester
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        void Run()
        {
            var head = "http://localhost:9876/";

            var _urls = new[]
            {
                head + "config?gameId={0}?userId={1}",
                head + "init?gameId={0}?userId={1}",
                head + "play?gameId={0}?userId={1}?betcount=1?rate=0.00",
                head + "correct?gameId={0}?userId={1}?reelstopleft=0?reelstopright=0?reelstopcenter=0?oshijun=0",
            };

            var urls = from gameId in Enumerable.Range(2, 1)
                       from userId in Enumerable.Range(1, 10)
                       from url in _urls
                       select String.Format(url, gameId, userId);

            var msg = String.Join(
                Environment.NewLine,
                urls.Select(url=> url + Environment.NewLine + Url2Json(url))
            );

            Console.WriteLine(msg);
            Console.ReadLine();
        }

        string Url2Json(string url)
        {
            var json = null as string;

            try
            {
                var req = WebRequest.Create(url);
                var res = req.GetResponse();
                var st = res.GetResponseStream();
                var sr = new StreamReader(st);
                json = sr.ReadToEnd();
                sr.Close();
                st.Close();
            }
            catch( Exception ex)
            {
                return ex.ToString();
            }

            return json;
        }
    }
}
