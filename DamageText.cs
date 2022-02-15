using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class DamageText : MonoBehaviour
    {
        public float maxTime;
        private TextMeshPro _textMeshPro;
        private Rect _rect;
        private Vector3 _posTmp;

        private float _currentTime = 0;

        private Player _player;
        // Start is called before the first frame update
        void Awake()
        {
            _player = Player.Instance;
            _textMeshPro = GetComponent<TextMeshPro>();
            _rect = _textMeshPro.rectTransform.rect;
        }

        // Update is called once per frame
        void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > maxTime)
            {
                _textMeshPro.text = "";
                DamageTextPool.Instance.AddToList(gameObject);
                gameObject.SetActive(false);
            }
            transform.rotation =  Quaternion.LookRotation(transform.position - _player.transform.position);
            _textMeshPro.alpha = Mathf.MoveTowards(_textMeshPro.alpha, 0, Time.deltaTime / maxTime * 2);
            transform.position = Vector3.MoveTowards(transform.position, _posTmp, Time.deltaTime / maxTime * _textMeshPro.fontSize * 0.1f);
        }

        public void SetDamageText(Vector3 pos, float damage)
        {
            _currentTime = 0;
            transform.position = pos;
            _textMeshPro.fontSize = Vector3.Distance(transform.position, _player.transform.position) / 5f;
            _posTmp = pos + new Vector3(0,_textMeshPro.fontSize * 0.2f,0);
            _rect.height = _textMeshPro.fontSize * 0.4f;
            _textMeshPro.alpha = 1;
            _textMeshPro.text = NumberConverter.ConvertNumberToString(Mathf.RoundToInt(damage));
        }
    }
}
