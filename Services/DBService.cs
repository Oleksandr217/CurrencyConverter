using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using currencyConverter.Core;
using currencyConverter.Model;

namespace currencyConverter.Services
{
    public class DBService
    {
        
        public List<DictionaryEntry> GetAllDictionaryEntries()
        {
            try
            {
                using (var context = new DataContext())
                {
                    return context.Dictionary.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка доступу до БД: {ex.Message}");
                return new List<DictionaryEntry>();
            }

        }
    }
}
