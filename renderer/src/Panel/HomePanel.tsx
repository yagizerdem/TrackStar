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
import emptyImage from "../assets/empty.jpeg";
import GradientShadowBox from "../Components/GradientShadowBox";
import { Container, Row, Col, setConfiguration } from "react-grid-system";
setConfiguration({ maxScreenClass: "xxl" });

const HomePanel: FC = () => {
  const { t } = useTranslation();
  const [selectedGenre, setSelectedGenre] = useState(() =>
    SD.genres.length > 0
      ? SD.genres[Math.floor(Math.random() * SD.genres.length)]
      : null
  );
  const [isLoading, setIsLoading] = useState(false);
  const [movies, setMovies] = useState<Array<TMDbApiResponseWithGenres>>([]); // fetched over network - TMDB api
  const [mainMovie, setMainMovie] = useState<OmdbApiResponseMovie | null>(null); // fetched over network OMDB api
  const [isOpen, setIsOpen] = useState(false);

  useEffect(() => {
    initilize();
  }, [selectedGenre, t]);

  function initilize() {
    async function helper() {
      // fetch movies for diplay
      try {
        setIsLoading(true);
        const data: Array<TMDbApiResponseWithGenres> = (
          await fetchMoviesTMDB(selectedGenre!.id)
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
  }

  function changeGenre(genre: string) {
    // find genre
    const newGenre = SD.genres.find((g) => g.name == genre) ?? null;
    setSelectedGenre(newGenre);
  }

  useEffect(() => {
    console.log(movies);
  }, [movies]);

  return (
    <div className="flex flex-1 w-full h-full overflow-y-scroll  bg-gray-600 border-t-4 border-gray-900 ">
      <div className="flex-1 text-white flex flex-col ">
        {isLoading && (
          <div className="flex-1 h-full w-full items-center align-middle justify-center">
            <Spinner />
          </div>
        )}
        {!isLoading && (
          <Fragment>
            {/* main */}
            <div
              className="flex-row  h-fit w-fit flex-1"
              style={{
                padding: 10,
              }}
            >
              <div className="flex flex-row">
                <GradientShadowBox>
                  <img
                    src={mainMovie?.Poster ?? emptyImage}
                    className="gradient-shadow sm:h-52  md:h-72 lg:h-96 rounded-3xl"
                  ></img>
                </GradientShadowBox>
                <div style={{ margin: "0 30px" }}>
                  <ul>
                    <li>Title : {mainMovie?.Title ?? ""}</li>
                    <li>Year : {mainMovie?.Year ?? ""}</li>
                    <li>Rated : {mainMovie?.Rated ?? ""}</li>
                    <li>Released : {mainMovie?.Released ?? ""}</li>
                    <li>Runtime : {mainMovie?.Runtime ?? ""}</li>
                    <li>Genre : {mainMovie?.Genre ?? ""}</li>
                    <li>Director : {mainMovie?.Director ?? ""}</li>
                    <li>Writer : {mainMovie?.Writer ?? ""}</li>
                    <li>Language : {mainMovie?.Language ?? ""}</li>
                    <li>Country : {mainMovie?.Country ?? ""}</li>
                    <li>Awards : {mainMovie?.Awards ?? ""}</li>
                    <li>imdbRating : {mainMovie?.imdbRating ?? ""}</li>
                    <li>imdbVotes : {mainMovie?.imdbVotes ?? ""}</li>
                    <li>Website : {mainMovie?.Website ?? ""}</li>
                  </ul>
                </div>
              </div>
            </div>
            <div className="w-full  mt-auto">
              <Container
                style={{
                  textAlign: "center",
                  justifyContent: "center",
                  alignContent: "center",
                }}
              >
                <Row>
                  <Col
                    sm={4}
                    className="cursor-pointer flex  flex-1"
                    style={{
                      fontWeight: "700",
                      padding: 30,
                    }}
                  >
                    <div
                      className="w-full h-full flex-1 "
                      style={{
                        background: "#7CB9E8",
                        padding: 50,
                      }}
                      onClick={() => changeGenre(SD.genres[0].name)}
                    >
                      {SD.genres[0].name}
                    </div>
                  </Col>
                  <Col
                    sm={4}
                    className="cursor-pointer flex  flex-1"
                    style={{
                      fontWeight: "700",
                      padding: 30,
                    }}
                  >
                    <div
                      className="w-full h-full flex-1 "
                      style={{
                        background: "#fd5c63",
                        padding: 50,
                      }}
                      onClick={() => changeGenre(SD.genres[1].name)}
                    >
                      {SD.genres[1].name}
                    </div>
                  </Col>
                  <Col
                    sm={4}
                    className="cursor-pointer flex  flex-1"
                    style={{
                      fontWeight: "700",
                      padding: 30,
                    }}
                  >
                    <div
                      className="w-full h-full flex-1 "
                      style={{
                        background: "#DDA0DD",
                        padding: 50,
                      }}
                      onClick={() => changeGenre(SD.genres[2].name)}
                    >
                      {SD.genres[2].name}
                    </div>
                  </Col>
                </Row>
                <br></br>

                <Row
                  style={{
                    padding: 20,
                  }}
                >
                  <Col
                    sm={4}
                    className="cursor-pointer flex  flex-1"
                    style={{
                      fontWeight: "700",
                      padding: 30,
                    }}
                  >
                    <div
                      className="w-full h-full flex-1 "
                      style={{
                        background: "#662d91",
                        padding: 50,
                      }}
                      onClick={() => changeGenre(SD.genres[3].name)}
                    >
                      {SD.genres[3].name}
                    </div>
                  </Col>
                  <Col
                    sm={4}
                    className="cursor-pointer flex  flex-1"
                    style={{
                      fontWeight: "700",
                      padding: 30,
                    }}
                  >
                    <div
                      className="w-full h-full flex-1 "
                      style={{
                        background: "#006A4E",
                        padding: 50,
                      }}
                      onClick={() => changeGenre(SD.genres[4].name)}
                    >
                      {SD.genres[4].name}
                    </div>
                  </Col>
                  <Col
                    sm={4}
                    className="cursor-pointer flex  flex-1"
                    style={{
                      fontWeight: "700",
                      padding: 30,
                    }}
                  >
                    <div
                      className="w-full h-full flex-1 "
                      style={{
                        background: "#eedc82",
                        padding: 50,
                      }}
                      onClick={() => changeGenre(SD.genres[5].name)}
                    >
                      {SD.genres[5].name}
                    </div>
                  </Col>
                </Row>
              </Container>
            </div>

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
                  <ul className="overflow-y-auto max-h-full text-white">
                    {movies.map((m: TMDbApiResponseWithGenres) => (
                      <div className="uk-card uk-card-body" key={m.id}>
                        <h3
                          className="uk-card-title text-white"
                          style={{ color: "white" }}
                        >
                          {m.title}
                        </h3>
                        <img
                          src={`https://image.tmdb.org/t/p/w500/${m.poster_path}`}
                          alt={m.title}
                        />
                        <hr />
                      </div>
                    ))}
                  </ul>
                </div>
              </CSSTransition>
            </div>
          </Fragment>
        )}
      </div>
    </div>
  );
};

export default HomePanel;
