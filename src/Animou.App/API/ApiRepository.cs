namespace Animou.App.API
{
    public class ApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly Api _api;

        public ApiRepository(HttpClient httpClient, Api api)
        {
            _httpClient = httpClient;
            _api = api;
        }

        //public async Task<dynamic> GetAnimesBySearch(int page, int perPage, 
        //                                             string filters = "", string search = "",
        //                                             string order = "popularity")
        //{

        //    _api.query = ApiExtensions
        //}

        public async Task<dynamic> GetAnimeCard(int page, int perPage,
                                                string sort, string status, 
                                                int seasonYear = 0)
        {
            _api.query = ApiExtensions.GenerateQueryForAnimeCard();
            
            if (seasonYear == 0 && status == "")
            {
                _api.variables = ApiExtensions.GenerateVariables(page, perPage, sort);
            } else if (seasonYear == 0)
            {
                _api.variables = ApiExtensions.GenerateVariables(page, perPage, sort, status);
            }
            else
            {
                _api.variables = ApiExtensions.GenerateVariables(page, perPage, sort, status, seasonYear);
            }

            return await ApiExtensions.GetDynamicByApiPost(_api, _httpClient);
        }

        public async Task<dynamic> GetGenres()
        {
            _api.query = ApiExtensions.GenerateQueryForGenres();
            return await ApiExtensions.GetDynamicByApiPost(_api, _httpClient);
        }
    }
}
