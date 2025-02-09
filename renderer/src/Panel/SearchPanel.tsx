import { FC, useState } from "react";
import { ToggleSlider } from "react-toggle-slider";
import {
  fetchMoviesWithPaginationTMDB,
  fetchSereisDetailsByIdTMDB,
  fetchSeriesOMDB,
  fetchSeriesWithPaginationTMDB,
  getMovieDetailByIdOMDB,
  getMovieDetailByIdTMDB,
} from "../utils/NetworkRelated";
import { TMDbApiResponseWithGenres } from "../ApiResponse/TMDbApiResponse/TMDbApiResponseWithGenres";
import { showErrorToast, showSuccessToast } from "../utils/Toaster";
import Spinner from "../Components/Spinner";
import { TMDbApiResponseSeriesWithGenres } from "../ApiResponse/TMDbApiResponse/TMDbApiResponseSeriesWithGenres";
import { TMDbApiResponseMovieDetails } from "../ApiResponse/TMDbApiResponse/TMDbApiResponseMovieDetails";
import { OmdbApiResponseMovie } from "../ApiResponse/OmdbApiResponse/OmdbApiResponseMovie";
import { TMDbApiResponseSeriesDetails } from "../ApiResponse/TMDbApiResponse/TMDbApiResponseSeriesDetails";
import { OmdbApiResponseSeries } from "../ApiResponse/OmdbApiResponse/OmdbApiResponseSeries";
import { Series } from "../models/Series";
import { Movies } from "../models/Movies";

enum Media {
  Film,
  Series,
}
enum ViewMode {
  List,
  Grid,
}

const SearchPanel: FC = () => {
  const [inputValue, setInputValue] = useState(""); // Persist input value
  const [media, setMedia] = useState(Media.Film);
  const [view, setView] = useState(ViewMode.List);
  const [page, setPage] = useState<number>(1); // page starts at 1 and  max 500
  const [filmData, setFilmData] = useState<Array<TMDbApiResponseWithGenres>>(
    []
  );
  const [seriesData, setSeriesData] = useState<
    Array<TMDbApiResponseSeriesWithGenres>
  >([]);
  const [isLoading, setIsLoading] = useState(false);

  async function search() {
    try {
      setIsLoading(true);
      const mediaName: string = inputValue;

      if (media == Media.Film) {
        const data: Array<TMDbApiResponseWithGenres> =
          await fetchMoviesWithPaginationTMDB(mediaName, 1);

        console.log(data);

        if (data.results.length == 0) {
          showErrorToast("no data exist ...");
          return;
        }

        const newFilmdData = data.results.map((item) =>
          TMDbApiResponseWithGenres.fromJson(item)
        );

        // refreash
        setFilmData(() => [...newFilmdData]);
        setSeriesData([]);
        setPage(1);
      }

      if (media == Media.Series) {
        const data = await fetchSeriesWithPaginationTMDB(mediaName, 1);

        if (data.results.length == 0) {
          showErrorToast("no series exist ...");
          return;
        }

        const seriesList: Array<TMDbApiResponseSeriesWithGenres> =
          data.results.map((item: any) =>
            TMDbApiResponseSeriesWithGenres.fromJSON(item)
          );

        console.log(seriesList);

        setSeriesData(() => [...seriesList]);
        setFilmData([]);
        setPage(1);
      }
    } catch (err) {
      console.log(err);
      showErrorToast("error occurecd");
    } finally {
      setIsLoading(false);
    }
  }

  async function LoadMore() {
    try {
      setIsLoading(true);
      const mediaName: string = inputValue;

      if (media == Media.Film) {
        const data: Array<TMDbApiResponseWithGenres> =
          await fetchMoviesWithPaginationTMDB(mediaName, page + 1);

        console.log(data);

        if (data.results.length == 0) {
          showErrorToast("all data fetched ...");
          return;
        }

        const newFilmdData = data.results.map((item) =>
          TMDbApiResponseWithGenres.fromJson(item)
        );

        setFilmData((prev) => [...prev, ...newFilmdData]);
        setPage((prev) => prev + 1);
      }

      if (media == Media.Series) {
        const data: Array<TMDbApiResponseWithGenres> =
          await fetchSeriesWithPaginationTMDB(mediaName, page + 1);

        console.log(data);

        if (data.results.length == 0) {
          showErrorToast("all data fetched ...");
          return;
        }

        const newSeriesData = data.results.map((item: any) =>
          TMDbApiResponseSeriesWithGenres.fromJSON(item)
        );

        setSeriesData((prev) => [...prev, ...newSeriesData]);
        setPage((prev) => prev + 1);
      }
    } catch (err) {
      console.log(err);
      showErrorToast("error occured");
    } finally {
      setIsLoading(false);
    }
  }

  async function InsertToStarred(mediaType: Media, id: number) {
    try {
      setIsLoading(true);
      if (mediaType == Media.Film) {
        const detailsRaw = await getMovieDetailByIdTMDB(id);
        const details: TMDbApiResponseMovieDetails =
          TMDbApiResponseMovieDetails.fromJson(detailsRaw);
        const imdb_id: string = details.imdb_id;

        const movieRaw = await getMovieDetailByIdOMDB(imdb_id);
        const movie: OmdbApiResponseMovie =
          OmdbApiResponseMovie.fromJson(movieRaw);

        const moviesEntity: Movies = new Movies();
        Object.assign(moviesEntity, movie);

        //store in db
        console.log(moviesEntity);
        const response = await window.moviesAPI.add(moviesEntity);

        if (response.isSuccess) {
          showSuccessToast(response.message);
        } else {
          showErrorToast(response.message);
        }
      }

      if (media == Media.Series) {
        const detailsRaw = await fetchSereisDetailsByIdTMDB(id);
        const details: TMDbApiResponseSeriesDetails =
          TMDbApiResponseSeriesDetails.fromJson(detailsRaw);

        const seriesRaw = await fetchSeriesOMDB(details.imdb_id ?? "");
        const series: OmdbApiResponseSeries =
          OmdbApiResponseSeries.fromJson(seriesRaw);

        // map to entity
        const seriesEntity: Series = new Series();
        Object.assign(seriesEntity, series);

        //store in db
        console.log(seriesEntity);
        const response = await window.seriesAPI.add(seriesEntity);

        console.log(response);

        if (response.isSuccess) {
          showSuccessToast(response.message);
        } else {
          showErrorToast(response.message);
        }
      }
    } catch (err) {
      console.log(err);
      showErrorToast("error occured");
    } finally {
      setIsLoading(false);
    }
  }

  if (isLoading) {
    return (
      <div
        className="flex flex-col flex-1 text-white  bg-gray-600 border-t-4 border-gray-900 "
        style={{
          padding: "10px",
        }}
      >
        <Spinner></Spinner>
      </div>
    );
  }

  return (
    <div
      className="flex flex-col flex-1 text-white  bg-gray-600 border-t-4 border-gray-900 "
      style={{
        padding: "10px",
      }}
    >
      <div className="flex flex-col w-full  h-52  ">
        <div className="flex flex-row items-center justify-center align-middle">
          <input
            placeholder={`search for ${
              media == Media.Film ? "films" : "series"
            }`}
            value={inputValue} // Bind value to state
            onChange={(e) => setInputValue(e.target.value)} // Update state on change
            defaultValue={inputValue} // Keeps value persistent
            className="bg-white p-1 rounded-sm"
            style={{
              color: "black",
              width: "70%",
            }}
          ></input>
        </div>
        <button
          type="button"
          className="cursor-pointer btn btn-dark w-24 md:w-52 lg:w-80 block mx-auto "
          style={{
            marginTop: "20px",
          }}
          onClick={search}
        >
          Search
        </button>
        <div
          className="flex flex-row"
          style={{
            margin: "20px 0",
          }}
        >
          <div style={{ margin: "0 30px" }}>
            <ToggleSlider
              onToggle={() =>
                setMedia((prev) =>
                  prev == Media.Film ? Media.Series : Media.Film
                )
              }
              active={media == Media.Film ? true : false}
            />
            <br></br>
            <p>
              {media == Media.Film
                ? "switch to search series"
                : "switch to search films"}
            </p>
          </div>
          <div style={{ margin: "0 30px" }}>
            <ToggleSlider
              onToggle={() =>
                setView((prev) =>
                  prev == ViewMode.List ? ViewMode.Grid : ViewMode.List
                )
              }
              active={view == ViewMode.List ? true : false}
            />
            <br></br>
            <p>{view == ViewMode.List ? "List View" : "Grid View"}</p>
          </div>
        </div>
      </div>

      {view == ViewMode.List && media == Media.Film && (
        <div className="flex-1 flex flex-col overflow-y-scroll">
          {/* Fixed Height Button */}

          {/* Scrollable List (Takes Remaining Height) */}
          <ul>
            {filmData.map((data: TMDbApiResponseWithGenres) => (
              <li
                key={data.id}
                style={{
                  marginTop: "20px",
                }}
              >
                <div className="card">
                  <div className="card-header">
                    release date :
                    {data.release_date.trim() == ""
                      ? "N/A"
                      : data.release_date.trim()}
                  </div>
                  <div className="card-body">
                    <h5 className="card-title">{data.title}</h5>
                    <img
                      src={`https://image.tmdb.org/t/p/w500/${data.poster_path}`}
                      className="w-52"
                    ></img>
                    <p className="card-text">{data.overview}</p>
                    <a
                      href="#"
                      className="btn btn-primary"
                      onClick={() => InsertToStarred(Media.Film, data.id)}
                    >
                      Add to starred
                    </a>
                  </div>
                </div>
              </li>
            ))}
          </ul>
        </div>
      )}

      {view == ViewMode.Grid && media == Media.Film && (
        <div className="flex-1 flex flex-col overflow-y-scroll  bg-gray-500 ">
          <div
            className="flex-1 flex flex-wrap items-center  justify-center align-middle"
            style={{}}
          >
            {filmData.map((data: TMDbApiResponseWithGenres) => {
              return (
                <div
                  className="card !w-96 !h-[400px] overflow-y-scroll"
                  style={{ margin: "30px" }}
                >
                  <img
                    src={`https://image.tmdb.org/t/p/w500/${data.poster_path}`}
                    className="w-52"
                  ></img>
                  <div className="card-body">
                    <h5 className="card-title">{data.title}</h5>
                    <p className="card-text">{data.overview}</p>
                    <a
                      href="#"
                      className="btn btn-primary"
                      onClick={() => InsertToStarred(Media.Film, data.id)}
                    >
                      Add to starred
                    </a>
                  </div>
                </div>
              );
            })}
          </div>

          {/* {filmData.map((data) => {
            return (
              <Fragment>
                <div
                  className="card"
                  style={{
                    width: "18ram",
                  }}
                />
                <img className="card-img-top" src="..." alt="Card image cap" />
                <div className="card-body">
                  <h5 className="card-title">Card title</h5>
                  <p className="card-text">
                    Some quick example text to build on the card title and make
                    up the bulk of the card's content.
                  </p>
                  <a href="#" className="btn btn-primary">
                    Go somewhere
                  </a>
                </div>
              </Fragment>
            );
          })} */}
        </div>
      )}

      {view == ViewMode.List && media == Media.Series && (
        <div className="flex-1 flex flex-col overflow-y-scroll">
          {/* Fixed Height Button */}

          {/* Scrollable List (Takes Remaining Height) */}
          <ul>
            {seriesData.map((data: TMDbApiResponseSeriesWithGenres) => (
              <li
                key={data.id}
                style={{
                  marginTop: "20px",
                }}
              >
                <div className="card">
                  <div className="card-header">
                    release date :
                    {data.first_air_date.trim() == ""
                      ? "N/A"
                      : data.first_air_date.trim()}
                  </div>
                  <div className="card-body">
                    <h5 className="card-title">{data.name}</h5>
                    <img
                      src={`https://image.tmdb.org/t/p/w500/${data.poster_path}`}
                      className="w-52"
                    ></img>
                    <p className="card-text">{data.overview}</p>
                    <a
                      href="#"
                      className="btn btn-primary"
                      onClick={() => InsertToStarred(Media.Series, data.id)}
                    >
                      Add to starred
                    </a>
                  </div>
                </div>
              </li>
            ))}
          </ul>
        </div>
      )}

      {view == ViewMode.Grid && media == Media.Series && (
        <div className="flex-1 flex flex-col overflow-y-scroll  bg-gray-500 ">
          <div
            className="flex-1 flex flex-wrap items-center  justify-center align-middle"
            style={{}}
          >
            {seriesData.map((data: TMDbApiResponseSeriesWithGenres) => {
              return (
                <div
                  className="card !w-96 !h-[400px] overflow-y-scroll"
                  style={{ margin: "30px" }}
                >
                  <img
                    src={`https://image.tmdb.org/t/p/w500/${data.poster_path}`}
                    className="w-52"
                  ></img>
                  <div className="card-body">
                    <h5 className="card-title">{data.name}</h5>
                    <p className="card-text">{data.overview}</p>
                    <a
                      href="#"
                      className="btn btn-primary"
                      onClick={() => InsertToStarred(Media.Series, data.id)}
                    >
                      Add to starred
                    </a>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      )}

      {((media == Media.Film && filmData.length != 0) ||
        (media == Media.Series && seriesData.length != 0)) && (
        <button
          onClick={() => LoadMore()}
          className="cursor-pointer btn btn-dark w-52 mx-auto block "
          style={{ height: "50px" }} // Set a fixed height
        >
          Load More
        </button>
      )}
    </div>
  );
};

export default SearchPanel;
