using Core.Desks;
using Core.Sandboxes;
using DataLib.Cards;
using DataLib.Desks.Interfaces;
using DataLib.Distributors.Interfaces;
using DataLib.Opponents.Interfaces;
using DataLib.SandBoxes;
using DataLib.SandBoxes.Interfaces;
using DataLib.Shuffler.Interfaces;

namespace CoreTest;

[TestFixture]
public class SandBoxTest
{
    private Mock<IDeskShuffler> _shufflerMock;
    private Mock<IShuffleableDesk> _deskMock;

    private Mock<IDistributor> _distributor;
    private Mock<IChooseCard> _elonMock;
    private Mock<IChooseCard> _markMock;

    private ISandBox _sandbox;
    private Card[] _firstAfterSplit = { new(Color.Black, 1) };
    private Card[] _secondAfterSplit = { new(Color.Black, 2) };

    [SetUp]
    public void SetUp()
    {
        CreateMockDesk();
        CreateMockShuffle();
        CreateMockOpponents();
        CreateMockDistributor();
        CreateSandbox();
    }

    private void CreateMockShuffle()
    {
        _shufflerMock = new Mock<IDeskShuffler>();
    }

    [Test]
    public void SandboxRoundCallsSplitOnlyOnce()
    {
        _sandbox.Round(_deskMock.Object);

        _deskMock.Verify(desk => desk.Split(out _firstAfterSplit, out _secondAfterSplit), Times.Once);
    }

    [Test]
    public void SandboxRoundCallsShuffleOnlyOnce()
    {
        _sandbox.Round(_deskMock.Object);

        _shufflerMock.Verify(s => s.Shuffle(It.IsAny<IShuffleableDesk>()), Times.Once);
    }

    [Test]
    public void SandboxRoundHasExpectedResult()
    {
        var result = _sandbox.Round(_deskMock.Object);

        result.Should().Be(_firstAfterSplit.First().Color == _secondAfterSplit.First().Color);
    }

    private void CreateMockDesk()
    {
        _deskMock = new Mock<IShuffleableDesk>();

        _deskMock.Setup(d => d.Split(out _firstAfterSplit, out _secondAfterSplit)).Callback(() => { });
    }

    private void CreateMockOpponents()
    {
        _elonMock = new Mock<IChooseCard>();
        _markMock = new Mock<IChooseCard>();

        _elonMock.Setup(e => e.Choose(_firstAfterSplit)).Returns(_firstAfterSplit.First);
        _markMock.Setup(m => m.Choose(_secondAfterSplit)).Returns(_secondAfterSplit.First);
    }

    private void CreateMockDistributor()
    {
        _distributor = new Mock<IDistributor>();

        _distributor.Setup(distributor => distributor.Judge(_firstAfterSplit.First(), _secondAfterSplit.First()))
            .Returns(_firstAfterSplit.First().Color == _secondAfterSplit.First().Color);
    }

    private void CreateSandbox()
    {
        _sandbox = new ShuffleableDeskSandbox(new List<IChooseCard>
            {
                _elonMock.Object,
                _markMock.Object
            },
            _distributor.Object,
            _shufflerMock.Object
        );
    }
}