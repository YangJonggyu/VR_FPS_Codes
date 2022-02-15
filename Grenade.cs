using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class Grenade : Weapon
    {
        public int lightSlot;
        public Material offLight;
        public Material onLight;

        public AudioClip beepSound;
        public AudioClip explosionSound;

        public GameObject explosion;
        private GameObject _explosion;

        private bool isWork = false;
        
        public float timeLimit = 5f;
        private int _phase = 0;
        private readonly float[] _phaseTime = new float[] {0.5f, 0.5f, 0.5f, 0.5f,0.25f,0.25f,0.25f,0.25f,0.25f,0.25f,0.25f,0.25f,0.1f,0.1f,0.1f,0.1f,0.1f,0.1f,0.1f,0.1f,0.1f,0.1f};
        private float _currentPhaseTime = 0f;
        
        private Material[] _materials;

        private bool _isExploded = false;
        private float _deleteTime = 10f;

        protected override void Start()
        {
            base.Start();
            _materials = GetComponent<MeshRenderer>().materials;
            _currentPhaseTime = _phaseTime[_phase];
        }
        private void Update()
        {
            if (isWork) TickBomb();

            if (!_isExploded) return;
            _deleteTime -= Time.deltaTime;
            if (_deleteTime < 0)
            {
                Destroy(_explosion);
                Destroy(gameObject);
            }
        }

        private void TickBomb()
        {
            if (_isExploded) return;
            timeLimit -= Time.deltaTime;
            _currentPhaseTime -= Time.deltaTime;
            if (_currentPhaseTime < 0)
            {
                ChangeLightColor(_phase % 2 != 0);
                _phase += 1;
                _currentPhaseTime = _phaseTime[_phase];
                AudioSource.PlayOneShot(beepSound);
            }
            if (timeLimit <= 0) Attack();
        }
        
        
        public override void Attack()
        {
            base.Attack();
            AudioSource.PlayOneShot(explosionSound);
            _explosion = Instantiate(explosion,transform.position,Quaternion.identity);
            _isExploded = true;
        }

        public void OnActivated()
        {
            if (isWork) return;
            isWork = true;
            ChangeLightColor(true);
            AudioSource.PlayOneShot(beepSound);
        }

        private void ChangeLightColor(bool turnOn)
        {
            
            _materials[lightSlot] = turnOn ? onLight : offLight;
            GetComponent<MeshRenderer>().materials = _materials;
        }

        public override void SelectExited()
        {
            if (isWork) controller = null;
            else base.SelectExited();
        }
    }
}
