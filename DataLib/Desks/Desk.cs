using DataLib.Cards;

namespace DataLib.Desks;

public abstract class Desk
{
    protected readonly Card[] Cards;

    public int Length => Cards.Length;

    protected Desk(uint count)
    {
        var cards = new Card[count];  
        for (var i = 0; i < count; i += 2)
        {
            cards[i] = new Card(Color.Black, i);
            cards[i + 1] = new Card(Color.Red, i + 1);
        }

        Cards = cards;
    }

    public void Split(out Card[] first, out Card[] second)
    {
        var mid = Cards.Length / 2;
        first = Cards.Take(mid).ToArray();
        second = Cards.Skip(mid).ToArray();
    }
}