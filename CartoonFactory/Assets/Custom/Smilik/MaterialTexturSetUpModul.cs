using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MaterialTexturSetUpModul : MonoBehaviour
{
    [SerializeField]
    private List<Texture2D> setUpPacks = null;
    [SerializeField]
    private Material material = null;

    public void SetUpPuck(int number)
    {
        try
        {
            material.mainTexture = setUpPacks[number];
        }
        catch(Exception ex)
        {
            Debug.LogError("Возможно, не добавлен элемент с индексом " + number + " но он вызывается из события анимации. Ошибка: "
                + ex.Message);
        }
    }
}
