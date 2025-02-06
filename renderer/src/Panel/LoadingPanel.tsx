import React, { FC, Fragment } from "react";
import Spinner from "../Components/Spinner";

const LoadingPanel: FC = () => {
  return (
    <Fragment>
      <div className="fixed inset-0 bg-amber-50 flex items-center justify-center">
        <Spinner />
      </div>
    </Fragment>
  );
};

export default LoadingPanel;
