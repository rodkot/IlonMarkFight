using DataLib.Desks;
using DataLib.Desks.Interfaces;
using DataLib.Distributors.Interfaces;
using DataLib.Opponents;
using DataLib.SandBoxes;
using DataLib.Shuffler.Interfaces;

namespace Core.Sandboxes;
 

public class СolosseumSandbox: Sandbox
{
    public СolosseumSandbox(IEnumerable<Opponent> opponents, IShuffleableDesk desk, IDistributor distributor, IDeskShuffler deskShuffler) : base(opponents, desk, distributor, deskShuffler)
    {
    }
}