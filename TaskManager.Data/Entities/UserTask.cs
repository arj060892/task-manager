using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data.Entities
{
    public class UserTask
    {
        public enum TaskStatus
        {
            Pending,
            InProgress,
            Completed
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        public DateTime? DueDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
