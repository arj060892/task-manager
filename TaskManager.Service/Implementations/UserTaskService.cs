using AutoMapper;
using TaskManager.Core.DTOs;
using TaskManager.Data.Entities;
using TaskManager.Repository.Interfaces;
using TaskManager.Service.Interfaces;

namespace TaskManager.Service.Implementations
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _repository;
        private readonly IMapper _mapper;

        public UserTaskService(IUserTaskRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<UserTaskResponseDTO>> GetAllTasksAsync()
        {
            var tasks = await this._repository.GetAllAsync();
            return this._mapper.Map<IEnumerable<UserTaskResponseDTO>>(tasks);
        }

        public async Task<UserTaskResponseDTO> GetTaskByIdAsync(int id)
        {
            var task = await this._repository.GetByIdAsync(id);
            return this._mapper.Map<UserTaskResponseDTO>(task);
        }

        public async Task AddTaskAsync(UserTaskRequestDTO userTaskDTO)
        {
            var task = this._mapper.Map<UserTask>(userTaskDTO);
            await this._repository.AddAsync(task);
        }

        public void UpdateTask(UserTaskRequestDTO userTaskDTO)
        {
            var task = this._mapper.Map<UserTask>(userTaskDTO);
            this._repository.Update(task);
        }

        public void DeleteTask(UserTaskRequestDTO userTaskDTO)
        {
            var task = this._mapper.Map<UserTask>(userTaskDTO);
            this._repository.Delete(task);
        }

    }
}
