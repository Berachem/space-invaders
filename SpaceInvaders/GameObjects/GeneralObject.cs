using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects
{
    internal abstract class GeneralObject
    {
        readonly Bitmap bgImage = SpaceInvaders.Properties.Resources.default_bg; // TODO: change this to the background image

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public virtual void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(bgImage, 0, 0, gameInstance.gameSize.Width, gameInstance.gameSize.Height);

        }


        /// <summary>
        /// Update the state of an objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

    }
}
