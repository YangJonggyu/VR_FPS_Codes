using UnityEngine;

namespace Game.Scripts
{
    public class EnemyBulletEffectData : BulletEffectData
    {
        public ParticleSystem criticalEffect;
        
        public override void HitByBullet(BulletHitArgs args)
        {
            MakeBulletEffect(args);
            audioSource.PlayOneShot(hitSound);
            DamageTextPool.Instance.DisplayDamage(args.Pos,args.DamageArgs.CalculatedDamage);
        }
        
        protected override void MakeBulletEffect(BulletHitArgs args)
        {
            emitParam = new ParticleSystem.EmitParams {position = args.Pos};
            var rotation = Quaternion.LookRotation(args.Normal).eulerAngles - new Vector3(0, 180, 0);
            emitParam.rotation3D = rotation;
            if (sparks)
            {
                var shape = sparks.shape;
                shape.rotation = rotation;
            }
            if(args.DamageArgs.IsCritical) criticalEffect.Emit(emitParam,1);
            else effect.Emit(emitParam,1);
        }
    }
}
