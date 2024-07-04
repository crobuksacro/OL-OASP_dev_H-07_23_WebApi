using OL_OASP_dev_H_07_23_Shared.Models.Base;

namespace OL_OASP_dev_H_07_23_WebApi.Models.Dbo
{
    public class Actor : ActorBase
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}
