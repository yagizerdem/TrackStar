import React, { FC, Fragment, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useAppContext } from "../Context/AppContext";
import { showSuccessToast } from "../utils/Toaster";

const HomePanel: FC = () => {
  const { t } = useTranslation();
  const { settings } = useAppContext();

  return (
    <Fragment>
      <div className="flex-1 text-white bg-gray-800 border-t-4 border-gray-900 p-8">
        home panel
      </div>
    </Fragment>
  );
};

export default HomePanel;
