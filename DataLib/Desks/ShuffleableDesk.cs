namespace DataLib.Desks;

public class ShuffleableDesk : Desk
{
    protected ShuffleableDesk(uint count) : base(count)
    {
    }
    
    public void SwapCards(int i, int j)
    {
        if (i >= Cards.Length || j >= Cards.Length) return;
        (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
    }
}