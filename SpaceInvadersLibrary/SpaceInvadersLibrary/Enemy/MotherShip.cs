using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceInvadersLibrary
{

    public class MotherShip : ICollidable
    {
        private Direction currDirection;
        private Direction startDirection;
        private int speed;
        private int screenWidth;
        public Vector2 StartPosition { get; private set; }
        private Rectangle boundingBox;
        private bool isAlive;
        private int points;
        public int MinimumStartHeight { get; private set; }
        public event IncreasePoints MotherShipDeath;
        /// <summary>
        /// Creates a new Mother ship.
        /// </summary>
        /// <param name="startPosition">The start position of the mother ship.</param>
        /// <param name="startDirection">The start direction of the mother ship.</param>
        /// <param name="points">The points the mother ship is worth.</param>
        /// <param name="minimumStartHeight">The minimum height before a mother ship can be spawned.</param>
        /// <param name="width">The width of the mother ship.</param>
        /// <param name="height">The height of the mother ship.</param>
        /// <param name="screenWidth">The game screen's width.</param>
        /// <param name="velocityX">The mothership's horizontal velocity.</param>
        public MotherShip(Vector2 startPosition, Direction startDirection, int points, int minimumStartHeight, int width, int height, int screenWidth, int velocityX)
        {
            speed = velocityX;
            this.StartPosition = startPosition;
            this.startDirection = startDirection;
            this.points = points;
            currDirection = startDirection;
            this.screenWidth = screenWidth;
            this.MinimumStartHeight = minimumStartHeight;
            boundingBox = new Rectangle((int)startPosition.X, (int)startPosition.Y, width, height);
        }

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
        /// Get the points.
        /// </summary>
        public int Points
        {
            get { return points; }
        }
        /// <summary>
        /// Change the mother ship's direction.
        /// </summary>
        private void ChangeDirection()
        {
            if (currDirection == Direction.RIGHT)
                currDirection = Direction.LEFT;
            else
                currDirection = Direction.RIGHT;
        }
        /// <summary>
        ///  If the mothership is not alive spawn it.
        ///  Dependant on the direction.
        /// </summary>
        public void SpawnMotherShip()
        {
            if (!isAlive)
                isAlive = true;

            boundingBox.Y = 0;

            if(currDirection == Direction.LEFT)
                boundingBox.X = 0;
            else
                boundingBox.X = screenWidth - boundingBox.Width;
        }

        /// <summary>
        /// Move the mothership And handle collsion.
        /// </summary>
        public void Move()
        {
            if (currDirection == Direction.LEFT)
            {
                boundingBox.X += speed;
            }
            else
                boundingBox.X -= speed;
            ProcessCollsion();
        }
        /// <summary>
        /// Process collsion and kill the alien when it is
        /// out of screen bounds.
        /// </summary>
        private void ProcessCollsion()
        {
            if(boundingBox.X + boundingBox.Width >= screenWidth)
            {
                boundingBox.X = screenWidth - boundingBox.Width;
                ProcessMotherShipDeath();
            }
            else if(boundingBox.X < 0)
            {
                boundingBox.X = 0;
                ProcessMotherShipDeath();
            }
        }
        /// <summary>
        /// When the mother ship dies change its direction.
        /// </summary>
        public void ProcessMotherShipDeath()
        {
            if (isAlive)
            {
                ChangeDirection();
                isAlive = false;
            }

        }
        /// <summary>
        /// When the alien dies fire off an event.
        /// </summary>
        public void OnMotherSHipDeath()
        {
            if (MotherShipDeath != null)
            {
                MotherShipDeath(points);
            }
        }
        /// <summary>
        /// The event handler when the Mother ship is hit.
        /// process the aliens deaht as well as notiy its subscribers
        /// about the even that is thrown.
        /// </summary>
        /// <param name="boundingBox">The other collider's bounding box.</param>
        public void MotherSHipHit(Projectile projectile)
        {
            OnMotherSHipDeath();
            ProcessMotherShipDeath();

        }
    }
}
