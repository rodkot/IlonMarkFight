using DataLib.Cards;

namespace DataLib.Desks;

public interface IDesk
{
    public IList<Card> Cards { get; }
    
    public Card GetByIndex(int index);
    
    public void Split(out Card[] first, out Card[] second);
}