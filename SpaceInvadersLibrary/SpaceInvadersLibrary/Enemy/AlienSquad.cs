using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Collections;

namespace SpaceInvadersLibrary
{
    public delegate void NextWave();
    public delegate void IncreasePoints(int points);
    public delegate void ReachedBottom();
    public delegate void ReachedBunkers();
    public delegate void KillConfirmed();
    public class AlienSquad : ICollidableCollection, IEnumerable
    {

        private List<Alien> aliens;
        private Rectangle boundingBox;
        private Vector2 startPosition;
        private Random random;
        private MotherShip mother;
        private int bunkerHeight;
        private int screenHeight;
        private int screenWidth;
        public  int VelocityX {get; set;}
        private int VelocityY {get; set;}
        private int numRows;
        private int numColumns;

        public event IncreasePoints AlienDeath;
        public event ReachedBottom SquadReachedBootom;
        public event ReachedBunkers SquadReachedBunkers;
        public event KillConfirmed AlienKilled;
        private int playerHeight;

        private Direction currentDirection;
        /// <summary>
        /// Creates the alien squad out of aliens.
        /// </summary>
        /// <param name="aliens">A reference to an alien list.</param>
        /// <param name="screenHeight">The game screen's height.</param>
        /// <param name="stepX">The squad's horizontal velocity.</param>
        /// <param name="stepY">The squad's vertical velocity.</param>
        /// <param name="screenWidth">The game screen width.</param>
        /// <param name="bunkerHeight">The bunker's height.</param>
        /// <param name="playerPosY">The player's y position.</param>
        /// <param name="numRows">The number of alien rows.</param>
        /// <param name="numColumns">The number of alien columns.</param>
        /// <param name="startPosition">The squad's start position.</param>
        /// <param name="mother">A reference to a mothership.</param>
        public AlienSquad(List<Alien> aliens, int screenHeight, int stepX, int stepY, int screenWidth, 
            int bunkerHeight, int  playerHeight, int numRows, int numColumns, Vector2 startPosition, MotherShip mother)
        {
            this.numRows = numRows;
            this.numColumns = numColumns;
            this.bunkerHeight = bunkerHeight;
            this.aliens = aliens;
            this.startPosition = startPosition;
            this.playerHeight = playerHeight;
            random = new Random();
            this.VelocityX = stepX;
            this.VelocityY = stepY;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            currentDirection = Direction.RIGHT;
            this.mother = mother;
            ResizeBoundingBox();
        }

        /// <summary>
        /// Indexer to retrieve an alien from the collection.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public ICollidable this[int i]
        {
            get
            {
                if (i >= 0 && i < aliens.Count())
                {
                    return aliens[i];
                }
                else
                    throw new ArgumentOutOfRangeException("Index is out of range. Cannot access element at: " + i);
            }

        }
        /// <summary>
        /// Revive all the aliens in the collection.
        /// </summary>
        public void ReviveAllAliens()
        {
            foreach (var alien in aliens)
            {
                alien.IsAlive = true;
            }
        }
        /// <summary>
        /// Finds a random alien that is alive and allows it to shoot.
        /// The alien will be the one that is the least in height.
        /// </summary>
        public void Shoot()
        {
            int randShot = random.Next(1, 10);
            if(randShot == 1)
            {
                int column = random.Next(0, numColumns-1);
                Alien shooter = GetAlienShooter(column);

                if (shooter != null)
                    shooter.Shoot();

            }
            
        }
        /// <summary>
        /// Trys to spawn a mother ship.
        /// can only spawn one if the mother ship is currently dead.
        /// Also the minimum height of the squad must be reached before 
        /// it spawns a alien.
        /// </summary>
        public void TrySpawnMotherShip()
        {
            if (boundingBox.Y >= mother.MinimumStartHeight)
            {
                int randomMotherShip = random.Next(1, 600);
                if (!mother.IsAlive)
                    if (randomMotherShip == 1)
                        mother.SpawnMotherShip();
            }

        }
        /// <summary>
        /// Finds the best canadidate to be a shooter.
        /// </summary>
        /// <param name="column"> The colum the shooter will be selected from.</param>
        /// <returns></returns>
        private Alien GetAlienShooter(int column)
        {

            List<Alien> temp = new List<Alien>();

            for (int y = 0; y < numRows; y++)
            {
                // get the whole colum of aliens that are alive.
                int index = y * numColumns + column;
                temp.Add(aliens[index]);
            }
            // make sure the aliens quired are the ones alive.
            // get the alien with the biggest Y value for its boundingbox and then 
            //find the alien that has that value for its boundingbox y value.
            List<Alien> alive = temp.FindAll(x => x.IsAlive == true);
            Alien alien = null;
            if(alive.Count() > 0)
            {
                int height = alive.Max(z => z.BoundingBox.Y);
                alien = alive.Find(x => x.BoundingBox.Y == height);
            }

            return alien;
        }
        /// <summary>
        /// When the squad reaches the player the event will fire.
        /// </summary>
        public void OnReachBottom()
        {
            if (SquadReachedBootom != null)
                SquadReachedBootom();
        }
        /// <summary>
        /// Moves the squad and all of its child aliens.
        /// Dependant of the direction.
        /// </summary>
        public void Move()
        {
            TryMove();

            if (currentDirection == Direction.LEFT)
            {
                boundingBox.X -= VelocityX;
            }
            else if (currentDirection == Direction.RIGHT)
            {
                boundingBox.X += VelocityX;
            }

            foreach (var alien in aliens)
            {
                alien.MoveHorizontal(currentDirection);
            }
        }
        /// <summary>
        /// Change the squad's direction.
        /// </summary>
        private void ChangeDirection()
        {
            if (currentDirection == Direction.RIGHT)
            {
                currentDirection = Direction.LEFT;
            }
            else
                currentDirection = Direction.RIGHT;
                
        }
        /// <summary>
        /// Try to move the squad. if there is a collsiion with the edge of the screen
        /// change the squad's direction and move the squad down as well.
        /// </summary>
        private void TryMove()
        {
            if (boundingBox.X + boundingBox.Width >= screenWidth)
            {
                boundingBox.X = screenWidth - boundingBox.Width;
                ChangeDirection();
                MoveSquadDown();
            }

            else if (boundingBox.X < 0)
            {
                boundingBox.X = 0;
                ChangeDirection();
                MoveSquadDown();
            }

        }
        /// <summary>
        /// An event will be fired if the squad reaches the bunkers.
        /// </summary>
        protected void OnReachBunkers()
        {
            if (SquadReachedBunkers != null)
                SquadReachedBunkers();
        }
        /// <summary>
        /// Fires the reach bunker event.
        /// </summary>
        public void ReachedBunkers()
        {
            OnReachBunkers();
        }

        /// <summary>
        /// Moves the squad down. Accounts for reaching the player.
        /// </summary>
        private void MoveSquadDown()
        {
            foreach (var alien in aliens)
            {
                alien.MoveDown();
            }
            boundingBox.Y += VelocityY;
            if (HasReachedBunkers())
                OnReachBunkers();

            if (HasReachedBottom())
            {
                OnReachBottom();
            }


        }
        /// <summary>
        /// Determines if the squad's bounding box is greater than the 
        /// player's y cordinate.
        /// </summary>
        /// <returns>true if the bounding box y cordinate is greater than the player's, false otherwise.</returns>
        public bool HasReachedBunkers()
        {
            if (boundingBox.Y + boundingBox.Height > bunkerHeight)
            {
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Deternines if the squad has reached the player's Y cordinate.
        /// </summary>
        /// <returns>True if the player's Y cordinate is reached, false otherwise.</returns>
        private bool HasReachedBottom()
        {
            if (boundingBox.Y + boundingBox.Height >= playerHeight)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// When the squad's boundingBox is hit, figure out 
        /// which alien ahs been hit and fire off the events.
        /// to notify the subscribers about the aliens death.
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <returns></returns>
        public void AlienSquadHit(Projectile projecile)
        {
            foreach (var x in aliens)
            {
                if (x.IsAlive)
                {
                    if (x.BoundingBox.Intersects(projecile.BoundingBox))
                    {
                        projecile.IsAlive = false;
                        x.ProcessAlienDeath();
                        OnAlienDeath(x.Points);
                        AlienKilledConfirmed();
                        ResizeBoundingBox();

                    }
                }
            }
        }
        /// <summary>
        /// When an alien is killed fire off an event.
        /// </summary>
        protected void OnKillConfirmed()
        {
            if (AlienKilled != null)
            {
                AlienKilled();
            }
        }
        /// <summary>
        /// When an alien is killed fire off an event.
        /// </summary>
        public void AlienKilledConfirmed()
        {
            OnKillConfirmed();
        }

        /// <summary>
        /// Get the lenght of the collection.
        /// </summary>
        public int GetLength
        {
            get 
            {
                if (aliens != null)
                    return aliens.Count;
                else
                    throw new NullReferenceException("cannot get the lenght on a null object.");
            }
        }
        /// <summary>
        /// When the alien dies fire off an event giving the subscribes the
        /// alien's points.
        /// </summary>
        /// <param name="points">the alien's points.</param>
        public void OnAlienDeath(int points)
        {
            if (AlienDeath != null)
            {
                AlienDeath(points);
            }
        }
        /// <summary>
        /// Get the bounding box.
        /// </summary>
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }

        /// <summary>
        /// Resizes the squad's bounding box whenever an alien is killed.
        /// </summary>
        private void ResizeBoundingBox()
        {
            if(aliens == null)
                throw new NullReferenceException("cannot calculate the bounding box on a null object.");

            int startPosX = 0;
            int startPosY = 0;
            int width = 0;
            int height = 0;

            // consider only  the alive aliens.
            List<Alien> alive = aliens.FindAll(x => x.IsAlive == true);

            if (alive == null || alive.Count() <= 0)
                return;
            // here we get the bunker's minimum  x and y in the collection.
            startPosX = alive.Min(x => x.BoundingBox.X);
            startPosY = alive.Min(x => x.BoundingBox.Y);
            // here we get the bunker's maximum width and height in the collection.
            width = alive.Max(x => x.BoundingBox.X) + alive.Max(x => x.BoundingBox.Width) - startPosX;
            height = alive.Max(x => x.BoundingBox.Y) + alive.Max(x => x.BoundingBox.Height) - startPosY;
            Debug.WriteLine("x  is :{0} y is  {1}, width is {2} height is  {3}", startPosX, startPosY, width, height);
            boundingBox = new Rectangle(startPosX, startPosY, width, height);
        }
        /// <summary>
        /// Get the collectione Enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return aliens.GetEnumerator();
        }

        /// <summary>
        /// Reset the squad to defaults.
        /// </summary>
        public void Reset()
        {
            boundingBox.X = (int)startPosition.X;
            boundingBox.Y = (int)startPosition.Y;
            ResizeBoundingBox();
            currentDirection = Direction.RIGHT;
        }
    }
}
