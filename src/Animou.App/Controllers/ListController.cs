using Animou.App.ViewModels;
using Animou.Business.Interfaces;
using Animou.Business.Models;
using Animou.Data.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Animou.App.Controllers
{
    public class ListController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AnimeViewModel _animeViewModel;
        private readonly UserViewModel _userViewModel;

        public ListController(IAnimeRepository animeRepository,
                              IUserRepository userRepository,
                              IMapper mapper,
                              SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              AnimeViewModel animeViewModel,
                              UserViewModel userViewModel)
        {
            _animeRepository = animeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _animeViewModel = animeViewModel;
            _userViewModel = userViewModel;
        }

        [Route("my-list")]
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                //var data = _mapper.Map<IEnumerable<AnimeViewModel>>(await _animeRepository.GetAll());
                var data = _mapper.Map<IEnumerable<AnimeViewModel>>(await _animeRepository.GetAnimesByUserId(Guid.Parse(userId)));
                foreach (var animes in data) animes.User = null;
                return View(data);
            }
            else
            {
                return Redirect("Identity/Account/Register");
            }
        }

        [Route("my-list/add")]
        public async Task<IActionResult> AddToList(int status, int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                bool isSuccess = true;
                var userId = _userManager.GetUserId(User);

                var user = _mapper.Map<UserViewModel>(await _userRepository.GetUserAnimes(Guid.Parse(userId)));

                _animeViewModel.AnimeId = id;
                _animeViewModel.Status = status;
                _animeViewModel.UserId = Guid.Parse(userId);

                List<AnimeViewModel> list = new List<AnimeViewModel>();
                foreach (var anime in user.Animes)
                {
                    list.Add(anime);
                    if (anime.AnimeId == _animeViewModel.AnimeId)
                    {
                        isSuccess = false;
                        break;
                    }
                }
                if (isSuccess)
                {
                    list.Add(_animeViewModel);
                    user.Animes = list;
                }

                await _userRepository.Update(_mapper.Map<User>(user));

                return PartialView("_ListAlertPartial", isSuccess);
            } else
            {
                return Ok("NotLoggin");
            }
        }

        [Route("my-list/remove")]
        public async Task<IActionResult> RemoveFromList(int id, string path)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var isSuccess = true;
                var userId = _userManager.GetUserId(User);
                var anime = _mapper.Map<AnimeViewModel>(await _animeRepository
                                                    .GetAnimeByUserIdAndAnimeId(Guid.Parse(userId), id));
                try
                {
                    await _animeRepository.Delete(anime.Id);
                }
                catch
                {
                    isSuccess = false;
                }

                if (path == "/my-list") return Ok("Index");
                
                return PartialView("_ListAlertPartial", isSuccess);
            }
            return RedirectToAction("Identity/Account/Register");
        }
    }
}
