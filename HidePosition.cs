using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePosition : MonoBehaviour
{
    public ObjectHeight height;
    
    public bool reservation = false;

    public bool IsFrontOfTarget(Vector3 target)
    {
        var pos = new Vector3(transform.position.x, 0, transform.position.z);
        var pos2 = new Vector3(target.x, 0, target.z);
        var dir = (pos2 - pos).normalized;
        
        return Vector3.Dot(dir, transform.eulerAngles) < .525f ? true : false;
    }

    public bool IsValidPosition(Vector3 target)
    {
        return !reservation && IsFrontOfTarget(target);
    }

    public void ReservationHidePosition()
    {
        reservation = true;
    }
    
    public void CancelReservationHidePosition()
    {
        reservation = false;
    }
}

public enum ObjectHeight
{
    High,
    Middle,
    Low,
    VeryLow
}