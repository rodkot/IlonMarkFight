using DataLib.Desks.Interfaces;
using DataLib.Distributors.Interfaces;
using DataLib.Exceptions;
using DataLib.Opponents;
using DataLib.Opponents.Interfaces;
using DataLib.SandBoxes.Interfaces;

namespace Core.Sandboxes;
 

public class NoShuffleableDeskSandbox: ISandBox
{
    private readonly IChooseCard _opponentFirst;
    private readonly IChooseCard _opponentSecond;
    private readonly IDistributor _distributor;
    public NoShuffleableDeskSandbox(IEnumerable<Opponent> opponents, IDistributor distributor) 
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

        var firstCard = _opponentFirst.Choose(firstSplitCard);
        var secondCard = _opponentSecond.Choose(secondSplitCard);

        return _distributor.Judge(firstCard, secondCard);
    }
}