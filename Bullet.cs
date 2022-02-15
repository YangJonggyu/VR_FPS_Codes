using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class Bullet : MonoBehaviour
    {
        private ParticleSystem _part;
        private List<ParticleCollisionEvent> _collisionEvents;
        private Gun _gun;


        private void Start()
        {
            _part = GetComponent<ParticleSystem>();
            _collisionEvents = new List<ParticleCollisionEvent>();
            _gun = GetComponentInParent<Gun>();
        }

        private void OnParticleCollision(GameObject other)
        {
            var numCollisionEvents = _part.GetCollisionEvents(other, _collisionEvents);
            var col = other.GetComponentInChildren<BulletEffectCollider>();
            var i = 0;
            
            var hitArgs = new BulletHitArgs
            {
                DamageArgs = new DamageArgs()
                {
                    Attacker = _gun.attacker,
                    Target = other,
                    BaseDamage = _gun.damage,
                    CriticalMultiplier = _gun.criticalMultiplier
                },
                Pos = _collisionEvents[i].intersection,
                Normal = _collisionEvents[i].normal,
                ShootingGun = _gun
            };

            while (i < numCollisionEvents)
            {
                if (col)
                {
                    col.HitByBullet(hitArgs);
                
                }
                i++;
            }
        
        }
    }
}
