using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Classe abstraite représentant un objet simple dans le jeu.
    /// </summary>
    internal abstract class SimpleObject : GameObject
    {
        public Vecteur2D Position { get; protected set; }
        public int Lives { get; set; } 
        public Bitmap Image { get; protected set; }

        /// <summary>
        /// Constructeur de la classe SimpleObject.
        /// </summary>
        /// <param name="side">Le côté de l'objet (allié, ennemi, neutre).</param>
        /// <param name="position">La position initiale de l'objet.</param>
        /// <param name="lives">Le nombre de vies de l'objet.</param>
        /// <param name="image">L'image associée à l'objet.</param>
        public SimpleObject(Side side, Vecteur2D position, int lives, Bitmap image) : base(side)
        {
            Position = position;
            Lives = lives;
            Image = image;
        }

        /// <summary>
        /// Méthode pour dessiner l'objet dans le jeu.
        /// </summary>
        /// <param name="gameInstance">L'instance du jeu.</param>
        /// <param name="graphics">Le contexte graphique pour le dessin.</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            Vecteur2D TopLeft = GetTopLeftPosition();
            graphics.DrawImage(Image, (float)TopLeft.x, (float)TopLeft.y, Image.Width, Image.Height);
            //graphics.DrawRectangle(new Pen(Game.redBrush), new Rectangle((int)Position.x, (int)Position.y, (int)Position.x + Image.Width, (int)Position.y + Image.Height));
        }

        /// <summary>
        /// Méthode abstraite pour gérer la collision avec un missile.
        /// </summary>
        /// <param name="m">Le missile avec lequel il y a eu collision.</param>
        /// <param name="numberOfPixelsInCollision">Le nombre de pixels en collision.</param>
        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);

        /// <summary>
        /// Méthode pour gérer les collisions entre l'objet et un missile.
        /// </summary>
        /// <param name="missile">Le missile avec lequel il y a eu collision.</param>
        public override void Collision(Missile missile)
        {
            if (base.ObjectSide == missile.ObjectSide) return; // On ne veut pas de collision entre deux objets du même côté ou avec les décors

            Rectangle currentObjectRect = this.GetRectangleArea();
            Rectangle missileRect = missile.GetRectangleArea();
            Rectangle intersectionRect = Rectangle.Intersect(currentObjectRect, missileRect);

            if (intersectionRect.Width > 0 && intersectionRect.Height > 0)
            {
                int numberOfPixelsInCollision = CountPixelsIntersection(missile, currentObjectRect, intersectionRect);

                OnCollision(missile, numberOfPixelsInCollision);
            }
        }

        /// <summary>
        /// Méthode pour compter les pixels en collision entre l'objet et un missile.
        /// </summary>
        /// <param name="missile">Le missile avec lequel il y a eu collision.</param>
        /// <param name="currentObjectRect">Le rectangle englobant l'objet.</param>
        /// <param name="intersectionRect">Le rectangle d'intersection entre l'objet et le missile.</param>
        /// <returns>Le nombre de pixels en collision.</returns>
        private int CountPixelsIntersection(Missile missile, Rectangle currentObjectRect, Rectangle intersectionRect)
        {
            int numberOfPixelsInCollision = 0;
            for (int x = intersectionRect.X; x < intersectionRect.X + intersectionRect.Width; x++)
            {
                for (int y = intersectionRect.Y; y < intersectionRect.Y + intersectionRect.Height; y++)
                {
                    if (Image.GetPixel(x - currentObjectRect.X, y - currentObjectRect.Y).A != 0)
                    {
                        Image.SetPixel(x - currentObjectRect.X, y - currentObjectRect.Y, Color.Transparent);
                        numberOfPixelsInCollision++;
                    }
                }
            }

            return numberOfPixelsInCollision;
        }



        /// <summary>
        /// Méthode pour vérifier si l'objet est en vie.
        /// </summary>
        /// <returns>True si l'objet est en vie, sinon False.</returns>
        public override bool IsAlive()
        {
            return Lives > 0;
        }

        /// <summary>
        /// Méthode pour retirer un nombre spécifique de vies à l'objet.
        /// </summary>
        /// <param name="numberOfLivesToRemove">Le nombre de vies à retirer.</param>
        public void RemoveLives(int numberOfLivesToRemove)
        {
            Lives -= numberOfLivesToRemove;
            if (Lives < 0) Destroy(); // On ne veut pas de nombre de vies négatif
        }

        /// <summary>
        /// Méthode pour ajouter un nombre spécifique de vies à l'objet.
        /// </summary>
        /// <param name="numberOfLivesToAdd">Le nombre de vies à ajouter.</param>
        public void AddLives(int numberOfLivesToAdd)
        {
            Lives += numberOfLivesToAdd;
        }


        /// <summary>
        /// Méthode pour détruire complètement l'objet en le faisant perdre toutes ses vies.
        /// </summary>
        public void Destroy()
        {
            Lives = 0;
        }

        /// <summary>
        /// Méthode pour obtenir la position du coin supérieur gauche de l'objet.
        /// </summary>
        /// <returns>Un vecteur 2D représentant la position du coin supérieur gauche.</returns>
        public Vecteur2D GetTopLeftPosition()
        {
            return new Vecteur2D(Position.x - Image.Width / 2, Position.y);
        }

        /// <summary>
        /// Méthode pour obtenir le rectangle englobant de l'objet.
        /// </summary>
        /// <returns>Un rectangle représentant la zone occupée par l'objet.</returns>
        public Rectangle GetRectangleArea()
        {
            Vecteur2D TopLeft = GetTopLeftPosition();
            return new Rectangle((int) TopLeft.X, (int) TopLeft.Y, Image.Width, Image.Height);
        }
       
           
    
    }
}
