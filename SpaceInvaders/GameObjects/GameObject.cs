using SpaceInvaders.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject : GeneralObject
    {
        #region fields
        /// <summary>
        /// Enumeration for the side of the object
        /// </summary>
        public enum Side{ Ally,Enemy, Neutral }

        /// <summary>
        /// Side of the object
        /// </summary>
        public Side ObjectSide { get; private set; }
        #endregion

        #region constructor
        public GameObject(Side side)
        {
            ObjectSide = side;
        }
        #endregion

        #region methods
        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();

        /// <summary>
        /// Method to manage collisions between the object and a missile.
        /// </summary>
        /// <param name="missile">The missile with which there was a collision.</param>
        public abstract void Collision(Missile m);
        #endregion

    }
}
