using DataLib.Desks;
using DataLib.Desks.Interfaces;

namespace Core.Shufflers;
public class RandomDeskShuffler:IDeskShuffler
{
    private static readonly Random Rnd = new();
    
    public void Shuffle(ShuffleableDesk desk)
    {
        for (var i = desk.Length - 1; i >= 0; i--)
        {
            var j = Rnd.Next(i + 1);
            desk.SwapCards(i, j);
        }
    }


}