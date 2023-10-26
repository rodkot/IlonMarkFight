using DataLib.Cards;

namespace CoreTest;
[TestFixture]
public class CardTests
{
    
    [Test]
    public void Card_Has_SameColorPassedToConstructor()
    {
        var c = new Card(Color.Black, 1);
        
        c.Color.Should().Be(Color.Black);
    }

    [Test]
    public void Card_Has_SameNumberPassedToConstructor()
    {
        const int num = 1;
        var c = new Card(Color.Black, num);
        
        c.Number.Should().Be(num);
    }
    
    [Test]
    public void Card_Equals_CardWithNull_False()
    {
        var c = new Card(Color.Black, 1);
        
        c.Should().NotBe(null);
    }

    [Test]
    public void Card_Equals_CardWithSelf_True()
    {
        var c = new Card(Color.Black, 1);

        c.Should().Be(c);
    }
    
    [Test]
    public void Card_Equals_CardWithDifferentColor_False()
    {
        var c1 = new Card(Color.Black, 1);
        var c2 = new Card(Color.Red, 1);

        c1.Should().NotBe(c2);
    }
    
    [Test]
    public void Card_Equals_CardWithDifferentNumber_False()
    {
        var c1 = new Card(Color.Black, 1);
        var c2 = new Card(Color.Black, 2);

        c1.Should().NotBe(c2);
    }
    
    [Test]
    public void Card_Equals_CardWithSameNumberAndColor_True()
    {
        var c1 = new Card(Color.Black, 1);
        var c2 = new Card(Color.Black, 1);
        
        c1.Should().Be(c2);
    }
}