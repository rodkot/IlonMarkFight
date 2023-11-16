using Core.Desks;
using Core.Strategies;
using DataLib.Cards;
using DataLib.Desks;

namespace CoreTest;

[TestFixture]
public class StrategyTest
{
    [TestCase(36)]
    [TestCase(54)]
    public void PickFirstStrategyAlwaysReturnsFirst(int count)
    {
        Card[] firstAfterSplit = { new(Color.Black, 1) };
        Card[] secondAfterSplit = { new(Color.Black, 2) };
        var strategy = new PickFirstCardStrategy();

        strategy.Pick(firstAfterSplit).Should().Be(firstAfterSplit.First());
        strategy.Pick(secondAfterSplit).Should().Be(secondAfterSplit.First());
    }
}