const { Database } = require("../DbContext/Database");
const { Response } = require("../Utils/Response");
const util = require("util");

class SettingsController {
  /**
   *
   */
  constructor() {}

  async getSettings() {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.get)
        .bind(Database.Instance.db);
      const query = `SELECT *
  FROM settings
  LIMIT 1`;

      const row = await runAsync(query);

      return Response.success(row, "");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "");
    }
  }

  async updateSettings(event, settings) {
    try {
      const runAsync = util
        .promisify(Database.Instance.db.run)
        .bind(Database.Instance.db);

      const updateSettings = async (id, language, name) => {
        const query = `UPDATE settings SET language = ?, name = ? WHERE id = ?`;

        try {
          // Execute the update query using promisified .run
          const result = await runAsync(query, [language, name, id]);
          console.log(`Rows updated: ${result.changes}`);
        } catch (err) {
          console.error("Error updating settings:", err);
        }
      };

      await updateSettings(settings.id, settings.language, settings.name);

      return Response.success(settings, "data updated");
    } catch (err) {
      console.log(err);
      return Response.fail(null, "error occured");
    }
  }
}

module.exports = { SettingsController };
