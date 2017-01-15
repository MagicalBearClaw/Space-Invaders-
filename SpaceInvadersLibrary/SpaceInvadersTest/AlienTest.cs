using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvadersLibrary;
using Microsoft.Xna.Framework;

namespace SpaceInvadersTest
{
    [TestClass]
    public class AlienTest
    {
        [TestMethod]
        public void TestShootWithAliveProjectile()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(10, 500, 500, 10, new Vector2());
            Alien alien = new Alien(new Vector2(250, 250), 0, 0, 500, 500, 20, 20, 0, bomb);
            //act
            alien.Shoot();
            //assert
            Assert.AreEqual(true, bomb.IsAlive);
        }
        [TestMethod]
        public void TestShootWithDeadProjectile()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(0, 500, 500, 10, new Vector2());
            bomb.IsAlive = false;
            Alien alien = new Alien(new Vector2(250, 250), 0, 0, 500, 500, 20, 20, 0, bomb);
            //act
            alien.Shoot();
            //assert
            Assert.AreEqual(true, bomb.IsAlive);
        }
        [TestMethod]
        public void TestMoveHorizontalRight()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(0, 500, 500, 10, new Vector2());
            Alien alien = new Alien(new Vector2(250, 250), 20, 20, 500, 500, 20, 20, 0, bomb);
            //act
            alien.MoveHorizontal(Direction.RIGHT);
            //assert
            Assert.AreEqual(270, alien.BoundingBox.X);
        }
        [TestMethod]
        public void TestMoveHorizontalLeft()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(0, 500, 500, 10, new Vector2());
            Alien alien = new Alien(new Vector2(250, 250), 20, 20, 500, 500, 20, 20, 0, bomb);
            //act
            alien.MoveHorizontal(Direction.LEFT);
            //assert
            Assert.AreEqual(230, alien.BoundingBox.X);
        }
        [TestMethod]
        public void TestMoveDown()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(0, 500, 500, 10, new Vector2());
            Alien alien = new Alien(new Vector2(250, 250), 20, 20, 500, 500, 20, 20, 0, bomb);
            //act
            alien.MoveDown();
            //assert
            Assert.AreEqual(270, alien.BoundingBox.Y);
        }
    }
}
