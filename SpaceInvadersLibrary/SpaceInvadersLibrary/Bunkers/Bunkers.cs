using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceInvadersLibrary
{
    /// <summary>
    /// A collection of bunkers. The collection is ment to facilitate the itteration 
    /// of the bunkers as well as to facilitate collsiion detection.
    /// </summary>
    public class Bunkers : ICollidableCollection, IEnumerable
    {
        private List<Bunker> bunkers;
        public Rectangle BoundingBox { get; private set; }

        /// <summary>
        /// Create a new Bunkers collection.
        /// </summary>
        public Bunkers()
        {
            bunkers = new List<Bunker>();
        }

        /// <summary>
        /// Bunkers's indexer.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public ICollidable this[int i]
        {
            get
            {
                if (i >= 0 && i < bunkers.Count())
                {
                    return bunkers[i];
                }
                else
                    throw new ArgumentOutOfRangeException("Index is out of range. Cannot access element at: "+  i);
            }
        }
        /// <summary>
        /// Adds a new Bunker to the collection .
        /// </summary>
        /// <param name="bunker">The bunker to add to the collection</param>
        public void AddBunker(Bunker bunker)
        {
            if (bunker != null)
            {   
                bunkers.Add(bunker);
                ResizeBoundingBox();
            }
            else
                throw new ArgumentNullException("Cannot add a null value. make sure the parameter passed in is not null.");
        }
        /// <summary>
        /// Gets the collection length.
        /// </summary>
        public int GetLength
        {
            get
            {
                return bunkers.Count();
            }
        }
        /// <summary>
        /// Gets the IEnumerator to iterate thought the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return bunkers.GetEnumerator();
        }

        /// <summary>
        /// The event handler that is invoked when another object collides with the
        /// collections bounding box. If there is a collsion it iterates throught the 
        /// collection to determine which Bunker was hit and damages it.
        /// </summary>
        /// <param name="boundingBox"> the bounding box that hit the collection's bounding box</param>
        /// <returns></returns>
        public void BunkersHit(Projectile projectile)
        {
            foreach(var x in bunkers)
            {
                if(x.IsAlive)
                {
                    if (x.BoundingBox.Intersects(projectile.BoundingBox))
                    {
                        x.DamageBunker(25);
                        projectile.IsAlive = false;
                    }
                }
            }
        }
        /// <summary>
        /// Resizes the collections box. If there is a bunker that dies
        /// it is important to resize the collections bounding box to avoid 
        /// processiong unnecessary collision checks.
        /// </summary>
        private void ResizeBoundingBox()
        {
            int startPosX = 0;
            int startPosY = 0;
            int width = 0;
            int height = 0;
            
            // here we get the bunker's minimum  x and y in the collection.
            startPosX = bunkers.Min(x => x.BoundingBox.X);
            startPosY = bunkers.Min(x => x.BoundingBox.Y);
            // here we get the bunker's maximum width and height in the collection.
            width = bunkers.Max(x => x.BoundingBox.X) + bunkers.Max(x => x.BoundingBox.Width) - startPosX;
            height = bunkers.Max(x => x.BoundingBox.Y) + bunkers.Max(x => x.BoundingBox.Height) - startPosY;

            BoundingBox = new Rectangle(startPosX, startPosY, width, height);
        }
        /// <summary>
        /// If the bunkers are reached kill them all.
        /// </summary>
        public void BunkersReached()
        {
            for (int i = 0; i < bunkers.Count; i++)
            {
                this[i].IsAlive = false;
            }
        }
        /// <summary>
        /// Revive all the bunker.
        /// </summary>
        public void ReviveBunkers()
        {
            for (int i = 0; i < bunkers.Count; i++)
            {
                this[i].IsAlive = true;
                ((Bunker)this[i]).ResetHealth();
            }
        }
    }
}
