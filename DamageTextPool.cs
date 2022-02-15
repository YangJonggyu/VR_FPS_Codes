using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class DamageTextPool : MonoBehaviour
    {
        public static DamageTextPool Instance { private set; get; } = null;
    
        private List<GameObject> _textObjects = new List<GameObject>();
        public GameObject textPrefab;
    
        private void Awake()
        {
            if (null == Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        // Start is called before the first frame update
        void Start()
        {
            for (var i = 0; i < 30; i++)
            {
                var text = Instantiate(textPrefab,transform);
                _textObjects.Add(text);
                text.SetActive(false);
            }
        
        }

        public void DisplayDamage(Vector3 pos, float damage)
        {
        
            if (_textObjects.Count <= 0)
            {
                var newText = Instantiate(textPrefab,transform);
                _textObjects.Add(newText);
                newText.SetActive(false);
            }

            var text = _textObjects[0];
            text.SetActive(true);
            text.GetComponent<DamageText>().SetDamageText(pos, damage);
            _textObjects.RemoveAt(0);
        }

        public void AddToList(GameObject text)
        {
            _textObjects.Add(text);
        }
    }
}
