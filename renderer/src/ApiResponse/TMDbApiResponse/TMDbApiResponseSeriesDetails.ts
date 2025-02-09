export class TMDbApiResponseSeriesDetails {
  id: number;
  name: string;
  original_name: string;
  overview: string;
  first_air_date: string;
  last_air_date: string;
  in_production: boolean;
  imdb_id: string | null;
  genres: { id: number; name: string }[];
  networks: {
    id: number;
    name: string;
    logo_path: string | null;
    origin_country: string;
  }[];
  origin_country: string[];
  original_language: string;
  popularity: number;
  poster_path: string | null;
  backdrop_path: string | null;
  vote_average: number;
  vote_count: number;
  status: string;
  type: string;
  homepage: string;
  tagline: string;
  number_of_seasons: number;
  number_of_episodes: number;
  production_companies: { id: number; name: string }[];
  production_countries: { iso_3166_1: string; name: string }[];
  spoken_languages: { iso_639_1: string; name: string }[];
  created_by: { id: number; name: string }[];
  episode_run_time: number[];
  seasons: {
    id: number;
    name: string;
    season_number: number;
    episode_count: number;
    air_date: string;
  }[];
  last_episode_to_air: {
    id: number;
    name: string;
    overview: string;
    vote_average: number;
    vote_count: number;
  } | null;
  next_episode_to_air: { id: number; name: string; air_date: string } | null;
  adult: boolean;

  constructor(data: Partial<TMDbApiResponseSeriesDetails>) {
    this.id = data.id || 0;
    this.name = data.name || "Unknown";
    this.original_name = data.original_name || "Unknown";
    this.overview = data.overview || "No overview available.";
    this.first_air_date = data.first_air_date || "";
    this.last_air_date = data.last_air_date || "";
    this.in_production = data.in_production || false;
    this.imdb_id = data.imdb_id || null;
    this.genres = data.genres || [];
    this.networks = data.networks || [];
    this.origin_country = data.origin_country || [];
    this.original_language = data.original_language || "Unknown";
    this.popularity = data.popularity || 0;
    this.poster_path = data.poster_path || "";
    this.backdrop_path = data.backdrop_path || "";
    this.vote_average = data.vote_average || 0;
    this.vote_count = data.vote_count || 0;
    this.status = data.status || "Unknown";
    this.type = data.type || "Unknown";
    this.homepage = data.homepage || "";
    this.tagline = data.tagline || "";
    this.number_of_seasons = data.number_of_seasons || 0;
    this.number_of_episodes = data.number_of_episodes || 0;
    this.production_companies = data.production_companies || [];
    this.production_countries = data.production_countries || [];
    this.spoken_languages = data.spoken_languages || [];
    this.created_by = data.created_by || [];
    this.episode_run_time = data.episode_run_time || [];
    this.seasons = data.seasons || [];
    this.last_episode_to_air = data.last_episode_to_air || null;
    this.next_episode_to_air = data.next_episode_to_air || null;
    this.adult = data.adult || false;
  }

  /** ✅ **Static Method to Create an Instance from JSON Data** */
  static fromJson(json: any): TMDbApiResponseSeriesDetails {
    return new TMDbApiResponseSeriesDetails(json);
  }
}
