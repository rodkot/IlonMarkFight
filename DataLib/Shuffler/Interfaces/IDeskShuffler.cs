using DataLib.Desks;
using DataLib.Desks.Interfaces;

namespace DataLib.Shuffler.Interfaces;

public interface IDeskShuffler
{
    void Shuffle(IShuffleableDesk desk);
}