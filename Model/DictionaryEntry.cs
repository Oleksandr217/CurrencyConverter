using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace currencyConverter.Model
{
    public class DictionaryEntry
    {
        private int id;
        private string code;
        private string description;
        
        public int Id
        {
            get => id;
            
        }
        public string Code
        {
            get => code;
        }
        public string Description
        {
            get => description;
        }
    }
}
