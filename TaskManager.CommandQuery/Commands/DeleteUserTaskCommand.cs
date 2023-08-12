using MediatR;

namespace TaskManager.Core.Commands
{
    /// <summary>
    /// Command to delete a UserTask.
    /// </summary>
    public class DeleteUserTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

}
