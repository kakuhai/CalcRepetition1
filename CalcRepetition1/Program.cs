using System;
using KalkylatorLibrary;

namespace KalkylatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Välkomna användaren och sätt titel
            Console.Title = "Kalkylator";
            Console.WriteLine("Välkommen till Kalkylatorn\r");
            Console.WriteLine("--------------------------\n");

            Kalkylator kalkylator = new Kalkylator();

            //Här sparas listan för historik
            List<double> ResultatHistoria = new List<double>();

            while (!endApp)
            {
                // Deklarera tomma variabler
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;


                // Följande är inmatningen för Tal 1
                // Be användaren ange första talet.
                Console.Write("Ange ett tal, bekräfta med Enter: ");
                numInput1 = Console.ReadLine();

                // Användarens inmatning "numInput1" testas för att se om den är giltig eller inte.
                // Om giltig omvandlas numInput1 till cleanNum1. Annars visas felmeddelande
                // och användaren ombedjes att försöka igen med heltalsvärde.
                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("Felaktig inmatning. Ange ett heltalsvärde: ");
                    numInput1 = Console.ReadLine();
                }

                // Nedan är inmatningen för Tal 2 med samma funktion som Tal 1
                Console.Write("Ange det andra talet, bekräfta med Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2 , out cleanNum2))
                {
                    Console.Write("Felaktig inmatning. Ange ett heltalsvärde: ");
                    numInput2 = Console.ReadLine();
                }

                // Be användaren välja en operator.
                Console.WriteLine("Välj en operator från följande lista:");
                Console.WriteLine("\ta - Addera");
                Console.WriteLine("\ts - Subtrahera");
                Console.WriteLine("\tm - Multiplicera");
                Console.WriteLine("\td - Dividera");
                Console.Write("Ditt val?: ");

                string op = Console.ReadLine();

                // Följande testar om användarens uträkning är giltig eller inte. Om uträkningen
                // är ogiltig kommer detta visas för användaren. Annars visar den resultatet.
                try
                {
                    result = kalkylator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("Denna uträkning kommer leda till ett matematiskt fel.\n");
                    }
                    else Console.WriteLine("Resultat: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oj! Ett undantag skedde i uträkningen.\n - Detaljer: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                //Lägger till resultat till listan
                ResultatHistoria.Add(result);

                //Här frågas användaren vill se historik av beräkningar eller förtsätta
                Console.Write("För att se tidigare resultat ange 'r' och Enter.\n " +
                    "För att fortsätta, tryck Enter: ");
                if (Console.ReadLine() == "r")
                {
                    foreach (double r in ResultatHistoria)
                    {
                        Console.WriteLine("\n" + r);
                    }
                }

                //Här frågas användaren om den vill avsluta eller fortsätta
                Console.Write("För att avsluta programmet ange 'n' och Enter, eller ange valfri tangent och Enter för ny uträkning: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
            // Stäng JSON writer innan return.
            kalkylator.Finish();
            return;
        }
    }
}