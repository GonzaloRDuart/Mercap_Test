using Mercap_Test.Domain.Excepctions;
using Mercap_Test.Domain.PricesByLocation;

namespace Mercap_Test.Domain.Call.CallTypes
{
    public class InternationalCall(string _Country) : CallByLocation(_Country, PricesManager.PriceByCountry)
    {
    }
}
