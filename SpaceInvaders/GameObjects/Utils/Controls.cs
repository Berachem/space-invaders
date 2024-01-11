using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SpaceInvaders.GameObjects.Utils
{
    /// <summary>
    /// Class for the controls
    /// </summary>
    internal class Controls
    {
        #region fields
        /// <summary>
        /// Key that represent the left key
        /// </summary>
        public Keys Left {  get; private set; }

        /// <summary>
        /// Keys that represent the right key
        /// </summary>
        public Keys Right { get; private set; }

        /// <summary>
        /// Keys that represent the shoot key
        /// </summary>
        public Keys Shoot { get; private set; }
        #endregion

        #region constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="left"> Key that represent the left key</param>
        /// <param name="right"> Keys that represent the right key</param>
        /// <param name="shoot"> Keys that represent the shoot key</param>
        public Controls(Keys left, Keys right, Keys shoot)
        {
            this.Left = left;
            this.Right = right;
            this.Shoot = shoot;
        }

        #endregion

        #region methods
        /// <summary>
        ///  Method to get the default controls
        /// </summary>
        /// <returns> the default controls</returns>
        public static Controls GetDefaultControls()
        {
            return new Controls(Keys.Left, Keys.Right, Keys.Space);
        }

        /// <summary>
        ///  Method to get the alternative controls
        /// </summary>
        /// <returns> the alternative controls</returns>
        public static Controls GetAlternativeControls()
        {
            return new Controls(Keys.Q, Keys.D, Keys.S);
        }
        #endregion
    }
}
