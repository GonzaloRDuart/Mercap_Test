
using System.Collections.Immutable;

namespace Mercap_Test.Domain.PricesByLocation
{
    public static class PricesManager
    {
        public static readonly ImmutableDictionary<string, double> PriceByCountry =
            ImmutableDictionary<string, double>.Empty
                .Add("Bolivia", 0.9)
                .Add("Spain", 2.2)
                .Add("Germany", 2.7)
            ;

        public static readonly ImmutableDictionary<string, double> PriceByCity =
            ImmutableDictionary<string, double>.Empty
                .Add("Berazategui", 0.1)
                .Add("General Belgrano", 0.5)
                .Add("Tandil", 0.3)
            ;
    }
}