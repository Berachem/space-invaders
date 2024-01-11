using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects
{

    internal class EnemyBlock : GameObject
    {

        #region general informations
        /// <summary>
        /// probability to shoot for each ennemy
        /// </summary>
        private double randomShootProbability;

        /// <summary>
        /// rate of increase in shot probability
        /// </summary>
        private double randomShootProbabilityIncreaseSide = 1.1;

        /// <summary>
        /// speed of the block
        /// </summary>
        private double speed = 10;

        /// <summary>
        /// direction of the block
        /// </summary>
        private int direction = 1; // 1 : right / -1 : left

        /// <summary>
        /// rate of increase in speed
        /// </summary>
        private double speedIncreaseSide = 1.1;

        /// <summary>
        /// space beetween each line
        /// </summary>
        private double offsetOnSideY = 5;

        /// <summary>
        /// Set containign all the ennemies
        /// </summary>
        private HashSet<SpaceShip> enemyShips;

        /// <summary>
        /// Y coordinate of the next line when added
        /// </summary>
        private double nextLineY;
        #endregion

        #region line informations
        /// <summary>
        /// last line level
        /// </summary>
        static int LineLevel = 0;

        /// <summary>
        /// Dictionnary used to find wich ennemy is at wich level 
        /// </summary>
        private Dictionary<SpaceShip, int>  EnemyShipsLineLevel;
        #endregion

        #region dimension

        /// <summary>
        /// width of a line
        /// </summary>
        private int baseWidth;

        /// <summary>
        /// Size of the block
        /// </summary>
        public Size size { get; private set; }

        /// <summary>
        /// Position of the block
        /// </summary>
        public Vecteur2D position { get; protected set; }
        #endregion

        #region constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Position of the block</param>
        /// <param name="size">Size of the block</param>
        /// <param name="randomShootProbability">Random shoot probability when created</param>
        public EnemyBlock(Vecteur2D position,Size size, double randomShootProbability) : base(Side.Enemy)
        {
            this.position = position;
            this.size = size;
            this.nextLineY = position.Y;
            this.baseWidth = size.Width;
            this.randomShootProbability = randomShootProbability;
            this.enemyShips = new HashSet<SpaceShip>();
            this.EnemyShipsLineLevel = new Dictionary<SpaceShip, int>();
        }

        /// <summary>
        /// Constructor generating a default EnnemyBlock depending on the game Size
        /// </summary>
        /// <param name="gameSize">Size of The Game</param>
        /// <param name="randomShootProbability">Random shoot probability when created</param>
        public EnemyBlock(Size gameSize, double randomShootProbability) : this(new Vecteur2D(gameSize.Width * 0.2,gameSize.Height * 0.1), new Size((int)(gameSize.Width * 0.6), (int)(gameSize.Height * 0.4)), randomShootProbability)
        {}


        /// <summary>
        /// Constructor generating a default EnnemyBlock depending on the game Size, with a default shoot probabilty
        /// </summary>
        /// <param name="gameSize">Size of The Gam</param>
        public EnemyBlock(Size gameSize) : this(gameSize, 0.05)
        { }
        #endregion

        #region methods
        /// <summary>
        /// Add a line to the block
        /// </summary>
        /// <param name="nbships">numbers of ships in the line</param>
        /// <param name="nblives">number of lives of each ships in the line</param>
        /// <param name="ShipImage">Image used to generate the ships</param>
        public void AddLine(int nbships,int nblives, Bitmap ShipImage)
        {
            LineLevel++;

            double marge = (baseWidth - (nbships * ShipImage.Width)) / (nbships-1);
            for (int i = 0; i < nbships; i++)
            {
                SpaceShip currentShip = new SpaceShip(Side.Enemy, new Vecteur2D(ShipImage.Width / 2 + i * ShipImage.Width + i * marge + position.x, nextLineY), nblives, (Bitmap)ShipImage.Clone());
                enemyShips.Add(currentShip);
                EnemyShipsLineLevel.Add(currentShip, LineLevel);
            }
            nextLineY += ShipImage.Height + 10;
            this.UpdateSize();
            
        }

        /// <summary>
        /// Update the size of the block depending the death of the ennemies in it
        /// </summary>
        public void UpdateSize()
        {
            if (enemyShips.Count == 0) return;
            double minX = enemyShips.Min(ship => ship.Position.x - (ship.Image.Width / 2));
            double minY = enemyShips.Min(ship => ship.Position.y - (ship.Image.Height / 2));
            double maxX = enemyShips.Max(ship => ship.Position.x + (ship.Image.Width / 2));
            double maxY = enemyShips.Max(ship => ship.Position.y + (ship.Image.Height / 2));

            position.x = minX;
            position.y = minY;
            this.size = new Size((int)(maxX - minX), (int)(maxY - minY));
        }

        /// <summary>
        /// Function to manage the intersection between the enemy block and the player to properly end the game
        /// </summary>
        private void ManageIntersectionWithPlayer(Game gameInstance)
        {
            // if enemy block is at the same level as a player spaceship, we destroy the player spaceship
            List<PlayerSpaceship> playersSpaceships = gameInstance.GetPlayersAliveSpaceship();
            foreach (PlayerSpaceship playerSpaceship in playersSpaceships)
            {
                if (playerSpaceship != null && playerSpaceship.Position.y + playerSpaceship.Image.Height <= position.y + size.Height)
                {
                    playerSpaceship.Destroy();
                }
            }
        }
        #endregion

        #region overides

        /// <summary>
        /// Draw the object in the game
        /// </summary>
        /// <param name="gameInstance">The instance of the game</param>
        /// <param name="graphics">The graphic context for the drawing</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach(SpaceShip ship in enemyShips)
            {
                if (ship.IsAlive()) ship.Draw(gameInstance, graphics);
            }
            //graphics.DrawRectangle(new Pen(Game.blueBrush),new Rectangle((int)position.x, (int)position.y, (int)position.x + size.Width, (int)position.y + size.Height));
        }

        /// <summary>
        /// Method to check if the object is alive.
        /// </summary>
        /// <returns>True if the object is alive, otherwise False.</returns>
        public override bool IsAlive()
        {
            return enemyShips.Count > 0;
        }

        /// <summary>
        /// Method to manage collisions between the object and a missile.
        /// </summary>
        /// <param name="missile">The missile with which there was a collision.</param>
        public override void Collision(Missile m)
        {
            HashSet<SpaceShip> toRemove = new HashSet<SpaceShip>();
            foreach (SpaceShip ship in enemyShips)
            {
                ship.Collision(m);
                if (ship.Lives == 0)
                {
                    // on augmente le score du joueur en fonction de la ligne sur laquelle se trouvait le vaisseau
                    m.player.AddScore(10 * (LineLevel - EnemyShipsLineLevel[ship] + 1));
                    toRemove.Add(ship); // on ajoute le vaisseau à la liste des vaisseaux à détruire
                }
            }
            foreach (SpaceShip ship in toRemove)
            {
                enemyShips.Remove(ship);
            }
            this.UpdateSize();
        }

        /// <summary>
        /// Update the state of an objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            double newposX = position.X + (speed * deltaT * direction);

            if (newposX < 0 || (newposX + size.Width) > gameInstance.gameSize.Width)
            {
                direction *= -1; // changement de direction
                position.y += offsetOnSideY;

                foreach (SpaceShip ship in enemyShips)
                {
                    ship.Position.y += offsetOnSideY;
                }

                // on augmente la vitesse et la probabilité de tirer (pour augmenter la difficulté)
                speed *= speedIncreaseSide;
                randomShootProbability *= randomShootProbabilityIncreaseSide;
            }
            else
            {
                position.x = newposX;

                double shootChance;
                foreach (SpaceShip ship in enemyShips)
                {
                    ship.Position.x += speed * deltaT * direction;

                    // on tire aléatoirement
                    shootChance = gameInstance.random.NextDouble();
                    if (shootChance <= randomShootProbability * deltaT) ship.Shoot(gameInstance);


                }
            }

            ManageIntersectionWithPlayer(gameInstance);

        }
        #endregion
    }
}
