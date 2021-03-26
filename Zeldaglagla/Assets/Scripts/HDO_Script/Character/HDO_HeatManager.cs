using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_HeatManager : MonoBehaviour
{
    public Image heatBar;

    public float heatValue;
    public int maxHeat;
    public float heatModifierPerSecond;

    // Update is called once per frame
    void Update()
    {
        if (heatBar)
        {
            heatBar.rectTransform.localScale = new Vector2(heatValue / maxHeat, heatBar.rectTransform.localScale.y);
        }
        heatValue += heatModifierPerSecond * Time.deltaTime;

        if(heatValue > maxHeat)
        {
            heatValue = maxHeat;
        }
    }
}
