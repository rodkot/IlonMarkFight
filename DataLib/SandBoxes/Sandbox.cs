using DataLib.Desks;
using DataLib.Desks.Interfaces;
using DataLib.Exceptions;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;

namespace DataLib.SandBoxes;

public class Sandbox
{
    private readonly IChooseCard _opponentFirst;
    private readonly IChooseCard _opponentSecond;
    private readonly ShuffleableDesk _desk;
    private readonly IDistributor _distributor;
    private readonly IDeskShuffler _deskShuffler;

    public Sandbox(IEnumerable<IChooseCard> opponents, ShuffleableDesk desk, IDistributor distributor,
        IDeskShuffler deskShuffler)
    {
        var enumerable = opponents.ToArray();
        
        if (enumerable.Length < 2)
        {
            throw new NotEnoughPlayersException($"expected 2 players, have {enumerable.Length}");
        }

        _opponentFirst = enumerable[0];
        _opponentSecond = enumerable[1];
        _desk = desk;
        _distributor = distributor;
        _deskShuffler = deskShuffler;
    }

    public bool Round()
    {
        _deskShuffler.Shuffle(_desk);
        _desk.Split(out var firstSplitCard, out var secondSplitCard);

        var firstCard = _opponentFirst.Choose(firstSplitCard);
        var secondCard = _opponentSecond.Choose(secondSplitCard);

        return _distributor.Judge(firstCard, secondCard);
    }
}