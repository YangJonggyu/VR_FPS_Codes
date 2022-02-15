using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class BulletEffectCollider : MonoBehaviour
    {
        public Character character;
        public BulletEffectData bulletEffectData;
        public bool isCritical;

        private void Start()
        {
            character = GetComponentInParent<Character>();
            if(!character) bulletEffectData = GetComponentInParent<BulletEffectData>();
        }

        public void HitByBullet(BulletHitArgs args)
        {
            args.DamageArgs.IsCritical = isCritical;
            if (character) character.HitByBullet(args);
            else bulletEffectData.HitByBullet(args);
        }
    }
}
