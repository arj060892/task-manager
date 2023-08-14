namespace TaskManager.Domain.DTOs
{
    /// <summary>
    /// Represents the data of a UserTask for API responses.
    /// </summary>
    public class UserTaskResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the start time of the task.
        /// </summary>
        public TimeSpan? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the task.
        /// </summary>
        public TimeSpan? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the date when the task was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the task was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}