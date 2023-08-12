using TaskManager.Domain.DTOs;

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
        /// <returns>Added UserTaskResponseDTO</returns>
        Task<UserTaskResponseDTO> AddTaskAsync(UserTaskRequestDTO userTaskDTO);

        /// <summary>
        /// Updates an existing user task.
        /// </summary>
        /// <param name="userTaskDTO">The UserTaskRequestDTO to update.</param>
        /// <returns>Updated UserTaskResponseDTO</returns>
        Task<UserTaskResponseDTO> UpdateTask(UserTaskRequestDTO userTaskDTO);

        /// <summary>
        /// Deletes a user task.
        /// </summary>
        /// <param name="userTaskId">The userTaskId to delete.</param>
        /// <returns>true or false based on the delete status</returns>
        Task<bool> DeleteTask(int userTaskId);

    }
}
