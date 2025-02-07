export class TMDbApiResponseWithGenres {
  adult: boolean;
  backdrop_path: string;
  genre_ids: number[];
  id: number;
  original_language: string;
  original_title: string;
  overview: string;
  popularity: number;
  poster_path: string;
  release_date: string;
  title: string;
  video: boolean;
  vote_average: number;
  vote_count: number;

  constructor(data: Partial<TMDbApiResponseWithGenres>) {
    this.adult = data.adult || false;
    this.backdrop_path = data.backdrop_path || "";
    this.genre_ids = data.genre_ids || [];
    this.id = data.id || 0;
    this.original_language = data.original_language || "";
    this.original_title = data.original_title || "";
    this.overview = data.overview || "";
    this.popularity = data.popularity || 0;
    this.poster_path = data.poster_path || "";
    this.release_date = data.release_date || "";
    this.title = data.title || "";
    this.video = data.video || false;
    this.vote_average = data.vote_average || 0;
    this.vote_count = data.vote_count || 0;
  }

  // Static method to map JSON to TMDBApiResponse instance
  static fromJson(json: any): TMDbApiResponseWithGenres {
    return new TMDbApiResponseWithGenres({
      adult: json.adult,
      backdrop_path: json.backdrop_path,
      genre_ids: json.genre_ids,
      id: json.id,
      original_language: json.original_language,
      original_title: json.original_title,
      overview: json.overview,
      popularity: json.popularity,
      poster_path: json.poster_path,
      release_date: json.release_date,
      title: json.title,
      video: json.video,
      vote_average: json.vote_average,
      vote_count: json.vote_count,
    });
  }
}
