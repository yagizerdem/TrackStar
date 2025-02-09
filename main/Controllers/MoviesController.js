const { Database } = require("../DbContext/Database");
const { Response } = require("../Utils/Response");
const util = require("util");

class MoviesController {
  /**
   *
   */
  constructor() {
    this.addMovie = this.addMovie.bind(this); // Ensure `this` is bound
    this.getByIMDB_ID = this.getByIMDB_ID.bind(this);
    this.get = this.get.bind(this);
    this.delete = this.delete.bind(this);
  }

  async addMovie(event, movie) {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.run)
        .bind(Database.Instance.db);

      // check movei exist or not
      const exist = (await this.getByIMDB_ID(movie.imdbID)).data;
      if (exist) {
        return Response.fail(null, "film already exist");
      }

      const sql = `
  INSERT INTO Movies (
    Title, Year, Rated, Released, Runtime, Genre, Director, Writer, Actors, Plot,
    Language, Country, Awards, Poster, Metascore, imdbRating, imdbVotes, imdbID, Type,
    DVD, BoxOffice, Production, Website, Response, Ratings
  ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
`;

      const values = [
        movie.Title ?? null,
        movie.Year ?? null,
        movie.Rated ?? null,
        movie.Released ?? null,
        movie.Runtime ?? null,
        movie.Genre ?? null,
        movie.Director ?? null,
        movie.Writer ?? null,
        movie.Actors ?? null,
        movie.Plot ?? null,
        movie.Language ?? null,
        movie.Country ?? null,
        movie.Awards ?? null,
        movie.Poster ?? null,
        movie.Metascore ?? null,
        movie.imdbRating ?? null,
        movie.imdbVotes ?? null,
        movie.imdbID ?? null,
        movie.Type ?? null,
        movie.DVD ?? null,
        movie.BoxOffice ?? null,
        movie.Production ?? null,
        movie.Website ?? null,
        movie.Response ?? null,
        movie.Ratings ? JSON.stringify(movie.Ratings) : null,
      ];
      await runAsync(sql, values);
      return Response.success(movie, "added to database successfully");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "error occured");
    }
  }

  async getByIMDB_ID(imdbId) {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.get)
        .bind(Database.Instance.db);

      const sql = `SELECT * FROM Movies WHERE imdbID = ? LIMIT 1`;

      const response = await runAsync(sql, [imdbId]);

      return Response.success(response, "fetched from database");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "error occured");
    }
  }

  async get() {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.all)
        .bind(Database.Instance.db);

      const sql = `SELECT * FROM Movies`;

      const response = await runAsync(sql);

      return Response.success(response, "films fetched from database");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "error occured");
    }
  }

  async delete(event, imdbId) {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.run)
        .bind(Database.Instance.db);

      const sql = `DELETE FROM Movies WHERE imdbID = ?`;

      await runAsync(sql, [imdbId]);

      return Response.success(null, "media deleted successfully");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "error occured");
    }
  }
}

module.exports = { MoviesController };
