using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class Data {

        public string IP { get; set; }
        public string Port { get; set; }


    }

    public class SendSelf
    {

        public async Task Main (string [] args)
        {

            var InfoNode = new Data();

            var json = JsonConvert.SerializeObject(InfoNode);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://httpbin.org/post";
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);


        }

    }
}
