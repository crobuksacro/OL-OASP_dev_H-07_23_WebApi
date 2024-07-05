using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;

namespace OL_OASP_dev_H_07_23_WebApi.Services.Interfaces
{
    public interface IMoviesService
    {
        MovieViewModel Add(MovieBinding model);
        Task Delete(int id);
        MovieViewModel Get(int id);
        Task<MovieViewModel> GetAsync(int id);
        Task<List<MovieViewModel>> GetMovies();
        Task<List<MovieViewModel>> GetMoviesWithPagination(int page, int pageSize);
        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="model">The movie update data</param>
        /// <returns>The updated MovieViewModel</returns>
         MovieViewModel Update(MovieUpdateBinding model);
    }
}