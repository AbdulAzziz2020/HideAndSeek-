using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObject : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.isTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.isTrigger = false;
        }
    }
}
