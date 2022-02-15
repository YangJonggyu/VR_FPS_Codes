using UnityEngine;
using UnityEngine.XR;

namespace Game.Scripts
{
    /// <summary>
    /// 손과 컨트롤러의 모델과 애니메이션을 조작하는 클래스
    /// </summary>
    public class HandPresence : MonoBehaviour
    {
    
        public bool showController = false;
        public GameObject controllerPrefabs;
        public GameObject handModelPrefab;

        private ControllerManager controllerManager;
        private InputDevice targetDevice;
    
        private GameObject spawnedController;
        private GameObject spawnedHandModel;
    
        public InputFeatureUsage<bool> triggerTouch = new InputFeatureUsage<bool>("TriggerTouch");

        //Hand Animation
        private Animator handAnimator;

        private int flexHash;
        private int pinchHash;

        private float pointBlend;
        private int pointLayer;
        private float thumbBlend;
        private int thumbLayer;
    
        //Controller Animation
        private Animator controllerAnimator;

        private int button1Hash;
        private int button2Hash;
        private int button3Hash;
        private int gripHash;
        private int triggerHash;
        private int joystickXHash;
        private int joystickYHash;
    
    
        private void Start()
        {
            controllerManager = GetComponentInParent<ControllerManager>();
            if(!controllerManager) Debug.Log("Can't find controller manager in parents");
        
        
            spawnedController = Instantiate(controllerPrefabs, transform);
            spawnedHandModel = Instantiate(handModelPrefab, transform);
        
            handAnimator = spawnedHandModel.GetComponent<Animator>();
            handAnimator.keepAnimatorControllerStateOnDisable = true;
            pointLayer = handAnimator.GetLayerIndex("Point Layer");
            thumbLayer = handAnimator.GetLayerIndex("Thumb Layer");
            flexHash = Animator.StringToHash("Flex");
            pinchHash = Animator.StringToHash("Pinch");
        
            controllerAnimator = spawnedController.GetComponent<Animator>();
            controllerAnimator.keepAnimatorControllerStateOnDisable = true;
            button1Hash = Animator.StringToHash("Button 1");
            button2Hash = Animator.StringToHash("Button 2");
            button3Hash = Animator.StringToHash("Button 3");
            joystickXHash = Animator.StringToHash("Joy X");
            joystickYHash = Animator.StringToHash("Joy Y");
            gripHash = Animator.StringToHash("Grip");
            triggerHash = Animator.StringToHash("Trigger");
        }

        // Update is called once per frame
        void Update()
        {
            if (!controllerManager.targetDevice.isValid) return;
            targetDevice = controllerManager.targetDevice;
        
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
            }
            if (showController) UpdateControllerAnimStates(); 
            else UpdateHandAnimStates();

        }
    
        private float InputValueRateChange(bool isDown, float value)
        {
            float rateDelta = Time.deltaTime * 20.0f;
            float sign = isDown ? 1.0f : -1.0f;
            return Mathf.Clamp01(value + rateDelta * sign);
        }

        private void UpdateHandAnimStates()
        {
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float flex);
            handAnimator.SetFloat(flexHash, flex);

            // Point
            targetDevice.TryGetFeatureValue(triggerTouch, out bool indexTouched);
            pointBlend = InputValueRateChange(!indexTouched, pointBlend);
            handAnimator.SetLayerWeight(pointLayer, pointBlend);

            // Thumbs up
            targetDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out bool thumb1);
            targetDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool thumb2);
            targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool thumb3);
            thumbBlend = InputValueRateChange(!(thumb1 | thumb2 | thumb3), thumbBlend);
            handAnimator.SetLayerWeight(thumbLayer, thumbBlend);

            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float pinch);
            handAnimator.SetFloat(pinchHash, pinch);
        
        }
    
        private void UpdateControllerAnimStates()
        {
            targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryBottenPushed);
            controllerAnimator.SetFloat(button1Hash, primaryBottenPushed ? 1.0f : 0.0f);
        
            targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryBottenPushed);
            controllerAnimator.SetFloat(button2Hash, secondaryBottenPushed ? 1.0f : 0.0f);
        
            targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuBottenPushed);
            controllerAnimator.SetFloat(button3Hash, menuBottenPushed ? 1.0f : 0.0f);
        
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripPushed);
            controllerAnimator.SetFloat(gripHash, gripPushed);
        
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerPushed);
            controllerAnimator.SetFloat(triggerHash, triggerPushed);
        
            targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystick);
            controllerAnimator.SetFloat(joystickXHash, joystick.x);
            controllerAnimator.SetFloat(joystickYHash, joystick.y);
        
        
        }

    }
}
