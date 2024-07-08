using AutoMapper;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;
using OL_OASP_dev_H_07_23_WebApi.Models.Dbo;

namespace OL_OASP_dev_H_07_23_WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Actor, ActorViewModel>();
            CreateMap<ActorBinding, Actor>();
            CreateMap<ActorUpdateBinding, Actor>();


            CreateMap<Movie, MovieViewModel>();
            CreateMap<MovieBinding, Movie>();
            CreateMap<MovieUpdateBinding, Movie>();
        }
    }
}
