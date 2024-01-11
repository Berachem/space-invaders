using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceInvaders.GameObjects.Utils;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Class for the player spaceship
    /// </summary>
    internal class PlayerSpaceship : SpaceShip
    {
        #region fields
        /// <summary>
        ///  Controls of the player
        /// </summary>
        private Controls controls;

        /// <summary>
        /// Integer to know the number of the player
        /// </summary>
        public int playerNumber;

        /// <summary>
        ///  Integer to know the score of the player
        /// </summary>
        public int Score = 0;

        /// <summary>
        /// String to know the ID of the player
        /// </summary>
        public String playerID;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerID"> ID of the player</param>
        /// <param name="position"> Position of the player</param>
        /// <param name="lives"> Number of lives of the player</param>
        /// <param name="image"> Image of the player</param>
        /// <param name="controls"> Controls of the player</param>
        /// <param name="playerNumber"> Number of the player</param>
        public PlayerSpaceship(String playerID, Vecteur2D position, int lives, Bitmap image, Controls controls, int playerNumber) : base(Side.Ally, position, lives, image)
        {
            this.controls = controls;
            this.playerNumber = playerNumber;
            //this.setShield();
            this.playerID = playerID;
        }

        #endregion

        #region methods

        /// <summary>
        ///  Method to get the default player
        /// </summary>
        /// <param name="gameSize"> Size of the game</param>
        /// <param name="playerID"> ID of the player</param>
        /// <returns></returns>
        public static PlayerSpaceship GetPlayerDefault(Size gameSize, String playerID)
        {
    
            Bitmap image = SpaceInvaders.Properties.Resources.player_ship; // OR ship3
            double playerX = gameSize.Width / 2;

            return new PlayerSpaceship(playerID, new Vecteur2D(playerX, gameSize.Height - (image.Height * 1.5)), 3, image, Controls.GetDefaultControls(), 1);
        }

        /// <summary>
        /// Method to get the alternative player
        /// </summary>
        /// <param name="gameSize"> Size of the game</param>
        /// <param name="playerID"> ID of the player</param>
        /// <returns></returns>
        public static PlayerSpaceship GetPlayerAlternative(Size gameSize, String playerID)
        {

            Bitmap image = SpaceInvaders.Properties.Resources.player_ship2;
            double playerX = 2* gameSize.Width / 2.5;

            return new PlayerSpaceship(playerID, new Vecteur2D(playerX, gameSize.Height - (image.Height * 1.5)), 3, image, Controls.GetAlternativeControls(), 2);
        }


        public override void Update(Game gameInstance, double deltaT)
        {

            // Gauche
            if (gameInstance.keyPressed.Contains(controls.Left) && Position.x > (Image.Width / 2))
            {
                Position.x -= deltaT * base.SpeedPixelPerSecond;
            }

            // Droite
            if (gameInstance.keyPressed.Contains(controls.Right) && Position.x < (gameInstance.gameSize.Width - (Image.Width / 2)))
            {
                Position.x += deltaT * base.SpeedPixelPerSecond;
                
            }

            // Espace (pour tirer un missile)
            if (gameInstance.keyPressed.Contains(controls.Shoot)){ Shoot(gameInstance);   gameInstance.ReleaseKey(controls.Shoot); }
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            // Draw number player at the bottom of the player
            graphics.DrawString(playerID.ToString(), new Font("Arial", 7), new SolidBrush(Color.White), new PointF((float) Position.x - 15, (float)Position.y + Image.Height));


            // Draw Lives
            for (int i = 0; i < Lives + (hasShield ? 1 : 0); i++)
            {
                Bitmap healthImage = (i == Lives && hasShield) ? SpaceInvaders.Properties.Resources.shield : SpaceInvaders.Properties.Resources.life;
                int xPosition = (playerNumber == 1) ? 10 + (i * healthImage.Width) : gameInstance.gameSize.Width - 10 - ((i + 1) * healthImage.Width);
                graphics.DrawImage(healthImage, xPosition, 10, healthImage.Width, healthImage.Height);
            }

            // Draw Score
            int scoreXPosition = (playerNumber == 1) ? 10 : gameInstance.gameSize.Width - 10 - (Score.ToString().Length * 10);
            graphics.DrawString(Score.ToString(), new Font("Arial", 10), Game.whiteBrush, new PointF(scoreXPosition, 40));

        }

        /// <summary>
        ///  Method to add score to the player
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(int score)
        {
            Score += score;
        }
        #endregion
    }
}
