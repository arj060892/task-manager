using TaskManager.Data.Entities;
using TaskManager.Repository.Interfaces;
using TaskManager.Service.Interfaces;

namespace TaskManager.Service.Implementations
{
    /// <summary>
    /// Service for performing operations on UserTask.
    /// </summary>
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _repository;

        public UserTaskService(IUserTaskRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<UserTask>> GetAllTasksAsync()
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<UserTask> GetTaskByIdAsync(int id)
        {
            return await this._repository.GetByIdAsync(id);
        }

        public async Task AddTaskAsync(UserTask userTask)
        {
            await this._repository.AddAsync(userTask);
        }

        public void UpdateTask(UserTask userTask)
        {
            this._repository.Update(userTask);
        }

        public void DeleteTask(UserTask userTask)
        {
            this._repository.Delete(userTask);
        }
    }
}