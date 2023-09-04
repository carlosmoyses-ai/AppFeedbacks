using Feedback.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Feedback.RazorPages.Pages.Feedbacks
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;

        public FeedbackModel Feedback { get; set; } = new();

        public DetailsModel(ILogger<DetailsModel> logger)
        {
            _logger = logger;
        }

         public async Task<IActionResult> OnGetAsync(int id)
        {
            Uri url = new Uri($"http://localhost:5242/GetFeedback/{id}");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url.ToString());

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Feedback = JsonConvert.DeserializeObject<FeedbackModel>(responseContent) ?? new();
            }

            return Page();
        }
    }
}