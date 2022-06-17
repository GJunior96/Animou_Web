using Animou.App.Extensions;

namespace Animou.App.API
{
    public class ApiViewModel
    {
        public IEnumerable<ApiAnimeViewModel>? TopAiringAnimes { get; set; }
        public IEnumerable<ApiAnimeViewModel>? UpcomingAnimes { get; set; }
        public IEnumerable<ApiAnimeViewModel>? MostPopularAnimes { get; set; }
        public IEnumerable<ApiAnimeViewModel>? SearchAnimes { get; set; }
        public string? UrlParameters { get; set; }
        public IEnumerable<string>? Genres { get; set; }
        public string? Search { get; set; }
    }
}
