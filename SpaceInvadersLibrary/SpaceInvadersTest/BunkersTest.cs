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
    public class BunkersTest
    {
        [TestMethod]
        public void TestAddBunker()
        {
            Bunkers allBunkers = new Bunkers();
            Bunker bunker = new Bunker(100, 96, 96, new Vector2(72, 375));
            allBunkers.AddBunker(bunker);

            Assert.AreEqual(1, allBunkers.GetLength);
        }

        [TestMethod]
        public void TestBunkerHitEvent()
        {
            Bunkers allBunkers = new Bunkers();
            Bunker bunker1 = new Bunker(100, 96, 96, new Vector2(72, 375));
            Bunker bunker2 = new Bunker(100, 96, 96, new Vector2(335, 375));
            allBunkers.AddBunker(bunker1);
            allBunkers.AddBunker(bunker2);

            allBunkers.BunkersHit(new LaserProjectile(10, 800, 10, 10, new Vector2(72, 375)));

            Assert.AreEqual(75, bunker1.Health);

        }

        [TestMethod]
        public void TestResizeBoundingBox()
        {
            Bunkers allBunkers = new Bunkers();
            Bunker bunker1 = new Bunker(100, 96, 96, new Vector2(72, 375));
            allBunkers.AddBunker(bunker1);
            Rectangle bb = allBunkers.BoundingBox;

            Bunker bunker2 = new Bunker(100, 96, 96, new Vector2(335, 375));
            allBunkers.AddBunker(bunker2);

            Assert.AreNotEqual(bb, allBunkers.BoundingBox);

        }
    }
}
