using TaskManager.Data.Entities;

namespace TaskManager.Service.Interfaces
{
    /// <summary>
    /// Provides an interface for UserTask service operations.
    /// </summary>
    public interface IUserTaskService
    {
        Task<IEnumerable<UserTask>> GetAllTasksAsync();

        Task<UserTask> GetTaskByIdAsync(int id);

        Task AddTaskAsync(UserTask userTask);

        void UpdateTask(UserTask userTask);

        void DeleteTask(UserTask userTask);

    }
}