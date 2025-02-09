export class Movies {
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
  DVD: string;
  BoxOffice: string;
  Production: string;
  Website: string;
  Response: string;
  Ratings: Array<{ Source: string; Value: string }>;

  constructor(data: Partial<Movies> = {}) {
    this.Title = data.Title || "";
    this.Year = data.Year || "";
    this.Rated = data.Rated || "";
    this.Released = data.Released || "";
    this.Runtime = data.Runtime || "";
    this.Genre = data.Genre || "";
    this.Director = data.Director || "";
    this.Writer = data.Writer || "";
    this.Actors = data.Actors || "";
    this.Plot = data.Plot || "";
    this.Language = data.Language || "";
    this.Country = data.Country || "";
    this.Awards = data.Awards || "";
    this.Poster = data.Poster || "";
    this.Metascore = data.Metascore || "";
    this.imdbRating = data.imdbRating || "";
    this.imdbVotes = data.imdbVotes || "";
    this.imdbID = data.imdbID || "";
    this.Type = data.Type || "";
    this.DVD = data.DVD || "";
    this.BoxOffice = data.BoxOffice || "";
    this.Production = data.Production || "";
    this.Website = data.Website || "";
    this.Response = data.Response || "";
    this.Ratings = data.Ratings || [];
  }

  // Factory method to map JSON data to the Movie object
  static fromJson(json: any): Movies {
    return new Movies({
      Title: json.Title,
      Year: json.Year,
      Rated: json.Rated,
      Released: json.Released,
      Runtime: json.Runtime,
      Genre: json.Genre,
      Director: json.Director,
      Writer: json.Writer,
      Actors: json.Actors,
      Plot: json.Plot,
      Language: json.Language,
      Country: json.Country,
      Awards: json.Awards,
      Poster: json.Poster,
      Metascore: json.Metascore,
      imdbRating: json.imdbRating,
      imdbVotes: json.imdbVotes,
      imdbID: json.imdbID,
      Type: json.Type,
      DVD: json.DVD,
      BoxOffice: json.BoxOffice,
      Production: json.Production,
      Website: json.Website,
      Response: json.Response,
      Ratings: json.Ratings
        ? json.Ratings.map((rating: any) => ({
            Source: rating.Source,
            Value: rating.Value,
          }))
        : [],
    });
  }
}
