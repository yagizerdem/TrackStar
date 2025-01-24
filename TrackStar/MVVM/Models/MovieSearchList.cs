using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackStar.Util;

namespace TrackStar.MVVM.Models
{
    public partial class MovieSearchList
    {
        public List<Search> Search { get; set; }
        [JsonProperty("totalResults")]
        public string totalResults { get; set; }
        [JsonProperty("Response")]
        public string Response { get; set; }

    }

    public class Search
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Year")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Year { get; set; }
        [JsonProperty("imdbID")]
        public string imdbID { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Poster")]
        public string Poster { get; set; }
    }

    public partial class MovieSearchList
    {
        public static MovieSearchList FromJson(string json) => JsonConvert.DeserializeObject<MovieSearchList>(json, Converter.Settings);
    }
 
}
