using System.Collections;
using DataLib.Cards;
using DataLib.Desks.Interfaces;

namespace Models;

public class ShuffleableDesk : IShuffleableDesk, IEnumerable
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

    public ShuffleableDesk(IList<Card> cards)
    {
        Cards = cards;
    }

    public IEnumerator GetEnumerator()
    {
        return Cards.GetEnumerator();
    }
}