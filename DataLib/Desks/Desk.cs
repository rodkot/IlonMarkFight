namespace DataLib.Desk;

using Card;

public abstract class Desk
{
    protected readonly Card[] Cards;

    public int Length => Cards.Length;

    public Desk(uint count)
    {
        Card[] cards = new Card[count];  
        for (int i = 0; i < count; i += 2)
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