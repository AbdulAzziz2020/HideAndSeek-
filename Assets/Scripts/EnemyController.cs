using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyController : MonoBehaviour
{
    [Header("Status")]
    public float speed;
    public float timeBetweenDelay;
    private float _timeDelay;

    [Header("Partrol Point")] 
    public Transform[] patrolPoint;

    private CharacterController _characterController;
    private int curPoint = 0;

    private void Awake()
    {
        if (_characterController == null)
            _characterController = GetComponent<CharacterController>();

        _timeDelay = timeBetweenDelay;
    }

    private void Update() => Patrol();
    
    private void Patrol()
    {
        if (patrolPoint == null) return;
        
        if (_timeDelay >= 0)
        {
            _timeDelay -= Time.deltaTime;
        }
        else
        {
            if (curPoint < patrolPoint.Length)
            {
                Vector3 target = patrolPoint[curPoint].position;
                target.y = transform.position.y;
                
                Vector3 direction = target - transform.position;

                if (direction.magnitude < 0.1f)
                {
                    transform.position = target;
                    curPoint++;
                    _timeDelay = timeBetweenDelay;
                }
                else
                {
                    _characterController.Move(direction.normalized * speed * Time.deltaTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.14f);
                }
            }
            else
            {
                curPoint = 0;
            }
        }
    }
}
