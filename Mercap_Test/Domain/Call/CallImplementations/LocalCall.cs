using Mercap_Test.Domain.Excepctions;
using System.Text.RegularExpressions;

namespace Mercap_Test.Domain.Call.CallTypes
{
    public class LocalCall : ICall
    {
        private const double WorkingDayPrice8to20 = 0.2;
        private const double WorkingDayPrice21to7 = 0.1;

        private const double WeekendPrice = 0.1;

        private readonly char Day;
        private readonly int Hour;

        private readonly Regex Weekend = new(@"^[Ss]$");

        public LocalCall(char _Day, int _Hour)
        {
            Regex EveryDayRegex = new(@"^[MmTtWwFfSs]$");

            if (!EveryDayRegex.IsMatch(_Day.ToString()))
                throw new InvalidDayException("The day of the call was not properly loaded");

            if (_Hour < 0 || _Hour > 24)
                throw new InvalidTimeException("The hour of the call was not properly loaded");

            Day = _Day;
            Hour = _Hour;
        }

        public double GetCallPrice()
        {

            if (Weekend.IsMatch(Day.ToString())) return WeekendPrice;
            else
            {
                if (Hour >= 8 && Hour <= 20) return WorkingDayPrice8to20;
                else return WorkingDayPrice21to7;
            }
        }
    }
}
