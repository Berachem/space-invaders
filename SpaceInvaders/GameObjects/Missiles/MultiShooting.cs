using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpaceInvaders.GameObject;

namespace SpaceInvaders.GameObjects.Missiles
{
    internal class MultiShooting : ShootingSystem
    {
        /// <summary>
        /// list of missiles shooted by the system
        /// </summary>
        List<Missile> missiles = new List<Missile>();

        /// <summary>
        /// maximum of missiles alive at the same time in the system
        /// </summary>
        public int max { get; private set; }

        /// <summary>
        /// Consttructor of the MultishootingSystem
        /// </summary>
        /// <param name="linkedEntity">the entity linked to this system</param>
        /// <param name="max">maximum of missiles alive at the same time in the system</param>
        public MultiShooting(SpaceShip linkedEntity, int max) : base(linkedEntity)
        {
            this.max = max;
        }

        /// <summary>
        /// manage the shooting 
        /// </summary>
        /// <param name="gameInstance">the game where it shoot</param>
        public override void Shoot(Game gameInstance)
        {
            //update of missiles
            List<Missile> stillAlives = new List<Missile>();
            foreach (Missile m in missiles)
            {
                if(m.IsAlive())
                {
                    stillAlives.Add(m);
                }
            }
            missiles = stillAlives;
            
            //shooting of a new missile
            if (missiles.Count() < max)
            {
                // the new missile is in the middle of the ship
                double missileX = linkedEntity.Position.x;
                double missileY = linkedEntity.Position.y - linkedEntity.Image.Height / 1.5;

                
                // Creation of the new missile
                Missile MissileLaunched = new Missile(linkedEntity.ObjectSide, new Vecteur2D(missileX, missileY), 1, linkedEntity.ObjectSide == Side.Ally ? SpaceInvaders.Properties.Resources.enemy_projectile : (SpaceInvaders.Properties.Resources.ally_projectile), linkedEntity.ObjectSide == Side.Ally ? (PlayerSpaceship)linkedEntity : null);

                // add the new missile to the system storage and game objects
                missiles.Add(MissileLaunched);
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
            foreach (Missile m in missiles)
            {
                if (m.IsAlive())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
