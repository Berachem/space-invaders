using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Class for the background
    /// </summary>
    internal class Background : GeneralObject
    {
        #region fields
        /// <summary>
        /// Image used for the background
        /// </summary>
        readonly Bitmap bgImage = SpaceInvaders.Properties.Resources.default_bg;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Background() 
        { }
        #endregion

        #region override

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(bgImage, 0, 0, gameInstance.gameSize.Width, gameInstance.gameSize.Height);
            
        }

        /// <summary>
        /// Update the state of an objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            //maybe change the background on win or loose
        }
        #endregion

    }
}
