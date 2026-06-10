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

var profiles = SeriesCatalog.AllProfiles();
Assert(profiles.Count == 5, "Catalog should expose the five harmonized resume series.");
Assert(profiles.Sum(profile => profile.ResumeCount) == 243, "Series profile counts should cover all provided resume briefs.");

var ai = SeriesCatalog.Recommend("ai-data", "python");
Assert(ai.Any(item => item.ProjectId == "ai-pathfinding-lab"), "AI recommendations should include pathfinding.");

var mobile = SeriesCatalog.Recommend("mobile", "mobile");
Assert(mobile[0].ProjectId == "time-trial-cards", "Mobile recommendations should lead with release-quality evidence.");

var none = SeriesCatalog.Recommend("unknown", "");
Assert(none.Count == 0, "Unknown variants should not return recommendations.");

Console.WriteLine("All Portfolio.BackendApiWorkbench specs passed.");
