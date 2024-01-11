using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpaceInvaders.GameObject;

namespace SpaceInvaders.GameObjects.Missiles
{
    internal class MonoShooting : ShootingSystem
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="linkedEntity">the entity linked to the shooting system</param>
        public MonoShooting(SpaceShip linkedEntity) : base(linkedEntity)
        {}

        /// <summary>
        /// Missile linked to the shooting system
        /// </summary>
        public Missile MissileLaunched { get; set; }
        
        /// <summary>
        /// manage the shooting 
        /// </summary>
        /// <param name="gameInstance">the game where it shoot</param>
        public override void Shoot(Game gameInstance)
        {
            // if there is no missile we can shoot
            if (isMissilesEmpty())
            {
                // the new missile is in the middle of the ship
                double missileX = linkedEntity.Position.x;
                double missileY = linkedEntity.Position.y - linkedEntity.Image.Height / 1.5;

                // random between 0 and 1 to determine if the missile is a wiggly one
                int choice = gameInstance.random.Next(0, 2);

                // Creation of the new missile
                MissileLaunched = new Missile(linkedEntity.ObjectSide, new Vecteur2D(missileX, missileY), 1, linkedEntity.ObjectSide == Side.Ally ? SpaceInvaders.Properties.Resources.enemy_projectile : (choice == 0 ? SpaceInvaders.Properties.Resources.ally_projectile : SpaceInvaders.Properties.Resources.wiggly_projectile), linkedEntity.ObjectSide == Side.Ally ? (PlayerSpaceship)linkedEntity : null);

                // add the new missile to the game objects
                gameInstance.AddNewGameObject(MissileLaunched);

                // Shoot sound
                if (linkedEntity.ObjectSide == Side.Ally) Game.soundManager.PlayPlayerShootingSound();
            }
        }

        /// <summary>
        /// return true if there is no more missiles alive in the system storage
        /// </summary>
        /// <returns>true if there is no more missile in the system storage</returns>
        public override bool isMissilesEmpty()
        {
            return MissileLaunched == null || !MissileLaunched.IsAlive();
        }
    }
}
