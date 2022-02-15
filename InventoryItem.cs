using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryItem : MonoBehaviour
{
    private XRSocketInteractor _interactor;

    private void Start()
    {
        _interactor = GetComponent<XRSocketInteractor>();
        _interactor.selectEntered.AddListener(OnSelectEntered);
        _interactor.selectExited.AddListener(OnSelectExited);
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        args.interactable.transform.parent = transform;
    }
    
    public void OnSelectExited(SelectExitEventArgs args)
    {
        args.interactable.transform.parent = null;
    }
}
