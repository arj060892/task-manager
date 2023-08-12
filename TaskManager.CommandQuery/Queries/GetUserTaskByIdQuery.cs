using MediatR;
using TaskManager.Domain.DTOs;

namespace TaskManager.Core.Queries
{
    public class GetUserTaskByIdQuery : IRequest<UserTaskResponseDTO>
    {
        public int Id { get; set; }

        public GetUserTaskByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}