using DataLib.Cards;

namespace Test;
[TestFixture]
public class CardTests
{
    
    [Test]
    public void CardHasSameColorPassedToConstructor()
    {
        var c = new Card(Color.Black, 1);
        
        c.Color.Should().Be(Color.Black);
    }

    [Test]
    public void CardHasSameNumberPassedToConstructor()
    {
        const int num = 1;
        var c = new Card(Color.Black, num);
        
        c.Should().NotBeNull();
        c.Number.Should().Be(num);
    }
    
    [Test]
    public void CardEqualsCardWithSelfTrue()
    {
        var c = new Card(Color.Black, 1);

        c.Should().Be(c);
    }
    
    [Test]
    public void CardEqualsCardWithDifferentColorFalse()
    {
        var c1 = new Card(Color.Black, 1);
        var c2 = new Card(Color.Red, 1);

        c1.Should().NotBe(c2);
    }
    
    [Test]
    public void CardEqualsCardWithDifferentNumberFalse()
    {
        var c1 = new Card(Color.Black, 1);
        var c2 = new Card(Color.Black, 2);

        c1.Should().NotBe(c2);
    }
    
    [Test]
    public void CardEqualsCardWithSameNumberAndColorTrue()
    {
        var c1 = new Card(Color.Black, 1);
        var c2 = new Card(Color.Black, 1);
        
        c1.Should().Be(c2);
    }
}