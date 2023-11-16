using DataLib.Cards;

namespace DataLib.Desks;

public interface IDesk
{
    IList<Card> Cards { get; }

    int Length => Cards.Count;

    Card GetByIndex(int index) => Cards[index];

    void Split(out Card[] first, out Card[] second)
    {
        var mid = Length / 2;
        first = Cards.Take(mid).ToArray();
        second = Cards.Skip(mid).ToArray();
    }
}