using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.Models.Entity
{
    public class MovieRatingEntity : BaseEntity
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}
