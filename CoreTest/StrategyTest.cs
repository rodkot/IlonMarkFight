using Core.Desks;
using Core.Strategies;

namespace CoreTest;

[TestFixture]
public class StrategyTest
{
    [TestCase(36)]
    [TestCase(54)]
    public void PickFirstStrategy_Always_Returns0(int count)
    {
        var strategy = new PickFirstCardStrategy();
        var desk = new ShuffleableDesk(count);
        
        desk.Split(out var firstPart,out var secondPart);
        
        strategy.Pick(firstPart).Should().Be(firstPart.First());
        strategy.Pick(secondPart).Should().Be(secondPart.First());
    }
}