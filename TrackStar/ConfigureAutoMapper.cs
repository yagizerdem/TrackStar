using AutoMapper;
using TrackStar.Models.DTO;
using TrackStar.Models.Entity;

namespace TrackStar
{
    public static class ConfigureAutoMapper
    {
        public static MapperConfiguration Configure()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<OmdbMovieDetailsDTO, MovieEntity>();
                cfg.CreateMap<OmdbSeriesDetailsDTO, SeriesEntity>();
            });

            return configuration;
        }            
        
    }
}
