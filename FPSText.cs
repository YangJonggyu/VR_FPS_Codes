using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class FPSText : MonoBehaviour
    {
        private TextMeshPro text;
    
        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TextMeshPro>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = Mathf.RoundToInt(1 / Time.deltaTime).ToString();
        }
    }
}
