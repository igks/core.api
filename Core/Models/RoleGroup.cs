using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE.API.Core.Models
{
    public class RoleGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(225)")]
        public string ModulesReadId { get; set; }

        [Column(TypeName = "varchar(225)")]
        public string ModulesWriteId { get; set; }
    }
}