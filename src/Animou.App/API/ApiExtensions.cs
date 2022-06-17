using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Animou.App.API
{
    public static class ApiExtensions
    {
        public static object GenerateAnimeCards(dynamic result, ApiAnimeViewModel _apiAnimeViewModel)
        {
            List<ApiAnimeViewModel> data = new List<ApiAnimeViewModel>();

            foreach (var item in result.data.Page.media)
            {
                var status = item.status.ToString().Substring(0, 1).ToUpper() +
                             item.status.ToString().Substring(1).ToLower();
                var coverImage = item.coverImage.large;
                var titleRomaji = item.title.romaji;
                var averageScore = item.averageScore != null ? item.averageScore : 0;
                List<string> genres = new List<string>();
                List<string> studios = new List<string>();
                var episodes = item.episodes == null ? "" : item.episodes.ToString() + " episodes";
                var format = item.format.ToString();

                if (status == "Not_yet_released") status = "Soon";

                switch (format)
                {
                    case "TV": format = "TV Show"; break;
                    case "TV_SHORT": format = "TV Short"; break;
                }

                foreach (var edge in item.studios.edges)
                {
                    if (studios.Count == 2) break;
                    studios.Add(edge.node.name.ToString());
                }

                foreach (var genre in item.genres)
                {
                    if (genres.Count == 2) break;
                    genres.Add(genre.ToString().ToLower());
                }

                _apiAnimeViewModel.Id = item.id;
                _apiAnimeViewModel.title = titleRomaji;
                _apiAnimeViewModel.coverImage = coverImage;
                _apiAnimeViewModel.score = averageScore;
                _apiAnimeViewModel.status = status;
                _apiAnimeViewModel.studios = studios;
                _apiAnimeViewModel.format = format;
                _apiAnimeViewModel.genres = genres;
                _apiAnimeViewModel.episodes = episodes;

                var viewModel = _apiAnimeViewModel.ShallowCopy();
                data.Add(viewModel);
            }
            return data;
        }

        public static IEnumerable<string> GenerateGenres(dynamic result)
        {
            List<string> data = new List<string>();

            foreach (var item in result.data.GenreCollection) data.Add(item.ToString().Replace(' ', '-'));

            return data;
        }

        public static async Task<dynamic> GetDynamicByApiPost(Api apiPost, HttpClient httpClient)
        {
            var postUrl = "https://graphql.anilist.co";
            var json = JsonConvert.SerializeObject(apiPost);
            var content = new StringContent(json, Encoding.UTF8, Application.Json);

            using var httpResponseMessage = await httpClient.PostAsync(postUrl, content);

            httpResponseMessage.EnsureSuccessStatusCode();
            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject(result);

        }

        #region Query Generators
        
        public static string GenerateQueryForAnimeCard()
        {
            return "query ($page: Int, $perPage: Int, $sort: [MediaSort], $status: MediaStatus, $seasonYear: Int) {" +
                        "Page (page: $page, perPage: $perPage) {" +
                            "pageInfo {" +
                                "hasNextPage" +
                            "}" +
                            "media (type: ANIME, sort: $sort, status: $status, seasonYear: $seasonYear) {" +
                                "id title {romaji english} averageScore seasonYear coverImage {large} format episodes genres status studios { edges { node { name } } }" +
                    "}}}";
        }

        public static string GenerateQueryForGenres() { return "{GenreCollection}"; }
        
        #endregion Query Generators

        public static string GenerateVariables(int page, int perPage, string sort)
        {
            return $"{{\"page\":{page}, \"perPage\":{perPage}, \"sort\":\"{sort}\"}}";
        }

        public static string GenerateVariables(int page, int perPage, string sort, string status)
        {
            return $"{{\"page\":{page}, \"perPage\":{perPage}, \"sort\":\"{sort}\", \"status\":\"{status}\"}}";
        }
        
        public static string GenerateVariables(int page, int perPage, string sort, string status, int year)
        {
            return $"{{\"page\":{page}, \"perPage\":{perPage}, \"sort\":\"{sort}\", \"status\":\"{status}\", \"year\":{year}}}";
        }
    }
}
