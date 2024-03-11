using Mercap_Test.Domain.Call;

namespace Mercap_Test.Domain.Invoice
{
    public class Invoice(double _monthlyFee = 50)
    {
        private readonly double MonthlyFee = _monthlyFee;

        private double Currentconsumption = 0;


        public void AddConsumption(ICall call)
        {
            Currentconsumption += call.GetCallPrice();
        }
        public double GenerateMonthlyInvoice()
        {
            double MonthlyInvoice = MonthlyFee + Currentconsumption;
            Currentconsumption = 0;
            return MonthlyInvoice;
        }
    }
}
