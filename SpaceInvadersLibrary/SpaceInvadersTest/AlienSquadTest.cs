using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvadersLibrary;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceInvadersTest
{
    [TestClass]
    public class AlienSquadTest
    {

        [TestMethod]
        public void ShootTest()
        {
            BombProjectile bomb1 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 1));
            BombProjectile bomb2 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 1));
            BombProjectile bomb3 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 21));
            BombProjectile bomb4 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 21));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);
            List<Alien> list = new List<Alien>();
            Alien alien1 = new Alien(new Vector2(1, 1), 0, 0, 500, 500, 20, 20, 0, bomb1);
            Alien alien2 = new Alien(new Vector2(21, 1), 0, 0, 500, 500, 20, 20, 0, bomb4);
            Alien alien3 = new Alien(new Vector2(1, 21), 0, 0, 500, 500, 20, 20, 0, bomb3);
            Alien alien4 = new Alien(new Vector2(21, 21), 0, 0, 500, 500, 20, 20, 0, bomb4);
            list.Add(alien1);
            list.Add(alien2);
            list.Add(alien3);
            list.Add(alien4);
            AlienSquad aSquad = new AlienSquad(list, 500, 0, 0, 500, 10, 10, 1, 2, new Vector2(1, 1), mShip);

            bool shot = false;

            while (!(bomb1.IsAlive || bomb2.IsAlive || bomb3.IsAlive || bomb4.IsAlive))
                aSquad.Shoot();

            shot = true;

            Assert.AreEqual(true, shot);
        }
        [TestMethod]
        public void TrySpawnMotherShipTest()
        {
            BombProjectile bomb1 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 1));
            BombProjectile bomb2 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 1));
            BombProjectile bomb3 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 21));
            BombProjectile bomb4 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 21));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);
            List<Alien> list = new List<Alien>();
            Alien alien1 = new Alien(new Vector2(1, 1), 1, 1, 500, 500, 20, 20, 0, bomb1);
            Alien alien2 = new Alien(new Vector2(21, 1), 1, 1, 500, 500, 20, 20, 0, bomb4);
            Alien alien3 = new Alien(new Vector2(1, 21), 1, 1, 500, 500, 20, 20, 0, bomb3);
            Alien alien4 = new Alien(new Vector2(21, 21), 1, 1, 500, 500, 20, 20, 0, bomb4);
            list.Add(alien1);
            list.Add(alien2);
            list.Add(alien3);
            list.Add(alien4);
            AlienSquad aSquad = new AlienSquad(list, 500, 0, 0, 500, 10, 10, 2, 2, new Vector2(1, 1), mShip);

        }
        [TestMethod]
        public void MoveTest()
        {
            BombProjectile bomb1 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 1));
            BombProjectile bomb2 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 1));
            BombProjectile bomb3 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 21));
            BombProjectile bomb4 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 21));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);
            List<Alien> list = new List<Alien>();
            Alien alien1 = new Alien(new Vector2(1, 1), 0, 0, 500, 500, 20, 20, 0, bomb1);
            Alien alien2 = new Alien(new Vector2(21, 1), 0, 0, 500, 500, 20, 20, 0, bomb4);
            Alien alien3 = new Alien(new Vector2(1, 21), 0, 0, 500, 500, 20, 20, 0, bomb3);
            Alien alien4 = new Alien(new Vector2(21, 21), 0, 0, 500, 500, 20, 20, 0, bomb4);
            list.Add(alien1);
            list.Add(alien2);
            list.Add(alien3);
            list.Add(alien4);
            AlienSquad aSquad = new AlienSquad(list, 500, 20, 20, 500, 10, 10, 2, 2, new Vector2(1, 1), mShip);

            aSquad.Move();
            Assert.AreEqual(21, aSquad.BoundingBox.X);
        }
        [TestMethod]
        public void HasReachedBunkersTest()
        {
            BombProjectile bomb1 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 1));
            BombProjectile bomb2 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 1));
            BombProjectile bomb3 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 21));
            BombProjectile bomb4 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 21));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);
            List<Alien> list = new List<Alien>();
            Alien alien1 = new Alien(new Vector2(1, 1), 0, 0, 500, 500, 20, 20, 0, bomb1);
            Alien alien2 = new Alien(new Vector2(21, 1), 0, 0, 500, 500, 20, 20, 0, bomb4);
            Alien alien3 = new Alien(new Vector2(1, 21), 0, 0, 500, 500, 20, 20, 0, bomb3);
            Alien alien4 = new Alien(new Vector2(21, 21), 0, 0, 500, 500, 20, 20, 0, bomb4);
            list.Add(alien1);
            list.Add(alien2);
            list.Add(alien3);
            list.Add(alien4);
            AlienSquad aSquad = new AlienSquad(list, 500, 20, 20, 500, 10, 10, 2, 2, new Vector2(1, 1), mShip);

            aSquad.Move();
            Assert.AreEqual(21, aSquad.BoundingBox.X);

        }
        [TestMethod]
        public void AlienSquadHitTest()
        {
            BombProjectile bomb1 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 1));
            BombProjectile bomb2 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 1));
            BombProjectile bomb3 = new BombProjectile(10, 500, 10, 10, new Vector2(1, 21));
            BombProjectile bomb4 = new BombProjectile(10, 500, 10, 10, new Vector2(21, 21));
            MotherShip mShip = new MotherShip(new Vector2(150, 35), Direction.LEFT, 1, 0, 10, 10, 500, 0);
            List<Alien> list = new List<Alien>();
            Alien alien1 = new Alien(new Vector2(1, 1), 0, 0, 500, 500, 20, 20, 0, bomb1);
            Alien alien2 = new Alien(new Vector2(21, 1), 0, 0, 500, 500, 20, 20, 0, bomb4);
            Alien alien3 = new Alien(new Vector2(1, 21), 0, 0, 500, 500, 20, 20, 0, bomb3);
            Alien alien4 = new Alien(new Vector2(21, 21), 0, 0, 500, 500, 20, 20, 0, bomb4);
            list.Add(alien1);
            list.Add(alien2);
            list.Add(alien3);
            list.Add(alien4);
            AlienSquad aSquad = new AlienSquad(list, 500, 20, 20, 500, 10, 10, 2, 2, new Vector2(0, 0), mShip);
            Player player = new Player(new Vector2(0, 10), 50, 600, 800, 80, 80, new LaserProjectile(-10, 500, 32, 32, Vector2.Zero));
            Rectangle currentBoundingBox = aSquad.BoundingBox;
            player.Projectile.RegisterAlienSquad(aSquad);
            player.Projectile.Move();

            Assert.AreNotEqual(currentBoundingBox, aSquad.BoundingBox);
        }
    }
}
