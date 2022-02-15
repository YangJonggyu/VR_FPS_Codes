using UnityEngine;

namespace Game.Scripts
{
    public class BulletEffectData : MonoBehaviour
    {
        public ParticleSystem effect;
        public ParticleSystem sparks;
    
        public AudioSource audioSource;
        public AudioClip hitSound;
        protected ParticleSystem.EmitParams emitParam;

        public virtual void HitByBullet(BulletHitArgs args)
        {
            MakeBulletEffect(args);
            audioSource.PlayOneShot(hitSound);
        }
    
        protected virtual void MakeBulletEffect(BulletHitArgs args)
        {
            emitParam = new ParticleSystem.EmitParams {position = args.Pos};
            var rotation = Quaternion.LookRotation(args.Normal).eulerAngles - new Vector3(0, 180, 0);
            emitParam.rotation3D = rotation;
            if (sparks)
            {
                var shape = sparks.shape;
                shape.rotation = rotation;
            }
            effect.Emit(emitParam,1);
        }
    }
}
