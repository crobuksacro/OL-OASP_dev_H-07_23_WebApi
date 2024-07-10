using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;

namespace OL_OASP_dev_H_07_23_WebApi.Services.Interfaces
{
    public interface IMoviesService
    {
        /// <summary>
        /// Validates if a movie exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> MovieExists(int id);
        MovieViewModel Add(MovieBinding model);
        ActorViewModel AddActor(ActorBinding model);
        Task Delete(int id);
        Task DeleteActor(int id);
        MovieViewModel Get(int id);
        Task<List<ActorViewModel>> GetActors();
        Task<List<ActorViewModel>> GetActorsWithPagination(int page, int pageSize);
        Task<MovieViewModel> GetAsync(int id);
        MovieViewModel GetMovieByActorId(int id);
        Task<List<MovieViewModel>> GetMovies();
        Task<List<MovieViewModel>> GetMoviesWithPagination(int page, int pageSize);
        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="model">The movie update data</param>
        /// <returns>The updated MovieViewModel</returns>
         MovieViewModel Update(MovieUpdateBinding model);
        ActorViewModel UpdateActor(ActorUpdateBinding model);
    }
}