using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeForGames.Models
{
    public class Games
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string? Story { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public float Size { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string ImgLink { get; set; }
    }
}
