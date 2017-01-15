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
    class BunkerSprite : DrawableGameComponent
    {
        public Bunker bunker {get; private set;}
        private SpriteBatch batch;
        private Texture2D texture;

        /// <summary>
        /// Creates a new bunker.
        /// </summary>
        /// <param name="health">The bunker's health.</param>
        /// <param name="width">The bunker's width.</param>
        /// <param name="height">The bunker;s height.</param>
        /// <param name="position">The bunker's inital positon.</param>
        /// <param name="game">A reference to the game,</param>
        public BunkerSprite(int health, int width, int height, Vector2 position, Game game) : base(game)
        {
            bunker = new Bunker(health, width, height, position);
        }
        /// <summary>
        /// Initialize the bunker.
        /// </summary>
        public override void Initialize()
        {
            bunker.IsAlive = true;
            batch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }
        /// <summary>
        /// Load the bunker's content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("bunker");
            base.LoadContent();
        }
        /// <summary>
        /// Update the bunker.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Dispose any unmanaged ressource.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// Draw the bunker.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Draw(GameTime gameTime)
        {
            if (bunker.IsAlive)
            {
                batch.Begin();
                batch.Draw(texture, new Vector2(bunker.BoundingBox.X, bunker.BoundingBox.Y), Color.White);
                batch.End();
            }
            base.Draw(gameTime);
        }
    }
}
