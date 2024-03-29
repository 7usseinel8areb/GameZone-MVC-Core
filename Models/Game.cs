using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Image { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default;

        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();
    }
}
