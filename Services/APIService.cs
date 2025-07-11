using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using currencyConverter.Model;
using Newtonsoft.Json.Linq;

namespace currencyConverter.Services
{
    public interface IAPIService
    {
        Task<Currency> GetCurrencyAsync(string name);
    }
    public class APIService : IAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<Currency> GetCurrencyAsync(string name)
        {
            string url = $"https://api.exchangerate.host/latest?base={name}&symbols=USD";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Помилка: {(int)response.StatusCode} {response.ReasonPhrase}");
            }

            string json  = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(json);

            var result = data["results"]?.FirstOrDefault();
            if (result == null)
            {
                throw new Exception("Валюта не знайдена");
            }

            double rate = data["rates"]?["USD"]?.Value<double>() ?? throw new Exception("Не вдалося отримати курс.");

            return new Currency
            {
                Name = name,
                RateToUSD = rate
            };
        }
    }
}
