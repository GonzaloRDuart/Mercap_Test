using Mercap_Test.Domain.PricesByLocation;

namespace Mercap_Test.Domain.Call.CallTypes
{
    internal class NationalCall(string _City) : CallByLocation(_City, PricesManager.PriceByCity)
    {
    }
}
