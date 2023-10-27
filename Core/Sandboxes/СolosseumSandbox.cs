using DataLib.Desks;
using DataLib.Desks.Interfaces;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;
using DataLib.SandBoxes;

namespace Core.Sandboxes;
 

public class СolosseumSandbox: Sandbox
{
    public СolosseumSandbox(IEnumerable<Opponent> opponents, ShuffleableDesk desk, IDistributor distributor, IDeskShuffler deskShuffler) : base(opponents, desk, distributor, deskShuffler)
    {
    }
}