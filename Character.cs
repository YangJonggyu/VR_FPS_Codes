using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(TalentList),typeof(AudioSource))]
    public class Character : MonoBehaviour
    {
        public CharacterStat characterStat;
        public TalentList talentList;
        public BulletEffectData bulletEffectData;
    
        protected AudioSource AudioSource;
        public float currentHealth;

        protected virtual void Start()
        {
            AudioSource = GetComponent<AudioSource>();
            talentList = GetComponent<TalentList>();
            currentHealth = characterStat.MaxHealth;
        }
    
        protected virtual void Update()
        {
            
        }

        public virtual void HitByBullet(BulletHitArgs args)
        {
            bulletEffectData.HitByBullet(args);
        }

        protected virtual int GetDamage()
        {
            return 0;
        }
    }
}
