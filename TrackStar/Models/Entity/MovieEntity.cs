using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace TrackStar.Models.Entity
{
    public class MovieEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public List<MovieRatingEntity> Ratings { get; set; }
        public string Metascore { get; set; }
        public string ImdbRating { get; set; }
        public string ImdbVotes { get; set; }
        public string ImdbID { get; set; }
        public string Type { get; set; }
        public string DVD { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public string Response { get; set; }
        public bool IsStarred { get; set; } = false;    
        public bool IsInWatchlist { get; set; } = false;


        public override bool Equals(object obj)
        {
            var item = obj as MovieEntity;

            if (item == null)
            {
                return false;
            }

            return item.ImdbID != null ? this.ImdbID.Equals(item.ImdbID) : this.Title.Equals(item.Title);
        }

    }


}
