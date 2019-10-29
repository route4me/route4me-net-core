using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Route4MeDB.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Route4MeDB.ApplicationCore.Specifications;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;

namespace Route4MeDB.IntegrationTests.Repositories.OptimizationRepositoryTests
{
    public class OptimizationRepositoryTests
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly OptimizationRepository _optimizationRepository;
        private OptimizationBuilder optimizationBuilder { get; } = new OptimizationBuilder();
        private readonly ITestOutputHelper _output;

        public OptimizationRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseInMemoryDatabase(databaseName: "Route4MeDB")
                .Options;
            _route4meDbContext = new Route4MeDbContext(dbOptions);
            _optimizationRepository = new OptimizationRepository(_route4meDbContext);
        }

        [Fact]
        public async Task GetsExistingOptimization()
        {
            var existingOptimization = optimizationBuilder.OptimizationWith10Stops();
            _route4meDbContext.Optimizations.Add(existingOptimization);
            _route4meDbContext.SaveChanges();
            var optimizationDbId = existingOptimization.OptimizationProblemDbId;
            _output.WriteLine($"optimizationDbId: {optimizationDbId}");

            var optimizationSpec = new OptimizationSpecification(optimizationDbId);

            var optimizationFromRepo = await _optimizationRepository.GetByIdAsync(optimizationSpec);

            Assert.Equal(existingOptimization.OptimizationProblemId, optimizationFromRepo.OptimizationProblemId);
            Assert.Equal(existingOptimization.Parameters, optimizationFromRepo.Parameters);
        }

        [Fact]
        public async Task GetsExistingOptimizationAsync()
        {
            var firstOptimization = optimizationBuilder.OptimizationWith10Stops();
            _route4meDbContext.Optimizations.Add(firstOptimization);
            var firstOptimizationDbId = firstOptimization.OptimizationProblemDbId;

            var secondOptimization = optimizationBuilder.OptimizationWith24Stops();
            _route4meDbContext.Optimizations.Add(secondOptimization);
            var secondOptimizationDbId = secondOptimization.OptimizationProblemDbId;

            _route4meDbContext.SaveChanges();

            var optimizationFilterSpecification = new OptimizationSpecification(new string[] { firstOptimizationDbId, secondOptimizationDbId });
            int optimizationCount = await _optimizationRepository.CountAsync(optimizationFilterSpecification);
            Assert.True(optimizationCount >= 2);

            var optimizationFromRepo = await _optimizationRepository.GetOptimizationByIdAsync(secondOptimizationDbId);

            Assert.Equal(secondOptimizationDbId, optimizationFromRepo.OptimizationProblemDbId);
            Assert.Equal(secondOptimization.ParametersObj.RouteName, optimizationFromRepo.ParametersObj.RouteName);
            Assert.Equal(secondOptimization.Addresses.Count(), optimizationFromRepo.Addresses.Count());
        }

        [Fact]
        public async Task GetOptimizationsAsync()
        {
            var optimizationDbIDs = new List<string>();

            var firstOptimization = optimizationBuilder.OptimizationWith10Stops();
            _route4meDbContext.Optimizations.Add(firstOptimization);
            var firstOptimizationDbId = firstOptimization.OptimizationProblemDbId;
            optimizationDbIDs.Add(firstOptimizationDbId);

            var secondOptimization = optimizationBuilder.OptimizationWith24Stops();
            _route4meDbContext.Optimizations.Add(secondOptimization);
            var secondOptimizationDbId = secondOptimization.OptimizationProblemDbId;
            optimizationDbIDs.Add(secondOptimizationDbId);

            _route4meDbContext.SaveChanges();

            var optimizations = await _optimizationRepository.GetOptimizationsAsync(0, 1000);

            var linqOptimizations = await _route4meDbContext.Optimizations
                .Where(x => optimizationDbIDs.Contains(x.OptimizationProblemDbId)).ToListAsync<OptimizationProblem>();

            foreach (var linqOptimization in linqOptimizations)
            {
                Assert.Contains(linqOptimization, optimizations);
            }
        }

        [Fact]
        public async Task UpdateOptimizationAsync()
        {
            var optimization = optimizationBuilder.OptimizationWith10Stops();
            _route4meDbContext.Optimizations.Add(optimization);
            var firstOPtimizationDbId = optimization.OptimizationProblemDbId;

            _route4meDbContext.SaveChanges();

            optimization.ParametersObj.RouteName = "Route Modified";
            optimization.ScheduledFor = 1568246400;

            var updatedOptimization = await _optimizationRepository.UpdateOptimizationAsync(optimization.OptimizationProblemDbId, optimization);

            _route4meDbContext.SaveChanges();

            var linqOptimization = _route4meDbContext.Optimizations
                .Where(x => x.OptimizationProblemDbId == updatedOptimization.OptimizationProblemDbId).FirstOrDefault();

            Assert.Equal<OptimizationProblem>(updatedOptimization, linqOptimization);
        }
    }
}
