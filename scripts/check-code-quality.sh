#!/usr/bin/env bash
# Run format check and build with analyzers (matches .github/workflows/code-quality.yml).
# Requires: dotnet SDK (e.g. .NET 10.0.x)
set -e
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
SLN="$ROOT/route4me-csharp-sdk/Route4MeSDK.sln"

echo "Restoring..."
dotnet restore "$SLN"

echo "Checking code formatting (no changes applied)..."
dotnet format "$SLN" --verify-no-changes --verbosity diagnostic

echo "Building with analyzers (warnings as errors)..."
dotnet build "$SLN" --configuration Release --no-restore /p:TreatWarningsAsErrors=true

echo "Code quality checks passed."
