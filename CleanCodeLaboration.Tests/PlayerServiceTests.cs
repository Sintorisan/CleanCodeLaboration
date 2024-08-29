using CleanCodeLaboration.Services;

namespace CleanCodeLaboration.Tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        private PlayerService _playerService;
        private string _playerId;

        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists("playersDb.csv"))
                File.Delete("playersDb.csv");

            _playerId = "TestPlayerId";
            _playerService = new PlayerService(_playerId);
        }

        [TestMethod]
        public void InitialLoadAssignPlayerField()
        {
            var currentPlayer = _playerService.GetCurrentPlayer();
            Assert.AreEqual(_playerId, currentPlayer.PlayerId);
        }

        [TestMethod]
        public void CreateAPlayer()
        {
            _playerService.SetPlayer(_playerId);
            var player = _playerService.GetSinglePlayer(_playerId);
            Assert.AreEqual(_playerId, player.PlayerId);
        }

        [TestMethod]
        public void GetASinglePlayer()
        {
            var player = _playerService.GetSinglePlayer(_playerId);
            Assert.AreEqual(player.PlayerId, _playerId);
        }

        [TestMethod]
        public void FindPlayerInList()
        {
            Assert.IsTrue(_playerService.isPlayerFound(_playerId));
        }
    }
}