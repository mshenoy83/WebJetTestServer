using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebjetTestServer.Dto;

namespace WebjetTestServer.Services
{
    public class FilmworldMovieRepository : IMovieRepository
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public FilmworldMovieRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MovieDetailsDto> GetMovieDetails(string Id)
        {
            try
            {
                var cinemaWorlClient = _httpClientFactory.CreateClient("FilmWorldClient");
                var response = await cinemaWorlClient.GetAsync("movie/fw" + Id);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<MovieDetailsDto>();
                }

                return new MovieDetailsDto();
            }
            catch (Exception ex)
            {
                return new MovieDetailsDto();
            }
        }

        public async Task<MovieSearchDto> SearchMovies()
        {
            try
            {
                var cinemaWorlClient = _httpClientFactory.CreateClient("FilmWorldClient");
                var response = await cinemaWorlClient.GetAsync("movies");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<MovieSearchDto>();
                }

                return new MovieSearchDto();
            }
            catch (Exception ex)
            {
                return new MovieSearchDto();
            }
        }
    }
}
