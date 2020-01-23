using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Service
{
    public class BingSearchEngine : SearchEngine
    {
        private readonly HttpClient _httpClient;

        public BingSearchEngine()
        {
            _httpClient = new HttpClient();
        }

        public Task<int> GetSearchResultCount(string term)
        {
            throw new NotImplementedException();
        }
    }
}
