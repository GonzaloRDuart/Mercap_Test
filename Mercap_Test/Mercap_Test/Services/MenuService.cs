using Mercap_Test.Domain.Call.CallTypes;
using Mercap_Test.Domain.Excepctions;
using Mercap_Test.Domain.Invoice;

namespace Mercap_Test.Services
{
    public static class MenuService
    {
        private static readonly Invoice monthly_invoice = new(50);

        static private bool end_program = false;

        static private int current_day = 0;
        static private int selected_option;

        static private double current_invoice = 0;

        private static void ShowMenu()
        {
            Console.Write("Please select and option:\n" +
                "1. Make local call\n" +
                "2. Make national call\n" +
                "3. Make International call\n" +
                "4. End day\n");
            if (current_invoice == 0) Console.WriteLine("5. End program");
            else Console.Write("5. Print last invoice \n6. End program");
        }

        private static void ReadOption()
        {
            ConsoleKeyInfo selected_key;

            selected_key = Console.ReadKey(intercept: true);

            if (char.IsDigit(selected_key.KeyChar))
            {
                selected_option = int.Parse(selected_key.KeyChar.ToString());
            }
            else
            {
                Console.WriteLine("Please enter a numeric value.");
            }

            Console.Clear();
        }

        private static void MakeLocalCall()
        {
            Console.WriteLine("Please enter the first letter of the day of the call (M, T, W, T, F, S, S)");
            bool sucess = false;
            char day = ' ';
            while (!sucess)
            {
                try
                {
                    Console.Write("Day: ");
                    day = char.Parse(Console.ReadLine());
                    sucess = true;
                }
                catch
                {
                    Console.WriteLine("Please load an only letter");
                }
            }
            sucess = false;
            Console.WriteLine("\nPlease enter the hour of the call (From 0 to 23) ");
            int hour = 0;
            while (!sucess)
            {
                try
                {
                    Console.Write("Hour: ");
                    hour = int.Parse(Console.ReadLine());
                    sucess = true;
                }
                catch
                {
                    Console.WriteLine("Please load a number");
                }
            }
            try
            {
                monthly_invoice.Add_Consumption(new LocalCall(day, hour));
                Console.WriteLine("\nThe call was properly registered");
            }
            catch (InvalidDayException)
            {
                Console.WriteLine("\nThere was an error. Please enter a correct day");
            }
            catch (InvalidTimeException)
            {
                Console.WriteLine("\nThere was an error. Please enter a correct hour");
            }
        }

        private static void MakeNationalCall()
        {
            Console.WriteLine("Please enter the City of the destiny of the call");
            bool success = false;
            while (!success) { 
                try
                {
                    Console.Write("City: ");
                    string city = Console.ReadLine();
                    monthly_invoice.Add_Consumption(new NationalCall(city));
                    Console.WriteLine("\nThe call was properly registered");
                    success = true;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("\nPlease enter a value for the city");
                }
                catch (InvalidLocationException)
                {
                    Console.WriteLine("\nThe city was not found in our registry");
                }
            }
        }

        private static void MakeInternationalCall()
        {
            Console.WriteLine("Pleace enter the Country of the destiny of the call");
            bool success = false;
            while (!success)
            {
                try
                {
                    Console.Write("Country: ");
                    string country = Console.ReadLine();
                    monthly_invoice.Add_Consumption(new InternationalCall(country));
                    Console.WriteLine("\nThe call was properly registered.");
                    success = true;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("\nPlease enter a value for the country.");
                }
                catch (InvalidLocationException)
                {
                    Console.WriteLine("\nThe Country was not found in our registry.");
                }
            }
        }
        private static void ShowInvoice()
        {
            Console.WriteLine("Your last invoice has a value of: $" + current_invoice);
        }
        private static void RunOption()
        {
            switch (selected_option)
            {
                case 1:
                    MakeLocalCall();
                    break;
                case 2:
                    MakeNationalCall();
                    break;
                case 3:
                    MakeInternationalCall();
                    break;
                case 4:
                    current_day++;
                    break;
                case 5:
                    if (current_invoice == 0) end_program = true;
                    else ShowInvoice();
                    break;
                case 6:
                    end_program = true;
                    break;
                default:
                    Console.WriteLine("Please enter a correct option.");
                    break;
            }
        }

        private static void GenerateInvoiceIfMonthEnds()
        {
            current_invoice = monthly_invoice.Generate_monthly_invoice();
            current_day = 0;
        }

        public static void ExecutePipeline()
        {
            while (!end_program)
            {

                ShowMenu();
                ReadOption();
                Console.Clear();
                RunOption();
                if (current_day >= 30) GenerateInvoiceIfMonthEnds();
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
