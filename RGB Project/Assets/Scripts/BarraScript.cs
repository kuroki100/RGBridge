using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraScript : MonoBehaviour
{
    float fillAmount;
    public Image content;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AtualizarBarra();
    }

    void AtualizarBarra()
    {
        //content.fillAmount = fillAmount;
        content.fillAmount = Map(PersistentManagerScript.Instance.vida = PersistentManagerScript.Instance.vida, 0, 5, 0, 1);
    }
    
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return ((value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin);
    }
}
