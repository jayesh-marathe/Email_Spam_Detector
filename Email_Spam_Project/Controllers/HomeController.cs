using Email_Spam_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Email_Spam_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpamDetectionService _spamService;

        public HomeController(ILogger<HomeController> logger, SpamDetectionService spamService)
        {
            _logger = logger;
            _spamService = spamService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Analyze(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return Json(new { success = false, error = "Please enter a message to analyze." });
            }

            var prediction = _spamService.Predict(message);
            
            return Json(new { 
                success = true, 
                isSpam = prediction.Prediction, 
                probability = prediction.Probability,
                score = prediction.Score
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
