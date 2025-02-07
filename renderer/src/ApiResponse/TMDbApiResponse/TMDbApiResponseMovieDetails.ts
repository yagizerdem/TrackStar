export class TMDbApiResponseMovieDetails {
  adult: boolean;
  backdrop_path: string;
  belongs_to_collection: any;
  budget: number;
  genres: { id: number; name: string }[];
  homepage: string;
  id: number;
  imdb_id: string;
  origin_country: string[];
  original_language: string;
  original_title: string;
  overview: string;
  popularity: number;
  poster_path: string;
  production_companies: { id: number; name: string }[];
  production_countries: { iso_3166_1: string; name: string }[];
  release_date: string;
  revenue: number;
  runtime: number;
  spoken_languages: { iso_639_1: string; name: string }[];
  status: string;
  tagline: string;
  title: string;
  video: boolean;
  vote_average: number;
  vote_count: number;

  constructor(data: Partial<TMDbApiResponseMovieDetails>) {
    this.adult = data.adult || false;
    this.backdrop_path = data.backdrop_path || "";
    this.belongs_to_collection = data.belongs_to_collection || null;
    this.budget = data.budget || 0;
    this.genres = data.genres || [];
    this.homepage = data.homepage || "";
    this.id = data.id || 0;
    this.imdb_id = data.imdb_id || "";
    this.origin_country = data.origin_country || [];
    this.original_language = data.original_language || "";
    this.original_title = data.original_title || "";
    this.overview = data.overview || "";
    this.popularity = data.popularity || 0;
    this.poster_path = data.poster_path || "";
    this.production_companies = data.production_companies || [];
    this.production_countries = data.production_countries || [];
    this.release_date = data.release_date || "";
    this.revenue = data.revenue || 0;
    this.runtime = data.runtime || 0;
    this.spoken_languages = data.spoken_languages || [];
    this.status = data.status || "";
    this.tagline = data.tagline || "";
    this.title = data.title || "";
    this.video = data.video || false;
    this.vote_average = data.vote_average || 0;
    this.vote_count = data.vote_count || 0;
  }

  // Static method to map JSON to TMDBMovie instance
  static fromJson(json: any): TMDbApiResponseMovieDetails {
    return new TMDbApiResponseMovieDetails({
      adult: json.adult,
      backdrop_path: json.backdrop_path,
      belongs_to_collection: json.belongs_to_collection,
      budget: json.budget,
      genres:
        json.genres?.map((genre: any) => ({
          id: genre.id,
          name: genre.name,
        })) || [],
      homepage: json.homepage,
      id: json.id,
      imdb_id: json.imdb_id,
      origin_country: json.origin_country,
      original_language: json.original_language,
      original_title: json.original_title,
      overview: json.overview,
      popularity: json.popularity,
      poster_path: json.poster_path,
      production_companies:
        json.production_companies?.map((company: any) => ({
          id: company.id,
          name: company.name,
        })) || [],
      production_countries:
        json.production_countries?.map((country: any) => ({
          iso_3166_1: country.iso_3166_1,
          name: country.name,
        })) || [],
      release_date: json.release_date,
      revenue: json.revenue,
      runtime: json.runtime,
      spoken_languages:
        json.spoken_languages?.map((lang: any) => ({
          iso_639_1: lang.iso_639_1,
          name: lang.name,
        })) || [],
      status: json.status,
      tagline: json.tagline,
      title: json.title,
      video: json.video,
      vote_average: json.vote_average,
      vote_count: json.vote_count,
    });
  }
}
