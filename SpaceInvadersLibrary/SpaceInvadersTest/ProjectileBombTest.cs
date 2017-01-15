using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvadersLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace SpaceInvadersTest
{
    [TestClass]
    public class ProjectileBombTest
    {
        [TestMethod]
        public void TestMoveBomb()
        {
            //arrange
            Projectile projectile = new BombProjectile(20, 500, 10, 10, new Vector2(250, 250));
            //act
            projectile.Move();
            Vector2 secondPosition = new Vector2(projectile.BoundingBox.X, projectile.BoundingBox.Y);
            //assert
            Assert.AreEqual(new Vector2(250, 270).Y, secondPosition.Y);
        }

        [TestMethod]
        public void TestHasBombGoneOffScreen()
        {
            //arrange
            Projectile bomb = new BombProjectile(20, 500, 10, 10, new Vector2(250, 9));
            //act
            bomb.Move();
            //assert
            Assert.AreEqual(false, bomb.IsAlive);
        }

        [TestMethod]
        private void TestBombCollisionWithPlayer()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(10, 500, 20, 20, new Vector2(100, 50));
            Player player = new Player(new Vector2(100, 75), 0, 500, 500, 20, 20, null);
            bomb.RegisterPlayer(player);
            //act
            bomb.Move();
            //assert
            Assert.AreEqual(false, bomb.IsAlive);
        }
        [TestMethod]
        public void TestBombCollisionWithBunker()
        {
            //arrange
            BombProjectile bomb = new BombProjectile(10, 500, 20, 20, new Vector2(150, 50));
            Bunker bunker = new Bunker(1, 10, 10, new Vector2(150, 65));
            Bunkers bunkers = new Bunkers();
            bunkers.AddBunker(bunker);
            bomb.RegisterBunkers(bunkers);
            //act
            bomb.Move();
            //assert
            Assert.AreEqual(false, bomb.IsAlive);
        }

    }
}
