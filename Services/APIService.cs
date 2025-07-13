using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;   
using System.Windows;
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
            string encodedName = Uri.EscapeDataString(name.ToUpper());
            string url = $"https://open.er-api.com/v6/latest/{encodedName}";

            var response = await _httpClient.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"HTTP помилка: {(int)response.StatusCode} — {response.ReasonPhrase}");
                return null;
            }
            var data = JObject.Parse(json);
            
            double? rate = data["rates"]?["USD"]?.Value<double>();

            if (rate == null)
            {
                MessageBox.Show("Курс не знайдено. Перевір код валюти.");
                return null;
            }

            return new Currency
            {
                Name = name,
                RateToUSD = rate
            };
        }

    }
}
