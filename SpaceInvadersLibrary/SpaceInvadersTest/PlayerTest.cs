using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvadersLibrary;
using Microsoft.Xna.Framework;

//public Player(Vector2 position, int velocityX, int screenWidth, int screenHeight, int playerWidth, int playerHeight)
namespace SpaceInvadersTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestPlayerDeathEvent()
        {
            //player moves and shoots
            int StartPositionX = 400;
            Player player = new Player(new Vector2(StartPositionX, 500), 50, 600, 800, 80, 80,new LaserProjectile(5, 50, 10, 10, new Vector2(400, 300)));
            player.IsAlive = true;
            player.MoveRight();
            player.Shoot();

            //player dies and comes back to original position
            player.ProcessPlayerDeath(player.Projectile);

            Assert.AreEqual(StartPositionX, player.BoundingBox.X);

        }

        //used when player is hit. wont be activated when player is already dead
        [TestMethod]
        public void TestProcessedPlayerDeath()
        {
            Player player = new Player(new Vector2(400, 500), 50, 600, 800, 80, 80,new LaserProjectile(5, 50, 10, 10, new Vector2(400, 300)));
            player.IsAlive = true;
            player.ProcessPlayerDeath(new BombProjectile(10, 800, 10, 10, new Vector2(400, 500)));

            Assert.AreEqual(false, player.Projectile.IsAlive);

        }

        //if projectile already alive nothing happens so cant test
        [TestMethod]
        public void TestShoot()
        {
            Player player = new Player(new Vector2(400, 500), 50, 600, 800, 80, 80, new LaserProjectile(5, 50, 10, 10, new Vector2(400, 300)));
            player.Projectile.IsAlive = false;
            player.Shoot();

            Assert.AreEqual(true, player.Projectile.IsAlive);
        }

        [TestMethod]
        public void TestMoveLeft_EnoughSpace()
        {
            Player player = new Player(new Vector2(400, 500), 50, 600, 800, 80, 80, null);
            player.MoveLeft();
            Assert.AreEqual(350, player.BoundingBox.X);
        }

        [TestMethod]
        public void TestMoveLeft_NotEnoughSpace()
        {
            Player player = new Player(new Vector2(400, 500), 500, 600, 800, 80, 80, null);
            player.MoveLeft();
            Assert.AreEqual(0, player.BoundingBox.X);
        }

        [TestMethod]
        public void TestMoveRight_EnoughSpace()
        {
            Player player = new Player(new Vector2(400, 500), 50, 600, 800, 80, 80, null);
            player.MoveRight();
            Assert.AreEqual(450, player.BoundingBox.X);
        }

        [TestMethod]
        public void TestMoveRight_NotEnoughSpace()
        {
            int screenWidth = 600;
            Player player = new Player(new Vector2(400, 500), 100, screenWidth, 800, 80, 80, null);
            player.MoveRight();
            player.MoveRight();
            player.MoveRight();
            Assert.AreEqual(screenWidth, player.BoundingBox.X + player.BoundingBox.Width);
        }


    }
}
