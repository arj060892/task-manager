namespace TaskManager.Core.Queries
{
    /// <summary>
    /// Query to retrieve a specific UserTask by its ID.
    /// </summary>
    public class GetUserTaskByIdQuery
    {
        public int Id { get; set; }
    }
}