using AutoMapper;
using TaskManager.Data.Entities;
using TaskManager.Domain.DTOs;
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

        public async Task<UserTaskResponseDTO> AddTaskAsync(UserTaskRequestDTO userTaskDTO)
        {
            try
            {
                var xx = this._mapper.Map<UserTask>(userTaskDTO);
                var addedTask = await this._repository
                .AddAsync(xx);
                return this._mapper.Map<UserTaskResponseDTO>(addedTask);

            }
            catch (Exception e)
            {

                await Console.Out.WriteLineAsync(e.Message);
                throw e;

            }

        }

        public async Task<UserTaskResponseDTO> UpdateTask(UserTaskRequestDTO userTaskDTO)
        {
            var updatedTask = await this._repository
                .UpdateAsync(this._mapper.Map<UserTask>(userTaskDTO));

            return this._mapper.Map<UserTaskResponseDTO>(updatedTask);
        }

        public async Task<bool> DeleteTask(int userTaskId)
        {
            var task = await this._repository.GetByIdAsync(userTaskId);
            return await this._repository.DeleteAsync(task);
        }
    }
}