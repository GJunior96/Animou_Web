using Animou.App.ViewModels;
using Animou.Business.Models;
using AutoMapper;

namespace Animou.App.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Anime, AnimeViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Like, LikeViewModel>().ReverseMap();
            CreateMap<Deslike, DeslikeViewModel>().ReverseMap();
        }
    }
}
