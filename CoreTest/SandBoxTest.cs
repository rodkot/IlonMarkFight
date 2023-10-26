using Core.Strategies;
using DataLib.Cards;
using DataLib.Desks;
using DataLib.Desks.Interfacies;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;
using DataLib.Persons.Opponents.Strategies;
using DataLib.SandBoxes;
using Microsoft.Extensions.Logging;

namespace CoreTest;

[TestFixture]
public class SandBoxTest
{
    private Mock<IDeskShuffler> _shufflerMock;
    private Mock<ShuffleableDesk> _deskMock;

    private Mock<IDistributor> _distributor;
    private Mock<IChooseCard> _elonMock;
    private Mock<IChooseCard> _markMock;

    private Sandbox _sandbox;
    private Card[] _firstAfterSplit;
    private Card[] _secondAfterSplit;

    [SetUp]
    public void SetUp()
    {
        MockDesk();
        MockShuffler();
        MockOpponents();
        MockDistributor();
        CreateSandbox();
    }

    [Test]
    public void Sandbox_Round_CallsSplit_OnlyOnce()
    {
        _sandbox.Round();
        
        _deskMock.Verify(desk => desk.Split(out _firstAfterSplit, out _secondAfterSplit), Times.Once);
    }
    
    [Test]
    public void Sandbox_Round_CallsShuffle_OnlyOnce()
    {
        _sandbox.Round();

        _shufflerMock.Verify(s => s.Shuffle(It.IsAny<ShuffleableDesk>()), Times.Once);
    }

    [Test]
    public void Sandbox_Round_Has_ExpectedResult()
    {
        var result = _sandbox.Round();

        result.Should().Be(_firstAfterSplit[0].Color == _secondAfterSplit[0].Color);
    }

    private void MockDesk()
    {
        var deck = new ShuffleableDesk(36);
        deck.Split(out _firstAfterSplit, out _secondAfterSplit);

        _deskMock = new Mock<ShuffleableDesk>(36);
        _deskMock.Setup(d => d.Split(out _firstAfterSplit, out _secondAfterSplit)).Callback(() =>
        {
            // Logic to populate firstHalf and secondHalf mock data.
        });
    }

    private void MockShuffler()
    {
        _shufflerMock = new Mock<IDeskShuffler>();
        // _shufflerMock.Setup(s => s.Shuffle(It.IsAny<ShuffleableDesk>())).Callback(() => {
        //     Console.Out.WriteLine("helllo");
        // });
    }

    private void MockOpponents()
    {
        _elonMock = new Mock<IChooseCard>();
        _markMock = new Mock<IChooseCard>();

        _elonMock.Setup(e => e.Choose(_firstAfterSplit)).Returns(_firstAfterSplit.First);
        _markMock.Setup(m => m.Choose(_secondAfterSplit)).Returns(_secondAfterSplit.First);
    }

    private void MockDistributor()
    {
        _distributor = new Mock<IDistributor>();

        _distributor.Setup(distributor => distributor.Judge(_firstAfterSplit.First(), _secondAfterSplit.First()))
            .Returns(_firstAfterSplit.First().Color == _secondAfterSplit.First().Color);
    }

    private void CreateSandbox()
    {
        _sandbox = new Sandbox(new List<IChooseCard>
            {
                _elonMock.Object,
                _markMock.Object
            },
            _deskMock.Object,
            _distributor.Object,
            _shufflerMock.Object
        );
    }
}