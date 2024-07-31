using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Stock { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Status { get; set; }
    }
}
