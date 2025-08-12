using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.Models.DTO
{
    public class OmdbSearchMovieResponse
    {
        public string Response { get; set; }
        public string TotalResults { get; set; }
        public List<OmdbMovieDTO> Search { get; set; }
    }
}
