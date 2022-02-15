using UnityEngine;

namespace Game.Scripts
{
    [CreateAssetMenu(fileName = "CharacterStat", menuName = "ScriptableObjects/CharacterStat", order = 1)]
    public class CharacterStat : ScriptableObject
    {
        [SerializeField] protected int maxHealth;

        public virtual int MaxHealth
        {
            get => maxHealth;
            private set => maxHealth = value;
        }
    
        
    }
}
