const fs = require("fs");
const { getDbPath } = require("../Utils/getDbPath");
const sqlite3 = require("sqlite3").verbose();

const fullPath = getDbPath();

console.log(fullPath);

class Database {
  static Instance = null;
  constructor() {
    if (Database.Instance != null) return;
    Database.Instance = this;

    this.fullPath = getDbPath();

    this.ensureDatabaseFile();

    // connect to database
    this.db = this.createDbConnection(fullPath);

    this.initializeTables();
  }

  ensureDatabaseFile() {
    // create file if not exist
    if (!fs.existsSync(this.fullPath)) {
      var createStream = fs.createWriteStream(this.fullPath);
      createStream.end();
    }
  }

  async initializeTables() {
    // create settings table
    this.db.run(
      `CREATE TABLE IF NOT EXISTS settings (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      language TEXT NOT NULL,
      name TEXT
  )`,
      (err) => {
        if (err) {
          console.error("Error creating table:", err.message);
        } else {
          // console.log("Table created or already exists.");

          this.db.all("SELECT * FROM settings", (err, rows) => {
            if (err) {
              console.error(
                "Error while fetching data from settings table:",
                err.message
              );
            } else {
              // console.log("Settings data:", rows);
              if (rows.length == 0) {
                this.db.run(
                  `INSERT INTO settings (language, name) VALUES (?, ?)`,
                  ["en", null],
                  function (err) {
                    if (err) {
                      console.error(
                        "Error inserting into settings table:",
                        err.message
                      );
                    } else {
                      // console.log(
                      //   "Inserted successfully with ID:",
                      //   this.lastID
                      // );
                    }
                  }
                );
              }
            }
          });
        }
      }
    );
  }

  createDbConnection(filepath) {
    const db = new sqlite3.Database(filepath, (error) => {
      if (error) {
        return console.error(error.message);
      }
    });
    console.log("Connection with SQLite has been established");
    return db;
  }
}

module.exports = { Database };
