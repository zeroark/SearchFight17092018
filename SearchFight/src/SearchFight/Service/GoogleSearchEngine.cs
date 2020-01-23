using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SearchFight.Service
{
    public class GoogleSearchEngine : SearchEngine
    {
        private readonly HttpClient _httpClient;

        const string REQUEST_TEMPLATE = "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}";

        const string API_KEY = "AIzaSyAxbhLAiKBGnhyd4hZQSLZ7XVCSK45oaNw";

        const string CONTEXT_ID = "018022716490518442170:uctnoratmy8";

        public GoogleSearchEngine()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> GetSearchResultCount(string term)
        {
            var response = await _httpClient.GetAsync(BuildUrl(term));

            var result = await response.Content.ReadAsStringAsync();

            var searchInformation = JsonConvert.DeserializeObject<GoogleResponse>(result);

            return searchInformation.SearchInformation.TotalResults;
        }

        private string BuildUrl(string searchTerm) => string.Format(REQUEST_TEMPLATE, API_KEY, CONTEXT_ID, searchTerm);
    }
}
