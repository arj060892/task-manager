using TaskManager.Core.DTOs;

namespace TaskManager.Service.Interfaces
{
    /// <summary>
    /// Provides an interface for UserTask service operations.
    /// </summary>
    public interface IUserTaskService
    {
        /// <summary>
        /// Retrieves all user tasks.
        /// </summary>
        /// <returns>A list of UserTaskResponseDTOs.</returns>
        Task<IEnumerable<UserTaskResponseDTO>> GetAllTasksAsync();

        /// <summary>
        /// Retrieves a user task by its identifier.
        /// </summary>
        /// <param name="id">The task identifier.</param>
        /// <returns>A UserTaskResponseDTO if found; otherwise, null.</returns>
        Task<UserTaskResponseDTO> GetTaskByIdAsync(int id);

        /// <summary>
        /// Adds a new user task.
        /// </summary>
        /// <param name="userTaskDTO">The UserTaskRequestDTO to add.</param>
        Task AddTaskAsync(UserTaskRequestDTO userTaskDTO);

        /// <summary>
        /// Updates an existing user task.
        /// </summary>
        /// <param name="userTaskDTO">The UserTaskRequestDTO to update.</param>
        void UpdateTask(UserTaskRequestDTO userTaskDTO);

        /// <summary>
        /// Deletes a user task.
        /// </summary>
        /// <param name="userTaskDTO">The UserTaskRequestDTO to delete.</param>
        void DeleteTask(UserTaskRequestDTO userTaskDTO);

    }
}
