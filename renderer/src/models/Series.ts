export class Series {
  Title: string;
  Year: string;
  Rated: string;
  Released: string;
  Runtime: string;
  Genre: string;
  Director: string;
  Writer: string;
  Actors: string;
  Plot: string;
  Language: string;
  Country: string;
  Awards: string;
  Poster: string;
  Metascore: string;
  imdbRating: string;
  imdbVotes: string;
  imdbID: string;
  Type: string;
  totalSeasons: string;
  Response: string;
  Ratings: { Source: string; Value: string }[];

  constructor(data: Partial<Series> = {}) {
    this.Title = data.Title ?? "Unknown";
    this.Year = data.Year ?? "Unknown";
    this.Rated = data.Rated ?? "Not Rated";
    this.Released = data.Released ?? "Unknown";
    this.Runtime = data.Runtime ?? "N/A";
    this.Genre = data.Genre ?? "Unknown";
    this.Director = data.Director ?? "N/A";
    this.Writer = data.Writer ?? "N/A";
    this.Actors = data.Actors ?? "Unknown";
    this.Plot = data.Plot ?? "No plot available.";
    this.Language = data.Language ?? "Unknown";
    this.Country = data.Country ?? "Unknown";
    this.Awards = data.Awards ?? "N/A";
    this.Poster = data.Poster ?? "";
    this.Metascore = data.Metascore ?? "N/A";
    this.imdbRating = data.imdbRating ?? "N/A";
    this.imdbVotes = data.imdbVotes ?? "N/A";
    this.imdbID = data.imdbID ?? "";
    this.Type = data.Type ?? "series";
    this.totalSeasons = data.totalSeasons ?? "N/A";
    this.Response = data.Response ?? "False";
    this.Ratings = data.Ratings ?? [];
  }
  static fromJson(json: any): Series {
    return new Series(json);
  }
}
