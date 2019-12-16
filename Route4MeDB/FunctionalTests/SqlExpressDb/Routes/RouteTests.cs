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
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route4MeDB.FunctionalTest;
using System.IO;

namespace Route4MeDB.FunctionalTests.SqlExpressDb
{
    public class DatabaseRoutesFixture : DatabaseFixtureBase
    {
        public DatabaseRoutesFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.Sqlexpress;

            GetDbContext(DatabaseProviders.Sqlexpress);

            _routeRepository = new RouteRepository(_route4meDbContext);
        }

        public RouteRepository _routeRepository;
    }

    public class RouteTests : IDisposable, IClassFixture<DatabaseRoutesFixture>
    {
        private readonly ITestOutputHelper _output;
        DatabaseRoutesFixture fixture;
        public IConfigurationRoot Configuration { get; }

        public RouteTests(DatabaseRoutesFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        public void Dispose()
        {
            //_route4meDbContext.Dispose();
        }

        [IgnoreIfNoSqlexpressDb]
        public async Task GetRoutesTest()
        {
            var routesDbIDs = new List<string>();

            var firstRoute = fixture.routeBuilder.RouteWith10Stops();
            await fixture._route4meDbContext.Routes.AddAsync(firstRoute);
            string firstRouteDbId = firstRoute.RouteDbId;
            routesDbIDs.Add(firstRouteDbId);

            var secondRoute = fixture.routeBuilder.RouteWith97Stops();
            await fixture._route4meDbContext.Routes.AddAsync(secondRoute);
            string secondRouteDbId = secondRoute.RouteDbId;
            routesDbIDs.Add(secondRouteDbId);

            var thirdRoute = fixture.routeBuilder.RouteWithAllParameters();
            await fixture._route4meDbContext.Routes.AddAsync(thirdRoute);
            string thirdRouteDbId = thirdRoute.RouteDbId;
            routesDbIDs.Add(thirdRouteDbId);

            await fixture._route4meDbContext.SaveChangesAsync();

            var routeIds = fixture._route4meDbContext.Routes
                .OrderByDescending(x => x.CreatedDate)
                .Skip(0).Take(3).Select(o => o.RouteDbId);

            var linqRouteIds = fixture._route4meDbContext.Routes
                .Where(x => routesDbIDs.Contains(x.RouteDbId))
                .ToList<Route>().Select(o => o.RouteDbId).ToList();

            foreach (var linqRouteId in linqRouteIds)
            {
                Assert.Contains(linqRouteId, routeIds);
            }
        }

        [IgnoreIfNoSqlexpressDb]
        public async Task GetExistingRouteTest()
        {
            var firstRoute = fixture.routeBuilder.RouteWith10Stops();
            await fixture._route4meDbContext.Routes.AddAsync(firstRoute);

            await fixture._route4meDbContext.SaveChangesAsync();

            var createdRouteDbId = firstRoute.RouteDbId;

            var routeFromRepo = await fixture._routeRepository
                .GetRouteByIdAsync(createdRouteDbId);

            var linqRoute = fixture._route4meDbContext.Routes
                .Where(x => createdRouteDbId == x.RouteDbId).FirstOrDefault();

            Assert.Equal(createdRouteDbId, routeFromRepo.RouteDbId);
            Assert.Equal(firstRoute.Parameters, routeFromRepo.Parameters);
            Assert.Equal(firstRoute.Addresses.Count, routeFromRepo.Addresses.Count);
            Assert.Equal(firstRoute.CreatedTimestamp, linqRoute.CreatedTimestamp);
            Assert.Equal(firstRoute.Links, linqRoute.Links);
        }

        [IgnoreIfNoSqlexpressDb]
        public async void ImportJsonDataToDataBaseTest()
        {
            string testDataFile = @"TestData/route_with_all_parameters.json";

            DataExchangeHelper dataExchange = new DataExchangeHelper();

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                var importedRoute = dataExchange.ConvertSdkJsonContentToEntity<Route>(jsonContent, out string errorString);

                fixture._route4meDbContext.Routes.Add(importedRoute);

                await fixture._route4meDbContext.SaveChangesAsync();

                string routeDbId = importedRoute.RouteDbId;

                var routeSpec = new RouteSpecification(routeDbId);

                var routeFromRepo = await fixture.r4mdbManager.RoutesRepository.GetByIdAsync(routeSpec);

                Assert.IsType<Route>(routeFromRepo);
            }
        }

        [IgnoreIfNoSqlexpressDb]
        public async void ExportRouteEntityToSdkRouteObject()
        {
            var firstRoute = fixture.routeBuilder.RouteWith10Stops();
            var route = await fixture._route4meDbContext.Routes.AddAsync(firstRoute);

            await fixture._route4meDbContext.SaveChangesAsync();

            DataExchangeHelper dataExchange = new DataExchangeHelper();

            var sdkRoute = dataExchange.ConvertEntityToSDK<Route4MeSDK.DataTypes.DataObjectRoute>(route.Entity, out string errorString);

            Assert.IsType<Route4MeSDK.DataTypes.DataObjectRoute>(sdkRoute);
        }

        [IgnoreIfNoSqlexpressDb]
        public async Task UpdateRouteAsync()
        {
            var firstRoute = fixture.routeBuilder.RouteWith10Stops();
            await fixture._route4meDbContext.Routes.AddAsync(firstRoute);

            await fixture._route4meDbContext.SaveChangesAsync();

            firstRoute.ParametersObj.RouteName = "Route Name Modified";
            firstRoute.ParametersObj.DeviceType = EnumR4M.DeviceType.AndroidPhone;

            var updatedRoute = await fixture._routeRepository
                .UpdateRouteAsync(firstRoute.RouteDbId, firstRoute);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqRoute = fixture._route4meDbContext.Routes
                .Where(x => x.RouteDbId == updatedRoute.RouteDbId).FirstOrDefault();

            Assert.Equal<Route>(updatedRoute, linqRoute);
        }

        [IgnoreIfNoSqlexpressDb]
        public async Task RemoveRouteAsync()
        {
            var firstRoute = fixture.routeBuilder.RouteWith10Stops();
            await fixture._route4meDbContext.Routes.AddAsync(firstRoute);

            await fixture._route4meDbContext.SaveChangesAsync();

            var createdRouteDbId = firstRoute.RouteDbId;

            var routeState = await fixture._routeRepository
                .RemoveRouteAsync(createdRouteDbId);

            Assert.Equal(EntityState.Detached, routeState);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqRoute = fixture._route4meDbContext.Routes
                .Where(x => x.RouteDbId == firstRoute.RouteDbId).FirstOrDefault();

            Assert.Null(linqRoute);
        }
    }
}
