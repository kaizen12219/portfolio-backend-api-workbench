# Portfolio Backend API Workbench

Backend/platform portfolio project for API-heavy resumes. The project models a small scoring service that maps resume targets to portfolio projects, stack accents, and evidence signals.

## Series Coverage

- Backend/platform: API design, scoring boundaries, typed domain model, testable core logic.
- Full-stack/product: JSON API that a front end can consume for tailored portfolio pages.
- Go and .NET accents: Go remains a portfolio positioning accent; this implementation uses .NET because the local workspace has .NET 8 available.

## Run

```powershell
dotnet run --project src/Portfolio.BackendApiWorkbench
```

Then open:

- `http://localhost:5056/health`
- `http://localhost:5056/api/series`
- `http://localhost:5056/api/recommendations/backend-platform?accent=go`

## Test

```powershell
dotnet run --project tests/Portfolio.BackendApiWorkbench.Specs
```

## Honest Development Plan

This repo is intended to be pushed as a current portfolio project. The issue and PR templates are ready for real GitHub workflow, but no historical activity should be fabricated or backdated.
