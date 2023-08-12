using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Data.Entities;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Repository.Implementations
{
    /// <summary>
    /// Repository for performing CRUD operations on UserTask.
    /// </summary>
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly UserTaskManagerDbContext _context;

        public UserTaskRepository(UserTaskManagerDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<UserTask>> GetAllAsync()
        {
            return await this._context.UserTasks.ToListAsync();
        }

        public async Task<UserTask> GetByIdAsync(int id)
        {
            return await this._context.UserTasks.FindAsync(id);
        }

        public async Task<UserTask> AddAsync(UserTask userTask)
        {
            var entry = await this._context.UserTasks.AddAsync(userTask);
            await this._context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<UserTask> UpdateAsync(UserTask userTask)
        {
            var entry = this._context.UserTasks.Update(userTask);
            await this._context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(UserTask userTask)
        {
            this._context.UserTasks.Remove(userTask);
            var result = await this._context.SaveChangesAsync();
            return result > 0;
        }

    }
}