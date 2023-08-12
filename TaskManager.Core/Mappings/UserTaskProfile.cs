using AutoMapper;
using TaskManager.Core.DTOs;
using TaskManager.Data.Entities;

namespace TaskManager.Core.Mappings
{
    /// <summary>
    /// AutoMapper profile for UserTask entity and its related DTOs.
    /// </summary>
    public class UserTaskProfile : Profile
    {
        public UserTaskProfile()
        {
            // Mapping from UserTask entity to UserTaskResponseDTO
            this.CreateMap<UserTask, UserTaskResponseDTO>();

            // Mapping from UserTaskRequestDTO to UserTask entity
            this.CreateMap<UserTaskRequestDTO, UserTask>();
        }
    }
}