using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GripAndPinchRayInteractor : XRRayInteractor
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
