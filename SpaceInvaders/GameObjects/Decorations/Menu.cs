using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Decorations
{
    /// <summary>
    /// Class for the menu
    /// </summary>
    internal class Menu : GeneralObject
    {
        #region fields
        /// <summary>
        /// List of the buttons of the menu
        /// </summary>
        public List<MenuButton> buttons { get; private set; }

        /// <summary>
        ///  Selected button index
        /// </summary>
        private int selectedIndex = 0;

        #endregion

        #region constructor
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="buttons"> List of the buttons of the menu</param>
        public Menu(List<MenuButton> buttons)
        {
            this.buttons = buttons;

            //initialisiation of button placement
            Size gameSize = Game.game.gameSize;
            Bitmap reference = buttons[0].buttonImage;
            int size = buttons.Count();
            
            double spacing = 30;
            double totalSizeMenu = (spacing* (size-1) ) + (reference.Height * size);
            double marginTop = (gameSize.Height - totalSizeMenu)/2;

            double x = (gameSize.Width / 2) - (reference.Width / 2);
            for (int i = 0; i < size; i++)
            {
                buttons[i].Position = new Vecteur2D(x, (reference.Height / 2) + (i * reference.Height) + (i * spacing)+marginTop);
            }

            //selection par defaut du premier boutton
            buttons[selectedIndex].select();
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
            foreach(MenuButton button in buttons)
            {
                button.Draw(gameInstance, graphics);
            }
            graphics.DrawString("Press Space to choose", Game.defaultFont, Game.whiteBrush, gameInstance.gameSize.Width / 2 - 110, gameInstance.gameSize.Height - 50); 
        }

        /// <summary>
        /// Update the state of an objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Up))
            {
                buttons[selectedIndex].unSelect();
                if(selectedIndex == 0)
                {
                    selectedIndex = buttons.Count() - 1;
                }
                else
                {
                    selectedIndex--;
                }
                buttons[selectedIndex].select();
                gameInstance.keyPressed.Remove(System.Windows.Forms.Keys.Up);

            }
            else if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Down))
            {
                buttons[selectedIndex].unSelect();
                selectedIndex = (selectedIndex + 1) % buttons.Count();
                buttons[selectedIndex].select();
                gameInstance.keyPressed.Remove(System.Windows.Forms.Keys.Down);

            }
            else if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Space)){
                buttons[selectedIndex].Update(gameInstance, deltaT);
            }



            
        }
        #endregion

    }
}
