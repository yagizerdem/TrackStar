// See the Electron documentation for details on how to use preload scripts:
// https://www.electronjs.org/docs/latest/tutorial/process-model#preload-scripts

import { contextBridge, ipcRenderer } from "electron";

contextBridge.exposeInMainWorld("windowController", {
  minimize: () => ipcRenderer.send("windowController:minimize-window"),
  maximize: () => ipcRenderer.send("windowController:maximize-window"),
  close: () => ipcRenderer.send("windowController:close-window"),
});
