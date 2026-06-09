using Portfolio.BackendApiWorkbench.Core;

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}

var backend = SeriesCatalog.Recommend("backend-platform", "go");
Assert(backend.Count >= 3, "Backend recommendations should include multiple projects.");
Assert(backend[0].ProjectId == "fast-endpoints-api", "API workbench should lead backend/go recommendations.");

var ai = SeriesCatalog.Recommend("ai-data", "python");
Assert(ai.Any(item => item.ProjectId == "ai-pathfinding-lab"), "AI recommendations should include pathfinding.");

var none = SeriesCatalog.Recommend("unknown", "");
Assert(none.Count == 0, "Unknown variants should not return recommendations.");

Console.WriteLine("All Portfolio.BackendApiWorkbench specs passed.");
