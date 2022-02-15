using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Scripts
{
    public class GrabAndPinchInteractable : XRGrabInteractable
    {
        public enum GrabType
        {
            Grab,
            Pinch
        }

        public GrabType grabType;
    
    }
}
