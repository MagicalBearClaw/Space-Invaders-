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
using System.Diagnostics;

namespace SpaceInvadersGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private LaserProjectileSprite projectile;
        private PlayerSprite player;
        private BunkerSprite bunker1;
        private BunkerSprite bunker2;
        private BunkerSprite bunker3;
        private Bunkers bunkers;
        private ScoreAndLife scoreAndLife;
        private SpriteFont font;
        private Vector2 playerPosition;
        private Vector2 motherShipPosition = Vector2.Zero;
        private KeyboardState oldState;
        private AlienSquad squad;
        private List<Alien> aliens;
        private MotherShipSprite motherShip;
        private Texture2D background;

        private const string gameOverMsg = "Game Over! Press enter to restart ";
        private const string roundFinished = "Round Finished! Press enter to start round";

        // Constants.
        private const int NUM_ALIEN_X = 10;
        private const int NUM_ALIEN_Y = 5;
        private const int TOTAL_ALIENS = NUM_ALIEN_X * NUM_ALIEN_Y;
        private const int ALIEN_SIZE_X = 64;
        private const int ALIEN_SIZE_Y = 64;
        private const int BASE_ALIEN_POINTS = 100;
        private const int PLAYER_HEIGHT = 64;
        private const int PLAYER_WIDTH = 64;
        private const int PLAYER_VELOCITY_X = 5;
        private const int NUM_LIVES = 3;
        private const int PLAYER_PROJECTILE_VELOCITY_Y = -17;
        private const int BUNKER_HEIGHT = 96;
        private const int BUNKER_WIDTH = 96;
        private const int BUNKER_HELATH = 300;
        private const int LASER_PROJECTILE_HEIGHT = 16;
        private const int LASER_PROJECTILE_WIDTH = 16;
        private const int BUNKER_POSITION_Y = 525;
        private const int ALIEN_Y_OFFSET = 64;

        private int squadVelocityX = 1;
        private int squadVelocityY = 64;


        private int screenWidth;
        private int screenHeight;
        private bool isGameOver;
        private bool isRoundFinished;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            screenHeight = this.graphics.PreferredBackBufferHeight = 768;
            screenWidth = this.graphics.PreferredBackBufferWidth = 1024;

            bunker1 = new BunkerSprite(BUNKER_HELATH, BUNKER_WIDTH, BUNKER_HEIGHT, new Vector2(172, BUNKER_POSITION_Y), this);
            bunker2 = new BunkerSprite(BUNKER_HELATH, BUNKER_WIDTH, BUNKER_HEIGHT, new Vector2(435, BUNKER_POSITION_Y), this);
            bunker3 = new BunkerSprite(BUNKER_HELATH, BUNKER_WIDTH, BUNKER_HEIGHT, new Vector2(700, BUNKER_POSITION_Y), this);
            bunkers = new Bunkers();
            aliens = new List<Alien>();
            scoreAndLife = new ScoreAndLife(NUM_LIVES);
            isGameOver = false;
            playerPosition = new Vector2(screenWidth / 2, screenHeight  - 100);
            isRoundFinished = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        { 
            // TODO: Add your initialization logic here
            projectile = new LaserProjectileSprite(LASER_PROJECTILE_WIDTH, LASER_PROJECTILE_HEIGHT, screenHeight, PLAYER_PROJECTILE_VELOCITY_Y, new Vector2(screenWidth / 2, screenHeight / 2), this);
            player = new PlayerSprite(PLAYER_WIDTH, PLAYER_HEIGHT, screenHeight, screenWidth, PLAYER_VELOCITY_X, playerPosition, this, projectile);

            Components.Add(projectile);
            Components.Add(player);
            Components.Add(bunker1);
            Components.Add(bunker2);
            Components.Add(bunker3);

            bunkers.AddBunker(bunker1.bunker);
            bunkers.AddBunker(bunker2.bunker);
            bunkers.AddBunker(bunker3.bunker);

            projectile.projectile.RegisterBunkers(bunkers);

            player.player.PlayerDeath += scoreAndLife.DecrementLives;
            scoreAndLife.NoLives += player.player.GameOver;

            // Create the aliens.
            // we acess the list like it was a 2d array here.
            for (int i = 0; i < NUM_ALIEN_Y; i++)
            {
                for (int j = 0; j < NUM_ALIEN_X; j++)
                {

                    AlienSprite alien = new AlienSprite(new Vector2( j * (ALIEN_SIZE_X ) , i * (ALIEN_SIZE_Y)), squadVelocityX, 
                        squadVelocityY, screenWidth, screenHeight, ALIEN_SIZE_X, ALIEN_SIZE_Y, (j + 1) * BASE_ALIEN_POINTS, this);
                    alien.Projectile.projectile.RegisterBunkers(bunkers);
                    alien.Projectile.projectile.RegisterPlayer(player.player);
                    alien.Alien.IsAlive = true;
                    aliens.Add(alien.Alien);
                    Components.Add(alien);
                }
            }
            motherShip = new MotherShipSprite(motherShipPosition, Direction.LEFT, 64, 64,64,screenWidth,5,1000, this);
            // Create the squad.
            squad = new AlienSquad(aliens, screenHeight, squadVelocityX, squadVelocityY, 
                screenWidth, BUNKER_POSITION_Y, player.player.BoundingBox.Y, NUM_ALIEN_Y, NUM_ALIEN_X, Vector2.Zero,motherShip.Mother);
            projectile.projectile.RegisterAlienSquad(squad);

            projectile.projectile.RegisterMotherShip(motherShip.Mother);

            // Hook up the events.
            squad.SquadReachedBunkers += bunkers.BunkersReached;
            squad.SquadReachedBootom += scoreAndLife.OnbottomReached;
            squad.AlienDeath += scoreAndLife.IncreasePoints;
            motherShip.Mother.MotherShipDeath += scoreAndLife.IncreasePoints;
            scoreAndLife.NoLives += OnGameOver;
            squad.AlienKilled += player.player.AlienKilled;
            scoreAndLife.ProceedToNextWave += PrepareNextWave;
            Components.Add(motherShip);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Starfield");
            font = Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state =  Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            bool roundFinsished =  IsWaveFinished();
            // On playing.
            if(!isGameOver && !roundFinsished)
            {
                squad.Move();
                squad.Shoot();
                squad.TrySpawnMotherShip();

                // On next wave.
  
            }
            else if (roundFinsished)
            {
                if (state.IsKeyDown(Keys.Enter) && !oldState.IsKeyUp(Keys.Enter))
                {
                    scoreAndLife.StartNextWave();
                    motherShip.Mother.ProcessMotherShipDeath();
                    if (squad.HasReachedBunkers())
                        squad.ReachedBunkers();
                }
            }
            else
            {
                // On game over.
                if(state.IsKeyDown(Keys.Enter) && !oldState.IsKeyUp(Keys.Enter))
                {
                    squad.ReviveAllAliens();
                    RepositionAliensToStart();
                    bunkers.ReviveBunkers();
                    squad.Reset();
                    player.player.IsAlive = true;
                    scoreAndLife.Reset();
                    player.player.Reset();
                    isGameOver = false;
                }
            }

            oldState = state;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            if (isRoundFinished)
            {
                spriteBatch.DrawString(font, roundFinished, new Vector2(screenWidth / 3.5f, screenHeight / 2), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }

            if (!isGameOver)
            {
                spriteBatch.DrawString(font, "Score: " + scoreAndLife.Score, new Vector2(10, 750 - 3 * font.LineSpacing), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);

                spriteBatch.DrawString(font, "Lives: " + scoreAndLife.Lives, new Vector2(10, 750 - 2 * font.LineSpacing), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);

                spriteBatch.DrawString(font, "Wave: " + scoreAndLife.Wave, new Vector2(10, 750 - 1 * font.LineSpacing), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
            if (isGameOver)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, gameOverMsg, new Vector2(screenWidth / 3,
                    screenHeight / 2), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);

                spriteBatch.DrawString(font, "Your Score is: " + scoreAndLife.Score,
                    new Vector2(screenWidth / 3 , screenHeight / 2 -(2 * font.LineSpacing)), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.End();
            }
        }
        /// <summary>
        /// Game over.
        /// </summary>
        public void OnGameOver()
        {
            isGameOver = true;
        }

        public void RepositionAliensToStart()
        {
            for (int y = 0; y < NUM_ALIEN_Y; y++)
            {
                for (int x = 0; x < NUM_ALIEN_X; x++)
                {
                    aliens[y * NUM_ALIEN_X + x].ResetPosition(new Vector2(x * (ALIEN_SIZE_X),
                        y * (ALIEN_SIZE_Y)));
                }
            }
        }
        /// <summary>
        /// Determine if the wave is cleared.
        /// </summary>
        /// <returns>Returns true if the wave is cleared or else false.</returns>
        public bool IsWaveFinished()
        {

            if (player.player.KillCount >= TOTAL_ALIENS)
            {
                isRoundFinished = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Set up for the next wave.
        /// </summary>
        public void PrepareNextWave()
        {
            player.player.Reset();
            squad.ReviveAllAliens();
            for (int y = 0; y < NUM_ALIEN_Y; y++)
            {
                for (int x = 0; x < NUM_ALIEN_X; x++)
                {
                    aliens[y * NUM_ALIEN_X + x].ResetPosition(new Vector2(x * (ALIEN_SIZE_X), 
                        y * (ALIEN_SIZE_Y) + ALIEN_Y_OFFSET * (scoreAndLife.Wave + 1)));
                }
            }
            isRoundFinished = false;
            squad.Reset();
        }
    }
}
