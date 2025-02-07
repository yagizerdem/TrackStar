import axios from "axios";
import SD from "../utils/SD";

export async function fetchMoviesTMDB(genreId: number) {
  const url = `https://api.themoviedb.org/3/discover/movie?api_key=${
    SD.TMDbAPIKey
  }&with_genres=${genreId}&language=en-US&page=${
    Math.floor(Math.random() * 10) + 1
  }`;
  const response = await axios.get(url);
  return response.data;
}

export async function getMovieDetailByIdTMDB(movieId: number) {
  const url = `https://api.themoviedb.org/3/movie/${movieId}?api_key=${SD.TMDbAPIKey}`;
  const response = await axios.get(url);
  return response.data;
}
export async function getMovieDetailByIdOMDB(movieId: string) {
  const url = `http://www.omdbapi.com/?apikey=${SD.omdbAPIKey}&i=${movieId}`;
  const response = await axios.get(url);
  return response.data;
}
