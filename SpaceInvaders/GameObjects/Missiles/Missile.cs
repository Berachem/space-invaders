using System;
using System.Drawing;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {
        /// <summary>
        /// speed of the missile
        /// </summary>
        public double Vitesse = 500.0;

        /// <summary>
        /// Player who shoot the missile (null if it's an ennemy)
        /// </summary>
        public PlayerSpaceship player = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="side">Side of the missile</param>
        /// <param name="position">Position of the missile</param>
        /// <param name="lives">Lifes of the missile</param>
        /// <param name="image">Image of the missile </param>
        public Missile(Side side, Vecteur2D position, int lives, Bitmap image, PlayerSpaceship player) : base(side, position, lives, image)
        {
            this.player = player;
        }


        /// <summary>
        /// Update the state of the missile
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            // Move the missile vertically according to its speed and side
            if (ObjectSide == Side.Ally) Position.y -= Vitesse * deltaT;
            else if (ObjectSide == Side.Enemy) Position.y += Vitesse * deltaT;

            if (Position.y < 0 || Position.y>gameInstance.gameSize.Height){Lives = 0;}

            foreach (GameObject obj in gameInstance.gameObjects)
            {
                obj.Collision(this);
            }
        }


        /// <summary>
        /// Manage the collision with another GameObject
        /// </summary>
        /// <param name="m">The missile where there was a collision</param>
        /// <param name="numberOfPixelsInCollision">Number of pixel that are concerned</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            if (this != m)
            {
                this.Destroy();
                m.Destroy();

                Console.WriteLine("Destruction de deux missiles après collision. Nombre de pixels en collision : " + numberOfPixelsInCollision);
            }
        }

        /// <summary>
        /// Return true if the missile was shoot by the player
        /// </summary>  
        public bool IsPlayerMissile()
        {
            return player != null;
        }

    }
}
