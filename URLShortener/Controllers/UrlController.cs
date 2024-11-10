using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlContext _context;

        public UrlController(UrlContext context)
        {
            _context = context;
        }

        // POST api/url/shorten
        [HttpPost("api/url/shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
        {
            if (!Uri.IsWellFormedUriString(request.OriginalUrl, UriKind.Absolute))
                return BadRequest("Invalid URL format.");

            var shortCode = await GenerateUniqueShortCodeAsync();

            var url = new Url
            {
                OriginalUrl = request.OriginalUrl,
                ShortenedCode = shortCode,
                CreatedAt = DateTime.UtcNow
            };

            _context.Urls.Add(url);
            await _context.SaveChangesAsync();

            return Ok(new { ShortenedUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}" });
        }

        // GET /{shortCode} - catch-all route for redirection
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortCode)
        {
            if (string.IsNullOrEmpty(shortCode))
                return BadRequest("Short code cannot be empty.");

            var url = await _context.Urls.AsNoTracking()
                .FirstOrDefaultAsync(u => u.ShortenedCode == shortCode);

            if (url == null)
                return NotFound("URL not found.");

            if (url.ExpirationDate.HasValue && url.ExpirationDate.Value < DateTime.UtcNow)
                return BadRequest("URL has expired.");

            await UpdateHitCount(shortCode);

            return Redirect(url.OriginalUrl);
        }

        private async Task<string> GenerateUniqueShortCodeAsync()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string shortCode;

            do
            {
                shortCode = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (await _context.Urls.AnyAsync(u => u.ShortenedCode == shortCode));

            return shortCode;
        }

        private async Task UpdateHitCount(string shortCode)
        {
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortenedCode == shortCode);

            if (url != null)
            {
                url.HitCount++;
                await _context.SaveChangesAsync();
            }
        }
    }
}
