using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.Helpers
{
    public static class ChargerStationCommentHelper
    {

        // Helper method to map DTO to Entity
        public static ChargerStationComments MapDtoToEntity(ChargerStationCommentBaseDto commentDto)
        {
            return new ChargerStationComments
            {
                Id = Guid.NewGuid(),
                UserId = commentDto.UserId,
                StationId = commentDto.StationId,
                CommentText = commentDto.CommentText
            };
        }

        // Helper method to map Entity to DTO
        public static ChargerStationCommentResponeDto MapEntityToDto(ChargerStationComments comment)
        {
            return new ChargerStationCommentResponeDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                StationId = comment.StationId,
                CommentText = comment.CommentText
            };
        }
    }
}
