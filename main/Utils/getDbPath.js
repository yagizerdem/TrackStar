const path = require("path");

function getDbPath() {
  const basePath = process.env.APPDATA;
  const fullPath = path.join(basePath, "app.Service.db");

  return fullPath;
}

module.exports = { getDbPath };
