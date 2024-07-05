using OL_OASP_dev_H_07_23_Shared.Models.Base;

namespace OL_OASP_dev_H_07_23_Shared.Models.Binding
{
    public class MovieUpdateBinding : MovieBase
    {
       /// <summary>
       /// Id of the movie to update
       /// </summary>
        public int Id { get; set; }
    }
}
