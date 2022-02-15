using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class InventoryWeapon : MonoBehaviour
{
    private XRSocketInteractor _interactor;
    private bool _interactorSelected = false;

    public GameObject inMyPocket;

    private void Start()
    {
        _interactor = GetComponent<XRSocketInteractor>();
    }

    public void HandOnSelectEntered(SelectEnterEventArgs args)
    {
        DontDestroyOnLoad(args.interactable.gameObject);
        if (inMyPocket && args.interactable.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            _interactor.socketActive = false;
        }
    }
    
    public void HandOnSelectExited(SelectExitEventArgs args)
    {
        var obj = args.interactable.gameObject;
        if (!inMyPocket && obj.layer == LayerMask.NameToLayer("Weapon"))
        {
            _interactor.socketActive = true;
            args.interactable.gameObject.transform.position = transform.position;
        }
    }
    
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        inMyPocket = args.interactable.gameObject;
    }
    
    public void OnSelectExited(SelectExitEventArgs args)
    {
        inMyPocket = null;
        SceneManager.MoveGameObjectToScene(args.interactable.gameObject, SceneManager.GetActiveScene());
    }
}
