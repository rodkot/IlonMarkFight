namespace DataLib.Desks.Interfaces;

public interface IShuffleableDesk : IDesk
{
    void SwapCards(int i, int j);
}