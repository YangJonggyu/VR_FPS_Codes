using UnityEngine;

namespace Game.Scripts
{
    public class Mag : MonoBehaviour
    {
        public int lightSlot;
        public Material emptyLight;
    
        public int maxMag;
        public int CurrentMag { get; set; }
        public bool isAttached = false;

        private Material[] _materials;
        private float _time = 0;
        private bool _isLightOff = false;

        private void Start()
        {
            _materials = GetComponent<MeshRenderer>().materials;
            CurrentMag = maxMag;
        }

        private void Update()
        {
            if (CurrentMag > 0) return;
            if (!_isLightOff)
            {
                _materials[lightSlot] = emptyLight;
                GetComponent<MeshRenderer>().materials = _materials;
                _isLightOff = true;
            }
        
            if (!isAttached)
            {
                _time += Time.deltaTime;
                if(_time > 5f) Destroy(gameObject);
            }
 
        }

        public void Attach()
        {
            isAttached = true;
        }

        public void Detach()
        {
            isAttached = false;
        }
    }
}
