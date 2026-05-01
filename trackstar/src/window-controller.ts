import { BrowserWindow } from "electron";

export function minimizeWindow() {
  BrowserWindow?.getFocusedWindow()?.minimize();
}

export function maximizeWindow() {
  BrowserWindow?.getFocusedWindow()?.maximize();
}

export function closeWindow() {
  BrowserWindow?.getFocusedWindow()?.close();
}
