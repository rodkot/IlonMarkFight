namespace DataLib.Desks;

public class ShuffleableDesk : Desk
{
    public ShuffleableDesk(int count) : base(count)
    {
    }
    
    public void SwapCards(int i, int j)
    {
        if (i >= Cards.Count || j >= Cards.Count) return;
        (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
    }
}