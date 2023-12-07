using DataLib.Cards;
using DataLib.Desks;
using Models;

namespace Test;

[TestFixture]
public class DeskTest
{

    [Test] 
    public void DeskLengthHasNotZero()
    {
        var action = () => { new ShuffleableDesk(0); };
        
        action.Should().Throw<ArgumentException>();   
    }
    
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(1000)]
    public void DeskLengthHasExpectedLength(int count)
    {
        IDesk desk = new ShuffleableDesk(count);
    
        count.Should().Be(desk.Length);
    }
    //
    [TestCase(36)]
    [TestCase(54)]
    public void DeskSplitTwoHalvesEqualLength(int count)
    {
        IDesk desk = new ShuffleableDesk(count);
        
        desk.Split(out var firstSplitCard, out var secondSplitCard);
    
        secondSplitCard.Length.Should().Be(firstSplitCard.Length);
    }
    
    [TestCase(36)]
    [TestCase(54)]
    public void DeskSplitTowNotEqualHalves(int count)
    {
        IDesk desk = new ShuffleableDesk(count);
        var countEqualsCard = 0;
        
        desk.Split(out var firstSplitCard, out var secondSplitCard);
        for (var i = 0u; i < count/2; i++)
        {
            for (var j = 0u; j < count/2; j++)
            {
                if (firstSplitCard[i].Equals(secondSplitCard[j]))
                {
                    countEqualsCard++;
                }
            }
        }
        
        countEqualsCard.Should().Be(0);
    }

    [TestCase(36)]
    [TestCase(54)]
    public void DeskHasTwoHalvesNotEqualColor(int count)
    {
        var desk = new ShuffleableDesk(count);
        
        var countBlackCards = desk.Cards.Count(c => c.Color == Color.Black);
        var countRedCards = desk.Cards.Count(c => c.Color == Color.Red);
        
        countRedCards.Should().Be(countBlackCards);
    }

    [TestCase(36)]
    [TestCase(54)]
    public void DeskHasNotSameCard(int count)
    {
        IDesk desk = new ShuffleableDesk(count);
        var countEqualsCard = 0;
        
        for (var i = 0; i < count - 1; i++)
        {
            for (var j = i + 1; j < count; j++)
            {
                if (desk.GetByIndex(j).Equals(desk.GetByIndex(i)))
                {
                    countEqualsCard++;
                }
            }
        }
    
        countEqualsCard.Should().Be(0);
    }

    [TestCase(1)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(1001)]
    public void DeskOddLengthThrowsException(int count)
    {
        var action = () => { new ShuffleableDesk(count); };
        
        action.Should().Throw<ArgumentException>();
    }
}