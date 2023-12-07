using DataLib.Desks.Interfaces;
using DataLib.Distributors.Interfaces;
using DataLib.Exceptions;
using DataLib.Opponents;
using DataLib.Opponents.Interfaces;
using DataLib.SandBoxes.Interfaces;

namespace Core.Sandboxes;
 

public class NoShuffleableDeskSandbox: ISandBox
{
    private readonly IChooseCardAsync _opponentFirst;
    private readonly IChooseCardAsync _opponentSecond;
    private readonly IDistributor _distributor;
    public NoShuffleableDeskSandbox(IEnumerable<IChooseCardAsync> opponents, IDistributor distributor) 
    {
        var enumerable = opponents.ToArray();

        if (enumerable.Length < 2)
        {
            throw new NotEnoughPlayersException($"expected 2 players, have {enumerable.Length}");
        }

        _opponentFirst = enumerable[0];
        _opponentSecond = enumerable[1];
        _distributor = distributor;
    }

    public bool Round(IShuffleableDesk desk)
    {
        desk.Split(out var firstSplitCard, out var secondSplitCard);

        var t1 = _opponentFirst.Choose(firstSplitCard);
        var t2 = _opponentSecond.Choose(secondSplitCard);
        
        Task.WaitAll(t1, t2);

        return _distributor.Judge(t1.Result, t2.Result);
    }
}