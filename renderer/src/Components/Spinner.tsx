import React, { FC } from "react";

const Spinner: FC = () => {
  const loaderStyle: React.CSSProperties = {
    width: "48px",
    height: "48px",
    border: "3px solid #FFF",
    borderRadius: "50%",
    display: "inline-block",
    position: "relative",
    boxSizing: "border-box",
    animation: "rotation 1s linear infinite",
  };

  const loaderAfterStyle: React.CSSProperties = {
    content: "''",
    boxSizing: "border-box",
    position: "absolute",
    left: "50%",
    top: "50%",
    transform: "translate(-50%, -50%)",
    width: "56px",
    height: "56px",
    borderRadius: "50%",
    border: "3px solid transparent",
    borderBottomColor: "#FF3D00",
  };

  return (
    <span style={loaderStyle}>
      <span style={loaderAfterStyle}></span>
      <style>
        {`
          @keyframes rotation {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
          }
        `}
      </style>
    </span>
  );
};

export default Spinner;
