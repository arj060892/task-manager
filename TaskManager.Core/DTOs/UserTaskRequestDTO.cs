namespace TaskManager.Core.DTOs
{
    /// <summary>
    /// Represents the data required to create or update a UserTask.
    /// </summary>
    public class UserTaskRequestDTO
    {
        /// <summary>
        /// Gets or sets the title of the task.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description or notes about the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the task.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date by which the task should be completed.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
