using Animou.App.API;
using Animou.App.ViewModels;
using Animou.Business.Interfaces;
using Animou.Data.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Animou.App.Controllers
{
    public class AnimeController : Controller
    {
        private readonly ApiAnimeViewModel _apiAnimeViewModel;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAnimeRepository _animeRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AnimeController(ApiAnimeViewModel apiAnimeViewModel,
                               UserManager<ApplicationUser> userManager,
                               IMapper mapper,
                               IAnimeRepository animeRepository,
                               SignInManager<ApplicationUser> signInManager)
        {
            _apiAnimeViewModel = apiAnimeViewModel;
            _userManager = userManager;
            _mapper = mapper;
            _animeRepository = animeRepository;
            _signInManager = signInManager;
        }

        [Route("anime")]
        public async  Task<IActionResult> Index(int id, string title)
        {
            if (_signInManager.IsSignedIn(User)) 
            {
                var userId = _userManager.GetUserId(User);
                var anime = _mapper.Map<AnimeViewModel>(await _animeRepository
                    .GetAnimeByUserIdAndAnimeId(Guid.Parse(userId), id));

                if (anime != null) _apiAnimeViewModel.onList = true;
            } 

            _apiAnimeViewModel.Id = id;
            _apiAnimeViewModel.title = title;
            return View(_apiAnimeViewModel);
        }
    }
}
