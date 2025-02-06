import React, {
  createContext,
  useState,
  ReactNode,
  useContext,
  useEffect,
} from "react";
import Settings from "../models/Settings";
import HomePanel from "../Panel/HomePanel";
import { showSuccessToast } from "../utils/Toaster";

interface AppContextType {
  isLoading: boolean;
  setIsLoading: (isloading: boolean) => void;
  settings: Settings | null;
  setSettings: (settings: Settings) => void;
  activePanel: React.ReactElement;
  setActivePanel: (panel: React.ReactElement) => void;
}

const AppContext = createContext<AppContextType | null>(null);

interface AppProviderProps {
  children: ReactNode;
}

export const AppProvider: React.FC<AppProviderProps> = ({ children }) => {
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [settings, setSettings] = useState<Settings | null>(null);
  const [activePanel, setActivePanel] = useState<React.ReactElement>(
    <HomePanel />
  );

  // initilze app
  useEffect(() => {
    async function helper() {
      try {
        setIsLoading(true);
        //@ts-ignore
        const settingsResponse = await window.settingsAPI.get();

        setSettings(settingsResponse.data);

        // ensure 1 time render
        if (
          JSON.stringify(settingsResponse.data) !== JSON.stringify(settings)
        ) {
          setSettings(settingsResponse.data);
          showSuccessToast(`Welcome ${settingsResponse.data.name ?? ""}`);
        }
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    }
    helper();
  }, []);

  return (
    <AppContext.Provider
      value={{
        isLoading,
        setIsLoading,
        settings,
        setSettings,
        activePanel,
        setActivePanel,
      }}
    >
      {children}
    </AppContext.Provider>
  );
};

export const useAppContext = (): AppContextType => {
  const context = useContext(AppContext);
  if (!context) {
    throw new Error("useAppContext must be used within an AppProvider");
  }
  return context;
};
