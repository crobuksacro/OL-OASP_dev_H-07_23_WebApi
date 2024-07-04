using OL_OASP_dev_H_07_23_Shared.Models.Base;

namespace OL_OASP_dev_H_07_23_WebApi.Models.Dbo
{
    public class Movie : MovieBase
    {
        public int Id { get; set; }
        public ICollection<Actor> Actors { get; set; }
    }
}
