using Feedback.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Feedback.RazorPages.Pages.Feedbacks
{
    public class ViewModel : PageModel
    {
        private readonly ILogger<ViewModel> _logger;

        public List<FeedbackModel> FeedbackList { get; set; } = new();

        public ViewModel(ILogger<ViewModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Uri uri = new Uri("http://localhost:5242/GetFeedbackList");
            using HttpClient httpClient = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                FeedbackList = JsonConvert.DeserializeObject<List<FeedbackModel>>(responseContent) ?? new();
            }

            return Page();
        }
    }
}