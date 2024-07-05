﻿using Microsoft.AspNetCore.Mvc;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;
using OL_OASP_dev_H_07_23_WebApi.Services.Interfaces;

namespace OL_OASP_dev_H_07_23_WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MovieController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
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
            var movie = _moviesService.Update(model);
            return Ok(movie);
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
    }
}