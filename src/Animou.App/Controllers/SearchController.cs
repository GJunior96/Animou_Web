using Animou.App.API;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Animou.App.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApiRepository _apiRepository;
        private readonly ApiViewModel _apiViewModel;
        private readonly ApiAnimeViewModel _apiAnimeViewModel;
        public SearchController(ApiRepository apiRepository,
                                ApiViewModel apiViewModel,
                                ApiAnimeViewModel apiAnimeViewModel)
        {
            _apiRepository = apiRepository;
            _apiViewModel = apiViewModel;
            _apiAnimeViewModel = apiAnimeViewModel;
        }

        [Route("search")]
        public async Task<IActionResult> Index(string filter, string search)
        {
            dynamic genres = await _apiRepository.GetGenres();
            _apiViewModel.Genres = ApiExtensions.GenerateGenres(genres);
            
            if (filter == null && search == null)
            {
                dynamic resultTopAiringAnimes = await _apiRepository.GetAnimeCard(1, 6, "TRENDING_DESC", "RELEASING");
                dynamic resultGenres = await _apiRepository.GetGenres();
                dynamic resultUpcomingAnimes = await _apiRepository.GetAnimeCard(1, 6, "POPULARITY_DESC", "NOT_YET_RELEASED", DateTime.Now.Year);
                dynamic resultAllTimePopular = await _apiRepository.GetAnimeCard(1, 6, "POPULARITY_DESC", "");


                _apiViewModel.Genres = ApiExtensions.GenerateGenres(resultGenres);
                _apiViewModel.TopAiringAnimes = ApiExtensions.GenerateAnimeCards(resultTopAiringAnimes, _apiAnimeViewModel);
                _apiViewModel.UpcomingAnimes = ApiExtensions.GenerateAnimeCards(resultUpcomingAnimes,
                                                                                _apiAnimeViewModel);
                _apiViewModel.MostPopularAnimes = ApiExtensions.GenerateAnimeCards(resultAllTimePopular,
                                                                                _apiAnimeViewModel);
            }
             else
            {
                _apiViewModel.UrlParameters = Request.QueryString.Value.ToString();
            }
            return View(_apiViewModel);
        }

        [Route("teste")]
        public async Task<IActionResult> UpdateSearch(ApiViewModel viewModel)
        {
            if (viewModel.UrlParameters == null)
            {
                dynamic resultTopAiringAnimes = await _apiRepository.GetAnimeCard(1, 6, "TRENDING_DESC", "RELEASING");
                dynamic resultGenres = await _apiRepository.GetGenres();
                dynamic resultUpcomingAnimes = await _apiRepository.GetAnimeCard(1, 6, "POPULARITY_DESC", "NOT_YET_RELEASED", DateTime.Now.Year);
                dynamic resultAllTimePopular = await _apiRepository.GetAnimeCard(1, 6, "POPULARITY_DESC", "");


                _apiViewModel.Genres = ApiExtensions.GenerateGenres(resultGenres);
                _apiViewModel.TopAiringAnimes = ApiExtensions.GenerateAnimeCards(resultTopAiringAnimes, _apiAnimeViewModel);
                _apiViewModel.UpcomingAnimes = ApiExtensions.GenerateAnimeCards(resultUpcomingAnimes,
                                                                                _apiAnimeViewModel);
                _apiViewModel.MostPopularAnimes = ApiExtensions.GenerateAnimeCards(resultAllTimePopular,
                                                                                _apiAnimeViewModel);
                return PartialView("_AnimeCardsPartial", viewModel);
            }
            else
            {
                int index = viewModel.UrlParameters.IndexOf('?');
                string query = index >= 0 ? viewModel.UrlParameters.Substring(index) : "";
                var filters = HttpUtility.ParseQueryString(query).Get("filter")?.Split(',');
                var search = HttpUtility.ParseQueryString(query).Get("search");
                //dynamic searchResult = await _apiRepository.Sea
                return PartialView("_SearchResultPartial", viewModel);
            } 
        }
    }
}
