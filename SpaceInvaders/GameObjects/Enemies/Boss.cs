using SpaceInvaders.GameObjects.Missiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Enemies
{
    internal class Boss : SpaceShip
    {
        #region fields
        /// <summary>
        /// Stopwatch for the shield cadence
        /// </summary>
        Stopwatch shieldStopwatch = new Stopwatch();

        /// <summary>
        /// Stopwatch for the shooting cadence
        /// </summary>
        Stopwatch shootingStopwatch = new Stopwatch();

        /// <summary>
        /// Number of shoot left in a burst
        /// </summary>
        int shootingLeft = 0;

        /// <summary>
        /// variation of the movement
        /// </summary>
        int variations = 0;

        /// <summary>
        /// speed of the boss
        /// </summary>
        int speed = 50;

        /// <summary>
        /// direction
        /// </summary>
        int direction = 1; // 1 : right / -1 : left

        /// <summary>
        /// Image used for the life
        /// </summary>
        Bitmap life = SpaceInvaders.Properties.Resources.life;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Position in the Game</param>
        /// <param name="lives">Number of lives</param>
        /// <param name="image">Image used for the boss</param>
        public Boss(Vecteur2D position, int lives, Bitmap image) : base(Side.Enemy, position, lives, image,ShootingSystem.ShootingType.MultiShooting)
        {
            this.setShield();
            shootingStopwatch.Start();
        }
        #endregion

        #region method
        /// <summary>
        /// Manage the intersection with the player to end the game properly
        /// </summary>
        /// <param name="gameInstance">the game instance</param>
        private void ManageIntersectionWithPlayer(Game gameInstance)
        {
            // if enemy block is at the same level as a player spaceship, we destroy the player spaceship
            List<PlayerSpaceship> playersSpaceships = gameInstance.GetPlayersAliveSpaceship();
            foreach (PlayerSpaceship playerSpaceship in playersSpaceships)
            {
                if (playerSpaceship != null && playerSpaceship.Position.y <= this.Position.y + this.Image.Height)
                {
                    playerSpaceship.Destroy();
                }
            }
        }
        #endregion

        #region overrides

        /// <summary>
        /// Update the state of an objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            //updtate shield
            if (shieldStopwatch.Elapsed >= TimeSpan.FromSeconds(5))
            {
                this.setShield();
                shieldStopwatch.Stop();
            }

            //reset de la salve
            if (this.shootingSystem.isMissilesEmpty())
            {
                shootingLeft = 3;
            }

            //salve de trois tirs
            if (shootingLeft > 0)
            {
                if (shootingStopwatch.Elapsed >= TimeSpan.FromMilliseconds(200))
                {
                    shootingStopwatch.Restart();
                    Shoot(gameInstance);
                    shootingLeft--;
                }
            }


            //deplacement
            double newposX = this.Position.X + (speed * deltaT * direction);
            if ((newposX - this.Image.Width / 2) < 0 || (newposX + this.Image.Width / 2) > gameInstance.gameSize.Width)
            {
                direction *= -1; // changement de direction
            }
            else
            {
                this.Position.X = newposX;
            }

            //déplacment vertical en zigzag
            if (variations < 500)
            {
                this.Position.Y += 0.07;
            }
            else
            {
                this.Position.Y -= 0.05;
            }
            variations = (variations + 1) % 1000;




            //verification de la fin de aprtie
            ManageIntersectionWithPlayer(gameInstance);
        }

        /// <summary>
        /// Draw the object in the game
        /// </summary>
        /// <param name="gameInstance">The instance of the game</param>
        /// <param name="graphics">The graphic context for the drawing</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            float lifeSide = life.Height / 2;
            float baseY = (float)(this.Position.Y - lifeSide);
            float baseX = (float)(this.Position.X - (lifeSide * this.Lives / 2));

            for (int i = 0; i < this.Lives; i++)
            {
                graphics.DrawImage(this.life, baseX + (i * lifeSide), baseY, lifeSide, lifeSide);
            }

        }

        /// <summary>
        /// Abstract method to handle collision with a missile.
        /// </summary>
        /// <param name="m">The missile that collided with.</param>
        /// <param name="numberOfPixelsInCollision">The number of pixels in collision.</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {

            base.OnCollision(m, numberOfPixelsInCollision);
            if (!hasShield && !shieldStopwatch.IsRunning)
            {
                shieldStopwatch.Restart();
            }
        }
        #endregion
    }
}
