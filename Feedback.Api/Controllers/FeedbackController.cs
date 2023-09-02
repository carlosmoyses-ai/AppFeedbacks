using Feedback.Api.Data;
using Feedback.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public FeedbackController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("ReadFeedbacksList")]
        public async Task<IActionResult> GetFeedbacksList()
        {
            return Ok(await _dbContext.Feedbacks.ToListAsync());
        }

        [HttpGet]
        [Route("ReadFeedbackById/{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _dbContext.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpPost]
        [Route("CreateFeedback")]
        public async Task<IActionResult> CreateFeedback(FeedbacksModel feedback)
        {
            _dbContext.Feedbacks.Add(feedback);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.IdFeedback }, feedback);
        }

        [HttpPut]
        [Route("UpdateFeedback/{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, FeedbacksModel feedback)
        {
            if (id != feedback.IdFeedback)
            {
                return BadRequest();
            }

            _dbContext.Entry(feedback).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteFeedback/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _dbContext.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            _dbContext.Feedbacks.Remove(feedback);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}