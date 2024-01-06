namespace DataLib.Desks.Interfaces;

public interface IShuffleableDesk : IDesk
{
    void SwapCards(int i, int j)
    {
        if (i >= Cards.Count || j >= Cards.Count) return;
        (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
    }
}