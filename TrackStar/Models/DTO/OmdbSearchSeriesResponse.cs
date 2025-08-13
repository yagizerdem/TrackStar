using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.Models.DTO
{
    public class OmdbSearchSeriesResponse
    {
        public List<OmdbSeriesDTO> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }
    }
}
