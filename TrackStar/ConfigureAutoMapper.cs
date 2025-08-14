using AutoMapper;
using TrackStar.Models.DTO;
using TrackStar.Models.Entity;

namespace TrackStar
{
    public static class ConfigureAutoMapper
    {
        public static MapperConfiguration Configure()
        {
            var cfg = new MapperConfiguration(expression =>
            {
                // === Movie Ratings: DTO -> Entity ===
                expression.CreateMap<OmdbRating, MovieRatingEntity>()
                    .ForMember(d => d.Source, o => o.MapFrom(s => s.Source))
                    .ForMember(d => d.Value, o => o.MapFrom(s => s.Value));

                // === Movie: DTO -> Entity ===
                expression.CreateMap<OmdbMovieDetailsDTO, MovieEntity>()
                    .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                    .ForMember(d => d.Year, o => o.MapFrom(s => s.Year))
                    .ForMember(d => d.Rated, o => o.MapFrom(s => s.Rated))
                    .ForMember(d => d.Released, o => o.MapFrom(s => s.Released))
                    .ForMember(d => d.Runtime, o => o.MapFrom(s => s.Runtime))
                    .ForMember(d => d.Genre, o => o.MapFrom(s => s.Genre))
                    .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
                    .ForMember(d => d.Writer, o => o.MapFrom(s => s.Writer))
                    .ForMember(d => d.Actors, o => o.MapFrom(s => s.Actors))
                    .ForMember(d => d.Plot, o => o.MapFrom(s => s.Plot))
                    .ForMember(d => d.Language, o => o.MapFrom(s => s.Language))
                    .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
                    .ForMember(d => d.Awards, o => o.MapFrom(s => s.Awards))
                    .ForMember(d => d.Poster, o => o.MapFrom(s => s.Poster))
                    .ForMember(d => d.Metascore, o => o.MapFrom(s => s.Metascore))
                    .ForMember(d => d.ImdbRating, o => o.MapFrom(s => s.ImdbRating))
                    .ForMember(d => d.ImdbVotes, o => o.MapFrom(s => s.ImdbVotes))
                    .ForMember(d => d.ImdbID, o => o.MapFrom(s => s.ImdbID))
                    .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                    .ForMember(d => d.DVD, o => o.MapFrom(s => s.DVD))
                    .ForMember(d => d.BoxOffice, o => o.MapFrom(s => s.BoxOffice))
                    .ForMember(d => d.Production, o => o.MapFrom(s => s.Production))
                    .ForMember(d => d.Website, o => o.MapFrom(s => s.Website))
                    .ForMember(d => d.Response, o => o.MapFrom(s => s.Response))
                    .ForMember(d => d.Ratings, o =>
                    {
                        o.MapFrom(s => s.Ratings);
                        o.NullSubstitute(new List<OmdbRating>()); // source type matches DTO
                    })
                    .ForMember(d => d.IsStarred, o => o.Ignore())
                    .ForMember(d => d.IsInWatchlist, o => o.Ignore());

                // === Series Ratings: DTO -> Entity ===
                expression.CreateMap<OmdbRatingDTO, SeriesRatingEntity>()
                    .ForMember(d => d.Source, o => o.MapFrom(s => s.Source))
                    .ForMember(d => d.Value, o => o.MapFrom(s => s.Value));

                // === Series: DTO -> Entity ===
                expression.CreateMap<OmdbSeriesDetailsDTO, SeriesEntity>()
                    .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                    .ForMember(d => d.Year, o => o.MapFrom(s => s.Year))
                    .ForMember(d => d.Rated, o => o.MapFrom(s => s.Rated))
                    .ForMember(d => d.Released, o => o.MapFrom(s => s.Released))
                    .ForMember(d => d.Runtime, o => o.MapFrom(s => s.Runtime))
                    .ForMember(d => d.Genre, o => o.MapFrom(s => s.Genre))
                    .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
                    .ForMember(d => d.Writer, o => o.MapFrom(s => s.Writer))
                    .ForMember(d => d.Actors, o => o.MapFrom(s => s.Actors))
                    .ForMember(d => d.Plot, o => o.MapFrom(s => s.Plot))
                    .ForMember(d => d.Language, o => o.MapFrom(s => s.Language))
                    .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
                    .ForMember(d => d.Awards, o => o.MapFrom(s => s.Awards))
                    .ForMember(d => d.Poster, o => o.MapFrom(s => s.Poster))
                    .ForMember(d => d.Metascore, o => o.MapFrom(s => s.Metascore))
                    .ForMember(d => d.ImdbRating, o => o.MapFrom(s => s.ImdbRating))
                    .ForMember(d => d.ImdbVotes, o => o.MapFrom(s => s.ImdbVotes))
                    .ForMember(d => d.ImdbID, o => o.MapFrom(s => s.ImdbID))
                    .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                    .ForMember(d => d.TotalSeasons, o => o.MapFrom(s => s.TotalSeasons))
                    .ForMember(d => d.Response, o => o.MapFrom(s => s.Response))
                    .ForMember(d => d.Ratings, o =>
                    {
                        o.MapFrom(s => s.Ratings);
                        o.NullSubstitute(new List<OmdbRatingDTO>()); // source type matches DTO
                    })
                    .ForMember(d => d.IsStarred, o => o.Ignore())
                    .ForMember(d => d.IsInWatchlist, o => o.Ignore());
            });

            return cfg;
        }
    }
}
