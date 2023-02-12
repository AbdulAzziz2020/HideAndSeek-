using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        float veloX = Input.GetAxis("Horizontal");
        float veloZ = Input.GetAxis("Vertical");

        // character movement;
        Vector3 moveInput = Vector3.right * veloX + Vector3.forward * veloZ;
        transform.Translate(moveInput * speed * Time.deltaTime);

        // character direction;
        //if(moveInput != Vector3.zero)
           // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput),0.15f);
    }
}
