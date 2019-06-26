using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebjetTestServer.Dto;
using WebjetTestServer.Services;

namespace WebjetTestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieSearchController : ControllerBase
    {
        private readonly IEnumerable<IMovieRepository> _movieRepositories;

        public MovieSearchController(IEnumerable<IMovieRepository> movieRepositories)
        {
            _movieRepositories = movieRepositories;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetMovieList()
        {
            var taskList = new List<Task<MovieSearchDto>>();
            foreach(var repo in _movieRepositories)
                {

                taskList.Add(repo.SearchMovies());
                }
            await Task.WhenAll(taskList);

            var movieSearch = new MovieSearchDto();

            foreach(var tsk in taskList)
                {
                var result = await tsk;
                if(result.Movies.Any())
                    {
                    movieSearch.Movies.AddRange(result.Movies);
                    }
                }
            return Ok(movieSearch);
        }

    }
}
