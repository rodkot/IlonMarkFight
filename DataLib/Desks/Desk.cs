using DataLib.Cards;

namespace DataLib.Desks;

public abstract class Desk
{
    protected readonly Card[] Cards;

    public int Length => Cards.Length;

    protected Desk(int count)
    {
        if (count%2 == 1)
        {
            throw new ArgumentException("Не может существовать нечетной колоды");
        }
        
        var cards = new Card[count];  
        for (var i = 0; i < count; i += 2)
        {
            cards[i] = new Card(Color.Black, i);
            cards[i + 1] = new Card(Color.Red, i + 1);
        }

        Cards = cards;
    }

    public Card GetByIndex(uint index)
    {
        return Cards[index];
    }
    public virtual void Split(out Card[] first, out Card[] second)
    {
        var mid = Cards.Length / 2;
        first = Cards.Take(mid).ToArray();
        second = Cards.Skip(mid).ToArray();
    }
}