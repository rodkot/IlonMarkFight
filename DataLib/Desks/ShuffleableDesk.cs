namespace DataLib.Desks;

public class ShuffleableDesk : Desk
{
    public ShuffleableDesk(int count) : base(count)
    {
    }
    
    public void SwapCards(int i, int j)
    {
        if (i >= Cards.Length || j >= Cards.Length) return;
        (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
    }
}