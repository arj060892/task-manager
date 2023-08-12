using TaskManager.Data.Entities;

namespace TaskManager.Repository.Interfaces
{
    /// <summary>
    /// Provides an interface for UserTask repository operations.
    /// </summary>
    public interface IUserTaskRepository : IRepository<UserTask>
    {
    }
}