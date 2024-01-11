using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Bonus
{

    /// <summary>
    /// Abstract class for the bonus
    /// </summary>
    internal abstract class Bonus : SimpleObject
    {
        #region fields
        /// <summary>
        /// boolean to know if the bonus is moving right or left
        /// </summary>
        private bool MovingRight;

        /// <summary>
        /// double to know the speed of the bonus
        /// </summary>
        private double MovingSpeed = 30;
        #endregion

        #region constructor
        /// <summary>
        ///  Constructor 
        /// </summary>
        /// <param name="position"> Position of the bonus</param>
        /// <param name="moveRight"> boolean to know if the bonus is moving right or left</param>
        /// <param name="image"> Image of the bonus</param>
        public Bonus(Vecteur2D position, bool moveRight, Bitmap image) : base(Side.Enemy, position, 1, image )
        {
            Console.WriteLine("Bonus created");
            MovingRight = moveRight;
        }
        #endregion

        #region override 
        /// <summary>
        /// Method called when the bonus collide with a missile
        /// </summary>
        /// <param name="m"> Missile which collide with the bonus</param>
        /// <param name="numberOfPixelsInCollision"> Number of pixels in collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            if (m.IsPlayerMissile())
            {
                // On ajoute des vies au joueur et on augmente son score
                //m.player.AddLives(1);
                //m.player.AddScore(100);
                EffectOnPlayer(m);
                base.Destroy();
                m.Destroy();
            }
        }
        /// <summary>
        ///  Method called when the bonus collide with a player
        /// </summary>
        /// <param name="gameInstance"> Game instance</param>
        /// <param name="deltaT"> DeltaT</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (MovingRight)
            {
                Position.x += MovingSpeed * deltaT; 
            }
            else
            {
                Position.x -= MovingSpeed * deltaT; 
            }

            // Si le bonus sort de l'écran, on le détruit...
            if (Position.x < 0 || Position.x > gameInstance.gameSize.Width)
            {
                base.Destroy();
            }
        }
        #endregion

        #region abstract method
        /// <summary>
        /// Method called when the bonus collide with a missile
        /// </summary>
        /// <param name="m"> Missile which collide with the bonus</param>
        public abstract void EffectOnPlayer(Missile m);
        #endregion
    }

}
