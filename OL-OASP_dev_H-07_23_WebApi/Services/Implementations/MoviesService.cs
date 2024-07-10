using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;
using OL_OASP_dev_H_07_23_WebApi.Context;
using OL_OASP_dev_H_07_23_WebApi.Models.Dbo;
using OL_OASP_dev_H_07_23_WebApi.Services.Interfaces;

namespace OL_OASP_dev_H_07_23_WebApi.Services.Implementations
{
    public class MoviesService : IMoviesService
    {
        public readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public MoviesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Adds a movie to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MovieViewModel Add(MovieBinding model)
        {
            var dbo = mapper.Map<Movie>(model);
            dbContext.Movies.Add(dbo);
            dbContext.SaveChanges();
            return mapper.Map<MovieViewModel>(dbo);

        }

        /// <summary>
        /// Validates if a movie exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> MovieExists(int id)
        {
            return await dbContext.Movies.FindAsync(id) != null;
        }

        /// <summary>
        ///Gets a movie by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MovieViewModel Get(int id)
        {
            var dbo = dbContext.Movies.FirstOrDefault(m => m.Id == id);
            if (dbo == null)
            {
                return null;
            }
            return mapper.Map<MovieViewModel>(dbo);
        }
        /// <summary>
        ///Gets a movie by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MovieViewModel> GetAsync(int id)
        {
            var dbo = await dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (dbo == null)
            {
                return null;
            }
            return mapper.Map<MovieViewModel>(dbo);
        }
        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="model">The movie update data</param>
        /// <returns>The updated MovieViewModel</returns>
        public MovieViewModel Update(MovieUpdateBinding model)
        {
            var dbo = dbContext.Movies.FirstOrDefault(m => m.Id == model.Id);
            if (dbo == null)
            {
                return null;
            }
            mapper.Map(model, dbo);
            dbContext.SaveChanges();

            return mapper.Map<MovieViewModel>(dbo);
        }

        /// <summary>
        /// Deletes a movie by its id
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            var dbo = await dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (dbo != null)
            {
                dbContext.Movies.Remove(dbo);
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Fetches movies with pagination.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of movies per page.</param>
        /// <returns>A list of MovieViewModel objects.</returns>
        public async Task<List<MovieViewModel>> GetMoviesWithPagination(int page, int pageSize)
        {
            var movies = await dbContext.Movies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return mapper.Map<List<MovieViewModel>>(movies);
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieViewModel>> GetMovies()
        {
            var movies = await dbContext.Movies
                .ToListAsync();

            return mapper.Map<List<MovieViewModel>>(movies);
        }

        /// <summary>
        /// Adds an actor to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActorViewModel AddActor(ActorBinding model)
        {
            var dbo = mapper.Map<Actor>(model);
            dbContext.Actors.Add(dbo);
            dbContext.SaveChanges();
            return mapper.Map<ActorViewModel>(dbo);

        }
        /// <summary>
        /// Get Actors
        /// </summary>
        /// <returns></returns>
        public async Task<List<ActorViewModel>> GetActors()
        {
            var Actors = await dbContext.Actors
                .ToListAsync();

            return mapper.Map<List<ActorViewModel>>(Actors);
        }


        /// <summary>
        ///  Get Movie By Actor Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MovieViewModel GetMovieByActorId(int id)
        {
            var dbo = dbContext.Actors
                .Include(y => y.Movie)
                .FirstOrDefault(m => m.Id == id);
            if (dbo == null)
            {
                return null;
            }
            return mapper.Map<MovieViewModel>(dbo.Movie);
        }
        /// <summary>
        /// Get actors by paggination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<ActorViewModel>> GetActorsWithPagination(int page, int pageSize)
        {
            var Actors = await dbContext.Actors
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return mapper.Map<List<ActorViewModel>>(Actors);
        }
        /// <summary>
        /// Deletes an actor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteActor(int id)
        {
            var dbo = await dbContext.Actors.FirstOrDefaultAsync(m => m.Id == id);
            if (dbo != null)
            {
                dbContext.Actors.Remove(dbo);
                await dbContext.SaveChangesAsync();
            }

 
        }

        /// <summary>
        /// Updates an actor in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActorViewModel UpdateActor(ActorUpdateBinding model)
        {
            var dbo = dbContext.Actors.FirstOrDefault(m => m.Id == model.Id);
            if (dbo == null)
            {
                return null;
            }
            mapper.Map(model, dbo);
            dbContext.SaveChanges();

            return mapper.Map<ActorViewModel>(dbo);
        }
    }
}
