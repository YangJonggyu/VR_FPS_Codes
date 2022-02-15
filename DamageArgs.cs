using UnityEngine;

namespace Game.Scripts
{
    public struct DamageArgs
    {
        public float BaseDamage;
        public float CalculatedDamage;
        public bool IsCritical;
        public Character Attacker;
        public Gun ShootingGun;
        public GameObject Target;
        public float CriticalMultiplier;
    }
    
    public struct BulletHitArgs
    {
        public DamageArgs DamageArgs;
        public Vector3 Pos;
        public Vector3 Normal;
        public Gun ShootingGun;


    }

    

}
