using CleanCodeLaboration.Games.Moo;

namespace CleanCodeLaboration.Tests
{
    [TestClass]
    public class MooGameLogicTests
    {
        private MooGameLogic _gameLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            _gameLogic = new MooGameLogic();

            _gameLogic.FirstDataStorage = "1234";
            _gameLogic.SecondDataStorage = "1243";
        }

        [TestMethod]
        public void GameStartUpCreateFourDigitNumber()
        {
            _gameLogic.GameStartUp();

            Assert.IsTrue(_gameLogic.FirstDataStorage.Length == 4);
        }

        [TestMethod]
        public void GamePlayLoopAddScoreToUser()
        {
            _gameLogic.GamePlayLoop();

            Assert.AreEqual(1, _gameLogic.Score);
        }

        [TestMethod]
        public void GameShutDownResetStorage()
        {
            _gameLogic.GameShutDown();

            Assert.AreEqual(string.Empty, _gameLogic.FirstDataStorage);
            Assert.AreEqual(string.Empty, _gameLogic.SecondDataStorage);
        }

        [TestMethod]
        public void GamePlayLoopGiveRightResponse()
        {
            _gameLogic.GamePlayLoop();

            Assert.AreEqual("BB,CC", _gameLogic.SecondDataStorage);
        }
    }
}