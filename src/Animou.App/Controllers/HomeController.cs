using Animou.App.API;
using Animou.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Animou.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiViewModel _apiViewModel;
        private readonly ApiAnimeViewModel _apiAnimeViewModel;
        private readonly ApiRepository _apiRepository;

        public HomeController(ILogger<HomeController> logger, 
                              ApiViewModel apiViewModel,
                              ApiAnimeViewModel apiAnimeViewModel,
                              ApiRepository apiRepository)
        {
            _logger = logger;
            _apiViewModel = apiViewModel;
            _apiAnimeViewModel = apiAnimeViewModel;
            _apiRepository = apiRepository;
        }

        public async Task<IActionResult> Index()
        {
            dynamic resultTopAiringAnimes = await _apiRepository.GetAnimeCard(1, 6, "TRENDING_DESC", "RELEASING");
            dynamic resultGenres = await _apiRepository.GetGenres();
            dynamic resultUpcomingAnimes = await _apiRepository.GetAnimeCard(1, 6, "POPULARITY_DESC", "NOT_YET_RELEASED", DateTime.Now.Year);
            dynamic resultAllTimePopular = await _apiRepository.GetAnimeCard(1, 6, "POPULARITY_DESC", "");


            _apiViewModel.Genres = ApiExtensions.GenerateGenres(resultGenres);
            _apiViewModel.TopAiringAnimes = ApiExtensions.GenerateAnimeCards(resultTopAiringAnimes,                                                                       _apiAnimeViewModel);
            _apiViewModel.UpcomingAnimes = ApiExtensions.GenerateAnimeCards(resultUpcomingAnimes,
                                                                            _apiAnimeViewModel);
            _apiViewModel.MostPopularAnimes = ApiExtensions.GenerateAnimeCards(resultAllTimePopular,
                                                                            _apiAnimeViewModel);
            return View(_apiViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}