
using MediatR;
using TaskManager.Domain.DTOs;

namespace TaskManager.Core.Commands
{
    /// <summary>
    /// Command to create a new UserTask.
    /// </summary>
    public class CreateUserTaskCommand : IRequest<UserTaskResponseDTO>
    {
        public UserTaskRequestDTO UserTask { get; set; }
    }
}