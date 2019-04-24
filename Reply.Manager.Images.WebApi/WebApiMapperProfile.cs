using AutoMapper;
using Reply.Manager.Images.Domain.Models;
using Reply.Manager.Images.WebApi.Dtos;

namespace Reply.Manager.Images.WebApi
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            CreateMap<Filme, FilmesGetResult>();
            CreateMap<FilmesGet, Pesquisa>();
            CreateMap<PicturePost, Picture>();
        }
    }
}
