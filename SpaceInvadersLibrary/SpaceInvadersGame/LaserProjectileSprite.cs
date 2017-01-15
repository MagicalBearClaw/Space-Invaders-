using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SpaceInvadersLibrary;

namespace SpaceInvadersGame
{
    public class LaserProjectileSprite : DrawableGameComponent
    {
        public LaserProjectile projectile { get; private set; }
        private Texture2D texture;
        private SpriteBatch batch;
        /// <summary>
        /// Create a new Laser Projectile.
        /// </summary>
        /// <param name="width">The width for the laser.</param>
        /// <param name="height">The height of the laser.</param>
        /// <param name="screenHeight">The height of the game screen.</param>
        /// <param name="velocityY">The vertical velocity of the laser.</param>
        /// <param name="position">The initial position of the laser.</param>
        /// <param name="game">A reference to the game.</param>
        public LaserProjectileSprite(int width, int height, int screenHeight, int velocityY, Vector2 position, Game game)
            : base(game)
        {
            projectile = new LaserProjectile(velocityY, screenHeight, width, height, position);
        }
        /// <summary>
        /// Initialize the laser projectil.
        /// </summary>
        public override void Initialize()
        {
            projectile.IsAlive = false;
            batch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }
        /// <summary>
        /// Load the laser's content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("projectile");
            base.LoadContent();
        }
        /// <summary>
        /// Update the laser.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Update(GameTime gameTime)
        {
            if(projectile.IsAlive)
            {
                projectile.Move();
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// Dispose of any unmaged ressources.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// Draw the laser.
        /// </summary>
        /// <param name="gameTime"><The delta time since the last updae./param>
        public override void Draw(GameTime gameTime)
        {
            if (projectile.IsAlive)
            {
                batch.Begin();
                batch.Draw(texture, new Vector2(projectile.BoundingBox.X, projectile.BoundingBox.Y), Color.White);
                batch.End();
            }
            base.Draw(gameTime);
        }
    }
}
