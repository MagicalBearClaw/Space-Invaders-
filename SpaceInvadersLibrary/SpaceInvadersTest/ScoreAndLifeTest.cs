using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvadersLibrary;
using Microsoft.Xna.Framework;

namespace SpaceInvadersTest
{
    [TestClass]
    public class ScoreAndLifeTest
    {
        [TestMethod]
        public void TestDecrementLives()
        {
            ScoreAndLife scoreAndLife = new ScoreAndLife();
            int initialLives = scoreAndLife.Lives;

            scoreAndLife.DecrementLives();

            Assert.AreEqual(initialLives - 1, scoreAndLife.Lives);
        }

        [TestMethod]
        public void TestNoLivesEvent()
        {
            ScoreAndLife scoreAndLife = new ScoreAndLife(1);
            Player player = new Player(new Vector2(400, 500), 5, 600, 800, 80, 80, null);
            scoreAndLife.NoLives += player.GameOver;

            //should put lives to 0 and raise the NoLives event
            scoreAndLife.DecrementLives();

            Assert.AreEqual(false, player.IsAlive);
        }

        [TestMethod]
        public void TestIncreasePoints()
        {
            ScoreAndLife scoreAndLife = new ScoreAndLife();
            int initialScore = scoreAndLife.Score;
            int addingScore = 500;

            scoreAndLife.IncreasePoints(addingScore);

            Assert.AreEqual(initialScore + addingScore, scoreAndLife.Score);
        }
    }
}
