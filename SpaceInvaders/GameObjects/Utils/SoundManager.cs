using SpaceInvaders.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    ///  Class for the sound manager
    /// </summary>
    internal class SoundManager
    {
        #region fields
        /// <summary>
        /// Sounds for the game
        /// </summary>
        private SoundPlayer backgroundMusicPlayer;
        private SoundPlayer playerShoottingSoundPlayer;
        private SoundPlayer playerLosingLifeSoundPlayer;
        private SoundPlayer enemyDeathSoundPlayer;
        private SoundPlayer winSoundPlayer;
        private SoundPlayer loseSoundPlayer;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SoundManager()
        {
            // Load the sounds
            backgroundMusicPlayer = new SoundPlayer(Resources.background_sound);
            playerShoottingSoundPlayer = new SoundPlayer(Resources.player_shooting_sound);
            playerLosingLifeSoundPlayer = new SoundPlayer(Resources.player_losing_life_sound);
            enemyDeathSoundPlayer = new SoundPlayer(Resources.invader_explosion_sound);
            winSoundPlayer = new SoundPlayer(Resources.win_sound);
            loseSoundPlayer = new SoundPlayer(Resources.lose_sound);

        }
        #endregion

        #region methods

        /// <summary>
        /// Play background music
        /// </summary>
        public void PlayBackgroundMusic()
        { 
           backgroundMusicPlayer.PlayLooping(); 
        }

        /// <summary>
        /// Stop background music
        /// </summary>
        public void StopBackgroundMusic()
        {
            backgroundMusicPlayer.Stop();
        }

        /// <summary>
        /// Play shooting sounds
        /// </summary>
        public void PlayPlayerShootingSound()
        {
            playerShoottingSoundPlayer.Play();
        }

        /// <summary>
        /// Play loosing life effect
        /// </summary>
        public void PlayPlayerLosingLifeSound()
        {
            playerLosingLifeSoundPlayer.Play();
        }

        /// <summary>
        /// Play ennemy death sound
        /// </summary>
        public void PlayEnemyDeathSound()
        {
            enemyDeathSoundPlayer.Play();
        }   

        /// <summary>
        /// Play winning music
        /// </summary>
        public void PlayWinSound()
        {
            backgroundMusicPlayer.Stop();
            winSoundPlayer.Play();
        }

        /// <summary>
        /// Play loosing sound
        /// </summary>
        public void PlayLoseSound()
        {
            backgroundMusicPlayer.Stop();
            loseSoundPlayer.Play();
        }
        #endregion

    }
}
