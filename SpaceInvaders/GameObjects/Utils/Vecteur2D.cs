using System;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// Classe représentant un vecteur 2D
    /// </summary>
    public class Vecteur2D
    {
        public double x = 0;
        public double y = 0;
        public readonly double norme;
        public double Norme { get => norme; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        /// <summary>
        /// Constructeur de la classe Vecteur2D
        /// </summary>
        /// <param name="x">Composante x du vecteur</param>
        /// <param name="y">Composante y du vecteur</param>
        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.norme = Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// Convertit le vecteur en un point 2D
        /// </summary>
        /// <returns>Un point représentant le vecteur</returns>
        public Point ToPoint()
        {
            return new Point((int)x, (int)y);
        }

        /// <summary>
        /// Surcharge de l'opérateur d'addition pour les vecteurs
        /// </summary>
        public static Vecteur2D operator +(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// Surcharge de l'opérateur de soustraction pour les vecteurs
        /// </summary>
        public static Vecteur2D operator -(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }

        /// <summary>
        /// Surcharge de l'opérateur de négation pour inverser le sens du vecteur
        /// </summary>
        public static Vecteur2D operator -(Vecteur2D v1)
        {
            return new Vecteur2D(-v1.x, -v1.y);
        }

        /// <summary>
        /// Surcharge de l'opérateur de multiplication entre un vecteur et un scalaire
        /// </summary>
        public static Vecteur2D operator *(Vecteur2D v1, double d)
        {
            return new Vecteur2D(v1.x * d, v1.y * d);
        }

        /// <summary>
        /// Surcharge de l'opérateur de multiplication entre un scalaire et un vecteur
        /// </summary>
        public static Vecteur2D operator *(double d, Vecteur2D v1)
        {
            return new Vecteur2D(v1.x * d, v1.y * d);
        }

        /// <summary>
        /// Surcharge de l'opérateur de division entre un vecteur et un scalaire
        /// </summary>
        public static Vecteur2D operator /(Vecteur2D v1, double d)
        {
            if (d == 0)
                throw new ArgumentException("Division par un scalaire à zéro");

            return new Vecteur2D(v1.x / d, v1.y / d);
        }
    }
}
