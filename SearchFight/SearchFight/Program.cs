using System;
using System.Collections.Generic;
using System.Linq;
using SearchFight.BL;
using SearchFight.DA;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            BL.SearchFight cliente = new BL.SearchFight();
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Enter queries to search.");
                    cliente.Search(Console.ReadLine());
                }
                else
                {
                    cliente.Search(args);
                }
                List<string> resultados = cliente.PrintResults();
                foreach (string r in resultados)
                    Console.WriteLine(r);

                Console.WriteLine(cliente.PrintWinnerGoogle());
                Console.WriteLine(cliente.PrintWinnerBing());
                Console.WriteLine(cliente.PrintWinnerTotal());
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: "+ex.Message);
                Console.WriteLine("error: " + ex.TargetSite);
                Console.WriteLine("error: " + ex.StackTrace);
            }

            Console.ReadLine();
        }
    }
}
