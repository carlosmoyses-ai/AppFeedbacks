using Feedback.Api.Models;

namespace Feedback.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApiDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Feedbacks!.Any())
            {
                return;
            }

            var feedbacks = new FeedbackModel[]
            {
                new FeedbackModel
                {
                    NomeCliente = "Carlos Moyses",
                    EmailCliente = "carlosmoyses@gmail.com",
                    DataFeedback = DateTime.Parse("2021-08-01"),
                    Comentario = "Gostei muito do atendimento, recomendo!",
                    Avaliacao = 5
                }
            };

            context.AddRange(feedbacks);
            context.SaveChanges();
    }
}   
}   