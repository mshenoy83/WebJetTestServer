using System;
using System.Collections.Generic;

namespace WebjetTestServer.Dto
{
    public class MovieSearchDto
    {
        public MovieSearchDto()
        {
            Movies = new List<MovieDto>();
        }
        public List<MovieDto> Movies { get; set; }
    }
}
