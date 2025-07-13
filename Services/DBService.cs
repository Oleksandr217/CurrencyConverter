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
        public ObservableCollection<DictionaryItem> GetAllDictionaryEntries()
        {
            try
            {
                using var context = new DataContext();
                var list = context.Dictionary.ToList();
                return new ObservableCollection<DictionaryItem>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка доступу до БД: {ex.Message}");
                return new ObservableCollection<DictionaryItem>();
            }
        }
    }
}
