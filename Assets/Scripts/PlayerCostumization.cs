using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ArthurKnight.XORCipher;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCostumization : MonoBehaviour
{
    public TMP_Text dataLog;
    public readonly string securityKeys = "jaslkdoqw";
    public MeshRenderer body, eyeL, eyeR, eyeGlass, shoes;
    
    public void RandomColor(MeshRenderer mesh)
    {
        mesh.material.color = Random.ColorHSV();
    }

    private void Start()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        Vector4 _body = body.material.color;
        Vector4 _eyeL = eyeL.material.color;
        Vector4 _eyeR = eyeR.material.color;
        Vector4 _eyeglass = eyeGlass.material.color;
        Vector4 _shoes = shoes.material.color;

        PlayerData data = new PlayerData()
        {
            body = _body, eyeglass = _eyeglass, eyeL = _eyeL, eyeR = _eyeR, shoes = _shoes
        };
        
        Data.SafetySave(data,"/Data/", "color.json", securityKeys);
        StartCoroutine(DataLog(dataLog, "Save Data Sucessfull", "blue"));
        
    }

    public void LoadData()
    {
        PlayerData dataNew = Data.SafetyLoad<PlayerData>("/Data/", "color.json", securityKeys);
            
        if (dataNew != null)
        {
            StartCoroutine(DataLog(dataLog, "Load Data Sucessfull", "green"));
            body.material.color = dataNew.body;
            eyeL.material.color = dataNew.eyeL;
            eyeR.material.color = dataNew.eyeR;
            eyeGlass.material.color = dataNew.eyeglass;
            shoes.material.color = dataNew.shoes;
        }
        else
        {
            StartCoroutine(DataLog(dataLog, "Save Data Don't Exists", "red"));
        }
    }

    IEnumerator DataLog(TMP_Text log, string msg, string color)
    {
        log.gameObject.SetActive(true);
        log.text = $"<color={color.ToLower()}>{msg}";

        yield return new WaitForSeconds(2f);

        log.gameObject.SetActive(false);
        log.text = String.Empty;
    }
}
