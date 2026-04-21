using Email_Spam_Project.Data;
using Email_Spam_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Email_Spam_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly SpamDetectionService _mlService;
        private readonly AppDbContext _context;

        public EmailController(SpamDetectionService mlService, AppDbContext context)
        {
            _mlService = mlService;
            _context = context;
        }

        [HttpPost("predict")]
        public IActionResult Predict([FromBody] string message)
        {
            var result = _mlService.Predict(message);

            var email = new Email
            {
                Message = message,
                Prediction = result.Prediction,
                Confidence = result.Probability
            };

            _context.Emails.Add(email);
            _context.SaveChanges();

            return Ok(new
            {
                Spam = result.Prediction,
                Confidence = result.Probability
            });
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            return Ok(_context.Emails.ToList());
        }
    }
}
