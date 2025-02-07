import { FC, Fragment, useRef } from "react";
import { useTranslation } from "react-i18next";
import { useAppContext } from "../Context/AppContext";
import { showErrorToast, showSuccessToast } from "../utils/Toaster";

const SettingsPanel: FC = () => {
  const { setIsLoading, settings } = useAppContext();
  const { t } = useTranslation();
  const nameInputRef = useRef();

  async function updateName() {
    try {
      setIsLoading(true);
      //@ts-ignore
      const name: string = nameInputRef.current.value ?? "";

      // validate name
      if (name.length < 2 || name.length > 20) {
        showErrorToast(
          t("name must have at least 2 characters and most 20 characters")
        );
        return;
      }

      const settingsCopy = JSON.parse(JSON.stringify(settings));
      settingsCopy.name = name;
      // send to node process

      //@ts-ignore
      const response = await window.settingsAPI.update(settingsCopy);

      console.log(response);

      showSuccessToast(t("Name Updated"));
      console.log(settingsCopy);
    } catch (err) {
      console.log(err);
      showErrorToast(t("Error Occured"));
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <Fragment>
      <div className="flex-1 text-white  bg-gray-600 border-t-4 border-gray-900 p-8">
        <div className="m-auto flex flex-col align-middle justify-center">
          <input
            type="text"
            placeholder={t("Enter your name")}
            ref={nameInputRef}
            className="mx-auto w-full my-2 sm:w-80 lg:w-[500px] xl:w-[800px] p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          ></input>
          <button
            onClick={updateName}
            className="mx-auto my-2 cursor-pointer w-full sm:w-80 p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            {t("Change name")}
          </button>
        </div>
      </div>
    </Fragment>
  );
};

export default SettingsPanel;
