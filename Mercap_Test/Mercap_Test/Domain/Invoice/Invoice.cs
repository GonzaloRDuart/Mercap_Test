using Mercap_Test.Domain.Call;

namespace Mercap_Test.Domain.Invoice
{
    public class Invoice(double _monthly_fee)
    {
        private readonly double monthly_fee = _monthly_fee;

        private double current_consumption = 0;


        public void Add_Consumption(ICall call)
        {
            current_consumption += call.GetCallPrice();
        }
        public double Generate_monthly_invoice()
        {
            double monthly_invoice = monthly_fee + current_consumption;
            current_consumption = 0;
            return monthly_invoice;
        }
    }
}
