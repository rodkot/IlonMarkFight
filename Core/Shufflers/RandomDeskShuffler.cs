using DataLib.Desks;
using DataLib.Desks.Interfacies;

namespace Core.Shufflers;

public class RandomDeskShuffler:IDeskShuffler
{
    private static readonly Random Rnd = new();
    
    // uses Fisher–Yates shuffle
    public void Shuffle(in ShuffleableDesk desk)
    {
        for (var i = desk.Length - 1; i >= 0; i--)
        {
            var j = Rnd.Next(i + 1);
            desk.SwapCards(i, j);
        }
    }
}