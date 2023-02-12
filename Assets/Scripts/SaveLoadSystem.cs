using System;
using System.Collections;
using System.Collections.Generic;
using ArthurKnight.XORCipher;
using TMPro;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public PlayerCostumization playerCostumization;
    public TMP_Text dataLog;
    public readonly string securityKeys = "jaslkdoqw";

    private void Start() => LoadData();

    private void OnApplicationQuit() => SaveData();

    public void SaveData()
    {
        PlayerData data = new PlayerData()
        {
            body = playerCostumization.body.material.color,
            eyeglass = playerCostumization.eyeGlass.material.color,
            eyeL = playerCostumization.eyeL.material.color,
            eyeR = playerCostumization.eyeR.material.color,
            shoes = playerCostumization.shoes.material.color,
            
            isBody = playerCostumization.toggleBody.isOn,
            isEyes = playerCostumization.toggleEyes.isOn,
            isEyeglasss = playerCostumization.toggleEyeglass.isOn,
            isShoes = playerCostumization.toggleShoes.isOn,
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
            playerCostumization.body.material.color = dataNew.body;
            playerCostumization.eyeL.material.color = dataNew.eyeL;
            playerCostumization.eyeR.material.color = dataNew.eyeR;
            playerCostumization.eyeGlass.material.color = dataNew.eyeglass;
            playerCostumization.shoes.material.color = dataNew.shoes;
            
            playerCostumization.toggleBody.isOn = dataNew.isBody;
            playerCostumization.toggleEyes.isOn = dataNew.isEyes;
            playerCostumization.toggleEyeglass.isOn = dataNew.isEyeglasss;
            playerCostumization.toggleShoes.isOn = dataNew.isShoes;
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
