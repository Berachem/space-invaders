using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Missiles;

namespace SpaceInvaders
{
    /// <summary>
    /// Class for SpaceShip
    /// </summary>
    internal class SpaceShip : SimpleObject
    {
        // Vitesse en pixels par seconde de déplacement du vaisseau
        protected double SpeedPixelPerSecond = 100;

        // Missile actuellement lancé par le vaisseau
        public ShootingSystem shootingSystem;

        //Booleen indiquant si le vaisseau possède un bouclier
        public Boolean hasShield = false;

        /// <summary>
        /// Constructeur de la classe SpaceShip.
        /// </summary>
        /// <param name="side">Le côté du vaisseau (allié, ennemi, neutre).</param>
        /// <param name="position">La position initiale du vaisseau.</param>
        /// <param name="lives">Le nombre de vies du vaisseau.</param>
        /// <param name="image">L'image représentant le vaisseau.</param>
        public SpaceShip(Side side, Vecteur2D position, int lives, Bitmap image, ShootingSystem.ShootingType shootingType) : base(side, position, lives, image)
        {
            switch(shootingType)
            {
                case ShootingSystem.ShootingType.MonoShooting:
                    this.shootingSystem = new MonoShooting(this);
                    break;
                case ShootingSystem.ShootingType.MultiShooting:
                    this.shootingSystem = new MultiShooting(this,3);
                    break;
            }
        }

        public SpaceShip(Side side, Vecteur2D position, int lives, Bitmap image) : this(side, position, lives, image, ShootingSystem.ShootingType.MonoShooting)
        {}



        /// <summary>
        /// Méthode pour mettre à jour l'état du vaisseau.
        /// </summary>
        /// <param name="gameInstance">L'instance du jeu.</param>
        /// <param name="deltaT">La durée depuis la dernière mise à jour.</param>
        public override void Update(Game gameInstance, double deltaT)
        { }

        /// <summary>
        /// Méthode pour faire tirer le vaisseau.
        /// </summary>
        /// <param name="gameInstance">L'instance du jeu.</param>
        public void Shoot(Game gameInstance)
        {
            shootingSystem.Shoot(gameInstance);
        }

        /// <summary>
        /// Méthode pour gérer la collision entre le vaisseau et un missile.
        /// </summary>
        /// <param name="m">Le missile avec lequel il y a eu collision.</param>
        /// <param name="numberOfPixelsInCollision">Le nombre de pixels en collision.</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            int livesToRemoveToBoth = Math.Min(m.Lives, this.Lives);
            m.RemoveLives(livesToRemoveToBoth);
            if (hasShield)
            {
                this.hasShield = false;
            }
            else
            {
                RemoveLives(livesToRemoveToBoth);
                // Sound of destruction of the ship
                if (base.ObjectSide == Side.Enemy && !IsAlive()) Game.soundManager.PlayEnemyDeathSound();
                else if (base.ObjectSide == Side.Ally) Game.soundManager.PlayPlayerLosingLifeSound();
                Console.WriteLine( (base.ObjectSide == Side.Enemy ? "Destruction of an enemy " : "Reducing life of a player ") + "ship after collision.");
            }
            
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            if (hasShield)
            {
                Vecteur2D TopLeft = GetTopLeftPosition();
                Bitmap Image = this.Image;
                graphics.DrawEllipse(Game.whitePen, (float)TopLeft.x - 5, (float)TopLeft.y - 5, Image.Width + 10, Image.Height + 5);
            }
        }

        /// <summary>
        /// Méthode pour ajouter un bouclier à un vaisseau
        /// </summary>
        public void setShield()
        {
            this.hasShield = true;
        }
    }
}
