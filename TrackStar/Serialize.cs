using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackStar.MVVM.Models;
using TrackStar.Util;

namespace TrackStar
{
    public static class Serialize
    {
        public static string ToJson(this Movie self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this MovieSearchList self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
