using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
    public class CurrencyConvertor
    {
        public string Base { get; set; }
        public Rates Rates { get; set; }
        public string Date { get; set; }
    }

    public class Rates
    {
        public double AUD { get; set; }
    }

    public class FreeCurrencyConvertor
    {
        public double USD_AUD { get; set; }
    }
}
