using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using currencyConverter.Model;

namespace currencyConverter.Core
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Dictionary.DB");
        }
        public DbSet<DictionaryEntry> Dictionary { get; set; }
    }
}
