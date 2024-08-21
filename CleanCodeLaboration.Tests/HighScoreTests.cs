using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Games.Moo;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration.Tests
{
    [TestClass]
    public class HighScoreTests
    {
        private IGameLogic _gameLogic;
        private HighScoreService _highScoreService;
        private string _playerId;
        private string _filePath;
        private HighScoreForm _highScoreForm;

        [TestInitialize]
        public void TestInitialize()
        {
            _gameLogic = new MooGameLogic();
            _highScoreService = new HighScoreService(_gameLogic);
            _playerId = "TestPlayerId";
            _filePath = $"{_gameLogic.GameId}.csv";

            if (File.Exists(_filePath))
                File.Delete(_filePath);

            _highScoreForm = AddTestHighScoreToDb();
        }

        private HighScoreForm AddTestHighScoreToDb(int score = 5)
        {
            var highScoreForm = new HighScoreForm
            {
                Date = DateTime.Now,
                GameId = _gameLogic.GameId,
                PlayerId = _playerId,
                HighScore = score
            };

            _highScoreService.AddHighScore(highScoreForm);

            return highScoreForm;
        }

        [TestMethod]
        public void DoesGetAllHighScores()
        {
            var highScores = _highScoreService.GetAllHighScores();

            Assert.IsTrue(highScores.Any());
            Assert.IsTrue(highScores.Contains(_highScoreForm));
        }

        [TestMethod]
        public void DoesGetAllPlayerHighScore()
        {
            var playerHighScores = _highScoreService.GetAllUserHighScore(_playerId);

            Assert.IsTrue(playerHighScores.Any());
            Assert.IsTrue(playerHighScores.Contains(_highScoreForm));
        }

        [TestMethod]
        public void DoesGetTheHighestPlayerHighScore()
        {
            var newHighScoreForm = AddTestHighScoreToDb(10);

            var getHighestScore = _highScoreService.GetHighestPlayerScore(_playerId);
            Assert.AreEqual(newHighScoreForm.HighScore, getHighestScore.HighScore);
        }
    }
}