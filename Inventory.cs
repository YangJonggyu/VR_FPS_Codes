using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform head;
    public ControllerManager controllerManager;
    public GameObject inventoryUI;
    public GameObject inventoryPocket;
    private bool headSpin = false;

    private Quaternion target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controllerManager.menuButtonOnEvent.AddListener(InventoryUIOnOff);
    }

    private void OnDisable()
    {
        controllerManager.menuButtonOnEvent.RemoveListener(InventoryUIOnOff);
    }

    // Update is called once per frame
    void Update()
    {
        // camera rotation
      
        transform.position = head.position;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        
        target = Quaternion.Euler(new Vector3(0, head.eulerAngles.y, 0));
        if (Quaternion.Angle(transform.rotation, target) > 40f && !headSpin) headSpin = true;
        else if(Quaternion.Angle(transform.rotation, target) < 1f) headSpin = false;
        else if(headSpin)
            transform.rotation =
                Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
        inventoryPocket.transform.rotation = target;

    }

    public void InventoryUIOnOff()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

}
