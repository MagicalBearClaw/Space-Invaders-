using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvadersLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceInvadersTest
{
    [TestClass]
    public class ProjectileLaserTest
    {
        [TestMethod]
        public void TestMoveLaser()
        {
            //arrange
            Projectile laser = new LaserProjectile(20, 500, 10, 10, new Vector2(250, 250));
            //act
            laser.Move();
            Vector2 secondPosition = new Vector2(laser.BoundingBox.X, laser.BoundingBox.Y);
            //assert
            Assert.AreEqual(new Vector2(250, 270).Y, secondPosition.Y);
        }


        [TestMethod]
        public void TestHasLaserGoneOffScreen()
        {
            //arrange
            Projectile laser = new LaserProjectile(20, 500, 10, 10, new Vector2(250, 9));
            //act
            laser.Move();
            //assert
            Assert.AreEqual(false, laser.IsAlive);
        }
        [TestMethod]
        public void TestLaserCollisionWithBunker()
        {
            //arrange
            LaserProjectile laser1 = new LaserProjectile(-10, 500, 20, 20, new Vector2(100, 50));
            Bunker bunker = new Bunker(1, 10, 10, new Vector2(100, 35));
            Bunkers bunkers = new Bunkers();
            bunkers.AddBunker(bunker);

            laser1.RegisterBunkers(bunkers);

            laser1.Move();
            Assert.AreEqual(false, laser1.IsAlive);
        }
        [TestMethod]
        public void TestLaserCollisionWithMothersShip()
        {
            LaserProjectile laser2 = new LaserProjectile(-10, 500, 20, 20, new Vector2(150, 50));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);
            laser2.RegisterMotherShip(mShip);

            laser2.Move();
            Assert.AreEqual(false, laser2.IsAlive);
        }
        [TestMethod]
        public void TestLaserCollisionWithAlienSquad()
        {
            //arrange
            LaserProjectile laser3 = new LaserProjectile(-10, 500, 20, 20, new Vector2(200, 50));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);

            List<Alien> list = new List<Alien>();
            Alien alien1 = new Alien(new Vector2(0, 0), 0, 0, 500, 500, 20, 20, 0, null);
            Alien alien2 = new Alien(new Vector2(0, 20), 0, 0, 500, 500, 20, 20, 0, null);
            list.Add(alien1);
            list.Add(alien2);
            //AlienSquad aSquad = new AlienSquad(list, 40, 20, 500, 0, 0, 500, 20, 460, 1, 2, new Vector2(200, 25), mShip);
            AlienSquad aSquad = new AlienSquad(list, 500, 0, 0, 500, 10, 10, 1, 2, new Vector2(200, 25), mShip);
            laser3.RegisterAlienSquad(aSquad);

            //act
            laser3.Move();

            //assert
            Assert.AreEqual(false, laser3.IsAlive);

        }
       
    }
}
