using Mercap_Test.Domain.Excepctions;
using System.Collections.Immutable;

namespace Mercap_Test.Domain.Call
{
    public abstract class CallByLocation : ICall
    {
        private readonly ImmutableDictionary<string, double> PriceRegistry;

        private readonly string Location;

        protected CallByLocation(string _Location, ImmutableDictionary<string, double> _PriceRegistry)
        {
            if (_Location is null)
                throw new ArgumentException("The location can't be empty");
            if (_PriceRegistry is null)
                throw new ArgumentException("The registry can't be null");

            string[] SplitedLocation = _Location.Split(' ');

            string UpperLocation = "";
            foreach (string word in SplitedLocation)
            {
                if (word.Length > 0)
                {
                    string palabraMayuscula = word[..1].ToUpper() + word[1..].ToLower();
                    UpperLocation+= palabraMayuscula + " ";
                }
            }

            UpperLocation = UpperLocation.Trim();

            if (!_PriceRegistry.ContainsKey(UpperLocation))
                throw new InvalidLocationException("The loaded location is not in our registry");

            PriceRegistry = _PriceRegistry;
            Location = UpperLocation;
        }

        public double GetCallPrice()
        {
            return PriceRegistry[Location];
        }
    }
}
