using System;
using System.Threading.Tasks;
using SearchFight.Service;

namespace SearchFight.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();            
        }

        static async Task MainAsync(string[] args)
        {
            var googleSearchEngine = new GoogleSearchEngine();

            var result = await googleSearchEngine.GetSearchResultCount("javascript");

            Console.WriteLine($"We have found {result} results for search term 'javascript' (Google Search Engine)");

            Console.ReadLine();
        }
    }
}
