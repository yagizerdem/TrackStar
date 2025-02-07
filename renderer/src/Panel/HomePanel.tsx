import { FC, Fragment, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { showErrorToast } from "../utils/Toaster";
import SD from "../utils/SD";
import { OmdbApiResponseMovie } from "../ApiResponse/OmdbApiResponse/OmdbApiResponseMovie";
import Spinner from "../Components/Spinner";
import { TMDbApiResponseWithGenres } from "../ApiResponse/TMDbApiResponse/TMDbApiResponseWithGenres";
import { TMDbApiResponseMovieDetails } from "../ApiResponse/TMDbApiResponse/TMDbApiResponseMovieDetails";
import {
  fetchMoviesTMDB,
  getMovieDetailByIdTMDB,
  getMovieDetailByIdOMDB,
} from "../utils/NetworkRelated";
import { CSSTransition } from "react-transition-group";
import "../RightSidebar.css";

const HomePanel: FC = () => {
  const { t } = useTranslation();
  const [selectedGenre, setSelectedGenre] = useState(() => {
    return SD.genres[Math.floor(Math.random() * SD.genres.length)];
  });
  const [isLoading, setIsLoading] = useState(false);
  const [movies, setMovies] = useState<Array<TMDbApiResponseWithGenres>>([]); // fetched over network - TMDB api
  const [mainMovie, setMainMovie] = useState<OmdbApiResponseMovie | null>(null); // fetched over network OMDB api
  const [isOpen, setIsOpen] = useState(false);

  useEffect(() => {
    return;
    async function helper() {
      // fetch movies for diplay
      try {
        setIsLoading(true);
        const data: Array<TMDbApiResponseWithGenres> = (
          await fetchMoviesTMDB(selectedGenre.id)
        ).results;

        const newList = data.map((rawJson) =>
          TMDbApiResponseWithGenres.fromJson(rawJson)
        );

        // shuffle
        newList.sort(() => Math.random() * 100);

        const first: TMDbApiResponseWithGenres = newList[0];

        const movieDetailJson = await getMovieDetailByIdTMDB(first.id);
        // instance from raw json
        const movieDetail: TMDbApiResponseMovieDetails =
          TMDbApiResponseMovieDetails.fromJson(movieDetailJson);

        const omdbId = movieDetail.imdb_id;

        const movieDataJson = await getMovieDetailByIdOMDB(omdbId);

        const omdbMovie: OmdbApiResponseMovie =
          OmdbApiResponseMovie.fromJson(movieDataJson);

        // update states a
        setMovies(newList);
        setMainMovie(omdbMovie);
      } catch (err) {
        console.log(err);
        showErrorToast(t("Error Occured"));
      } finally {
        setIsLoading(false);
      }
    }
    helper();
  }, [selectedGenre, t]);

  // helper function - TMDB api

  return (
    <Fragment>
      <div className="flex-1 text-white bg-gray-600 border-t-4 border-gray-900 p-8">
        {isLoading && (
          <div className="flex-1 h-full w-full items-center align-middle justify-center">
            <Spinner />
          </div>
        )}
        {!isLoading && (
          <Fragment>
            {/* main */}
            <p>poster here</p>

            {/* right side bar  */}
            <div className="relative">
              {/* Toggle Button */}
              <button
                className={`fixed cursor-pointer top-5 right-5 z-50 bg-blue-600 text-white px-4 py-2 rounded-md shadow-lg transition-transform duration-300 ${
                  isOpen ? "-translate-x-64" : "translate-x-0"
                }`}
                onClick={() => setIsOpen(!isOpen)}
              >
                {isOpen ? "Close" : "Other Suggestions"}
              </button>

              {/* Sidebar */}
              <CSSTransition
                in={isOpen}
                timeout={300}
                classNames="sidebar"
                unmountOnExit
              >
                <div className="fixed top-0 right-0 h-full w-64 bg-gray-800 text-white shadow-lg p-4">
                  <h2 className="text-xl font-bold mb-4">Sidebar Content</h2>
                  <p>This is a toggleable sidebar.</p>
                </div>
              </CSSTransition>
            </div>
          </Fragment>
        )}
      </div>
    </Fragment>
  );
};

export default HomePanel;
