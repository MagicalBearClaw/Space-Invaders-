using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SpaceInvadersLibrary
{
    /// <summary>
    /// The Direction the alien is going in.
    /// </summary>
    public enum Direction
    {
        LEFT,
        RIGHT
    }
    public class Alien : ICollidable
    {
        private Rectangle boundingBox;
        private bool isAlive;
        private int points;
        private BombProjectile projectile;
        private int stepX;
        private int stepY;
        private int screenWidth;
        private int screenHeight;

        /// <summary>
        /// Get or set the bounding box.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return boundingBox;
            }
            private set
            {
                boundingBox = value;
            }
        }
        /// <summary>
        /// Get or set isAlive.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
            set
            {
                isAlive = value;
            }
        }
        /// <summary>
        /// Get the alien's point.
        /// </summary>
        public int Points
        {
            get
            {
                return points;
            }
        }
        /// <summary>
        /// Get the projectile.
        /// </summary>
        public BombProjectile Projectile
        {
            get
            {
                return projectile;
            }
            private set
            {
                projectile = value;
            }
        }
        /// <summary>
        /// Creates a new Alien.
        /// </summary>
        /// <param name="position">The alien's position.</param>
        /// <param name="stepX">The horizontal velocity of the alien.</param>
        /// <param name="stepY">The vertical velocity of the alien.</param>
        /// <param name="screenWidth">The game screen width.</param>
        /// <param name="screenHeight">The game screen height.</param>
        /// <param name="alienWidth">The alien width.</param>
        /// <param name="alienHeight">The alien height.</param>
        /// <param name="points"The amount of points the alien is worth.></param>
        /// <param name="projectile">A reference to a projectile.</param>
        public Alien(Vector2 position, int stepX, int stepY, int screenWidth, int screenHeight, int alienWidth, int alienHeight, int points, BombProjectile projectile)
        {
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, alienWidth, alienHeight);
            this.points = points;
            this.stepX = stepX;
            this.stepY = stepY;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.projectile = projectile;
            this.isAlive = true;
        }
        /// <summary>
        /// Shoot the projectile if it is alive.
        /// </summary>
        public void Shoot()
        {
            if(!projectile.IsAlive)
            {
                // Centere the projectile to the alien's position.
                Projectile.SetPosition(boundingBox.X + boundingBox.Width / 2 - projectile.BoundingBox.Width / 2, BoundingBox.Y + boundingBox.Height);
                projectile.Launch();
            }
        }
        /// <summary>
        /// Move the alien. It depends on the direction.
        /// </summary>
        /// <param name="dir">The direction to move.</param>
        public void MoveHorizontal(Direction dir)
        {
            if (dir == Direction.LEFT)
            {
                boundingBox.X -= stepX;
            }
            else if (dir == Direction.RIGHT)
            {
                boundingBox.X += stepX;
            }
        }
        /// <summary>
        /// Move the alien down.
        /// </summary>
        public void MoveDown()
        {
            boundingBox.Y += stepY;
        }


        /// <summary>
        /// Process the alien's death.
        /// </summary>
        public void ProcessAlienDeath()
        {
            if(isAlive)
            {
                isAlive = false;
            }
        }
        /// <summary>
        /// Reset the positon of the alien.
        /// </summary>
        /// <param name="positon">The position to set the alien to.</param>
        public void ResetPosition(Vector2 positon)
        {
            boundingBox.X = (int)positon.X;
            boundingBox.Y = (int)positon.Y;
        }
    }
}
