using DataLib.Desks;
using DataLib.Desks.Interfacies;
using DataLib.Exceptions;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;

namespace DataLib.SandBoxes;

public abstract class Sandbox
{
    private readonly Opponent _opponentFirst;
    private readonly Opponent _opponentSecond;
    private readonly ShuffleableDesk _desk;
    private readonly IDistributor _distributor;
    private readonly IDeskShuffler _deskShuffler;

    protected Sandbox(IEnumerable<Opponent> opponents, ShuffleableDesk desk, IDistributor distributor,
        IDeskShuffler deskShuffler)
    {
        var enumerable = opponents as Opponent[] ?? opponents.ToArray();
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

        _opponentFirst.Choose(firstSplitCard);
        _opponentSecond.Choose(secondSplitCard);

       return _distributor.Judge(_opponentFirst, _opponentSecond);
    }
}