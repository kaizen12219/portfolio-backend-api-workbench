namespace Portfolio.BackendApiWorkbench.Core;

public sealed record ProjectSignal(
    string Id,
    string Title,
    string Category,
    string[] Variants,
    string[] Accents,
    string[] Skills,
    int ProofDepth);

public sealed record SeriesRecommendation(
    string Variant,
    string Accent,
    string ProjectId,
    string Title,
    int Score,
    string[] Reasons);

public sealed record SeriesProfile(
    string Variant,
    string Label,
    int ResumeCount,
    string[] TargetTitles,
    string[] StackSignals);

public static class SeriesCatalog
{
    private static readonly SeriesProfile[] Profiles =
    [
        new(
            "backend-platform",
            "Backend and Platform",
            151,
            ["Backend Engineer", "API Platform Engineer", "Platform Engineer", "Cloud Software Engineer"],
            ["Go", "Python", "Java", "C#", "SQL", "AWS", "Docker", "Kubernetes", "Terraform"]),
        new(
            "ai-data",
            "AI and Data",
            38,
            ["AI Engineer", "Data Engineer", "Analytics Engineer", "Automation Engineer"],
            ["Python", "SQL", "Airflow", "Databricks", "Snowflake", "LLM", "RAG"]),
        new(
            "fullstack-product",
            "Full-Stack Product",
            30,
            ["Full-Stack Engineer", "Product Engineer", "TypeScript Engineer"],
            ["TypeScript", "React", "Node.js", "Next.js", "SQL", "Python"]),
        new(
            "frontend-web",
            "Frontend Web",
            5,
            ["Frontend Engineer", "Web Engineer", "React Engineer", "CMS Developer"],
            ["TypeScript", "JavaScript", "React", "Vue.js", "HTML", "CSS", "PHP"]),
        new(
            "mobile",
            "Mobile Quality",
            19,
            ["Mobile Engineer", "iOS Engineer", "Android Engineer", "Mobile QA Lead"],
            ["Swift", "Kotlin", "Android", "iOS", "React Native", "Unity", "CI/CD"])
    ];

    private static readonly ProjectSignal[] Projects =
    [
        new(
            "fast-endpoints-api",
            "FastEndpoints API Workbench",
            "backend-api",
            ["backend-platform", "fullstack-product", "ai-data"],
            ["dotnet", "go"],
            ["C#", ".NET", "OpenAPI", "API Design", "Testability"],
            5),
        new(
            "flags-net",
            "FlagsNET Async Coordination Library",
            "backend-library",
            ["backend-platform", "fullstack-product"],
            ["dotnet", "go"],
            ["C#", ".NET", "Async", "Library Design"],
            4),
        new(
            "ai-pathfinding-lab",
            "AI Pathfinding Lab",
            "algorithm-visualization",
            ["ai-data", "mobile", "backend-platform"],
            ["python", "dotnet"],
            ["Algorithms", "Pathfinding", "State Machines"],
            5),
        new(
            "portfolio-series-router",
            "Portfolio Series Router",
            "content-platform",
            ["fullstack-product", "frontend-web", "backend-platform", "ai-data"],
            ["node", "python", "go"],
            ["Hugo", "Configuration", "Content Modeling"],
            4),
        new(
            "vanilla-js-lab",
            "Vanilla JavaScript Projects Lab",
            "frontend-browser",
            ["frontend-web", "fullstack-product"],
            ["node"],
            ["JavaScript", "Browser APIs", "Accessibility"],
            4),
        new(
            "time-trial-cards",
            "Time Trial Cards",
            "mobile-quality",
            ["mobile", "fullstack-product"],
            ["mobile"],
            ["QA", "Touch Interaction", "Release Readiness"],
            4)
    ];

    public static IReadOnlyList<SeriesProfile> AllProfiles() => Profiles;

    public static IReadOnlyList<ProjectSignal> AllProjects() => Projects;

    public static IReadOnlyList<SeriesRecommendation> Recommend(string variant, string? accent)
    {
        var normalizedAccent = string.IsNullOrWhiteSpace(accent) ? "" : accent.Trim().ToLowerInvariant();

        return Projects
            .Select(project => Score(project, variant, normalizedAccent))
            .Where(result => result.Score > 0)
            .OrderByDescending(result => result.Score)
            .ThenBy(result => result.Title)
            .ToArray();
    }

    private static SeriesRecommendation Score(ProjectSignal project, string variant, string accent)
    {
        var reasons = new List<string>();
        var score = project.ProofDepth;

        var matchesVariant = project.Variants.Contains(variant);
        var matchesAccent = !string.IsNullOrWhiteSpace(accent) && project.Accents.Contains(accent);

        if (matchesVariant)
        {
            score += 8;
            reasons.Add($"matches {variant}");
        }

        if (matchesAccent)
        {
            score += 4;
            reasons.Add($"supports {accent} accent");
        }

        if (!matchesVariant && !matchesAccent)
        {
            score = 0;
        }

        if (score > 0 && project.Skills.Any(skill => skill.Contains("API", StringComparison.OrdinalIgnoreCase)))
        {
            score += 2;
            reasons.Add("shows API boundary judgment");
        }

        if (reasons.Count == 0)
        {
            score = 0;
        }

        return new SeriesRecommendation(variant, accent, project.Id, project.Title, score, reasons.ToArray());
    }
}
