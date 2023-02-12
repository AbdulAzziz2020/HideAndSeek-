using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlertSystem : MonoBehaviour
{
    public enum AlertState
    {
        Danger,
        Warning,
        Safe
    }
    
    [Header("Alert Setting")]
    public AlertState alert;
    public MeshRenderer alertColor;
    public float alertWarningTimeDelay = 5f;
    public float alertSafeTimeDelay = 10f;
    public float alertTime;

    [Header("Alert Indicator")]
    public Image alertIndicator;
    public Color dangerColor;
    public Color warningColor;
    

    public void Alert(bool danger, bool warning)
    {
        switch (alert)
        {
            case AlertState.Danger:
                alertColor.material.color = dangerColor;
                alertIndicator.gameObject.SetActive(true);
                alertIndicator.color = dangerColor;
                

                if (!danger)
                {
                    if (alertTime >= 0) alertTime -= Time.deltaTime;
                    else
                    {
                        alert = AlertState.Warning;
                        alertTime = alertSafeTimeDelay;
                    }
                }
                else
                {
                    alertTime = alertWarningTimeDelay;
                }
                
                break;
            case AlertState.Warning:
                alertColor.material.color = warningColor;
                alertIndicator.gameObject.SetActive(true);
                alertIndicator.color = warningColor;

                if (!warning)
                {
                    
                    if (alertTime >= 0) alertTime -= Time.deltaTime;
                    else alert = AlertState.Safe;
                }
                else
                {
                    alertTime = alertSafeTimeDelay;
                }
                
                break;
            case AlertState.Safe:
                alertColor.material.color = Color.white;
                alertIndicator.gameObject.SetActive(false);
                break;
        }
    }
}