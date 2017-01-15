using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace SpaceInvadersLibrary
{
    public delegate void Collide(Projectile projectile);

    /// <summary>
    /// The class is responsible for managing a projectile's lifetime 
    /// as well as process the objects it collides with.
    /// </summary>
    public abstract class Projectile 
    {
        private int velocityY;
        private int screenHeight;
        private Rectangle boundingBox;
        private Vector2 startPosition;

        public bool IsAlive { get; set; }
        /// <summary>
        /// Gets or sets the projectile's bounding
        /// box.
        /// </summary>
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set 
            {
                boundingBox = value;
            }
        }
        /// <summary>
        /// Creates a new Projectile.
        /// </summary>
        /// <param name="velocityY"> The upward velocity of the projectile.</param>
        /// <param name="screenHeight">The Game screen's height</param>
        /// <param name="projectileWidth">The width of the projectile.</param>
        /// <param name="projectileHeight">The Height of the projectile.</param>
        /// <param name="position">the position of the projectile.</param>
        public Projectile(int velocityY, int screenHeight, int projectileWidth, int projectileHeight, Vector2 position)
        {
            startPosition =  position;
            this.velocityY = velocityY;
            this.screenHeight = screenHeight;
            IsAlive = false ;
            // We just give the projectile a default location here we reposition the projectile anyway when its launched.
            boundingBox = new Rectangle((int)position.X, (int)position.Y, projectileWidth, projectileHeight);
        }
        /// <summary>
        /// Moves the projectile.
        /// </summary>
        public void Move()
        {
            // are we off screen?
            if (HadGoneOffScreen())
                IsAlive = false;
            else
            {
                boundingBox.Y += velocityY;
            }
            // have we collided with anything?
            UpdateProjectileCollision();

        }
        /// <summary>
        /// Launches the projectile.
        /// Simply makes the projectile alive if it's not.
        /// </summary>
        public void Launch()
        {
            if (!IsAlive)
            {
                IsAlive = true;
            }
        }
        /// <summary>
        /// Determines if the projectile is off the game screen or not.
        /// </summary>
        /// <returns>if the projectile is off screen or not</returns>
        private bool HadGoneOffScreen()
        {
            if (boundingBox.Y < 0 || boundingBox.Y + boundingBox.Height > screenHeight)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sets the projectiles position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            boundingBox.X = x;
            boundingBox.Y = y;
        }
        /// <summary>
        /// Abstract method. inherited.
        /// </summary>
        protected abstract void UpdateProjectileCollision();
    }
}
