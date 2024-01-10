using Core.Desks;
using Core.Shufflers;
using DataLib.Shuffler.Interfaces;
using DbStorage.Context;
using DbStorage.Entities;
using DbStorage.Service;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Test;

public class DbTests
{
    private SqliteConnection _connection;
    private DbContextOptions<ExperimentConditionContext> _contextOptions;
    private readonly IDeskShuffler _shuffler = new RandomDeskShuffler();

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("Data source=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<ExperimentConditionContext>()
            .UseSqlite(_connection)
            .Options;
    }

    private ExperimentConditionContext CreateContext() => new(_contextOptions);

    [TearDown]
    public void Dispose()
    {
        _connection.Dispose();
    }

    private Mock<IDbContextFactory<ExperimentConditionContext>> DbFactoryMock()
    {
        var factoryMock = new Mock<IDbContextFactory<ExperimentConditionContext>>();
        factoryMock.Setup(f => f.CreateDbContext()).Returns(CreateContext());
        return factoryMock;
    }

    [Test]
    public void ExperimentConditionServiceCallsCreateDbContextOnlyOnce()
    {
        var factoryMock = DbFactoryMock();

        var service = new ExperimentConditionService(
            factoryMock.Object);

          factoryMock.Verify(f => f.CreateDbContext(), Times.Once);
    }

    // [Test]
    // public void ExperimentConditionServiceAddOneExperimentCondition()
    // {
    //     var factoryMock = DbFactoryMock();
    //     var service = new ExperimentConditionService(factoryMock.Object);
    //     var desk = new Shuffleable36CardDesk();
    //     var context = CreateContext();
    //     var condition = context.Find<ExperimentConditionEntity>(1);
    //     var entities = condition!.CardEntities;
    //     
    //     _shuffler.Shuffle(desk);
    //     service.AddOne(desk);
    //     context.Database.EnsureCreated();
    //     context.Entry(condition!).Collection(c => c.CardEntities).Load();
    //    
    //
    //     condition.Should().NotBeNull("");
    //     desk.Cards.Should().BeEquivalentTo(entities, options => options.ExcludingMissingMembers(), "card values don't match");
    // }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public void ExperimentConditionServiceReturnsNSameCardDesks(int n)
    {
        var conditionsList = new List<ExperimentConditionEntity>();
        for (var i = 0; i < n; i++)
        {
            var desk = new Shuffleable36CardDesk();
            _shuffler.Shuffle(desk);
            conditionsList.Add(ExperimentConditionEntity.FromDesk(desk));
        }
        var factoryMock = DbFactoryMock();
        var context = CreateContext();
        context.Database.EnsureCreated();
        context.AddRange(conditionsList);
        context.SaveChanges();
        
        var service = new ExperimentConditionService(
            factoryMock.Object);
    
        var desks = service.GetFirstN(n);

        desks.Should().HaveCount(conditionsList.Count, "desk count mismatch");

        for (var i = 0; i < desks.Count; i++)
        {
            desks[i].Length.Should().Be(conditionsList[i].CardEntities.Count, $"desk lengths are not equal on iteration {i}");

            desks[i].Cards.Should().BeEquivalentTo(conditionsList[i].CardEntities,
                options => options.ExcludingMissingMembers(),
                $"card values don't match in desk {i}");
        }
    }
}