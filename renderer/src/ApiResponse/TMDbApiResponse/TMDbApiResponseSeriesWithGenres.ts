export class TMDbApiResponseSeriesWithGenres {
  adult: boolean;
  backdrop_path: string;
  first_air_date: string;
  genre_ids: number[];
  id: number;
  name: string;
  origin_country: string[];
  original_language: string;
  original_name: string;
  overview: string;
  popularity: number;
  poster_path: string;
  vote_average: number;
  vote_count: number;

  constructor(data: TMDbApiResponseSeriesWithGenres) {
    this.adult = data.adult;
    this.backdrop_path = data.backdrop_path;
    this.first_air_date = data.first_air_date;
    this.genre_ids = data.genre_ids;
    this.id = data.id;
    this.name = data.name;
    this.origin_country = data.origin_country;
    this.original_language = data.original_language;
    this.original_name = data.original_name;
    this.overview = data.overview;
    this.popularity = data.popularity;
    this.poster_path = data.poster_path;
    this.vote_average = data.vote_average;
    this.vote_count = data.vote_count;
  }

  // **Static Factory Method** - Creates an instance from raw JSON
  static fromJSON(data: any): TMDbApiResponseSeriesWithGenres {
    return new TMDbApiResponseSeriesWithGenres({
      adult: data.adult || false,
      backdrop_path: data.backdrop_path || "",
      first_air_date: data.first_air_date || "",
      genre_ids: data.genre_ids || [],
      id: data.id || 0,
      name: data.name || "Unknown",
      origin_country: data.origin_country || [],
      original_language: data.original_language || "unknown",
      original_name: data.original_name || "Unknown",
      overview: data.overview || "No overview available.",
      popularity: data.popularity || 0,
      poster_path: data.poster_path || "",
      vote_average: data.vote_average || 0,
      vote_count: data.vote_count || 0,
    });
  }
}
