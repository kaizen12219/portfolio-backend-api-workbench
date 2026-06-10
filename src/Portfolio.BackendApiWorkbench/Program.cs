using Portfolio.BackendApiWorkbench.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5056");

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    service = "portfolio-backend-api-workbench"
}));

app.MapGet("/api/series", () => Results.Ok(SeriesCatalog.AllProfiles()));

app.MapGet("/api/projects", () => Results.Ok(SeriesCatalog.AllProjects()));

app.MapGet("/api/recommendations/{variant}", (string variant, string? accent) =>
{
    var recommendations = SeriesCatalog.Recommend(variant, accent);
    return recommendations.Count == 0
        ? Results.NotFound(new { message = $"No recommendations found for {variant}." })
        : Results.Ok(recommendations);
});

app.Run();
