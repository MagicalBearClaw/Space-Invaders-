using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public delegate void NoLivesLeft();
    public class ScoreAndLife
    {
        public int Score { get; set; }
        public int Wave { get; set; }
        public int Lives { get; private set; }
        public event NoLivesLeft NoLives;
        public event NextWave ProceedToNextWave;

        private int startingLives;
        /// <summary>
        /// Creates a new ScoreAndLife object.
        /// </summary>
        /// <param name="lives"></param>
        public ScoreAndLife(int lives = 3)
        {
            this.Lives = lives;
            startingLives = lives;
        }
        /// <summary>
        /// Decrements the player lives.If there is no lives left
        /// fire the NoLives event.
        /// </summary>
        public void DecrementLives()
        {
            if (Lives > 0)
            {
                Lives--;
                if(Lives <= 0)
                {
                    OnNoLivesLeft();
                }
            }
        }
        /// <summary>
        /// Fire off the NoLives event.
        /// </summary>
        protected void OnNoLivesLeft()
        {
            if (NoLives != null)
                NoLives();
        }
         /// <summary>
         /// When the wave is cleared fire off an event.
         /// </summary>
        protected void OnNextWave()
        {
            if (ProceedToNextWave != null)
                ProceedToNextWave();
        }
        /// <summary>
        /// Fire the next wwave evnet as well as increase the wave count.
        /// </summary>
        public void StartNextWave()
        {
            OnNextWave();
            Wave++;
        }
        /// <summary>
        /// Increase the score..
        /// </summary>
        /// <param name="points">The points that are incoming.</param>
        public void IncreasePoints(int points)
        {
            Score += points;
        }
        /// <summary>
        /// When the player is reached by the alien squad
        /// deplete the players lives and fire off an event 
        /// because the player has no lives left.
        /// </summary>
        public void OnbottomReached()
        {
            this.Lives = 0;
            OnNoLivesLeft();
            
        }
        /// <summary>
        /// Reset to defaults.
        /// </summary>
        public void Reset()
        {
            Score = 0;
            Lives = startingLives;
            Wave = 0;
        }
    }
}
