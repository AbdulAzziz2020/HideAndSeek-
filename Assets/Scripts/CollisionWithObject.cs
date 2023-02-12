using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObject : MonoBehaviour
{
    public Collider groundCollider;

    public void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Target"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collisionInfo.gameObject.GetComponent<Collider>());
            collisionInfo.gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Target"))
        {
            Physics.IgnoreCollision(groundCollider, collisionInfo.gameObject.GetComponent<Collider>());
            collisionInfo.gameObject.layer = LayerMask.NameToLayer("Target");
        }
    }
}
