using DataLib.Desk;
using DataLib.Exceptions;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;

namespace DataLib.SandBoxes;

public abstract class Sandbox
{
    public readonly Opponent OpponentFirst;
    public readonly Opponent OpponentSecond;
    private readonly ShuffleableDesk _desk;
    private readonly Distributor _distributor;
    private readonly IDeskShuffler _deskShuffler;

    protected Sandbox(IEnumerable<Opponent> opponents, ShuffleableDesk desk, Distributor distributor,
        IDeskShuffler deskShuffler)
    {
        var enumerable = opponents as Opponent[] ?? opponents.ToArray();
        if (enumerable.Length < 2)
        {
            throw new NotEnoughPlayersException($"expected 2 players, have {enumerable.Length}");
        }
        OpponentFirst = enumerable[0];
        OpponentSecond = enumerable[1];
        _desk = desk;
        _distributor = distributor;
        _deskShuffler = deskShuffler;
    }

    public bool Round()
    {
        _deskShuffler.Shuffle(_desk);
        _desk.Split(out var firstSplitCard, out var secondSplitCard);

        OpponentFirst.Choose(firstSplitCard);
        OpponentSecond.Choose(secondSplitCard);

       return _distributor.Judge(OpponentFirst, OpponentSecond);
    }
}