﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var urls = new[]
            {
                head + "config?gameId=1?userId=1",
                head + "init?gameId=1?userId=1",
                head + "play?gameId=1?userId=1?betcount=1?rate=0.00",
                head + "correct?gameId=1?userId=1?reelstopleft=0?reelstopright=0?reelstopcenter=0?oshijun=0",
            };

            var msg = String.Join(
                Environment.NewLine,
                urls.Select(url=> url + Environment.NewLine + Url2Json(url))
            );

            Console.WriteLine(msg);
        }

        string Url2Json(string url)
        {
            var req = WebRequest.Create(url);
            var res = req.GetResponse();
            var st = res.GetResponseStream();
            var sr = new StreamReader(st);
            var json = sr.ReadToEnd();
            sr.Close();
            st.Close();

            return json;
        }
    }
}
