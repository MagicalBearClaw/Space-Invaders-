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
    public class BunkerTest
    {
        [TestMethod]
        public void TestDamageBunker()
        {
            Bunker bunker = new Bunker(100, 96, 96, new Vector2(72, 375));
            bunker.DamageBunker(75);
            Assert.AreEqual(25, bunker.Health);
        }
    }
}
