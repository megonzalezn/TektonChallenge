using System.ComponentModel.DataAnnotations;

namespace Tekton.Model
{
    public class ProductRequestDTO
    {
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
