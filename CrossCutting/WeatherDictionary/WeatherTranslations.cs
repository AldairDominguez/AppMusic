using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.WeatherDictionary
{
    public static class WeatherTranslations
    {
        public static readonly Dictionary<string, string> Translations = new Dictionary<string, string>
        {
            { "clear sky", "Cielo despejado" },
            { "few clouds", "Pocas nubes" },
            { "scattered clouds", "Nubes dispersas" },
            { "broken clouds", "Nubes rotas" },
            { "overcast clouds", "Nubes cubiertas" },
            { "shower rain", "Lluvia de ducha" },
            { "rain", "Lluvia" },
            { "thunderstorm", "Tormenta" },
            { "snow", "Nieve" },
            { "mist", "Niebla" } 
        };
    }
}
