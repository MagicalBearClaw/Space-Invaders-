using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceInvadersLibrary
{
    public class BombProjectile : Projectile
    {
        private ICollidableCollection bunkers;
        private ICollidable player;

        public event Collide PlayerHit;
        public event Collide BunkerHit;
        /// <summary>
        /// Creates a new Bomb projectile.
        /// </summary>
        /// <param name="velocityY">The bomb's vertical velocity.</param>
        /// <param name="screenHeight">The game screen's height.</param>
        /// <param name="projectileWidth">The projectile width.</param>
        /// <param name="projectileHeight">The projectile's height.</param>
        /// <param name="position">The projectile's position.</param>
        public BombProjectile(int velocityY, int screenHeight, int projectileWidth, int projectileHeight, Vector2 position) :
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
        /// Register the player as well as
        /// hook up its event handler.
        /// </summary>
        /// <param name="bunkers">The bunkers Collection.</param>
        public void RegisterPlayer(Player player)
        {
            this.player = player;
            PlayerHit += player.ProcessPlayerDeath;
        }
        /// <summary>
        /// Determines if the is a collsion with the bunker's bounding box
        /// fire off an event.
        /// </summary>
        /// <param name="boundingBox">The colliding bounding box.</param>
        /// <returns>True if there is a collsion, otherwise false.</returns>
        protected void OnBunkersHit(Projectile projectile)
        {
            if (BunkerHit != null)
                BunkerHit(projectile);
        }
        /// <summary>
        /// Determines if the is a collsion with the player's bounding box
        /// fire off an event.
        /// </summary>
        /// <param name="boundingBox">The colliding bounding box.</param>
        /// <returns>True if there is a collsion, otherwise false.</returns>
        protected void OnPlayerHit(Projectile projectile)
        {
            if (PlayerHit != null)
                PlayerHit(projectile);
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

            if (player != null)
            {
                if (BoundingBox.Intersects(player.BoundingBox))
                {
                    OnPlayerHit(this);
                }
            }
        }
    }
}
