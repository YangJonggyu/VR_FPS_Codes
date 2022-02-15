using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Scripts
{
    public class Gun : Weapon
    {
        [Header("Gun Shot Effect")]
        public ParticleSystem gunShotEffect;
        public ParticleSystem bulletEffect;
        public Bullet bullet;
        private ParticleSystem.ShapeModule shape;
        public TextMeshPro bulletText;

        [Header("Sound")]
        public AudioClip gunShotSound;
        public AudioClip gunMagInSound;
        public AudioClip gunEmptySound;

        private Vector3 _baseRotate;
        private Vector3 _basePosition;
        
        [Header("Weapon Status")] 
        public float verticalRecoilDegree;
        public float verticalRecoilRecovery;
        public float recoilDistance;
        public float recoilDistanceRecovery;
        public float horizontalRecoilDegree;
        public float horizontalRecoilRecovery;
        public float maxSpreadDegree = 2f;
        public float spreadDegree;
        public float spreadRecovery;
        public float rateOfFire;
        
        private float _verticalRecoiledDegree = 0;
        private float _horizontalRecoiledDegree = 0;
        private float _recoiledDistance = 0;
        private float _spread = 0;
        private float _fireTime = 0;

        public Transform gunForward;
        public bool needMag = true;
        public int maxMag;
        private int _currentMag;
        public float reloadDelay;
        private float _reloadDelay = 0;
        public float reloadTimeInterval;
        private float _reloadTimeInterval = 0;
        public int reloadAmount;
        private bool _isReloading = false;

        protected override void Start()
        {
            base.Start();
            _baseRotate = transform.localEulerAngles;
            _basePosition = transform.localPosition;
            shape = bulletEffect.shape;
            _currentMag = maxMag;
        }

        protected virtual void LateUpdate()
        {
            if (attacker)
            {
                UpdateGun();
            }

            if (needMag)
            {
                UpdateBullet();
                ReloadCheck();
            }
            
        }

        protected virtual void UpdateGun()
        {
            transform.localEulerAngles = new Vector3(_baseRotate.x, _baseRotate.y + _horizontalRecoiledDegree, _baseRotate.z + _verticalRecoiledDegree);
            transform.localPosition = new Vector3(_basePosition.x - _recoiledDistance, _basePosition.y, _basePosition.z);
            shape.angle = _spread;
        
            _verticalRecoiledDegree = Mathf.MoveTowards(_verticalRecoiledDegree, 0, Time.deltaTime * verticalRecoilRecovery);
            _horizontalRecoiledDegree = Mathf.MoveTowards(_horizontalRecoiledDegree, 0, Time.deltaTime * horizontalRecoilRecovery);
            _recoiledDistance = Mathf.MoveTowards(_recoiledDistance, 0, Time.deltaTime * recoilDistanceRecovery);
            _spread = Mathf.MoveTowards(_spread, 0, Time.deltaTime * spreadRecovery);
            _fireTime = Mathf.MoveTowards(_fireTime, 0, Time.deltaTime * rateOfFire);
        }

        protected virtual void UpdateBullet()
        {
            if(bulletText) bulletText.text = _currentMag.ToString();
        }

        protected virtual void ReloadCheck()
        {
            if (Vector3.Dot(gunForward.forward, Vector3.up) < -0.52f)
            {
                if (_isReloading)
                {
                    _reloadTimeInterval += Time.deltaTime;
                    if (_reloadTimeInterval > reloadTimeInterval)
                    {
                        Reload();
                        _reloadTimeInterval = 0;
                    }
                }
                else
                {
                    _reloadDelay += Time.deltaTime;
                    if (_reloadDelay > reloadDelay)
                    {
                        _isReloading = true;
                    }
                }
            }
            else
            {
                _isReloading = false;
                _reloadDelay = 0;
                _reloadTimeInterval = 0;
            }
        }

        public override void Attack()
        {
            if (_fireTime > 0) return;

            if (needMag && _currentMag <= 0)
            {
                AudioSource.PlayOneShot(gunEmptySound);
                return;
            }

            _currentMag--;
        
            if(controller) controller.SendHapticImpulse(1,0.1f);
            gunShotEffect.Play();
            AudioSource.PlayOneShot(gunShotSound);
        
            //VerticalRecoil
            _verticalRecoiledDegree += verticalRecoilDegree;
            _verticalRecoiledDegree = Mathf.Clamp(_verticalRecoiledDegree, 0, 10f);
        
            //Horizontal Recoil
            _horizontalRecoiledDegree += horizontalRecoilDegree * (Random.Range(0,2) - 1);
            _horizontalRecoiledDegree = Mathf.Clamp(_horizontalRecoiledDegree, -5f, 5f);
        
            //Backward Recoil
            _recoiledDistance += recoilDistance;
            _recoiledDistance = Mathf.Clamp(_recoiledDistance, 0f, 0.05f);
        
            //Spread
            _spread += spreadDegree;
            _spread = Mathf.Clamp(_spread, 0f, maxSpreadDegree);

        }
    
        public virtual void Reload()
        {
            if (_currentMag == maxMag) return;
            AudioSource.PlayOneShot(gunMagInSound);
            _currentMag = Mathf.Clamp(_currentMag + reloadAmount,0,maxMag);
            
        }

        public override void SelectEntered(SelectEnterEventArgs args)
        {
            base.SelectEntered(args);
        }
    
        public override void SelectExited()
        {
            base.SelectExited();

            _isReloading = false;
            _reloadDelay = 0;
            _reloadTimeInterval = 0;
            
            _verticalRecoiledDegree = 0;
            _horizontalRecoiledDegree = 0;
            _recoiledDistance = 0;
            _spread = 0;
        
            transform.localEulerAngles = _baseRotate;
            transform.localPosition = _basePosition;
        }
    }
}
