using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    /// <summary>
    /// Defines the logic of a bunker. A bunker is a game item that 
    /// is supposed to be an obstruction between the player and the
    /// enemies. The bunker can be destryed.
    /// </summary>
    public class Bunker : ICollidable
    {
        private bool isAlive;
        private Rectangle boundingBox;
        public int Health {get; set;}
        private int points;
        private int startHealth;

        /// <summary>
        /// Constructs a new bunker
        /// </summary>
        /// <param name="health">Bunker's health</param>
        /// <param name="width">Bunker's width</param>
        /// <param name="height">Bunker's height</param>
        /// <param name="position">Bunker's position</param>
        public Bunker(int health, int width, int height, Vector2 position)
        {
            this.startHealth = health;
            this.Health = health;
            boundingBox = new Rectangle((int)position.X, 
                (int)position.Y, width, height);
            isAlive = true;
        }
        /// <summary>
        /// Get or set the bunker's boundingBox field.
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
        /// Get or set the bunker's isAlive field.
        /// </summary>
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        /// <summary>
        /// Get or set the bunker's points field.
        /// </summary>
        public int Points
        {
            get { return points; }
            private set { points = value; }
        }
        /// <summary>
        /// Reduces the bunker's health by the specified damage.
        /// </summary>
        /// <param name="damage"></param>
        public void DamageBunker(int damage)
        {
            if (damage <= 0)
                throw new 
                    ArgumentOutOfRangeException
                    ("The damage passed in cannot be less than 0");
            if (isAlive)
            {
                if (Health >= 0)
                {
                    Health -= damage;
                    // we check thid condition right away to 
                    //avoid the bunker taking 5 hits
                    if (Health <= 0)
                    {
                        isAlive = false;
                    }
                }
            }
        }
        /// <summary>
        /// Resets the health to defaults.
        /// </summary>
        public void ResetHealth()
        {
            this.Health = startHealth;
        }
    }
}
