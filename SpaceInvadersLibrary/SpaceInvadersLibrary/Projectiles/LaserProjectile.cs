using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceInvadersLibrary
{
    public class LaserProjectile : Projectile
    {

        private ICollidableCollection bunkers;
        private ICollidableCollection alienSquad;
        private ICollidable motherShip;

        public event Collide MotherShipHit;
        public event Collide AlienSquadHit;
        public event Collide BunkerHit;
        /// <summary>
        /// Creates A Laser Projectile.
        /// </summary>
        /// <param name="velocityY">The laser's horizontal velocity</param>
        /// <param name="screenHeight">The game screen's height<./param>
        /// <param name="projectileWidth">The laser's width.</param>
        /// <param name="projectileHeight">The laser's height.</param>
        /// <param name="position">The laser's position.</param>
        public LaserProjectile(int velocityY, int screenHeight, int projectileWidth, int projectileHeight, Vector2 position):
            base(velocityY,screenHeight,projectileWidth,projectileHeight, position)
        {

        }
        /// <summary>
        /// Register the bunker's collection as well as
        /// hook up its event handler.
        /// </summary>
        /// <param name="bunkers">The bunkers Collection.</param>
        public void RegisterBunkers(Bunkers bunkers)
        {
            this.bunkers = bunkers;
            BunkerHit += bunkers.BunkersHit;
        }

        /// <summary>
        /// Register the aliensquad collection as well as
        /// hook up its event handlers.
        /// </summary>
        /// <param name="bunkers">The aliensquad Collection.</param>
        public void RegisterAlienSquad(AlienSquad squad)
        {
            this.alienSquad = squad;
            AlienSquadHit += squad.AlienSquadHit;
        }
        /// <summary>
        /// Register the mother ship as well as hook up its event
        /// handlers.
        /// </summary>
        /// <param name="motherShip"><The mother ship/param>
        public void RegisterMotherShip(MotherShip motherShip)
        {
            this.motherShip = motherShip;
            MotherShipHit += motherShip.MotherSHipHit;
        }
        
        /// <summary>
        /// Determines if the is a collsion with the bunker's bounding box
        /// fire off an event.
        /// </summary>
        /// <param name="boundingBox">The colliding the bounding box.</param>
        /// <returns>True if there is a collsion, otherwise false.</returns>
        protected void OnBunkersHit(Projectile projectile)
        {
            if (BunkerHit != null)
                BunkerHit(projectile);
     
        }
        /// <summary>
        /// Determines if the is a collsion with the mother ship's bounding box
        /// fire off an event.
        /// </summary>
        /// <param name="boundingBox">The colliding bounding box.</param>
        /// <returns>True if there is a collsion, otherwise false.</returns>
        protected void OnMotherShipHit(Projectile projectile)
        {
            if (MotherShipHit != null)
                MotherShipHit(projectile);
        }
        /// <summary>
        /// Determines if the is a collsion with the alien squad's bounding box
        /// fire off an event.
        /// </summary>
        /// <param name="boundingBox">The colliding  bounding box.</param>
        /// <returns>True if there is a collsion, otherwise false.</returns>
        protected void OnAlienSquadHit(Projectile projectile)
        {
            if (AlienSquadHit != null)
                AlienSquadHit(projectile);

        }

        /// <summary>
        /// Polled every frame to determine if a projectile is collidng with any of the registerd
        /// objects.
        /// </summary>
        protected override void UpdateProjectileCollision()
        {
            if(bunkers != null)
            {
                if (BoundingBox.Intersects(bunkers.BoundingBox))
                {
                    OnBunkersHit(this);
                }
            }

            if (alienSquad != null)
            {
                if (BoundingBox.Intersects(alienSquad.BoundingBox))
                {
                    OnAlienSquadHit(this);
                }
            }

            if (motherShip != null)
            {
                if (BoundingBox.Intersects(motherShip.BoundingBox))
                {
                    OnMotherShipHit(this);
                }
            }
        }
    }
}
