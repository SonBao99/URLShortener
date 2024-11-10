using System.ComponentModel.DataAnnotations;

public class Url
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2048)]
    public string OriginalUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(6)]
    public string ShortenedCode { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int HitCount { get; set; } = 0;
}