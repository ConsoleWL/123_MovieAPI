using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.FileProviders;
using MovieAPI.Data;
using MovieAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMovie()
        {
            List<Models.Movie> movies = _context.Movies.ToList();
            return StatusCode(200, movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            Movie? movie = _context.Movies.Find(id);
            if (movie == null)
                return NotFound();

            return StatusCode(200, movie);
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return StatusCode(201, movie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] Movie movieUpdate)
        {
            if (movieUpdate == null)
                return BadRequest();

            Movie? movie = _context.Movies.Find(id);

            if (movie == null)
                return NotFound();
            else
            {
                movie.Title = movieUpdate.Title;
                movie.Genre = movieUpdate.Genre;
                movie.RunningTime = movieUpdate.RunningTime;
            }

            _context.SaveChanges();
            return StatusCode(200, movie);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovieById(int id)
        {
            Movie? movieToDelete = _context.Movies.Find(id);

            if (movieToDelete == null)
                return NotFound();

            _context.Movies.Remove(movieToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
