using AutoMapper;
using TaskManager.Data.Entities;
using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Mappings
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