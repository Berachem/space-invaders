using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    
namespace SpaceInvaders.GameObjects.Bonus
{
    /// <summary>
    /// Class for the score bonus
    /// </summary>
    internal class ScoreBonus : Bonus
    {
        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"> Position of the bonus</param>
        /// <param name="moveRight"> boolean to know if the bonus is moving right or left</param>
        public ScoreBonus(Vecteur2D position, bool moveRight) : base(position, moveRight, SpaceInvaders.Properties.Resources.bonus) { }
        #endregion

        #region override
        /// <summary>
        ///  Method called when the bonus collide with a missile
        /// </summary>
        /// <param name="m"> Missile which collide with the bonus</param>
        public override void EffectOnPlayer(Missile m)
        {
            m.player.AddScore(1000);
        }
        #endregion
    }
}

