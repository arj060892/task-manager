using MediatR;
using TaskManager.Domain.DTOs;

namespace TaskManager.Core.Commands
{
    /// <summary>
    /// Command to update an existing UserTask.
    /// </summary>
    public class UpdateUserTaskCommand : IRequest<UserTaskResponseDTO>
    {
        public UserTaskRequestDTO UserTask { get; set; }
        public int Id { get; set; }
    }

}