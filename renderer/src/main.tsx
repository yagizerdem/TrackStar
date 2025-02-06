import { createRoot } from "react-dom/client";
import "./index.css";
import "./globals.css";
import "toastify-js/src/toastify.css";
import App from "./App.tsx";
import { AppProvider } from "./Context/AppContext.tsx";
import i18next from "i18next";
import { I18nextProvider } from "react-i18next";
import en from "./assets/Localization/en.json";
import fr from "./assets/Localization/fr.json";

Main();

async function Main() {
  // fetch language
  //@ts-ignore
  const response = await window.settingsAPI.get();

  let language = "en"; // default
  if (response.isSuccess) {
    language = response.data.language;
  }

  i18next.init(
    {
      lng: language, // if you're using a language detector, do not define the lng option
      debug: true,
      resources: {
        en: { translation: en },
        fr: { translation: fr },
      },
    },
    function () {
      createRoot(document.getElementById("root")!).render(
        <I18nextProvider i18n={i18next}>
          <AppProvider>
            <App />
          </AppProvider>
        </I18nextProvider>
      );
    }
  );
}
