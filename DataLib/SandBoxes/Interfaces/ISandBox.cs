using DataLib.Desks.Interfaces;

namespace DataLib.SandBoxes.Interfaces;

public interface ISandBox
{
    bool Round(IShuffleableDesk desk);
}