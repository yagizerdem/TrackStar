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

export async function fetchMoviesWithPaginationTMDB(
  query: string,
  page: number
) {
  const url = `https://api.themoviedb.org/3/search/movie?api_key=${SD.TMDbAPIKey}&query=${query}&page=${page}`;
  const response = await axios.get(url);
  return response.data;
}

export async function fetchSeriesWithPaginationTMDB(
  query: string,
  page: number
) {
  const response = await axios.get("https://api.themoviedb.org/3/search/tv", {
    params: {
      api_key: SD.TMDbAPIKey,
      query: query,
      page: page,
      include_adult: true,
      language: "en-US", // Set language
    },
  });
  return response.data;
}

export async function fetchSereisDetailsByIdTMDB(id: number) {
  const TMDB_API_KEY = SD.TMDbAPIKey;
  const TMDB_BASE_URL = "https://api.themoviedb.org/3";
  const [detailsResponse, externalIdsResponse] = await Promise.all([
    axios.get(`${TMDB_BASE_URL}/tv/${id}`, {
      params: { api_key: TMDB_API_KEY, language: "en-US" },
    }),
    axios.get(`${TMDB_BASE_URL}/tv/${id}/external_ids`, {
      params: { api_key: TMDB_API_KEY },
    }),
  ]);

  const merged = {
    ...detailsResponse.data,
    imdb_id: externalIdsResponse.data.imdb_id || null, // Merge IMDb ID into details
  };

  return merged;
}

export async function fetchSeriesOMDB(id: string) {
  const response = await axios.get("https://www.omdbapi.com/", {
    params: {
      apikey: SD.omdbAPIKey,
      i: id,
      type: "series",
    },
  });
  return response.data;
}
