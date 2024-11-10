using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("url")]
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlContext _context;

        public UrlController(UrlContext context)
        {
            _context = context;
        }

        // POST api/url/shorten
        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
        {
            // Validate the URL
            if (!Uri.IsWellFormedUriString(request.OriginalUrl, UriKind.Absolute))
                return BadRequest("Invalid URL format.");

            // Generate a unique code for the URL
            var shortCode = GenerateShortCode();

            // Create and save the URL entry
            var url = new Url
            {
                OriginalUrl = request.OriginalUrl,
                ShortenedCode = shortCode,
                CreatedAt = DateTime.UtcNow
            };

            _context.Urls.Add(url);
            await _context.SaveChangesAsync();

            // Return the shortened URL code
            return Ok(new { ShortenedUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}" });
        }


        // GET api/url/{shortCode}
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortCode)
        {
            // Find the original URL by short code
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortenedCode == shortCode);

            if (url == null)
                return NotFound("URL not found.");

            // Increment hit count and save
            url.HitCount++;
            await _context.SaveChangesAsync();

            // Redirect to the original URL
            return Redirect(url.OriginalUrl);
        }


        // Optional: GET api/url/stats/{shortCode}
        [HttpGet("stats/{shortCode}")]
        public IActionResult GetUrlStats(string shortCode)
        {
            var url = _context.Urls.FirstOrDefault(u => u.ShortenedCode == shortCode);

            if (url == null)
                return NotFound("URL not found.");

            return Ok(new
            {
                OriginalUrl = url.OriginalUrl,
                ShortenedCode = url.ShortenedCode,
                CreatedAt = url.CreatedAt,
                ExpirationDate = url.ExpirationDate,
                HitCount = url.HitCount
            });
        }

        // Helper method to generate a unique short code
        private string GenerateShortCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
