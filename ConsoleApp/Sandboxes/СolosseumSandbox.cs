using DataLib.Desk;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;
using DataLib.SandBoxes;

namespace ConsoleApp.Sandboxes;
 

public class СolosseumSandbox: Sandbox
{
    public СolosseumSandbox(IEnumerable<Opponent> opponents, ShuffleableDesk desk, Distributor distributor, IDeskShuffler deskShuffler) : base(opponents, desk, distributor, deskShuffler)
    {
    }
}