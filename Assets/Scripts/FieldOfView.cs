using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AlertSystem))]
public class FieldOfView : MonoBehaviour
{
    [Space]
    [Header("FOV Setting")]
    private AlertSystem _alertSystem;
    public float alertRadius;
    public float warningRadius;
    public float dangerRadius;
    [Range(0, 360)] public float attentionAngle;
    [Range(0, 360)] public float dangerAngle;
    
    [Space]
    public LineRenderer[] lineRenderer;

    [Header("Target Setting")]
    private GameObject target;

    [Header("LayerMask")] 
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LayerMask alliesMask;
        
    [SerializeField] private bool warningArea;
    [SerializeField] private bool dangerArea;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _alertSystem = GetComponent<AlertSystem>();
        
        _alertSystem.alert = AlertSystem.AlertState.Safe;
        
        for (int i = 0; i < lineRenderer.Length; i++)
        {
            lineRenderer[i].positionCount = 4;
            lineRenderer[i].useWorldSpace = true;
        }
    }
    
    private void Update()
    {
        FieldOfViewDanger(0);
        FieldOfViewAttention(1);
    }
    
    public void FieldOfViewDanger(int lineIndex)
    {
        Collider[] targetInCollider = Physics.OverlapSphere(transform.position, dangerRadius, targetMask);

        if (targetInCollider.Length != 0)
        {
            Transform target = targetInCollider[0].transform;
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, targetDirection) < dangerAngle / 2)
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, targetDirection, targetDistance, obstacleMask))
                {
                    this.dangerArea = true;
                    _alertSystem.alert = AlertSystem.AlertState.Danger;
                    SpreadingOut(AlertSystem.AlertState.Danger);
                }
                
                else
                {
                    Debug.Log("Hallo this spreading cancel");
                    this.dangerArea = false;
                    // SpreadingCancel(AlertSystem.AlertState.Safe);
                }
                    
            }
            else this.dangerArea = false;

        } else if (this.dangerArea) this.dangerArea = false;
        
        if(!warningArea && !this.dangerArea) _alertSystem.alert = AlertSystem.AlertState.Safe;

        Line(lineIndex, dangerAngle, dangerRadius);
    }
    
    public void FieldOfViewAttention(int lineIndex)
    {
        Collider[] targetInCollider = Physics.OverlapSphere(transform.position, warningRadius, targetMask);

        if (targetInCollider.Length != 0)
        {
            Transform target = targetInCollider[0].transform;
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, targetDirection) < attentionAngle / 2)
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, targetDirection, targetDistance, obstacleMask))
                {
                    if (!this.dangerArea)
                    {
                        warningArea = true;
                        _alertSystem.alert = AlertSystem.AlertState.Warning;
                    }
                    else warningArea = false;
                }
                else warningArea = false;
            }
            else warningArea = false;
        } else if (warningArea)  warningArea = false;
        
        if(!warningArea && !this.dangerArea) _alertSystem.alert = AlertSystem.AlertState.Safe;

        Line(lineIndex, attentionAngle, warningRadius);
    }
    
    public void Line(int index, float angle, float radius)
    {
        Vector3 angle1 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        Vector3 angle2 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);
        
        lineRenderer[index].SetPosition(0, transform.position);
        lineRenderer[index].SetPosition(1, transform.position + angle1 * radius);
        lineRenderer[index].SetPosition(2, transform.position);
        lineRenderer[index].SetPosition(3, transform.position + angle2 * radius);
    }
    
    private Vector3 DirectionFromAngle(float angle, float direction)
    {
        direction += angle;

        return new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad), 0, Mathf.Cos(direction * Mathf.Deg2Rad));
    }

    public void SpreadingOut(AlertSystem.AlertState alertState)
    {
        Collider[] allies = Physics.OverlapSphere(transform.position, alertRadius, alliesMask);

        for (int i = 0; i < allies.Length; i++)
        {
            FieldOfView fieldOfView = allies[i].GetComponent<FieldOfView>();

            if (fieldOfView.dangerArea) break;
            else
            {
                fieldOfView.dangerArea = true;
                fieldOfView._alertSystem.alert = alertState;
                fieldOfView.SpreadingOut(alertState);
                Debug.Log("Spreading Out!!!----------------------");
            }
        }
    }
}
