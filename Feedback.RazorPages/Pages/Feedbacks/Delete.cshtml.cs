using System.Text;
using Feedback.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Feedback.RazorPages.Pages.Feedbacks
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;

        public FeedbackModel Feedback { get; set; } = new();

        public DeleteModel(ILogger<DeleteModel> logger)
        {
            _logger = logger;
        }


         public async Task<IActionResult> OnGetAsync(int? id) {

            if (id == null) {
                return NotFound();
            }

            Uri uri = new Uri($"http://localhost:5242/GetFeedback/{id}");
            using HttpClient httpClient = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) {
                string responseContent = await response.Content.ReadAsStringAsync();
                Feedback = JsonConvert.DeserializeObject<FeedbackModel>(responseContent) ?? new();
            }

            return Page();
         }

        public async Task<IActionResult> OnPostAsync(FeedbackModel feedback)
        {
            Uri uri = new Uri($"http://localhost:5242/DeleteFeedback/{feedback.IdFeedback}");
            HttpClient httpClient = new HttpClient();

            string json = JsonConvert.SerializeObject(feedback);

            using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToPage("/Feedbacks/View");
            }

            return Page();
        }
    }
}