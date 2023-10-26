using DataLib.Cards;
using DataLib.Desks;

namespace CoreTest;

[TestFixture]
public class DeskTest
{
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(1000)]
    public void Desk_Length_HasExpectedLength(int count)
    {
        var desk = new ShuffleableDesk(count);

        count.Should().Be(desk.Length);
    }

    [TestCase(36)]
    [TestCase(54)]
    public void Desk_Split_TwoHalves_EqualLength(int count)
    {
        var desk = new ShuffleableDesk(count);
        
        desk.Split(out var firstSplitCard, out var secondSplitCard);

        secondSplitCard.Length.Should().Be(firstSplitCard.Length);
    }

    [TestCase(36)]
    [TestCase(54)]
    public void Desk_Split_Tow_NotEqual_Halves(int count)
    {
        var desk = new ShuffleableDesk(count);
        desk.Split(out var firstSplitCard, out var secondSplitCard);
        
        for (var i = 0u; i < count/2; i++)
        {
            for (var j = 0u; j < count/2; j++)
            {
                if (firstSplitCard[i].Equals(secondSplitCard[j]))
                {
                    Assert.Fail("Карта не может находиться одновременно в двух частях");
                }
            }
        }
        
        Assert.Pass();
    }

    [TestCase(36)]
    [TestCase(54)]
    public void Desk_Has_TwoHalves_NotEqualColor(int count)
    {
        var desk = new ShuffleableDesk(count);
        var (countRedCards, countBlackCards) = (0, 0);
        for (var i = 0u; i < count; i++)
        {
            switch (desk.GetByIndex(i).Color)
            {
                case Color.Red:
                    countRedCards++;
                    break;
                case Color.Black:
                    countBlackCards++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        countRedCards.Should().Be(countBlackCards);
    }

    [TestCase(36)]
    [TestCase(54)]
    public void Desk_HasNot_SameCard(int count)
    {
        var desk = new ShuffleableDesk(count);
        var countEqualsCard = 0;
        for (var i = 0u; i < count - 1; i++)
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
    public void Desk_Odd_Length_ThrowsException(int count)
    {
        var action = () => { new ShuffleableDesk(count); };
        
        action.Should().Throw<ArgumentException>();
    }
}