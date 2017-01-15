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
    public class AlienSprite : DrawableGameComponent
    {
        public Alien Alien { get; private set; }
        private Texture2D texture;
        private SpriteBatch batch;
        public BombProjectileSprite Projectile {get; private set;}

        /// <summary>
        /// Creates a new AlienSprite.
        /// </summary>
        /// <param name="position">Sprites Positon.</param>
        /// <param name="stepX">How much the sprite moves on the x axis.</param>
        /// <param name="stepY">How much the sprite moves on the y axis</param>
        /// <param name="screenWidth">The game screen width</param>
        /// <param name="screenHeight">The game screen height</param>
        /// <param name="width">the sprite's width</param>
        /// <param name="height">the sprite's height</param>
        /// <param name="points">How much it is worth</param>
        /// <param name="game">a reference to the game</param>
        public AlienSprite(Vector2 position, int stepX,int stepY, int screenWidth, int screenHeight, int width, int height, int points,Game game) : base(game)
        {
            Projectile = new BombProjectileSprite(16, 16, screenHeight, 5, new Vector2(0, 0), game);
            Alien = new Alien(position, stepX, stepY, screenWidth, screenHeight, width, height, points,Projectile.projectile);
            Projectile.projectile.IsAlive = false;
            game.Components.Add(Projectile);
        }
        /// <summary>
        /// Initialize the Alien sprite.
        /// </summary>
        public override void Initialize()
        {
            Alien.IsAlive = true;
            batch = new SpriteBatch(GraphicsDevice);
            base.Initialize();

        }
        /// <summary>
        /// Loads the alien texture.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Alien");
            base.LoadContent();
        }
        /// <summary>
        /// Updates the alien.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Update(GameTime gameTime)
        {
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
        /// Draw the alien.
        /// </summary>
        /// <param name="gameTime">The delta time since the last updae.</param>
        public override void Draw(GameTime gameTime)
        {
            if (Alien.IsAlive)
            {
                batch.Begin();
                batch.Draw(texture, new Vector2(Alien.BoundingBox.X, Alien.BoundingBox.Y), Color.FloralWhite);
                batch.End();
            }
            base.Draw(gameTime);
        }
    }
}
