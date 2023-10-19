namespace DataLib.Desk;
using Card;

public interface IDeskShuffler
{
    void Shuffle(in ShuffleableDesk desk);
}