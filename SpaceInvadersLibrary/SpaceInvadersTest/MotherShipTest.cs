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
    public class MotherShipTest
    {
        [TestMethod]
        public void TestSpawnMotherShip_DeathAndLeft()
        {
            MotherShip ms = new MotherShip(new Vector2(0, 0), Direction.LEFT, 100, 0, 10, 10, 100, 20);
            ms.IsAlive = false;

            ms.SpawnMotherShip();

            Assert.AreEqual(ms.BoundingBox.X, 0);

        }

        [TestMethod]
        public void TestSpawnMotherShip_DeathAndRight()
        {
            MotherShip ms = new MotherShip(new Vector2(0, 0), Direction.RIGHT, 100, 0, 10, 10, 100, 20);
            ms.IsAlive = false;

            ms.SpawnMotherShip();

            Assert.AreEqual(ms.BoundingBox.X, 100 - 10);
        }

        [TestMethod]
        public void TestMove_Left()
        {
            var speed = 20;
            MotherShip ms = new MotherShip(new Vector2(0, 0), Direction.LEFT, 100, 0, 10, 10, 100, speed);
            var bb = ms.BoundingBox.X;
            ms.Move();

            Assert.AreEqual(bb + speed, ms.BoundingBox.X);
        }

        [TestMethod]
        public void TestMove_Right()
        {
            var speed = 20;
            MotherShip ms = new MotherShip(new Vector2(100, 0), Direction.RIGHT, 100, 0, 10, 10, 100, speed);
            var bb = ms.BoundingBox.X;
            ms.Move();

            Assert.AreEqual(bb - speed, ms.BoundingBox.X);
        }

        [TestMethod]
        public void TestMotherShipDeath_alive()
        {
            var speed = 20;
            MotherShip ms = new MotherShip(new Vector2(100, 0), Direction.RIGHT, 100, 0, 10, 10, 100, speed);
            ms.ProcessMotherShipDeath();
            Assert.AreEqual(false, ms.IsAlive);
        }

        [TestMethod]
        public void TestMotherShipDeath_changeDir()
        {
            var dir = Direction.RIGHT;
            var speed = 20;
            MotherShip ms = new MotherShip(new Vector2(100, 0), dir, 100, 0, 10, 10, 100, speed);
            ms.ProcessMotherShipDeath();
            Assert.AreEqual(dir, Direction.RIGHT);
        }

        [TestMethod]
        public void TestIncreasePoints_DeathofMotherShip()
        {
            var points = 100;
            MotherShip ms = new MotherShip(new Vector2(100, 0), Direction.RIGHT, points, 0, 10, 10, 100, 20);
            ScoreAndLife x = new ScoreAndLife();
            var initialscore = x.Score;
            ms.MotherShipDeath += x.IncreasePoints;
            ms.OnMotherSHipDeath();

            Assert.AreEqual(initialscore + points, x.Score);
        }

        [TestMethod]
        public void TestMotherShipHit()
        {
            MotherShip ms = new MotherShip(new Vector2(100, 0), Direction.RIGHT, 100, 0, 10, 10, 100, 20);
            var alive = ms.IsAlive;
            ms.MotherSHipHit(new LaserProjectile(5, 50, 10, 10, new Vector2(400, 300)));

            Assert.AreEqual(false, ms.IsAlive);

        }
    }
}
