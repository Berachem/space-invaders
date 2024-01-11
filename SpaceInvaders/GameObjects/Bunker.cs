using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Classe représentant un bunker dans le jeu
    /// </summary>
    internal class Bunker : SimpleObject
    {
        /// <summary>
        /// Constructeur de la classe Bunker
        /// </summary>
        /// <param name="position">Position initiale du bunker</param>
        public Bunker(Vecteur2D position) : base(Side.Neutral, position, 1, SpaceInvaders.Properties.Resources.bunker) { }

        /// <summary>
        /// Met à jour l'état du bunker
        /// </summary>
        /// <param name="gameInstance">Instance du jeu</param>
        /// <param name="deltaT">Temps écoulé depuis la dernière mise à jour</param>
        public override void Update(Game gameInstance, double deltaT) { }

        /// <summary>
        /// Gère la collision entre un missile et le bunker
        /// </summary>
        /// <param name="m">Le missile avec lequel il y a eu collision</param>
        /// <param name="numberOfPixelsInCollision">Nombre de pixels en collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            m.RemoveLives(numberOfPixelsInCollision);
        }
    }
}
