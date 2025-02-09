const { app, BrowserWindow, ipcMain } = require("electron");
const path = require("path");
const { SettingsController } = require("./Controllers/SettingsController");
const { Database } = require("./DbContext/Database");
const isDev = import("electron-is-dev");
const { MoviesController } = require("./Controllers/MoviesController");
const { SeriesController } = require("./Controllers/SeriesController");

// initilize database
new Database();

const settingsController = new SettingsController();
const moviesController = new MoviesController();
const seriesController = new SeriesController();

ipcMain.handle("settings:get", settingsController.getSettings);
ipcMain.handle("settings:update", settingsController.updateSettings);

ipcMain.handle("movies:add", moviesController.addMovie);
ipcMain.handle("movies:get", moviesController.get);
ipcMain.handle("movies:delete", moviesController.delete);

ipcMain.handle("series:add", seriesController.addSeries);
ipcMain.handle("series:get", seriesController.get);
ipcMain.handle("series:delete", seriesController.delete);

const createWindow = () => {
  const mainWindow = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      preload: path.join(__dirname, "..", "preload", "preload.js"),
      contextIsolation: true,
      nodeIntegration: true,
    },
  });

  const startURL = isDev
    ? "http://localhost:5173"
    : `file://${path.join(__dirname, "../renderer/build/index.html")}`;

  ipcMain.handle("get-window-size", (event) => {
    return mainWindow.getBounds();
  });

  mainWindow.loadURL(startURL);

  mainWindow.on("closed", () => (mainWindow = null));
};

app.on("ready", createWindow);

app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});

app.on("activate", () => {
  if (mainWindow === null) {
    createWindow();
  }
});
