using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using currencyConverter.Core;
using currencyConverter.Model;
using currencyConverter.Services;

namespace currencyConverter.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand ConvertCommand { get; set; }
        public IAPIService _IAPIService { get; set; }
        public DBService _DBService { get; set; }
        public ObservableCollection<DictionaryItem> dictionary { get; set; }
        private Currency firstSelectedCurrency;
        private Currency secondSelectedCurrency;
        private double? firstPrice;
        private double? answerPrice;
        public MainViewModel()
        {
            _IAPIService = new APIService();
            _DBService = new DBService();
            dictionary = _DBService.GetAllDictionaryEntries();
            firstSelectedCurrency = new Currency();
            secondSelectedCurrency = new Currency();
            answerPrice = 0;
            ConvertCommand = new RelayCommand(convert, canConvert);
        }
        private async void convert(object parameter)
        {
            FirstSelectedCurrency = await _IAPIService.GetCurrencyAsync(FirstSelectedCurrency.Name);
            
            SecondSelectedCurrency = await _IAPIService.GetCurrencyAsync(SecondSelectedCurrency.Name);

            if (firstSelectedCurrency == null || secondSelectedCurrency == null) return;

            AnswerPrice = (FirstSelectedCurrency.RateToUSD / SecondSelectedCurrency.RateToUSD) * FirstPrice;
        }
        private bool canConvert(object parameter)
        {
            return !string.IsNullOrWhiteSpace(FirstSelectedCurrency?.Name) &&
                   !string.IsNullOrWhiteSpace(SecondSelectedCurrency?.Name);
        }
        
        public Currency FirstSelectedCurrency
        {
            get => firstSelectedCurrency;
            set
            {
                firstSelectedCurrency = value;
                OnPropertyChanged();
            }
        }
        public Currency SecondSelectedCurrency
        {
            get => secondSelectedCurrency;
            set
            {
                secondSelectedCurrency = value;
                OnPropertyChanged();
            }
        }
        public double? FirstPrice
        {
            get => firstPrice;
            set
            {
                firstPrice = value;
                OnPropertyChanged();
            }
        }
        
        public double? AnswerPrice
        {
            get => answerPrice;
            set
            {
                answerPrice = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
