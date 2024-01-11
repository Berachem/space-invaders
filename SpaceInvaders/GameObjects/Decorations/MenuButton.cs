using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Decorations
{
    /// <summary>
    /// Class for the menu button
    /// </summary>
    internal class MenuButton : GeneralObject
    {
        #region fields

        /// <summary>
        /// boolean to know if the button is selected
        /// </summary>
        private Boolean isSelected = false;

        /// <summary>
        ///  Image used for the button
        /// </summary>
        public Bitmap buttonImage { get; private set; }

        /// <summary>
        ///  Position of the button
        /// </summary>
        public Vecteur2D Position { get; set; }

        /// <summary>
        ///  action to do when the button is selected
        /// </summary>
        private Action<Game> onAction;

        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buttonImage"> Image used for the button</param>
        /// <param name="onAction"> action to do when the button is selected</param>
        public MenuButton(Bitmap buttonImage,Action<Game> onAction) 
        {
            this.buttonImage = buttonImage;
            this.onAction = onAction;

            //we need it to respect Draw signature but it will be initialised by our Menue
            this.Position = new Vecteur2D(0,0);
        }
        #endregion

        #region methods
        /// <summary>
        /// method to select the button
        /// </summary>
        public void select()
        {
            isSelected = true;
        }

        /// <summary>
        /// method to unselect the button
        /// </summary>
        public void unSelect()
        {
            isSelected = false;
        }

        #endregion

        #region override

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(buttonImage, (float)Position.X, (float)Position.Y, buttonImage.Width, buttonImage.Height);
            if(isSelected )
            {
                graphics.DrawRectangle(Game.whitePen, new Rectangle((int)Position.X - 10, (int)Position.Y - 10, (int)buttonImage.Width + 20, (int)buttonImage.Height + 20));
            }
        }

        /// <summary>
        /// Update the state of an objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if(isSelected && gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Space))
            {
                onAction?.Invoke(gameInstance);
                gameInstance.keyPressed.Remove(System.Windows.Forms.Keys.Space);
            }
        }
        #endregion


    }
}
