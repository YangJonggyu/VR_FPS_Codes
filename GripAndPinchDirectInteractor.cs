using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Scripts
{
    /// <summary>
    /// Interactable의 태그를 기준으로 Attach Transform을 변경
    /// Grip과 Pinch 두 위치 존재
    /// </summary>
    public class GripAndPinchDirectInteractor : XRDirectInteractor
    {
    
        public Transform pinchPosition;
        public Transform gripPosition;

        private GrabAndPinchInteractable grabAndPinchInteractable;

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            if (args.interactable.GetType() != typeof(GrabAndPinchInteractable)) attachTransform = gripPosition;
            else
            {
                grabAndPinchInteractable = (GrabAndPinchInteractable) args.interactable;
                if (grabAndPinchInteractable.grabType == GrabAndPinchInteractable.GrabType.Grab) attachTransform = gripPosition;
                if (grabAndPinchInteractable.grabType == GrabAndPinchInteractable.GrabType.Pinch) attachTransform = pinchPosition;
            }
            base.OnSelectEntering(args);
        }

    }
}
