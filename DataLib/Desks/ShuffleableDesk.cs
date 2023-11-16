using DataLib.Cards;
using DataLib.Desks.Interfaces;

namespace DataLib.Desks;

public class ShuffleableDesk : IShuffleableDesk
{
    public IList<Card> Cards { get; }
    
    public ShuffleableDesk(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Не может быть пустой колоды");
        }
        
        if (count % 2 == 1)
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

    public void SwapCards(int i, int j)
    {
        if (i >= Cards.Count || j >= Cards.Count) return;
        (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
    }
}