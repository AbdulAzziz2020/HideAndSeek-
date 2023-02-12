using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;

    private CharacterController _characterController;

    private void Awake()
    {
        if(_characterController == null)
            _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float veloX = Input.GetAxis("Horizontal");
        float veloZ = Input.GetAxis("Vertical");

        // character movement;
        Vector3 moveInput = Vector3.right * veloX + Vector3.forward * veloZ;
        _characterController.Move(moveInput * speed * Time.deltaTime);

        // character direction;
        if(moveInput != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput),0.15f);
    }
}
