import { FC, useEffect, useState } from "react";
import { Movies } from "../models/Movies";
import { Series } from "../models/Series";
import { showErrorToast, showSuccessToast } from "../utils/Toaster";
import "../flipCard.css";
import Swal from "sweetalert2";

enum ViewMode {
  Films,
  Series,
}

const StarredPanel: FC = () => {
  const [moveis, setMovies] = useState<Array<Movies>>([]);
  const [series, setSerise] = useState<Array<Series>>([]);
  const [viewMode, setViewMode] = useState(ViewMode.Films);
  const [isHidden, setIsHidden] = useState(false);
  const [width, setWidth] = useState<number>(0);

  // initilze panel
  useEffect(() => {
    async function helper() {
      const moviesResponse = await window.moviesAPI.get();

      if (!moviesResponse.isSuccess) {
        showErrorToast("error occured while fetching movies");
      }

      const seriesResponse = await window.seriesAPI.get();

      if (!seriesResponse.isSuccess) {
        showErrorToast("error occured while fetching series");
      }

      setMovies(moviesResponse.data);
      setSerise(seriesResponse.data);

      showSuccessToast("moveis and seris fetched susccessfully from database");
    }
    helper();
  }, []);

  useEffect(() => {
    const handleResize = async () => {
      const { width } = await window.electron.ipcRenderer.invoke(
        "get-window-size"
      );
      setWidth(width);
      setIsHidden(width < 750);
    };

    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  function deleteMoviePopUp(imdbID: string) {
    Swal.fire({
      icon: "error",
      text: "Are you sure about deleting this media",
      confirmButtonText: "Delete",
      cancelButtonText: "Cancel",
      showCancelButton: true,
      preConfirm: function () {
        deleteMovie(imdbID);
      },
    });
  }

  function deleteSeriesPopUp(imdbID: string) {
    Swal.fire({
      icon: "error",
      text: "Are you sure about deleting this media",
      confirmButtonText: "Delete",
      cancelButtonText: "Cancel",
      showCancelButton: true,
      preConfirm: function () {
        deleteSeries(imdbID);
      },
    });
  }

  async function deleteMovie(imdbID: string) {
    const response = await window.moviesAPI.delete(imdbID);
    if (response.isSuccess) {
      showSuccessToast(response.message);
    } else {
      showErrorToast(response.message);
    }
    // remove from ui
    setMovies((prev) => prev.filter((item) => item.imdbID != imdbID));
  }

  async function deleteSeries(imdbID: string) {
    const response = await window.seriesAPI.delete(imdbID);
    if (response.isSuccess) {
      showSuccessToast(response.message);
    } else {
      showErrorToast(response.message);
    }
    // remove from ui
    setSerise((prev) => prev.filter((item) => item.imdbID != imdbID));
  }

  return (
    <div className="flex flex-col flex-1 bg-gray-600 border-t-4 border-gray-900 overflow-hidden">
      <h1
        style={{
          color: "#fff",
          fontWeight: "bold",
          margin: "10px 40px",
        }}
      >
        {viewMode == ViewMode.Films ? "Displaying Films" : "Displaying Series"}
      </h1>
      {/* Flip Card - Takes up remaining space */}
      <div className="flex-1 flex items-center justify-center">
        <div
          className={`relative w-full h-full transition-transform duration-700 transform ${
            viewMode == ViewMode.Series ? "rotate-y-180" : ""
          } preserve-3d`}
        >
          {/* Front Side */}
          <div className="absolute  w-full h-full bg-gray-500 text-white flex items-center justify-center  shadow-lg backface-hidden">
            <ul
              className="overflow-y-scroll flex flex-col flex-1 h-full w-full  p-4"
              style={{
                padding: "10px",
              }}
            >
              {moveis.map((m) => {
                return (
                  <div
                    className="card"
                    style={{
                      margin: "20px 0",
                    }}
                  >
                    <h5 className="card-header">{`Date : ${m.Year}`}</h5>
                    <div className="card-body">
                      <h2
                        className="card-title "
                        style={{
                          fontWeight: "bolder",
                        }}
                      >
                        {m.Title}
                      </h2>
                      <br></br>
                      <div className="flex flex-row">
                        <img src={m.Poster} className="w-52"></img>
                        <ul className={`${isHidden ? "hidden" : "block"}`}>
                          <li>Actors : {m.Actors}</li>
                          <li>Director : {m.Director}</li>
                          <li>Country : {m.Country}</li>
                          <li>Metascore : {m.Metascore}</li>
                          <li>Production : {m.Production}</li>
                          <li>Language : {m.Language}</li>
                          <li>Genre : {m.Genre}</li>
                          <li>Awards : {m.Awards}</li>
                          <li>DVD : {m.DVD}</li>
                        </ul>
                      </div>
                      <p className="card-text">
                        {width < 800 ? `${m.Plot.substring(0, 20)}...` : m.Plot}
                      </p>
                    </div>
                    <button
                      type="button"
                      className="btn btn-danger lg:w-96 sm:w-52"
                      style={{
                        margin: "10px auto",
                      }}
                      onClick={() => deleteMoviePopUp(m.imdbID)}
                    >
                      Delete
                    </button>
                  </div>
                );
              })}
            </ul>
          </div>

          {/* Back Side */}
          <div className="absolute w-full h-full bg-gray-500 text-white flex items-center justify-center shadow-lg rotate-y-180 backface-hidden">
            <div className="absolute  w-full h-full bg-gray-500 text-white flex items-center justify-center  shadow-lg backface-hidden">
              <ul
                className="overflow-y-scroll flex flex-col flex-1 h-full w-full  p-4"
                style={{
                  padding: "10px",
                }}
              >
                {series.map((m) => {
                  return (
                    <div
                      className="card"
                      style={{
                        margin: "20px 0",
                      }}
                    >
                      <h5 className="card-header">{`Date : ${m.Year}`}</h5>
                      <div className="card-body">
                        <h2
                          className="card-title "
                          style={{
                            fontWeight: "bolder",
                          }}
                        >
                          {m.Title}
                        </h2>
                        <br></br>
                        <div className="flex flex-row">
                          <img src={m.Poster} className="w-52"></img>
                          <ul className={`${isHidden ? "hidden" : "block"}`}>
                            <li>Actors : {m.Actors}</li>
                            <li>Director : {m.Director}</li>
                            <li>Country : {m.Country}</li>
                            <li>Metascore : {m.Metascore}</li>
                            <li>Language : {m.Language}</li>
                            <li>Genre : {m.Genre}</li>
                            <li>Awards : {m.Awards}</li>
                          </ul>
                        </div>
                        <p className="card-text">
                          {width < 800
                            ? `${m.Plot.substring(0, 20)}...`
                            : m.Plot}
                        </p>
                      </div>
                      <button
                        type="button"
                        className="btn btn-danger lg:w-96 sm:w-52"
                        style={{
                          margin: "10px auto",
                        }}
                        onClick={() => deleteSeriesPopUp(m.imdbID)}
                      >
                        Delete
                      </button>
                    </div>
                  );
                })}
              </ul>
            </div>
          </div>
        </div>
      </div>

      {/* Docked Flip Button */}
      <div className="p-4">
        <button
          onClick={() =>
            setViewMode(
              viewMode == ViewMode.Films ? ViewMode.Series : ViewMode.Films
            )
          }
          className="lg:w-96 sm:w-52  mx-auto block cursor-pointer  px-4 py-2 bg-gray-800 text-white shadow-lg hover:bg-gray-700 transition duration-300"
          style={{
            borderRadius: "6px",
          }}
        >
          Flip Card
        </button>
      </div>
    </div>
  );
};

export default StarredPanel;
