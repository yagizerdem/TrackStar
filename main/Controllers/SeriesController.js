const { Database } = require("../DbContext/Database");
const { Response } = require("../Utils/Response");
const util = require("util");

class SeriesController {
  /**
   *
   */
  constructor() {
    this.addSeries = this.addSeries.bind(this); // Ensure `this` is bound
    this.getByIMDB_ID = this.getByIMDB_ID.bind(this);
    this.get = this.get.bind(this);
    this.delete = this.delete.bind(this);
  }

  async addSeries(event, series) {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.run)
        .bind(Database.Instance.db);

      // check series exist or not
      const exist = (await this.getByIMDB_ID(series.imdbID)).data;
      if (exist) {
        return Response.fail(null, "series already exist");
      }

      const sql = `
        INSERT INTO Series (
    Title, Year, Rated, Released, Runtime, Genre, Director, Writer, Actors, Plot,
    Language, Country, Awards, Poster, Metascore, imdbRating, imdbVotes, imdbID, Type,
    totalSeasons, Response, Ratings
) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);
`;

      const values = [
        series.Title ?? null,
        series.Year ?? null,
        series.Rated ?? null,
        series.Released ?? null,
        series.Runtime ?? null,
        series.Genre ?? null,
        series.Director ?? null,
        series.Writer ?? null,
        series.Actors ?? null,
        series.Plot ?? null,
        series.Language ?? null,
        series.Country ?? null,
        series.Awards ?? null,
        series.Poster ?? null,
        series.Metascore ?? null,
        series.imdbRating ?? null,
        series.imdbVotes ?? null,
        series.imdbID ?? null, // Unique identifier
        series.Type ?? null,
        series.totalSeasons ?? null,
        series.Response ?? null,
        series.Ratings ? JSON.stringify(series.Ratings) : null, // Convert Ratings to JSON
      ];

      await runAsync(sql, values);
      return Response.success(series, "added to database successfully");
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

      const sql = `SELECT * FROM Series WHERE imdbID = ? LIMIT 1`;

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

      const sql = `SELECT * FROM Series`;

      const response = await runAsync(sql);

      return Response.success(response, "series fetched from database");
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

      const sql = `DELETE FROM Series WHERE imdbID = ?`;

      await runAsync(sql, [imdbId]);

      return Response.success(null, "media deleted successfully");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "error occured");
    }
  }
}

module.exports = { SeriesController };
