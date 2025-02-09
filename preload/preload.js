const { contextBridge, ipcRenderer } = require("electron");

// Expose APIs
contextBridge.exposeInMainWorld("settingsAPI", {
  get: () => ipcRenderer.invoke("settings:get"),
  update: (settings) => ipcRenderer.invoke("settings:update", settings),
});

contextBridge.exposeInMainWorld("moviesAPI", {
  add: (movie) => ipcRenderer.invoke("movies:add", movie),
  get: () => ipcRenderer.invoke("movies:get"),
  delete: (imdbID) => ipcRenderer.invoke("movies:delete", imdbID),
});

contextBridge.exposeInMainWorld("seriesAPI", {
  add: (series) => ipcRenderer.invoke("series:add", series),
  get: () => ipcRenderer.invoke("series:get"),
  delete: (imdbID) => ipcRenderer.invoke("series:delete", imdbID),
});

contextBridge.exposeInMainWorld("electron", {
  ipcRenderer: {
    send: (channel, data) => ipcRenderer.send(channel, data),
    invoke: (channel, data) => ipcRenderer.invoke(channel, data),
    on: (channel, callback) =>
      ipcRenderer.on(channel, (event, ...args) => callback(...args)),
    removeAllListeners: (channel) => ipcRenderer.removeAllListeners(channel),
  },
});
