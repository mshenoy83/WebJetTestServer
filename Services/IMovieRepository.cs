using System;
using System.Threading.Tasks;
using WebjetTestServer.Dto;

namespace WebjetTestServer.Services
{
    public interface IMovieRepository
    {
        Task<MovieSearchDto> SearchMovies();

        Task<MovieDetailsDto> GetMovieDetails(string Id);
    }
}
