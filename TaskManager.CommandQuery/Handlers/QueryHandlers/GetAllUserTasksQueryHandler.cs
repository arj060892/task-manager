using MediatR;
using TaskManager.Core.Queries;
using TaskManager.Domain.DTOs;
using TaskManager.Service.Interfaces;

namespace TaskManager.Core.Handlers.QueryHandlers
{
    /// <summary>
    /// Handler responsible for processing the GetAllUserTasksQuery.
    /// </summary>
    public class GetAllUserTasksQueryHandler : IRequestHandler<GetAllUserTasksQuery, IEnumerable<UserTaskResponseDTO>>
    {
        private readonly IUserTaskService _service;

        public GetAllUserTasksQueryHandler(IUserTaskService service)
        {
            this._service = service;
        }

        public async Task<IEnumerable<UserTaskResponseDTO>> Handle(GetAllUserTasksQuery query, CancellationToken cancellationToken)
        {
            return await this._service.GetAllTasksAsync();
        }
    }
}
