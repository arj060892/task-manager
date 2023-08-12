using MediatR;
using TaskManager.Core.Queries;
using TaskManager.Domain.DTOs;
using TaskManager.Service.Interfaces;

namespace TaskManager.Core.Handlers.QueryHandlers
{
    /// <summary>
    /// Handler responsible for processing the GetUserTaskByIdQuery.
    /// </summary>
    public class GetUserTaskByIdQueryHandler : IRequestHandler<GetUserTaskByIdQuery, UserTaskResponseDTO>
    {
        private readonly IUserTaskService _service;

        public GetUserTaskByIdQueryHandler(IUserTaskService service)
        {
            this._service = service;
        }

        public async Task<UserTaskResponseDTO> Handle(GetUserTaskByIdQuery query, CancellationToken cancellationToken)
        {
            return await this._service.GetTaskByIdAsync(query.Id);
        }
    }
}
