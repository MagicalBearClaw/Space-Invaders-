using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SpaceInvadersLibrary;

namespace SpaceInvadersGame
{
    public class BombProjectileSprite : DrawableGameComponent
    {
        public BombProjectile projectile{ get; private set; }
        private Texture2D texture;
        private SpriteBatch batch;
        /// <summary>
        /// Creates a new BombProjectile.
        /// </summary>
        /// <param name="width">The projectile width.</param>
        /// <param name="height">The projectile height.</param>
        /// <param name="screenHeight">The game screen height.</param>
        /// <param name="velocityY">The verticale velocity.</param>
        /// <param name="position">The bomb's initial positon.</param>
        /// <param name="game">A reference to the game.</param>
        public BombProjectileSprite(int width, int height, int screenHeight, int velocityY, Vector2 position, Game game)
            : base(game)
        {
            projectile = new BombProjectile(velocityY, screenHeight, width, height, position);
        }
        /// <summary>
        /// Initialize the bomb sprite.
        /// </summary>
        public override void Initialize()
        {
            projectile.IsAlive = false;
            batch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Load the bomb's content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("AlienProjectile");
            base.LoadContent();
        }
        /// <summary>
        /// Update the bomb.
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
        /// Dispose of any unmanaged ressources.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// Draw the bomb.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
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
