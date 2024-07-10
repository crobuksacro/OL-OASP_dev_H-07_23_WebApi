using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;
using OL_OASP_dev_H_07_23_WebApi.Services.Interfaces;

namespace OL_OASP_dev_H_07_23_WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IValidator<MovieUpdateBinding> _movieUpdateBindingValidator;
        private readonly IValidator<ActorBinding> _actorBindingValidation;

        public MovieController(IMoviesService moviesService,
            IValidator<MovieUpdateBinding> movieUpdateBindingValidator,
            IValidator<ActorBinding> actorBindingValidation)
        {
            _moviesService = moviesService;
            _movieUpdateBindingValidator = movieUpdateBindingValidator;
            _actorBindingValidation = actorBindingValidation;
        }

        /// <summary>
        /// Gets a movie by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieViewModel>> Get(int id)
        {

            var movie = await _moviesService.GetAsync(id);
            return Ok(movie);

        }

        /// <summary>
        /// Adds a movie to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieViewModel>> Add([FromBody] MovieBinding model)
        {
            var movie = _moviesService.Add(model);
            return Ok(movie);
        }
        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="model">
        /// The movie update data
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieViewModel>> Update([FromBody] MovieUpdateBinding model)
        {
            var result = await _movieUpdateBindingValidator.ValidateAsync(model);
            if (result.IsValid)
            {
                var movie = _moviesService.Update(model);
                return Ok(movie);
            }

            return BadRequest(result.ToDictionary());
        }
        /// <summary>
        /// Deletes a movie by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieViewModel>> Delete(int id)
        {
            await _moviesService.Delete(id);
            return Ok(new { Poruka = $"Film sa idom {id} je uspješno obrisan!" });
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        [HttpGet("movies")]
        [ProducesResponseType(typeof(List<MovieViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MovieViewModel>>> GetMovies()
        {
            var movie = await _moviesService.GetMovies();
            return Ok(movie);
        }

        /// <summary>
        /// Get all actors
        /// </summary>
        /// <returns></returns>
        [HttpGet("actors")]
        [ProducesResponseType(typeof(List<ActorViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ActorViewModel>>> GetActors()
        {
            var actor = await _moviesService.GetActors();
            return Ok(actor);
        }
        /// <summary>
        /// Deletes an actor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("actor/{id}")]
        [ProducesResponseType(typeof(ActorViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<ActorViewModel>> DeleteActor(int id)
        {
            await _moviesService.DeleteActor(id);
            return Ok(new { Poruka = $"Film sa idom {id} je uspješno obrisan!" });
        }
        /// <summary>
        /// Updates an actor in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("actor")]
        [ProducesResponseType(typeof(ActorViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<ActorViewModel>> Update([FromBody] ActorUpdateBinding model)
        {
            var actor = _moviesService.UpdateActor(model);
            return Ok(actor);
        }
        /// <summary>
        /// Adds an actor to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("actor")]
        [ProducesResponseType(typeof(ActorViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<ActorViewModel>> Add([FromBody] ActorBinding model)
        {
            var result = await _actorBindingValidation.ValidateAsync(model);
            if (result.IsValid)
            {
                var actor = _moviesService.AddActor(model);
                return Ok(actor);
            }
            return BadRequest(result.ToDictionary());
        }
    }
}
