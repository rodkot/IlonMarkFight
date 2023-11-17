using DataLib.Desks.Interfaces;
using DataLib.Distributors.Interfaces;
using DataLib.Exceptions;
using DataLib.Opponents.Interfaces;
using DataLib.SandBoxes.Interfaces;
using DataLib.Shuffler.Interfaces;

namespace Core.Sandboxes;

public class ShuffleableDeskSandbox: ISandBox
{
    private readonly IChooseCard _opponentFirst;
    private readonly IChooseCard _opponentSecond;
    private readonly IDistributor _distributor;
    private readonly IDeskShuffler _deskShuffler;

    public ShuffleableDeskSandbox(IEnumerable<IChooseCard> opponents, IDistributor distributor,
        IDeskShuffler deskShuffler)
    {
        var enumerable = opponents.ToArray();

        if (enumerable.Length < 2)
        {
            throw new NotEnoughPlayersException($"expected 2 players, have {enumerable.Length}");
        }

        _opponentFirst = enumerable[0];
        _opponentSecond = enumerable[1];
        _distributor = distributor;
        _deskShuffler = deskShuffler;
    }

  
    public bool Round(IShuffleableDesk desk)
    {
        _deskShuffler.Shuffle(desk);
        desk.Split(out var firstSplitCard, out var secondSplitCard);

        var firstCard = _opponentFirst.Choose(firstSplitCard);
        var secondCard = _opponentSecond.Choose(secondSplitCard);

        return _distributor.Judge(firstCard, secondCard);
    }
}