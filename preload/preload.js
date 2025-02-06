const { contextBridge, ipcRenderer } = require("electron");

// Expose APIs
contextBridge.exposeInMainWorld("settingsAPI", {
  get: () => ipcRenderer.invoke("settings:get"),
  update: (settings) => ipcRenderer.invoke("settings:update", settings),
});
