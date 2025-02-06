const { app, BrowserWindow, ipcMain } = require("electron");
const path = require("path");
const { SettingsController } = require("./Controllers/SettingsController");
const { Database } = require("./DbContext/Database");
const isDev = import("electron-is-dev");

// initilize database
new Database();

const settingsController = new SettingsController();

ipcMain.handle("settings:get", settingsController.getSettings);
ipcMain.handle("settings:update", settingsController.updateSettings);

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
