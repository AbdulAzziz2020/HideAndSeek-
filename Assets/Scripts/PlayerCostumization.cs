using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ArthurKnight.XORCipher;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerCostumization : MonoBehaviour
{
    public MeshRenderer body, eyeL, eyeR, eyeGlass, shoes;
    public Toggle toggleEyes, toggleBody, toggleEyeglass, toggleShoes;
    
    public void RandomColor(MeshRenderer mesh)
    {
        mesh.material.color = Random.ColorHSV();
    }
}
