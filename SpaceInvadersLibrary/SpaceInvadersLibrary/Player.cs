using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace SpaceInvadersLibrary
{
    public delegate void PlayerDeath();
    /// <summary>
    /// The player that is controlled by the player.
    /// </summary>
    public class Player : ICollidable
    {
        private Rectangle boundingBox;
        private bool isAlive;
        private int screenWidth;
        private int velocityX;
        private int points;
        private LaserProjectile projectile;
        private Vector2 startPosition;
        public event PlayerDeath PlayerDeath;
        public int KillCount {get; private set;}

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="position">player's position</param>
        /// <param name="velocityX">players's horizontal velocity</param>
        /// <param name="screenWidth">the gmae's screen width</param>
        /// <param name="screenHeight">the game's screen height</param>
        /// <param name="playerWidth">The player's width</param>
        /// <param name="playerHeight">then player's height</param>
        public Player(Vector2 position, int velocityX, int screenWidth, int screenHeight, int playerWidth, int playerHeight, LaserProjectile projectile)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, playerWidth, playerHeight);
            startPosition = position;
            this.screenWidth = screenWidth;
            this.velocityX = velocityX;
            points = 0;
            isAlive = true;
            PlayerDeath += OnDeath;
            this.projectile = projectile;
        }

        /// <summary>
        /// Move the player left and Reconcile the 
        /// player's position if we are off screen.
        /// </summary>
        public void MoveLeft()
        {
            boundingBox.X -= velocityX;
            ReconcileCollision();
        }
        /// <summary>
        /// Move the player right and Reconcile the 
        /// player's position if we are off screen.
        /// </summary>
        public void MoveRight()
        {

            boundingBox.X += velocityX;
            ReconcileCollision();
        }
        /// <summary>
        /// Shoot the player's projectile if it is not alive.
        /// </summary>
        public void Shoot()
        {
            if (!projectile.IsAlive)
            {
                // reposition the projectile so it is centered to the player's position.
                // as well being just aboe the player's position.
                Projectile.SetPosition(BoundingBox.X + boundingBox.Width / 2 - projectile.BoundingBox.Width / 2, BoundingBox.Y);
                projectile.Launch();
            }
        }
        /// <summary>
        /// Player's points
        /// </summary>
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        /// <summary>
        /// The event hanadler that is called when the player dies.
        /// Reset the player to defaults.
        /// </summary>
        public void OnDeath()
        {
            boundingBox.X = (int)startPosition.X;
            boundingBox.Y = (int)startPosition.Y;
            projectile.IsAlive = false;
            isAlive = true;
        }
        /// <summary>
        /// Get or set isAlive field.
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
        /// Get or set the bounding box.
        /// </summary>
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            private set { boundingBox = value; }
        }
        /// <summary>
        /// Get or set the Projectile.
        /// </summary>
        public LaserProjectile Projectile
        {
            get { return projectile; }
        }
        /// <summary>
        /// Checks to see if the player is not off screen.
        /// </summary>
        private void ReconcileCollision()
        {
            if (boundingBox.X + boundingBox.Width >= screenWidth)
                boundingBox.X = screenWidth - boundingBox.Width;
            if (boundingBox.X <= 0)
                boundingBox.X = 0;
        }
        /// <summary>
        /// fire off the player death event if tje player is alive.
        /// </summary>
        public void ProcessPlayerDeath(Projectile projectile)
        {
            if (isAlive)
            {
                OnPlayerDeath();
                projectile.IsAlive = false;
            }
        }
        /// <summary>
        /// fires the player death event. 
        /// </summary>
        protected void OnPlayerDeath()
        {
            if (PlayerDeath != null)
                PlayerDeath();
        }
        /// <summary>
        /// When a alien is killed increment the kill count.
        /// </summary>
        public void AlienKilled()
        {
            KillCount++;
        }
        /// <summary>
        /// Event handler for when the player is dead.
        /// </summary>
        public void GameOver()
        {
            isAlive = false;
        }
        /// <summary>
        /// Reset the player to its defaults.
        /// </summary>
        public void Reset()
        {
            KillCount = 0;
            boundingBox.X = (int)startPosition.X;
            boundingBox.Y = (int)startPosition.Y;
        }
    }
}
