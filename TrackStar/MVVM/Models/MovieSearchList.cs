using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackStar.Util;

namespace TrackStar.MVVM.Models
{
    public partial class MovieSearchList
    {
        public List<Search> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }

    }

    public class Search
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }

    public partial class MovieSearchList
    {
        public static MovieSearchList FromJson(string json) => JsonConvert.DeserializeObject<MovieSearchList>(json, Converter.Settings);
    }
 
}
