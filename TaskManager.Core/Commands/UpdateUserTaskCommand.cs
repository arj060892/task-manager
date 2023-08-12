using TaskManager.Core.DTOs;

namespace TaskManager.Core.Commands
{
    /// <summary>
    /// Command to update an existing UserTask.
    /// </summary>
    public class UpdateUserTaskCommand
    {
        public int Id { get; set; }
        public UserTaskRequestDTO UserTask { get; set; }
    }
}