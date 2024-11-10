using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models
{
    public class Url
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Url]
        public string OriginalUrl { get; set; }

        [Required]
        [StringLength(6)]
        public string ShortenedCode { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int HitCount { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }

  
}