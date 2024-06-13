using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class MonkeyService
    {
        private List<Monkey>? monkeyList;
        private HttpClient httpClient;

        public MonkeyService()
        {
            httpClient = new HttpClient();
        }
        public async Task<List<Monkey>> GetMonkeys()
        {
            if (monkeyList.Count is > 0) return monkeyList;

            var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
            if(response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            }

            return monkeyList;
        }
    }
}
