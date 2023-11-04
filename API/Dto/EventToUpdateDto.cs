using Core.Model;

namespace API.Dto
{
    public class EventToUpdateDto
    {

        public string Status { get; set; }
        public string EventDate { get; set; }
        public string Note { get; set; }
        public int AssetId { get; set; }
    }
}