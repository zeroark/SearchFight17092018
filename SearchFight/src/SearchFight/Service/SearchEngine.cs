using System.Threading.Tasks;

namespace SearchFight
{
    public interface SearchEngine
    {
        Task<int> GetSearchResultCount(string term);
    }
}
