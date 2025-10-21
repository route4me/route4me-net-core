# =============================================================================
# Dockerfile for Route4Me C# SDK Testing Environment
# 
# This Dockerfile creates a containerized environment for building and testing
# the Route4Me C# SDK, based on the Travis CI configuration.
# 
# Usage:
#   Build: docker build -t route4me-sdk-test .
#   Run tests: docker run --rm route4me-sdk-test
#   Interactive shell: docker run -it --rm route4me-sdk-test /bin/bash
#
# =============================================================================

# Use the official .NET 6.0 SDK image as base
# This provides both build and runtime capabilities for .NET 6.0 projects
# Specify platform to avoid QEMU emulation issues on Apple Silicon
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:6.0 AS base

# Set environment variables for non-interactive installation
ENV DEBIAN_FRONTEND=noninteractive
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Install additional dependencies that might be needed for testing
# and development tools
RUN apt-get update && apt-get install -y \
    git \
    curl \
    wget \
    unzip \
    ca-certificates \
    && rm -rf /var/lib/apt/lists/*

# Set the working directory
WORKDIR /app

# Copy the entire project structure
# This includes all source code, test projects, and data files
COPY . .

# =============================================================================
# Build Stage
# =============================================================================
FROM base AS build

# Restore dependencies for the entire solution
# This matches the Travis CI command: dotnet restore ./route4me-csharp-sdk/Route4MeSDK.sln
RUN dotnet restore ./route4me-csharp-sdk/Route4MeSDK.sln

# Build the SDK library
# Note: SDK library targets netstandard2.0, so no framework override needed
RUN dotnet build -v q -c Release ./route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeSDKLibrary.csproj

# Build the unit test projects
# Note: Test projects target net6.0, so no framework override needed
RUN dotnet build -v q -c Release ./route4me-csharp-sdk/Route4MeSDKUnitTest/Route4MeSDKUnitTest.csproj
RUN dotnet build -v q -c Release ./route4me-csharp-sdk/Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj

# =============================================================================
# Test Stage
# =============================================================================
FROM build AS test

# Set the working directory to the SDK folder
WORKDIR /app/route4me-csharp-sdk

# Run the unit tests
# Note: Test projects target net6.0, so no framework override needed
RUN dotnet test -v n -p:ParallelizeTestCollections=false -c Release --filter Category!=Beacon ./Route4MeSDKUnitTest/Route4MeSDKUnitTest.csproj

# Run the V5 unit tests as well
RUN dotnet test -v n -p:ParallelizeTestCollections=false -c Release ./Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj

# =============================================================================
# Development Stage
# =============================================================================
FROM build AS development

# Set the working directory to the SDK folder
WORKDIR /app/route4me-csharp-sdk

# Create a script to run all tests
RUN echo '#!/bin/bash\n\
echo "Running Route4Me SDK Tests..."\n\
echo "================================"\n\
\n\
echo "Running Unit Tests (excluding Beacon category)..."\n\
dotnet test -v n -p:ParallelizeTestCollections=false -c Release --filter Category!=Beacon ./Route4MeSDKUnitTest/Route4MeSDKUnitTest.csproj\n\
\n\
echo "Running V5 Unit Tests..."\n\
dotnet test -v n -p:ParallelizeTestCollections=false -c Release ./Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj\n\
\n\
echo "All tests completed!"\n\
' > /usr/local/bin/run-tests.sh && chmod +x /usr/local/bin/run-tests.sh

# Create a script to build the solution
RUN echo '#!/bin/bash\n\
echo "Building Route4Me SDK..."\n\
echo "========================"\n\
\n\
echo "Restoring dependencies..."\n\
dotnet restore ./Route4MeSDK.sln\n\
\n\
echo "Building SDK Library..."\n\
dotnet build -v q -c Release ./Route4MeSDKLibrary/Route4MeSDKLibrary.csproj\n\
\n\
echo "Building Unit Test projects..."\n\
dotnet build -v q -c Release ./Route4MeSDKUnitTest/Route4MeSDKUnitTest.csproj\n\
dotnet build -v q -c Release ./Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj\n\
\n\
echo "Build completed!"\n\
' > /usr/local/bin/build-sdk.sh && chmod +x /usr/local/bin/build-sdk.sh

# Set the default command to run tests
# CMD ["/usr/local/bin/run-tests.sh"]

