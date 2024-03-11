using Mercap_Test.Domain.Call.CallTypes;
using Mercap_Test.Domain.Excepctions;
using Mercap_Test.Domain.Invoice;

namespace Mercap_Test.Services
{
    public static class MenuService
    {

        private static readonly Invoice MonthlyInvoice = new();

        static private bool EndProgram = false;

        static private int CurrentDay = 0;
        static private int SelectedOption;

        static private double CurrentInvoice = 0;

        private static void ShowMenu()
        {
            Console.Write("Please select and option:\n" +
                "1. Make local call\n" +
                "2. Make national call\n" +
                "3. Make International call\n" +
                "4. End day\n");
            if (CurrentInvoice == 0) Console.WriteLine("5. End program");
            else Console.Write("5. Print last invoice \n6. End program");
        }

        private static void ReadOption()
        {
            ConsoleKeyInfo selectedKey;

            selectedKey = Console.ReadKey(intercept: true);

            if (char.IsDigit(selectedKey.KeyChar))
            {
                SelectedOption = int.Parse(selectedKey.KeyChar.ToString());
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
                MonthlyInvoice.AddConsumption(new LocalCall(day, hour));
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
                    MonthlyInvoice.AddConsumption(new NationalCall(city));
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
                    MonthlyInvoice.AddConsumption(new InternationalCall(country));
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
            Console.WriteLine("Your last invoice has a value of: $" + CurrentInvoice);
        }
        private static void RunOption()
        {
            switch (SelectedOption)
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
                    CurrentDay++;
                    break;
                case 5:
                    if (CurrentInvoice == 0) EndProgram = true;
                    else ShowInvoice();
                    break;
                case 6:
                    EndProgram = true;
                    break;
                default:
                    Console.WriteLine("Please enter a correct option.");
                    break;
            }
        }

        private static void GenerateInvoiceIfMonthEnds()
        {
            CurrentInvoice = MonthlyInvoice.GenerateMonthlyInvoice();
            CurrentDay = 0;
        }

        public static void ExecutePipeline()
        {
            while (!EndProgram)
            {

                ShowMenu();
                ReadOption();
                Console.Clear();
                RunOption();
                if (CurrentDay >= 30) GenerateInvoiceIfMonthEnds();
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
