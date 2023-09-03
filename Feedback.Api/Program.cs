using Feedback.Api.Data;
using Feedback.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<ApiDbContext>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<ApiDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/GetFeedbackList", async (ApiDbContext context) =>
{
    var feedbacks = await context.Feedbacks.ToListAsync();
    await context.SaveChangesAsync();
    return Results.Ok(feedbacks);
});

app.MapGet("/GetFeedback/{id}", async (ApiDbContext context, int id) =>
{
    var feedback = await context.Feedbacks.FindAsync(id);
    await context.SaveChangesAsync();
    return (feedback == null) ? Results.NotFound() : Results.Ok(feedback);
});

app.MapPost("/PostFeedback", async (ApiDbContext context, FeedbackModel feedback) =>
{
    await context.Feedbacks.AddAsync(feedback);
    await context.SaveChangesAsync();
    return Results.Created($"/GetFeedback/{feedback.IdFeedback}", feedback);
});

app.MapPut("/PutFeedback/{id}", async (ApiDbContext context, int id, FeedbackModel feedback) =>
{
    var feedbackUpdate = await context.Feedbacks.FindAsync(id);
    if (feedbackUpdate == null)
    {
        return Results.NotFound();
    }
    feedbackUpdate.NomeCliente = feedback.NomeCliente;
    feedbackUpdate.EmailCliente = feedback.EmailCliente;
    feedbackUpdate.DataFeedback = feedback.DataFeedback;
    feedbackUpdate.Comentario = feedback.Comentario;
    feedbackUpdate.Avaliacao = feedback.Avaliacao;
    await context.SaveChangesAsync();
    return Results.Ok("Feedback atualizado com sucesso!");
});

app.MapDelete("/DeleteFeedback/{id}", async (ApiDbContext context, int id) =>
{
    var feedback = await context.Feedbacks.FindAsync(id);
    if (feedback == null)
    {
        return Results.NotFound();
    }
    context.Feedbacks.Remove(feedback);
    await context.SaveChangesAsync();
    return Results.Ok("Feedback removido com sucesso!");
});



app.Run();
