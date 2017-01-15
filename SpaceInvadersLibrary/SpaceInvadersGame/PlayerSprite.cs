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
    class PlayerSprite : DrawableGameComponent
    {
        public Player player { get; private set; }
        private Texture2D texture;
        private SpriteBatch batch;

        /// <summary>
        /// Creates a new player sprite.
        /// </summary>
        /// <param name="width">The width of the player.</param>
        /// <param name="height">The height of the player.</param>
        /// <param name="screenHeight">The game screen's height.</param>
        /// <param name="screenWidth">The game screen's height.</param>
        /// <param name="velocityX">The horizontal velocity of the player.</param>
        /// <param name="position">The initial position of the player.</param>
        /// <param name="game">A reference to the the game.</param>
        /// <param name="projectile">A reference to a projectile.</param>
        public PlayerSprite(int width, int height, int screenHeight, int screenWidth, int velocityX, Vector2 position, Game game, LaserProjectileSprite projectile)
            : base(game)
        {
            player = new Player(position, velocityX, screenWidth, screenHeight, width, height, projectile.projectile);
        }
        /// <summary>
        /// Initilize the player .
        /// </summary>
        public override void Initialize()
        {
            player.Projectile.IsAlive = false;
            batch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }
        /// <summary>
        /// Load the player's content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Player");
            base.LoadContent();
        }
        /// <summary>
        /// Update the player.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (player.IsAlive)
            {
                if(state.IsKeyDown(Keys.A))
                {
                    player.MoveLeft();
                }
                if (state.IsKeyDown(Keys.D))
                {
                    player.MoveRight();
                }
                if (state.IsKeyDown(Keys.Space) )
                {
                    player.Shoot();
                }               
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Dispose of any unmange ressources.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// Draw the player.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Draw(GameTime gameTime)
        {

            if (player.IsAlive)
            {
                batch.Begin();
                batch.Draw(texture, new Vector2(player.BoundingBox.X, player.BoundingBox.Y),Color.White);
                batch.End();
            }
            base.Draw(gameTime);
        }
    }
}
