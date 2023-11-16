namespace DataLib.Desks.Interfaces;

public interface IShuffleableDesk : IDesk
{
    public void SwapCards(int i, int j);
    
}