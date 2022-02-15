using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Scripts
{
    /// <summary>
    /// LeftHand Controller나 RightHand Controller에 부착
    /// 하위에 존재하는 Interactor에 제공되는 정보 보관
    /// </summary>
    public class ControllerManager : MonoBehaviour
    {
        public ActionBasedController controller;
    
        public InputDevice targetDevice;
        public InputDeviceCharacteristics controllerCharacteristics;
        public GameObject directInteractor;
        public GameObject rayInteractor;
        public GameObject UIRayInteractor;
        public Player player;
        public bool isSelectActive;
        public bool isUIRayOn = false;
        public bool isDirectInteractorOn = true;
        public bool isRayInteractorOn = false;

        private bool menuButtonPush;
        public UnityEvent menuButtonOnEvent;
        public UnityEvent menuButtonOffEvent;

        private void Start()
        {
            controller = GetComponentInParent<ActionBasedController>();
            
            if(!targetDevice.isValid)
            {
                List<InputDevice> devices = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

                if (devices.Count > 0)
                {
                    targetDevice = devices[0];
                }
            }
        }

        public void OnSelectActive(SelectEnterEventArgs args)
        {
            isSelectActive = true;
            var line = args.interactor.GetComponent<XRInteractorLineVisual>();
            if (line) line.enabled = false;
        }

        public void OnSelectDeactive(SelectExitEventArgs args)
        {
            isSelectActive = false;
            var line = args.interactor.GetComponent<XRInteractorLineVisual>();
            if (line) line.enabled = true;
        }

        public void OnUIEnter()
        {
            isUIRayOn = true;
        }
        
        public void OnUIExit()
        {
            isUIRayOn = false;
        }
        
        private void Update()
        {
            if(!targetDevice.isValid)
            {
                List<InputDevice> devices = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

                if (devices.Count > 0)
                {
                    targetDevice = devices[0];
                }

                return;
            }
            if (!isSelectActive)
            {
                targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton);
                if (isUIRayOn)
                {
                    directInteractor.SetActive(false);
                    rayInteractor.SetActive(false);
                    UIRayInteractor.SetActive(true);
                }
                else
                {
                    directInteractor.SetActive(!primaryButton);
                    rayInteractor.SetActive(primaryButton);
                    UIRayInteractor.SetActive(false);
                }
                
            }
            
            targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButton);
            if (menuButton != menuButtonPush)
            {
                if (menuButton)
                {
                    menuButtonOnEvent.Invoke();
                }
                else
                {
                    menuButtonOffEvent.Invoke();
                }

                menuButtonPush = menuButton;
            }
            
        }
    }
}
