using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAPI.EFRepository
{
    
    [Table("AppUsers")]
    public class AppUser
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }

        [Column("Username")]
        public string Name { get; set; }
        
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public List<AppGroup> AdminOfGroups { get; set; }

        public List<AppGroup> Groups { get; set; }

        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }

    [Table("AppGroups")]
    public class AppGroup
    {
        [Key]
        [Column("GroupId")]
        public int Id { get; set; }

        [Column("Groupname")]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public int? AdminId { get; set; }
        
        public AppUser? Admin { get; set; }

        public List<AppUser> Users { get; set; }

    }

    [Table("AppUsersInGroups")]
    public class AppUsersInGroups
    {
        [Column("UserId")]
        public int UserId { get; set; }
        // public AppUser AppUser { get; set; }

        [Column("GroupId")]
        public int GroupId { get; set; }
        // public AppGroup AppGroup { get; set; }

    }


}
