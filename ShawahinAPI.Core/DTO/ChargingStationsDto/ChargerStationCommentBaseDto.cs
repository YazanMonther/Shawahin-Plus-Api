namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class ChargerStationCommentBaseDto
    {
        public Guid UserId { get; set; }
        public Guid StationId { get; set; }
        public string? CommentText { get; set; }
    }
}
