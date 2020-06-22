using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE.API.Core.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Firstname { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Lastname { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Photo { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public byte[] PasswordHash { get; set; }

        [Column(TypeName = "varchar(255)")]
        public byte[] PasswordSalt { get; set; }
    }
}