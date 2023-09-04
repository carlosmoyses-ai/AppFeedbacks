using System.Text;
using Feedback.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Feedback.RazorPages.Pages.Feedbacks;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;

    public FeedbackModel Feedback { get; set; } = new();

    public CreateModel(ILogger<CreateModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnPostAsync(FeedbackModel feedback)
    {
        Uri uri = new Uri("http://localhost:5242/PostFeedback");
        using HttpClient httpClient = new HttpClient();
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        string json = JsonConvert.SerializeObject(feedback);
        using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        request.Content = content;
        using HttpResponseMessage response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return RedirectToPage("./View");
        }

        return Page();
    }
}