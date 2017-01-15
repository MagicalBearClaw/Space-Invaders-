using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvadersLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace SpaceInvadersGame
{
    public class MotherShipSprite : DrawableGameComponent
    {
        public MotherShip Mother { get; private set; }
        private Texture2D texture;
        private SpriteBatch batch;
        /// <summary>
        /// Creates a new MotherShip.
        /// </summary>
        /// <param name="position">The mothership's initial position.</param>
        /// <param name="direction">The direction the mother ship start's with.</param>
        /// <param name="minimumStartHeight">the minimum height before the first mother ship spawn.</param>
        /// <param name="width">The width of the mother ship.</param>
        /// <param name="height">The height of ther mother ship.</param>
        /// <param name="screenWidth">The game screen's width.</param>
        /// <param name="velocityX">The horizontal velocity.</param>
        /// <param name="points">How much the mother ship is worth.</param>
        /// <param name="game">A reference to the game.</param>
        public MotherShipSprite(Vector2 position, Direction direction, int minimumStartHeight, int width, int height, int screenWidth, int velocityX, int points,Game game) : base(game)
        {
            Mother = new MotherShip(position, direction, points, minimumStartHeight, width, height, screenWidth, velocityX);
            Mother.IsAlive = false;
        }
        /// <summary>
        /// Initializes the Mother ship.
        /// </summary>
        public override void Initialize()
        {
            batch = new SpriteBatch(GraphicsDevice);
            base.Initialize();

        }
        /// <summary>
        /// Load the mother ship's content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("motherShip");
            base.LoadContent();
        }
        /// <summary>
        /// Update the mother ship.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Update(GameTime gameTime)
        {
            if (Mother.IsAlive)
                Mother.Move();
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
        /// Draw the mother ship.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Draw(GameTime gameTime)
        {
            if (Mother.IsAlive)
            {
                batch.Begin();
                batch.Draw(texture, new Vector2(Mother.BoundingBox.X, Mother.BoundingBox.Y), Color.White);
                batch.End();
            }
            base.Draw(gameTime);
        }
    }
}
