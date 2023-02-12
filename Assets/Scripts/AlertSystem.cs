using System;
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

    [Header("Alert Indicator")]
    public Image alertIndicator;
    public Color dangerColor;
    public Color warningColor;

    private void Update()
    {
        Alert();
    }

    public void Alert()
    {
        switch (alert)
        {
            case AlertState.Danger:
                alertColor.material.color = dangerColor;
                
                alertIndicator.gameObject.SetActive(true);
                alertIndicator.color = dangerColor;
                break;
            case AlertState.Warning:
                alertColor.material.color = warningColor;
                
                alertIndicator.gameObject.SetActive(true);
                alertIndicator.color = warningColor;
                break;
            case AlertState.Safe:
                alertColor.material.color = Color.white;
                
                alertIndicator.gameObject.SetActive(false);
                break;
        }
    }
}