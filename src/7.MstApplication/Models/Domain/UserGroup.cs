using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.src._7MstApplication.Models.Domain
{
    public enum LevelPriority
    {
        System,
        Primary,
        UserCreated
    }

    public class UserGroup
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("level_priority")]
        public LevelPriority LevelPriority { get; set; }

        [Required]
        [Column("application_id")]
        public Guid ApplicationId { get; set; }

        [Required]
        [Column("created_by")]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("updated_by")]
        [StringLength(255)]
        public string UpdatedBy { get; set; }

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("status")]
        public int? Status { get; set; } = 1;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}