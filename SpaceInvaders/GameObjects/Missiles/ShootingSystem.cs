using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Missiles
{
    internal abstract class ShootingSystem
    {
        /// <summary>
        /// Enumerator of ShootingSystem types
        /// </summary>
        public enum ShootingType { MonoShooting, MultiShooting };

        /// <summary>
        /// entity linked to the System
        /// </summary>
        protected SpaceShip linkedEntity;

        /// <summary>
        /// constructor of the ShootingSystem
        /// </summary>
        /// <param name="linkedEntity"></param>
        public ShootingSystem(SpaceShip linkedEntity)
        {
            this.linkedEntity = linkedEntity;
        }
        
        /// <summary>
        /// manage the shooting 
        /// </summary>
        /// <param name="gameInstance">the game where it shoot</param>
        public abstract void Shoot(Game gameInstance);

        /// <summary>
        /// return true if there is no more missiles alive in the system storage
        /// </summary>
        /// <returns>true if there is no more missile in the system storage</returns>
        public abstract bool isMissilesEmpty();
    }
}
