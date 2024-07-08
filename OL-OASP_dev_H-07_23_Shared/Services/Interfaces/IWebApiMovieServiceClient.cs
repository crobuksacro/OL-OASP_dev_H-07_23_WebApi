using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Models.ViewModels;

namespace OL_OASP_dev_H_07_23_Shared.Services.Interfaces
{
    public interface IWebApiMovieServiceClient
    {
        MovieViewModel AddMovie(MovieBinding model, Action<HttpResponseMessage> unsuccessfulResponseAction = null);
        MovieViewModel GetMovie(int id, Action<HttpResponseMessage> unsuccessfulResponseAction = null);
        T GetMovie<T>(int id, Action<HttpResponseMessage> unsuccessfulResponseAction = null);
        List<MovieViewModel> GetMovies(Action<HttpResponseMessage> unsuccessfulResponseAction = null);
        MovieViewModel Update(MovieUpdateBinding model, Action<HttpResponseMessage> unsuccessfulResponseAction = null);
    }
}