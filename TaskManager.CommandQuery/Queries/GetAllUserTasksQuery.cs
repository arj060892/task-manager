using MediatR;
using TaskManager.Domain.DTOs;

namespace TaskManager.Core.Queries
{
    public class GetAllUserTasksQuery : IRequest<IEnumerable<UserTaskResponseDTO>>
    {
    }
}
