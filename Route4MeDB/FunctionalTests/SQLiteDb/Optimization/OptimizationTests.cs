using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.UnitTests.Builders;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using Route4MeDB.ApplicationCore.Specifications;
using EnumR4M = Route4MeDB.ApplicationCore.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using Route4MeDB.FunctionalTest;
using System.Threading.Tasks;

namespace Route4MeDB.FunctionalTests.SQLiteDb
{
    public class DatabaseOptimizationsFixture : DatabaseFixtureBase
    {
        public DatabaseOptimizationsFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.SQLite;

            GetDbContext(DatabaseProviders.SQLite);

            _optimizationRepository = new OptimizationRepository(_route4meDbContext);
        }

        public OptimizationRepository _optimizationRepository;
    }

    public class OptimizationTests : IDisposable, IClassFixture<DatabaseOptimizationsFixture>
    {
        private readonly ITestOutputHelper _output;
        DatabaseOptimizationsFixture fixture;
        public IConfigurationRoot Configuration { get; }

        public OptimizationTests(DatabaseOptimizationsFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        public void Dispose()
        {
            //_route4meDbContext.Dispose();
        }

        [IgnoreIfNoSqLiteDb]
        public async Task GetOptimizationsTest()
        {
            var optimizationDbIDs = new List<string>();

            var firstOptimization = fixture.optimizationBuilder.OptimizationWith24Stops();
            await fixture._route4meDbContext.Optimizations.AddAsync(firstOptimization);
            string firstOptimizationDbId = firstOptimization.OptimizationProblemDbId;
            optimizationDbIDs.Add(firstOptimizationDbId);

            var secondOptimization = fixture.optimizationBuilder.OptimizationWithSingleDriverRoundtrip();
            await fixture._route4meDbContext.Optimizations.AddAsync(secondOptimization);
            string secondOptimizationDbId = secondOptimization.OptimizationProblemDbId;
            optimizationDbIDs.Add(secondOptimizationDbId);

            var thirdOptimization = fixture.optimizationBuilder.OptimizationWith10Stops();
            await fixture._route4meDbContext.Optimizations.AddAsync(thirdOptimization);
            string thirdOptimizationDbId = thirdOptimization.OptimizationProblemDbId;
            optimizationDbIDs.Add(thirdOptimizationDbId);

            var forthOptimization = fixture.optimizationBuilder.OptimizationWithCustomFields();
            await fixture._route4meDbContext.Optimizations.AddAsync(forthOptimization);
            string forthOptimizationDbId = forthOptimization.OptimizationProblemDbId;
            optimizationDbIDs.Add(forthOptimizationDbId);

            await fixture._route4meDbContext.SaveChangesAsync();

            var optimizationIds = fixture._route4meDbContext.Optimizations
                .OrderByDescending(x => x.CreatedDate)
                .Skip(0).Take(4).Select(o => o.OptimizationProblemDbId);

            var linqOptimizationIds = fixture._route4meDbContext.Optimizations
                .Where(x => optimizationDbIDs.Contains(x.OptimizationProblemDbId))
                .ToList<OptimizationProblem>().Select(o => o.OptimizationProblemDbId).ToList();

            foreach (var linqOptimizationId in linqOptimizationIds)
            {
                Assert.Contains(linqOptimizationId, optimizationIds);
            }
        }

        [IgnoreIfNoSqLiteDb]
        public async Task GetExistingOptimizationTest()
        {
            var firstOptimization = fixture.optimizationBuilder.OptimizationWith24Stops();
            await fixture._route4meDbContext.Optimizations.AddAsync(firstOptimization);

            await fixture._route4meDbContext.SaveChangesAsync();

            var createOptimizationDbId = firstOptimization.OptimizationProblemDbId;

            var optimizationFromRepo = await fixture._optimizationRepository
                .GetOptimizationByIdAsync(createOptimizationDbId);

            var linqOPtimization = fixture._route4meDbContext.Optimizations
                .Where(x => createOptimizationDbId == x.OptimizationProblemDbId).FirstOrDefault();

            Assert.Equal(createOptimizationDbId, optimizationFromRepo.OptimizationProblemDbId);
            Assert.Equal(firstOptimization.Parameters, optimizationFromRepo.Parameters);
            Assert.Equal(firstOptimization.Addresses.Count, optimizationFromRepo.Addresses.Count);
            Assert.Equal(firstOptimization.CreatedTimestamp, linqOPtimization.CreatedTimestamp);
            Assert.Equal(firstOptimization.Links, linqOPtimization.Links);
        }

        [IgnoreIfNoSqLiteDb]
        public async Task UpdateOptimizationAsync()
        {
            var firstOptimization = fixture.optimizationBuilder.OptimizationWith24Stops();
            await fixture._route4meDbContext.Optimizations.AddAsync(firstOptimization);

            await fixture._route4meDbContext.SaveChangesAsync();

            firstOptimization.ParametersObj.RouteName = "Route Name Modified";
            firstOptimization.ParametersObj.DeviceType = EnumR4M.DeviceType.AndroidPhone;

            var updatedOptimization = await fixture._optimizationRepository
                .UpdateOptimizationAsync(firstOptimization.OptimizationProblemDbId, firstOptimization);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqOptimizatin = fixture._route4meDbContext.Optimizations
                .Where(x => x.OptimizationProblemDbId == updatedOptimization.OptimizationProblemDbId).FirstOrDefault();

            Assert.Equal<OptimizationProblem>(updatedOptimization, linqOptimizatin);
        }

        [IgnoreIfNoSqLiteDb]
        public async Task RemoveOptimizationAsync()
        {
            var firstOptimization = fixture.optimizationBuilder.OptimizationWith24Stops();
            await fixture._route4meDbContext.Optimizations.AddAsync(firstOptimization);

            await fixture._route4meDbContext.SaveChangesAsync();

            var createOptimizationDbId = firstOptimization.OptimizationProblemDbId;

            var optimizationState = await fixture._optimizationRepository
                .RemoveOptimizationAsync(createOptimizationDbId);

            Assert.Equal(EntityState.Detached, optimizationState);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqOptimizatin = fixture._route4meDbContext.Optimizations
                .Where(x => x.OptimizationProblemDbId == firstOptimization.OptimizationProblemDbId).FirstOrDefault();

            Assert.Null(linqOptimizatin);
        }
    }
}
